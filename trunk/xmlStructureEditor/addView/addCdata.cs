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
    public partial class addCdata : Form
    {
        string _cdatatxt;

        public addCdata()
        {
            InitializeComponent();
        }

        public string getCdata()
        {
            return _cdatatxt;
        }

        public void setCdata(string data)
        {
            _cdatatxt = data;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            _cdatatxt = txtCdata.Text;
            this.Close();
        }


        private void addCdata_Load(object sender, EventArgs e)
        {
            if (_cdatatxt != null) { txtCdata.Text = _cdatatxt; }
        }
    }
}
