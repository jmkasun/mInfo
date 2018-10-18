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

        private int frmLocationX = 400;
        private int frmLocationY = 400;

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
                frmObj.MdiParent = this;
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
                frmLoginWindow login = new frmLoginWindow(this);
                //if (login.ShowDialog() == DialogResult.OK)
                //{
                //    user = login.user;
                //}

                PrepareTabs();

                SetBGImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        }

        //private void bhikkuInfoButtonItem_Click(object sender, EventArgs e)
        //{
        //     try
        //     {
        //         if (frmBInfo == null || frmBInfo.MdiParent == null)
        //         {
        //             frmBInfo = new frmBikkuInfo();
        //             ViewChildForm(frmBInfo);
        //         }
        //         else
        //         {
        //             frmBInfo.Activate();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show(ex.Message);
        //     }
        //}

        private void asapuwaButtonItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmAsapuwa == null || frmAsapuwa.MdiParent == null)
                {
                    frmAsapuwa = new frmAsapu();
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
                    frmBInfo = new frmBikkuInfo();
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
                    frmAsapuwa = new frmAsapu();
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
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            asapuVistharaToolStripMenuItem_Click(sender, e);
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmChangeList == null || frmChangeList.MdiParent == null)
                {
                    frmChangeList = new frmChangelistParams();
                    frmChangeList.MdiParent = this;
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
                    rptNameID = new rptNameIDReport();
                    rptNameID.MdiParent = this;
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
    }
}
