using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MahamewnawaInfo.Common;
using ShopMannager.Admin;
using DBCore.Classes;
using MahamewnawaInfo.Forms;
using MahamewnawaInfo.Reporting.Viwer;
using MahamewnawaInfo.Admin;
using DBCore;
using MahamewnawaInfo.Reporting;

namespace MahamewnawaInfo
{
    public partial class MDIParent1 : Form
    {
        User user = null;

        private Form frmBInfo;
        private Form frmAsapuwa;
        private Form frmReportViwer;
        private Form frmChangeList;
        private Form rptNameID;
        private Form rptBinfo;
        private Form frmDBPwd;
        private Form frmUsers;
        private Form rptAllBhikkuSummary;
        private Form rptAllBhikkuasp;
        private Form rptAllBhikkuaspImg;
        private Form rptAllBhikkuImg;
        private Form rptSanghaUpasthayaka;
        private Form rptCustomReport;
        private Form frmChangeRequest;

        public MDIParent1()
        {
            InitializeComponent();
        }

        //private void agaDivisionButtonItem_Click(object sender, EventArgs e)
        //{
        //    ShowAGADivisionForm();
        //}

        public void ViewChildForm(Form frmObj)
        {
            try
            {
                ViewChildForm(frmObj, false);
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }



        public void ViewChildForm(Form frmObj, bool isMax)
        {
            try
            {
                //if (frmLocationY < 500)
                //{
                //    frmLocationY += 10;
                //    frmLocationX = ((this.Width - frmObj.Width) / 2) + (int)(frmLocationY / 1.5);
                //}
                //else
                //{
                //    frmLocationY = 10;
                //    frmLocationX = (this.Width - frmObj.Width) / 2;
                //}


                //frmObj.Location = new Point(frmLocationX, frmLocationY);

                frmObj.StartPosition = FormStartPosition.CenterScreen;
                //frmObj.MdiParent = this;
                frmObj.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void mdiMain_Load_1(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(DBCore.Utility.DBConfigDataFile))
                {
                    SetDBPassword();
                }

                logginHandle();

                PrepareTabs();

                SetBGImage();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logginHandle()
        {
            user = new User() { PermissionLevel = 3, Name = "kasun" };
           /** frmLoginWindow login = new frmLoginWindow(this);

            if (login.ShowDialog() == DialogResult.OK)
            {
                user = login.user;
            }
           */
            PrepareTabs();
        }

        private void SetBGImage()
        {

            string bgImageFile = DBCore.Utility.GetAppsetting(DBCore.AppSetting.BgImage);

            if (!string.IsNullOrEmpty(bgImageFile) && File.Exists(bgImageFile))
            {

                this.BackgroundImage = Image.FromFile(DBCore.Utility.GetAppsetting(DBCore.AppSetting.BgImage));
            }
            else
            {
                //this.BackgroundImage = global::CCMPData.Properties.Resources.bg1231;
            }
        }

        private void PrepareTabs()
        {
            if (user!=null && user.PermissionLevel == (int)UserLevel.SystemAdmin)
            {
                changeButton.Enabled = dbpwdBtn.Enabled = userBtn.Enabled = true;

            }
            else
            {
                changeButton.Enabled = dbpwdBtn.Enabled = userBtn.Enabled = false;
            }

            if (user != null && user.PermissionLevel > -1)
            {
                //myDataButton.Enabled = true;
                //addmemberButtonItem.Enabled = true;
                //occupatitionButtonItem.Enabled = true;
                //schoolBtn.Enabled = true;
                //educationBtn.Enabled = true;

            }
            else
            {
                //myDataButton.Enabled = false;
                //addmemberButtonItem.Enabled = false;
                //occupatitionButtonItem.Enabled = false;
                //schoolBtn.Enabled = false;
                //educationBtn.Enabled = false;
            }
        }


        private void asapuwaButtonItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmAsapuwa == null || frmAsapuwa.MdiParent == null)
                {
                    frmAsapuwa = new frmAsapu(user.PermissionLevel);
                    ViewChildForm(frmAsapuwa);
                }
                else
                {
                    frmAsapuwa.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmReportViwer == null || frmReportViwer.MdiParent == null)
                {
                    frmReportViwer = new frmReportViwer();
                    ViewChildForm(frmReportViwer);
                }
                else
                {
                    frmReportViwer.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmBInfo == null || frmBInfo.MdiParent == null)
                {
                    frmBInfo = new frmBikkuInfo(user.PermissionLevel);
                    ViewChildForm(frmBInfo);
                }
                else
                {
                    frmBInfo.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void asapuVistharaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmAsapuwa == null || frmAsapuwa.MdiParent == null)
                {
                    frmAsapuwa = new frmAsapu(user.PermissionLevel);
                    frmAsapuwa.WindowState = FormWindowState.Normal;
                    ViewChildForm(frmAsapuwa);
                }
                else
                {
                    frmAsapuwa.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            dataToolStripMenuItem_Click(sender, e);
            ribbonControl1.Height = 55;
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            asapuVistharaToolStripMenuItem_Click(sender, e);
            ribbonControl1.Height = 55;
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            ribbonControl1.Height = 55;
            try
            {
                if (frmChangeList == null || frmChangeList.MdiParent == null)
                {
                    frmChangeList = new frmChangelistParams();
                  //  frmChangeList.MdiParent = this;
                    frmChangeList.Location = new Point(0, 0);
                    frmChangeList.Show();

                }
                else
                {
                    frmChangeList.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptNameID == null || rptNameID.MdiParent == null)
                {
                    rptNameID = new rptBhikkuStatus();
                   // rptNameID.MdiParent = this;
                    rptNameID.Location = new Point(0, 0);
                    rptNameID.Show();

                }
                else
                {
                    rptNameID.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDBPassword()
        {
            try
            {
                if (frmDBPwd == null || frmDBPwd.MdiParent == null)
                {
                    frmDBPwd = new frmDBPassworod();
                    //frmDBPwd.MdiParent = this;
                    frmDBPwd.Location = new Point(0, 0);
                    frmDBPwd.ShowDialog();
                    frmDBPwd.StartPosition = FormStartPosition.CenterScreen;

                }
                else
                {
                    frmDBPwd.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SetDBPassword();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmUsers == null || frmUsers.MdiParent == null)
                {
                    frmUsers = new frmUser();
                   // frmUsers.MdiParent = this;
                    frmUsers.Location = new Point(0, 0);
                    frmUsers.Show();

                }
                else
                {
                    frmUsers.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logoffButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            logginHandle();
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Close Application ?") == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void ribbonTabItem1_Click(object sender, EventArgs e)
        {
            ribbonControl1.Height = 105;
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptNameID == null || rptNameID.MdiParent == null)
                {
                    rptNameID = new rptBhikkuStatus();
                   // rptNameID.MdiParent = this;
                    rptNameID.Location = new Point(0, 0);
                    rptNameID.Show();

                }
                else
                {
                    rptNameID.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (rptBinfo == null || rptBinfo.MdiParent == null)
                {
                    rptBinfo = new rptBhikkuReport();
                   // rptBinfo.MdiParent = this;
                    rptBinfo.Location = new Point(0, 0);
                    rptBinfo.Show();

                }
                else
                {
                    rptBinfo.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void allReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptAllBhikkuSummary == null || rptAllBhikkuSummary.MdiParent == null)
                {
                    rptAllBhikkuSummary = new rptAllBhikkuSummary();
                    //rptAllBhikkuSummary.MdiParent = this;
                    rptAllBhikkuSummary.Location = new Point(0, 0);
                    rptAllBhikkuSummary.Show();

                }
                else
                {
                    rptAllBhikkuSummary.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (rptAllBhikkuasp == null || rptAllBhikkuasp.MdiParent == null)
                {
                    rptAllBhikkuasp = new rpt_AllbhikkuAsapuwa();
                   // rptAllBhikkuasp.MdiParent = this;
                    rptAllBhikkuasp.Location = new Point(0, 0);
                    rptAllBhikkuasp.Show();

                }
                else
                {
                    rptAllBhikkuasp.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptAllBhikkuImg == null || rptAllBhikkuImg.MdiParent == null)
                {
                    rptAllBhikkuImg = new AllBhikkuImage();
                   // rptAllBhikkuImg.MdiParent = this;
                    rptAllBhikkuImg.Location = new Point(0, 0);
                    rptAllBhikkuImg.Show();

                }
                else
                {
                    rptAllBhikkuImg.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSanghaUpasthayaka_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptSanghaUpasthayaka == null || rptSanghaUpasthayaka.MdiParent == null)
                {
                    rptSanghaUpasthayaka = new rpt_SanghaUpasthayaka();
                   // rptSanghaUpasthayaka.MdiParent = this;
                    rptSanghaUpasthayaka.Location = new Point(0, 0);
                    rptSanghaUpasthayaka.Show();

                }
                else
                {
                    rptSanghaUpasthayaka.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptAllBhikkuaspImg == null || rptAllBhikkuaspImg.MdiParent == null)
                {
                    rptAllBhikkuaspImg = new rpt_AllbhikkuAsapuwaWithImage();
                   // rptAllBhikkuaspImg.MdiParent = this;
                    rptAllBhikkuaspImg.Location = new Point(0, 0);
                    rptAllBhikkuaspImg.Show();

                }
                else
                {
                    rptAllBhikkuaspImg.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptCustomReport == null || rptCustomReport.MdiParent == null)
                {
                    rptCustomReport = new frm_CustomReport();
                    ViewChildForm(rptCustomReport);
                }
                else
                {
                    rptCustomReport.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmChangeRequest == null || frmChangeRequest.MdiParent == null)
                {
                    frmChangeRequest = new frmChangeRequest();
                    ViewChildForm(frmChangeRequest);
                    frmChangeRequest.MdiParent = null;
                }
                else
                {
                    frmChangeRequest.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
