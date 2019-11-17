using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertFilesToBLOB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConvertForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = fbd.SelectedPath;

                string[] files = Directory.GetFiles(path);
                List<byte[]> blobList = new List<byte[]>();
                string message = "";
                foreach (var file in files)
                {
                    byte[] fileBytes = File.ReadAllBytes(file);
                    blobList.Add(fileBytes);
                    message += file + "\n";
                }

                foreach(byte[] blob in blobList)
                {
                    System.Diagnostics.Debug.WriteLine(blob);
                }

                MessageBox.Show(message);
            }
        }
    }
}
