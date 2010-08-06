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
    public partial class addComment : Form
    {

        string _comment;

        public addComment() { InitializeComponent(); }

        public string getComment() { return _comment; }

        public void setComment(string newStr)
        {
            _comment = newStr;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _comment = txtComment.Text;
            this.Close();
        }

        
    }
}
