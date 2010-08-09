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
        private EditBalloon editBalloon = new EditBalloon();

        public addRootElement()
        {
            InitializeComponent();

            // -- Hook KeyPress event for the MessageBox to the Keypress Method
            this.txtRootEleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRootEleName_KeyPress);

            // -- Format the Pop-Up Balloon
            editBalloon.Title = "Format Warning";
            editBalloon.TitleIcon = TooltipIcon.Warning;
            editBalloon.Text = "You cannot use spaces, or begin the root element with a number!";
            editBalloon.Parent = this.txtRootEleName;
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

       

        private void txtRootEleName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '1')
            {
                editBalloon.Show();            
            }
        }
    }
}
