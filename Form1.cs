using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace PattaguanMj
{
    public partial class Form1 : Form
    {
        string query;
        OleDbConnection conn;
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFn.Text) ||
            string.IsNullOrWhiteSpace(txtLn.Text) ||
            cbDept.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields before adding to the list.");
                return;
            }
            else
            {
                dt.Rows.Add(txtFn.Text, txtLn.Text, cbDept.SelectedItem);
                dtGridView.DataSource = dt;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("Department");

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dt.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            resetValues();
        }

        public void resetValues()
        {
            txtFn.Text = "";
            txtLn.Text = "";
            cbDept.SelectedIndex = -1;
        }

        private void openExcel_Click(object sender, EventArgs e)
        {
            openFile.Title = "Open Excel";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Filter = "All files (*.*)|*.*|Excel File (*.xlsx)|*.xlsx";
            openFile.FilterIndex = 2;

            if (openFile.ShowDialog() == DialogResult.OK)
            {

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + openFile.FileName + "; Extended Properties='Excel 12.0 Xml; HDR=Yes'");

                query = "SELECT * FROM [Sheet1$]";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbDept.Items.Add(dt.Rows[i]["dept"].ToString());
                }
            }

        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openText.Title = "Open Text";
            openText.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openText.Filter = "All files (*.*)|*.*|Text File (*.txt)|*.txt";
            openText.FilterIndex = 2;

            if (openText.ShowDialog() == DialogResult.OK)
            {
                string[] departments = File.ReadAllLines(openText.FileName);
                for (int i = 0; i < departments.Length; i++)
                {
                    cbDept.Items.Add(departments[i]);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            saveFile.Title = "Save as file";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "(*.*)|*.*|Text File(*.txt)|*.txt";
            saveFile.FilterIndex = 2;


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = File.CreateText(saveFile.FileName))
                {

                    for (int i = 0; i < dtGridView.Rows.Count - 1; i++)
                    {
                        writer.WriteLine(dtGridView.Rows[i].Cells["First Name"].Value.ToString());
                        writer.WriteLine(dtGridView.Rows[i].Cells["Last Name"].Value.ToString());
                        writer.WriteLine(dtGridView.Rows[i].Cells["Department"].Value.ToString() + "\n");
                    }
                }
            }
        }
    }
}
