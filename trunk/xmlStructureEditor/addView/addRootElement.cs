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
    public partial class addRootElement : Form
    {
        private string _rootElementName;

        public addRootElement()
        {
            InitializeComponent();
        }

        private void rootElement_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validity checking + tooltip advice here
            _rootElementName = this.txtRootEleName.Text;
            this.Close();
        }

        public string getRootElement()
        {
            return _rootElementName;
        }
    }
}
