using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MahamewnawaInfo.Common
{
    public partial class frmComment : DevComponents.DotNetBar.Office2007Form
    {
       public string Comment =string.Empty;

        public frmComment(string cmt)
        {
            Comment = cmt;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Comment = textBoxX1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmComment_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = Comment;
        }

    }
}
