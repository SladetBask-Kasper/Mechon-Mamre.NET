
namespace Mechon_Mamre.NET
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bBack = new System.Windows.Forms.Button();
            this.bNext = new System.Windows.Forms.Button();
            this.Books = new System.Windows.Forms.ComboBox();
            this.Chapters = new System.Windows.Forms.ComboBox();
            this.Output = new System.Windows.Forms.RichTextBox();
            this.fontSizeSelect = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // bBack
            // 
            this.bBack.Location = new System.Drawing.Point(12, 12);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(75, 23);
            this.bBack.TabIndex = 0;
            this.bBack.Text = "Back";
            this.bBack.UseVisualStyleBackColor = true;
            // 
            // bNext
            // 
            this.bNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bNext.Cursor = System.Windows.Forms.Cursors.Default;
            this.bNext.Location = new System.Drawing.Point(620, 12);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(75, 23);
            this.bNext.TabIndex = 1;
            this.bNext.Text = "Next";
            this.bNext.UseVisualStyleBackColor = true;
            // 
            // Books
            // 
            this.Books.FormattingEnabled = true;
            this.Books.Location = new System.Drawing.Point(93, 14);
            this.Books.Name = "Books";
            this.Books.Size = new System.Drawing.Size(121, 21);
            this.Books.TabIndex = 2;
            // 
            // Chapters
            // 
            this.Chapters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Chapters.FormattingEnabled = true;
            this.Chapters.Location = new System.Drawing.Point(493, 14);
            this.Chapters.Name = "Chapters";
            this.Chapters.Size = new System.Drawing.Size(121, 21);
            this.Chapters.TabIndex = 3;
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.Location = new System.Drawing.Point(0, 41);
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.Size = new System.Drawing.Size(711, 391);
            this.Output.TabIndex = 4;
            this.Output.Text = "";
            // 
            // fontSizeSelect
            // 
            this.fontSizeSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fontSizeSelect.Location = new System.Drawing.Point(290, 12);
            this.fontSizeSelect.Name = "fontSizeSelect";
            this.fontSizeSelect.Size = new System.Drawing.Size(120, 20);
            this.fontSizeSelect.TabIndex = 5;
            this.fontSizeSelect.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.fontSizeSelect.ValueChanged += new System.EventHandler(this.fontSizeSelect_ValueChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(707, 432);
            this.Controls.Add(this.fontSizeSelect);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Chapters);
            this.Controls.Add(this.Books);
            this.Controls.Add(this.bNext);
            this.Controls.Add(this.bBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button bBack;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.ComboBox Books;
        private System.Windows.Forms.ComboBox Chapters;
        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.NumericUpDown fontSizeSelect;
    }
}

