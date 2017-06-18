using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PathConverter
{
    public partial class Form1 : Form
    {
        private TextBox[] textboxes;

        public Form1()
        {
            InitializeComponent();

            textboxes = new[] { textBox2, textBox3, textBox4, textBox5 };
            var buttons = new[] { button1, button2, button3, button4 };
            for (int i = 0; i < buttons.Length; i++)
            {
                var btn = buttons[i];
                var index = i; // closures
                btn.Click += (sender, args) => copy(index);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();
            textBox1.Text = text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/yanxyz/PathConverter#readme");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var input = textBox1.Text.Trim();
            var results = Converter.Convert(input);
            for (int i = 0; i < textboxes.Length; i++)
            {
                textboxes[i].Text = results[i] ?? "";
            }
        }

        private void copy(int index)
        {
            var textbox = textboxes[index];
            var text = textbox.Text;
            if (text == String.Empty) return;

            try
            {
                Clipboard.SetText(text);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                var sb = new StringBuilder();
                sb.Append("Clipboard is being used by another process,\n");
                sb.Append("e.g. your download tool is watching Clipboard.\n");
                sb.Append("Use Ctrl+C shortcut instead.\n");
                MessageBox.Show(sb.ToString(), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            textbox.SelectAll();
            textbox.Focus();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                Application.Exit();
            }
        }
    }
}
