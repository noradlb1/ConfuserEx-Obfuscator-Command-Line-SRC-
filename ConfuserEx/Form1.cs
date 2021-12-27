using System;
using System.IO;
using System.Windows.Forms;

namespace ConfuserEx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public static bool CheckDotNet(byte[] executable)
        {
            return executable[60] == 128;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Executable(*.exe)|*.exe";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if(CheckDotNet(File.ReadAllBytes(ofd.FileName)))
                {
                    textBox1.Text = ofd.FileName;
                }
                else { MessageBox.Show("Is not .NET file", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {        
            if(textBox1.Text != String.Empty)
            {
                FileInfo fi = new FileInfo(textBox1.Text);
                string directory = fi.Directory.ToString();
                string filename = fi.FullName;
                button1.Enabled = false;
                textBox1.Enabled = false;
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();               
            }          
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Confuser.Obfuscate(textBox1.Text);
            button1.Enabled = true;
            textBox1.Enabled = true;
            button2.Enabled = true;
        }
    }
}
