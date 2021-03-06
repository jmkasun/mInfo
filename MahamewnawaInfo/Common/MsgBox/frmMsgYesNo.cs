﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MahamewnawaInfo.Common
{
    public partial class frmMsgYesNo : DevComponents.DotNetBar.Office2007Form
    {
        string message = "Xtreme Soft Solutions";
        string title = "Xtreme Soft Solutions";

        public frmMsgYesNo(string msg, MessageBoxIcon icon,Point location)
        {
            InitializeComponent();
            this.Location = location;

            SetMessageBox(msg, icon);
        }

        public frmMsgYesNo(string msg, MessageBoxIcon icon)
        {
            InitializeComponent();

            SetMessageBox(msg, icon);
        }

        private void SetMessageBox(string msg, MessageBoxIcon icon)
        {
            try
            {
                message = msg;

                //switch (icon)
                //{
                //    case MessageBoxIcon.Error:
                //        this.refImgMsgImage.Image = global::ShopMannager.Properties.Resources.Delete;
                //        break;
                //    case MessageBoxIcon.Warning:
                //        this.refImgMsgImage.Image = global::ShopMannager.Properties.Resources.Exclamation;
                //        break;

                //    case MessageBoxIcon.Information:
                //        this.refImgMsgImage.Image = global::ShopMannager.Properties.Resources.Symbol_Information1;
                //        break;

                //    case MessageBoxIcon.Question:
                //        this.refImgMsgImage.Image = global::ShopMannager.Properties.Resources.Help;
                //        break;
                //}


                this.ShowDialog();

            }
            catch
            {

            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmMsgYesNo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = title;
                lblMsg.Text = message;
                //this.Width = lblMsg.Width + 133;
                //this.Height = lblMsg.Height + 130;
            }
            catch
            {
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblMsg_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.Width = Math.Max(lblMsg.Width + 133, 330);
                this.Height = Math.Max(lblMsg.Height + 130, 190);
            }
            catch
            {

            }
        }

        private void lblMsg_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
