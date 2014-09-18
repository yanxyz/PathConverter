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

namespace Backslash2Slash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Clipboard.GetText();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            textBox1.Text = Clipboard.GetText();
            Activate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            textBox2.Text = txt.Replace(@"\", "/");
            textBox3.Text = txt.Replace(@"\", @"\\");
        }

        private void copy(string txt)
        {
            try
            {
                Clipboard.SetText(txt);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            copy(textBox2.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            copy(textBox3.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL);
        }
        
    }
}
