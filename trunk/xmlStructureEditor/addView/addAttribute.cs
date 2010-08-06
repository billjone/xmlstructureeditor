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
    public partial class addAttribute : Form
    {
        private string _attribName;
        private string _attribVal;

        public addAttribute()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // attrib checking code here

            _attribName = txtAttributeName.Text;
            _attribVal = txtAttributeValue.Text;
            
            this.Close();

        }

        public string getAttribName() { return _attribName; }
        public string getAttribVal() { return _attribVal; }
    }
}
