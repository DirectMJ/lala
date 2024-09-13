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

namespace _046_Pattaguan_Langcay_L3
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        DataTable _dt = new DataTable();

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

        private void excelFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileD.Title = "Excel File";
            openFileD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileD.Filter = "All Files (.)|*.*|Excel File (.xlsx)|.xlsx";
            openFileD.FilterIndex = 2;
            openFileD.ShowDialog();

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; " +
            "Data Source=" + openFileD.FileName + "; Extended Properties='Excel 12.0 Xml; HDR=Yes'");
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _dt = dt;


            cbDept.DataSource = _dt;
            cbDept.DisplayMember = "dept";


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

        private void openFile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveText.Title = "Save as file";
            saveText.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveText.DefaultExt = "txt*;
            saveText.Filter = "All files (*.*) | *.*| Text File(*.txt) |*.txt";
            saveText - FilterIndex = 2;

            if (saveText.ShowDialog() = DialogResult.OK)
            {
                using (Streashriter writer = File.CreateText(saveText.Filelase))
                {
                    writer.WriteLine(txtDisplay.Text);
                    txtDisplay.Clear();
                }
            }
        }
    }
}
