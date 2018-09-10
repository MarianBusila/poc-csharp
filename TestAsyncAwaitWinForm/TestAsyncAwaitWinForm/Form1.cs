using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAsyncAwaitWinForm
{
    using System.IO;
    using System.Threading;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int CountCharacters()
        {
            int count = 0;
            using (StreamReader reader = new StreamReader("../../data.txt"))
            {
                string content = reader.ReadToEnd();
                count = content.Length;
                Thread.Sleep(5000);
            }

            return count;
        }

        private async void buttonProcessFile_Click(object sender, EventArgs e)
        {
            Task<int> task = new Task<int>(this.CountCharacters);
            task.Start();

            this.labelProcessFile.Text = "Processing File. Please wait...";
            int count = await task;
            this.labelProcessFile.Text = count.ToString() + " characters in file";
        }
    }
}
