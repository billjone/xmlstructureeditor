using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xmlStructureEditor
{
    public partial class addData : Form
    {
        public string _data;

        public addData()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            setData(txtAddData.Text); // set the _data so it can be returned
            this.Close(); // kill the form
        }

        public void setData(string str)
        {
            _data = str;
        }

        public string getData()
        {
            return _data;
        }

        private void addData_Load(object sender, EventArgs e)
        {
            txtAddData.Text = getData();
        }
    }
}
