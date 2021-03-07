using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace Mechon_Mamre.NET
{
    public partial class Form1 : Form
    {
        public const string BASE_URL = "https://www.mechon-mamre.org/p/pt/pt{0}{1}.htm";
        public int book = 1;
        public int chapter = 1;
        public int max_chap = 50;
        private bool changed_from_chapchange = false;
        /// <summary>
        /// Source : https://stackoverflow.com/a/27108442/7090007
        /// </summary>
        /// <param name="uri">URL</param>
        /// <returns>HTML as TXT</returns>
        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public void getChapter()
        {
            string html = Get(String.Format(BASE_URL, book.ToString("00"), chapter.ToString("00")));
            changed_from_chapchange = true;
            Chapters.Items.Clear();
            //Books.Items.Clear();
            string rv = "";
            bool inTag = false;
            bool afterBreak = false;
            bool inChapters = false;
            int breakCounter = 0;
            string word_chapter = "Chapter";
            string chapterData = "";
            string currentTag = "";
            bool shouldAdd = false;
            for (int i = 0; i < html.Length; i++)
            {
                char c = html.ElementAt(i);
                if (afterBreak)
                {
                    if (breakCounter >= word_chapter.Length)
                    {
                        afterBreak = false;
                        inChapters = true;
                    }
                    else
                    {
                        char c2 = word_chapter.ElementAt(breakCounter);
                        if (c2 != c)
                        {
                            afterBreak = false;
                            inChapters = false;
                        }
                        breakCounter++;
                    }
                    
                }
                if (c == '<')
                {
                    inTag = true;
                }
                if (shouldAdd && !inTag) rv += c;
                if (inTag) currentTag += c;
                if (c=='>')
                { 
                    inTag = false;
                    string tag = currentTag.ToUpper();
                    currentTag = "";
                    if (tag == "<TD>") shouldAdd = true;
                    else if (tag.Contains("<TD CLASS") && !tag.Contains("<TD CLASS=H"))
                    {
                        shouldAdd = true;
                    }
                    else if (tag == "</TD>") shouldAdd = false;
                    else if (tag == "<BR>")
                    {
                        if (inChapters)
                        {
                            afterBreak = false;
                            inChapters = false;
                            breakCounter = 0;
                        }
                        else afterBreak = true;
                    }
                }
                if (inChapters)
                {
                    chapterData += c;
                }
            }
            string[] nums = "1,2,3,4,5,6,7,8,9,0".Split(',');
            chapterData = chapterData.Replace("<BR", "");
            int main = 0;
            List<int> chaps = new List<int>();
            //Console.WriteLine(chapterData.Split('\n').Length - 1);
            foreach (string row in chapterData.Split('\n'))
            {
                bool isInCurrent = false;
                if (row.Length < 1) continue;
                if (row.ElementAt(0) != '<')
                {
                    //Console.WriteLine(row.ElementAt(0));
                    isInCurrent = true;
                }

                string toParse = "";
                if (row.Contains(".htm"))
                {
                    toParse = row.Split(new string[] { ".htm" }, StringSplitOptions.None)[1];
                }
                else
                {
                    toParse = row;
                }
                string total = "";
                for (int a = 0; a < toParse.Length; a++)
                {
                    string c = toParse.ElementAt(a).ToString();
                    foreach (string num in nums)
                    {
                        if (num == c)
                        {
                            total += c;
                            break;
                        }
                    }
                }
                if (total != "")
                {
                    int result = int.Parse(total);
                    chaps.Add(result);
                    if (isInCurrent)
                    {
                        main = result;
                    }
                }
                else
                {
                    Console.WriteLine("Empty chapter line detected.");
                }
            }
            foreach (int n in chaps)
            {
                Chapters.Items.Add(n);
                if (n == main) Chapters.SelectedItem = n;
            }
            max_chap = chaps.ElementAt(chaps.Count - 1);
            Output.Text = rv;
        }
        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            Font currentFont = Output.Font;
            Output.Font = new Font(currentFont.FontFamily, 14, currentFont.Style);
            Tools.RichTextBoxExtensions.SetInnerMargins(Output, 24, 0, 24, 0);
            this.Text = "Mechon-Mamre.NET";
            getChapter();
        }

        private void Chapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!changed_from_chapchange)
            {
                chapter = (int)Chapters.SelectedIndex+1;
                getChapter();
            }
            else
            {
                changed_from_chapchange = false;
            }
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            if (chapter < max_chap)
            {
                chapter++;
                getChapter();
            }
            else if (book < 34)
            {
                book++;
                chapter = 1;
                getChapter();
            }
            else
            {
                MessageBox.Show("You have reachead the end.");
            }
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            if (chapter > 1)
            {
                chapter--;
                getChapter();
            }
            else if (book > 1)
            {
                book--;
                chapter = 1;
                getChapter();
            }
            else
            {
                MessageBox.Show("You have reachead the start.");
            }
        }
    }
}
