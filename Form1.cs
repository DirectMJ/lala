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
    }
}
