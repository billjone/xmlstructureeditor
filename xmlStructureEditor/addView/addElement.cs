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
        private EditBalloon editBalloon = new EditBalloon();

        public addElement()
        {
            InitializeComponent();

            // -- Hook KeyPress event for the MessageBox to the Keypress Method
            this.txtElementName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtElementName_KeyPress);

            // -- Format the Pop-Up Balloon
            editBalloon.Title = "Format Warning";
            editBalloon.TitleIcon = TooltipIcon.Warning;
            editBalloon.Text = "You cannot use spaces, or begin an element with a number!";
            editBalloon.Parent = this.txtElementName;
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

        private void txtElementName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '1')
            {
                editBalloon.Show();
            }
        }

    }
}
