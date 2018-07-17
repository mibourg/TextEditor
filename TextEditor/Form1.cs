using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                string fileName = Path.GetFileName(saveFileDialog.FileName);
                fileNameLabel.Text = fileName;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName);
                string fileName = Path.GetFileName(openFileDialog.FileName);
                fileNameLabel.Text = fileName;
            }
        }

        private void richTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            charactersValueLabel.Text = richTextBox.Text.Length.ToString();
            wordsValueLabel.Text = FindWordCount().ToString();
            paragraphsValueLabel.Text = FindParagraphCount().ToString();
            if (richTextBox.Text.Length == 0)
            {
                wordsValueLabel.Text = "0";
                paragraphsValueLabel.Text = "0";
            }
        }

        private int FindWordCount()
        {
            int wordCount = 1;
            string text = richTextBox.Text;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ' && i != text.Length - 1 && text[i + 1] != ' ')
                {
                    wordCount++;
                }
                else if (text[i] != ' ' && i != text.Length - 1 && i > 0 && text[i - 1] == '\n')
                {
                    wordCount++;
                }

            }
            return wordCount;
        }

        private int FindParagraphCount()
        {
            int paragraphCount = 1;
            string text = richTextBox.Text;
            foreach (char character in text)
            {
                if (character == '\n')
                {
                    paragraphCount++;
                }
            }
            return paragraphCount;
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.B)
            {
                if (richTextBox.SelectionFont.Style == FontStyle.Bold)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Regular);
                }
                else
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold);
                }
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                if (richTextBox.SelectionFont.Style == FontStyle.Italic)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Regular);
                    e.SuppressKeyPress = true;
                }
                else
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Italic);
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                saveToolStripMenuItem.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                loadToolStripMenuItem.PerformClick();
            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.ForeColor = colorDialog.Color;
                charactersTextLabel.ForeColor = colorDialog.Color;
                charactersValueLabel.ForeColor = colorDialog.Color;
                wordsTextLabel.ForeColor = colorDialog.Color;
                wordsValueLabel.ForeColor = colorDialog.Color;
                paragraphsTextLabel.ForeColor = colorDialog.Color;
                paragraphsValueLabel.ForeColor = colorDialog.Color;
                menuStrip.ForeColor = colorDialog.Color;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.BackColor = colorDialog.Color;
                menuStrip.BackColor = colorDialog.Color;
                panel1.BackColor = colorDialog.Color;
                this.BackColor = colorDialog.Color;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Font = fontDialog.Font;
            }
        }
    }
}
