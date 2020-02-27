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
            List<FileObject> files = new List<FileObject>();
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\phill\Source\Repos\ConvertFilesToBLOB-WindowsFormsApp\ConvertFilesToBLOB\DBmdf.mdf;Integrated Security=True"))
            {
                con.Open();
                List<string> tables = new List<string>();
                DataTable dt = con.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
                MessageBox.Show(tables.ToString(), "", "", MessageBoxButtons.);
                //string query = "SELECT * FROM PathStorage";
                //using (SqlCommand cmd = new SqlCommand(query, con))
                //{
                //    cmd.CommandType = CommandType.Text;
                //    using (SqlDataReader dr = cmd.ExecuteReader())
                //    {
                //        while (dr.Read())
                //        {
                //            FileObject fo = new FileObject();
                //            fo.fileData = File.ReadAllBytes(dr["FilePath"].ToString());
                //            fo.fileName = dr["FileName"].ToString();
                //            files.Add(fo);
                //        }

                //    }
                //}
                con.Close();
            }

            #region Direct Table Hits
            //using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\phill\Source\Repos\ConvertFilesToBLOB-WindowsFormsApp\ConvertFilesToBLOB\DBmdf.mdf;Integrated Security=True"))
            //{
            //    con.Open();
            //    string query = "SELECT * FROM PathStorage";
            //    using (SqlCommand cmd = new SqlCommand(query, con))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                FileObject fo = new FileObject();
            //                fo.fileData = File.ReadAllBytes(dr["FilePath"].ToString());
            //                fo.fileName = dr["FileName"].ToString();
            //                files.Add(fo);
            //            }

            //        }
            //    }
            //    con.Close();
            //}

            ////open db connection for insert of BLOB
            //using (SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\phill\Source\Repos\ConvertFilesToBLOB-WindowsFormsApp\ConvertFilesToBLOB\DBmdf.mdf;Integrated Security=True"))
            //{
            //    cn.Open();
            //    var command = "INSERT INTO BlobStorage(FileData, FileName) VALUES(@param1, @param2)";
            //    using (SqlCommand cmd = new SqlCommand(command, cn))
            //    {
            //        foreach (FileObject fo in files)
            //        {
            //            cmd.Parameters.Add("@param1", SqlDbType.VarBinary, -1).Value = fo.fileData;
            //            cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 100).Value = fo.fileName;
            //            cmd.ExecuteNonQuery();
            //            cmd.Parameters.Clear();
            //        }
            //    }
            //}
            #endregion

            #region OPEN A FOLDER and CONVERT ALL FILES IN THAT FOLDER TO BLOB
            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    var path = fbd.SelectedPath;

            //    string[] files = Directory.GetFiles(path);
            //    List<byte[]> blobList = new List<byte[]>();

            //    string message = "";

            //    foreach (var file in files)
            //    {
            //        byte[] fileBytes = File.ReadAllBytes(file);
            //        blobList.Add(fileBytes);
            //        message += file + "\n";
            //    }



            //    //open db connection for insert of BLOB
            //    using (SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\phill\Source\Repos\ConvertFilesToBLOB-WindowsFormsApp\ConvertFilesToBLOB\DBmdf.mdf;Integrated Security=True"))
            //    {
            //        cn.Open();
            //        var command = "INSERT INTO BlobStorage(FileData, FileName) VALUES(@param1, @param2)";
            //        using (SqlCommand cmd = new SqlCommand(command, cn))
            //        {
            //            foreach (byte[] blob in blobList)
            //            {
            //                try
            //                {
            //                    cmd.Parameters.Add("@param1", SqlDbType.VarBinary, -1).Value = blob;
            //                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 100).Value = "filename";
            //                    cmd.ExecuteNonQuery();
            //                }
            //                catch(Exception ex)
            //                {

            //                }
            //            }
            //        }
            //    }

            //    MessageBox.Show(message);
            //}
            #endregion
        }
    }
}

public class FileObject
{
    public byte[] fileData { get; set; }
    public string fileName { get; set; }
}
