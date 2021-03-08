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
        internal SaveFileDialog SaveFileDialog1;
        public const string BASE_URL = "https://www.mechon-mamre.org/p/pt/pt{0}{1}.htm";
        public int book = 1;
        public int chapter = 1;
        public int max_chap = 50;
        private bool changed_from_chapchange = false;
        private bool changed_from_bookchange = false;
        public string[] booknames = { "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", 
            "Joshua", "Judges", "1 Samuel", "2 Samuel", "1 Kings", "2 Kings", "Isaiah", 
            "Jeremiah", "Ezekiel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", 
            "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "1 Chronicles", 
            "2 Chronicles", "Psalms", "Job", "Proverbs", "Ruth", "Song of Songs", "Ecclesiastes", 
            "Lamentations", "Esther", "Daniel", "Ezra", "Nehemiah" };
   
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
            string chappers = chapter.ToString("00");
            string bookers = book.ToString("00");
            if (chapter >= 100)
            {
                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
                string strChap = chapter.ToString();
                chappers = alpha[int.Parse(strChap.ElementAt(1).ToString())].ToString() + strChap.ElementAt(2).ToString();
            }
            if (book == 35)
            {
                bookers = "35a";
            }
            else if (book == 36)
            {
                bookers = "35b";
            }
            else if (book == 8)
                bookers = "08a";
            else if (book == 9)
                bookers = "08b";
            else if (book == 10)
                bookers = "09a";
            else if (book == 11)
                bookers = "09b";
            else if (book > 11 && book < 27)
                bookers = (book - 2).ToString();
            else if (book == 27)
                bookers = "25a";
            else if (book == 28)
                bookers = "25b";
            else if (book > 28)
                bookers = (book - 3).ToString();
            string html = Get(String.Format(BASE_URL, bookers, chappers)).Replace("&nbsp;", "");
            changed_from_chapchange = true;
            changed_from_bookchange = true;
            Chapters.Items.Clear();
            Books.SelectedIndex = book - 1;
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
            bool isDoubleBook = false;
            if (book == 35 || book == 36 || book == 8 || book == 9 || book == 10 || book == 11 || book == 27 || book == 28)
            {
                isDoubleBook = true;
                chapterData = "";
                if (book == 35) max_chap = 10;
                if (book == 36) max_chap = 13;
                if (book == 8) max_chap = 31;
                if (book == 9) max_chap = 24;
                if (book == 10) max_chap = 22;
                if (book == 11) max_chap = 25;
                if (book == 27) max_chap = 29;
                if (book == 28) max_chap = 36;
                for (int n = 1; n <= max_chap; n++)
                    Chapters.Items.Add(n);
                Chapters.SelectedItem = chapter;
            }
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
            if (!isDoubleBook)
                max_chap = chaps.ElementAt(chaps.Count - 1);
            Output.Text = rv;
            changed_from_chapchange = false;
            changed_from_bookchange = false;
        }
        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            Font currentFont = Output.Font;
            Output.Font = new Font(currentFont.FontFamily, 14, currentFont.Style);
            Tools.RichTextBoxExtensions.SetInnerMargins(Output, 24, 0, 24, 0);
            this.Text = "Mechon-Mamre.NET";
            foreach (string bname in booknames)
            {
                Books.Items.Add(bname);
            }
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
            else if (book < booknames.Length)
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

        private void Books_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!changed_from_bookchange)
            {
                book = Books.SelectedIndex + 1;
                chapter = 1;
                getChapter();
            }
            else
            {
                changed_from_bookchange = false;
            }
        }

        private void Output_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveFileDialog1 = new SaveFileDialog();
                // SaveFileDialog1.CreatePrompt = true;
                SaveFileDialog1.OverwritePrompt = true;
                SaveFileDialog1.FileName = booknames[book-1] + (chapter).ToString();
                SaveFileDialog1.DefaultExt = "txt";
                SaveFileDialog1.Filter =
                    "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                SaveFileDialog1.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //DialogResult result = SaveFileDialog1.ShowDialog();
                if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && 
                    SaveFileDialog1.FileName.Length > 0)
                {
                    Output.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
    }
}
/*
 Book indecie map
0 - Genesis
1 - Exodus
2 - Leviticus
3 - Numbers
4 - Deuteronomy
5 - Joshua
6 - Judges
7 - 1 Samuel
8 - 2 Samuel
9 - 1 Kings
10 - 2 Kings
11 - Isaiah
12 - Jeremiah
13 - Ezekiel
14 - Hosea
15 - Joel
16 - Amos
17 - Obadiah
18 - Jonah
19 - Micah
20 - Nahum
21 - Habakkuk
22 - Zephaniah
23 - Haggai
24 - Zechariah
25 - Malachi
26 - 1 Chronicles
27 - 2 Chronicles
28 - Psalms
29 - Job
30 - Proverbs
31 - Ruth
32 - Song of Songs
33 - Ecclesiastes
34 - Lamentations
35 - Esther
36 - Daniel
37 - Ezra
38 - Nehemiah
 */