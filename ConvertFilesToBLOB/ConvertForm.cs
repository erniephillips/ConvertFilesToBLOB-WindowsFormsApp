using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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



                //open db connection for insert of BLOB
                using (SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBmdf.mdf;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        foreach (byte[] blob in blobList)
                        {
                            //cmd.CommandText = "INSERT INTO BlobStorage([File], FileName) VALUES(@param1, @param2)";
                            cmd.CommandText = String.Format("INSERT INTO BlobStorage(FileName, [File]) VALUES ('file', CAST('{0}' AS VARBINARY(MAX)))", blob.ToString());

                            cmd.Parameters.Add("@param1", SqlDbType.VarBinary, -1).Value = blob;
                            cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 100).Value = "filename";
                            cmd.Connection = cn; //this was where the error originated in the first place.
                            cn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBmdf.mdf;Integrated Security=True";
                //using (SqlConnection con = new SqlConnection(connectionString))
                //{
                //    foreach (byte[] blob in blobList)
                //    {
                //        string sql = "INSERT INTO BlobStorage([File], FileName) VALUES(@param1, @param2)";
                //        using (SqlCommand cmd = new SqlCommand(sql, con))
                //        {
                //            con.Open();

                //            cmd.Parameters.Add("@param1", SqlDbType.VarBinary).Value = blob;
                //            cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 100).Value = "filename";
                //            cmd.CommandType = CommandType.Text;
                //            cmd.ExecuteNonQuery();
                //        }
                //    }
                //    con.Close();
                //}

                MessageBox.Show(message);
            }
        }
    }
}
