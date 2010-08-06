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
    public partial class addElement : Form
    {

        private string _elementName;
        
        public addElement()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public string getElementName()
        {
            return _elementName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Code to check element name validity
            _elementName = this.txtElementName.Text;
            this.Close();

        }


    }
}
