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

        private eTip v_editBalloon = new eTip();
        private eTip a_editBalloon = new eTip();

        public string getAttribName() { return _attribName; }
        public string getAttribVal() { return _attribVal; }

        public addAttribute()
        {
            InitializeComponent();

            // -- Hook KeyPress event for the MessageBox to the Keypress Method
            this.txtAttributeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAttributeName_KeyPress);
            this.txtAttributeValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAttributeValue_KeyPress);

            // -- Format the Pop-Up Balloon
            a_editBalloon.Title = "Format Warning";
            a_editBalloon.TitleIcon = TooltipIcon.Warning;
            a_editBalloon.Text = "You cannot use spaces, or begin an attribute with a number!";
            a_editBalloon.Parent = this.txtAttributeName;

            v_editBalloon.Title = "Format Warning";
            v_editBalloon.TitleIcon = TooltipIcon.Warning;
            v_editBalloon.Text = "You cannot use spaces in an attribute value";
            v_editBalloon.Parent = this.txtAttributeValue;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // attrib checking code here
            _attribName = txtAttributeName.Text;
            _attribVal = txtAttributeValue.Text;
            
            this.Close();
        }

        private void txtAttributeName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '1')
            {
                a_editBalloon.Show();
            }
        }

        private void txtAttributeValue_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                v_editBalloon.Show();
            }
        }
     
    }
}
