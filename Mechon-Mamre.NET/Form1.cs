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
            string rv = "";
            bool inTag = false;
            string currentTag = "";
            bool shouldAdd = false;
            for (int i = 0; i < html.Length; i++)
            {
                char c = html.ElementAt(i);
                if (c == '<')
                {
                    inTag = true;
                }
                if (shouldAdd && !inTag) rv += c;
                if (inTag) currentTag += c;
                //if (inTag && c == '/')
                //    c = c; // nothing
                if (c=='>')
                { 
                    inTag = false;
                    if (currentTag.ToUpper() == "<TD>")
                        shouldAdd = true;
                    else if (currentTag.ToUpper().Contains("<TD CLASS=") && !currentTag.ToUpper().Contains("<TD CLASS=H"))
                    {
                        Console.WriteLine(currentTag);
                        shouldAdd = true;
                    }
                    else if (currentTag.ToUpper() == "</TD>")
                        shouldAdd = false;
                    currentTag = "";
                }
            }
            Output.Text = rv;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Font currentFont = Output.Font;
            //Output.SetInnerMargins(dist, dist, dist, 0);
            //Output.Margin(dist, dist, dist, 0);
            //Output.RightMargin = Output.Size.Width - 35;
            Output.Font = new Font(currentFont.FontFamily, (float)fontSizeSelect.Value, currentFont.Style);
            Tools.RichTextBoxExtensions.SetInnerMargins(Output, 24, 0, 24, 0);
            this.Text = "Mechon-Mamre.NET";
            getChapter();
        }

        private void fontSizeSelect_ValueChanged(object sender, EventArgs e)
        {
            Font currentFont = Output.Font;
            Output.Font = new Font(currentFont.FontFamily, (float)fontSizeSelect.Value, currentFont.Style);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //Tools.RichTextBoxExtensions.SetInnerMargins(Output, 24, 0, 24, 0);
        }
    }
}
