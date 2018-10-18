using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using System.IO;

namespace MahamewnawaInfo.Admin
{
    public partial class frmDBPassworod : DevComponents.DotNetBar.Office2007Form
    {
        public frmDBPassworod()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //validate
            if (pwdText.Text != repwdtext.Text)
            {
                errorProvider1.SetError(repwdtext, "Re-Password Not Match");
                return;
            }

            // reset error provider
            errorProvider1.SetError(repwdtext, "");

            // create password file
            DBCore.Utility.CreateDBPassword(pwdText.Text);

            /// test connection with current password
            try
            {
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.SelectBhikku(-1);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // reset connectionstring
                DBCore.Utility.ConnectionString = string.Empty;
                if (File.Exists(DBCore.Utility.DBConfigDataFile))
                {
                    File.Delete(DBCore.Utility.DBConfigDataFile);
                }

                pwdText.Focus();


                MessageBox.Show("Invalied Usename\n" + ex.Message);
            }
        }

        private void repwdtext_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && repwdtext.Focused)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
