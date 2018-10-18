using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using System.Collections;
using DBCore;
using MahamewnawaInfo.Common;

namespace MahamewnawaInfo.Forms
{
    public partial class frmUser : DevComponents.DotNetBar.Office2007Form
    {
        private int userID;
        public frmUser()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidateBeforeAdd())
                {
                    using (User user = new User(true))
                    {
                        user.Name = fNameTxt.Text;
                        user.UserName = usernameTxt.Text;
                        user.Password = MahamewnawaInfo.Common.Utility.GetMD5HashGUID(passwordTxt.Text).ToString();
                        user.Mobile = mobileTPTxt.Text;
                        user.PermissionLevel = userLevelCombo.SelectedIndex;
                        user.Email = emailText.Text;

                        if (validateName(user, userID))
                        {
                            if (userID == 0)
                            {

                                if (user.Add() == 1)
                                {
                                    MessageView.ShowMsg("Sucessfully added");
                                    clear();
                                }

                            }
                            else
                            {
                                user.ID = userID;
                                if (MessageView.ShowQuestionMsg("Update Current User") == DialogResult.OK)
                                {
                                    if (user.Update(changePwsCheckbox.Checked) == 1)
                                    {
                                        MessageView.ShowMsg("Sucessfully Updated");

                                        clear();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void clear()
        {
            userID = 0;

            fNameTxt.Clear();
            emailText.Clear();
            usernameTxt.Clear();
            passwordTxt.Clear();
            rePasswordTxt.Clear();
            mobileTPTxt.Clear();
            userLevelCombo.SelectedIndex = -1;

            errorProvider1.SetError(fNameTxt, string.Empty);
            errorProvider1.SetError(emailText, string.Empty);


            rePasswordTxt.Enabled = passwordTxt.Enabled = true;
            changePwsCheckbox.Visible = false;
            changePwsCheckbox.Checked = false;

            deleteBtn.Enabled = false;
            addbtn.Text = "Insert";
        }

        private bool validateName(User user, int userID)
        {
            //check username
            if (user.SelectExists(userID))
            {
                errorProvider1.SetError(usernameTxt, "User name Already Exist");
                return false;
            }
            else
            {
                errorProvider1.SetError(usernameTxt, string.Empty);
                return true;
            }
        }

        private bool ValidateBeforeAdd()
        {
            try
            {
                bool retVal = true;

                //////////
                if (string.IsNullOrEmpty(fNameTxt.Text))
                {
                    errorProvider1.SetError(fNameTxt, "Please Enter 'Name'");
                    retVal = false;
                }
                else
                {
                    errorProvider1.SetError(fNameTxt, string.Empty);
                }

                

                ////////////
                if (string.IsNullOrEmpty(usernameTxt.Text))
                {
                    errorProvider1.SetError(usernameTxt, "Please Enter 'User Name'");
                    retVal = false;
                }
                else
                {
                    errorProvider1.SetError(usernameTxt, string.Empty);
                }

                ////////////
                if (passwordTxt.Enabled && string.IsNullOrEmpty(passwordTxt.Text))
                {
                    errorProvider1.SetError(passwordTxt, "Please Enter 'Password'");
                    retVal = false;
                }
                else
                {
                    errorProvider1.SetError(passwordTxt, string.Empty);
                }          

                ////////////
                if (passwordTxt.Enabled)
                    if (!string.IsNullOrEmpty(passwordTxt.Text) && passwordTxt.Text == rePasswordTxt.Text)
                    {
                        errorProvider1.SetError(passwordTxt, string.Empty);
                        errorProvider1.SetError(rePasswordTxt, string.Empty);

                        if (passwordTxt.Text.Length < 3)
                        {
                            errorProvider1.SetError(passwordTxt, "Minimum password Length is 4");
                            retVal = false;
                        }

                    }
                    else
                    {
                        errorProvider1.SetError(passwordTxt, "Password does not match");
                        errorProvider1.SetError(rePasswordTxt, "Password does not match");
                        retVal = false;
                    }


                //if (DBCore.Utility.IsValiedPhoneNumber(mobileTPTxt.Text))
                //{
                //    errorProvider1.SetError(mobileTPTxt, string.Empty);

                //}
                //else
                //{
                //    retVal = false;
                //    errorProvider1.SetError(mobileTPTxt, "Invalied Number");
                //}

                //if (!string.IsNullOrEmpty(emailText.Text) && !YBS.Common.Utility.IsValidEmailAddress(emailText.Text))
                //{
                //    retVal = false;
                //    errorProvider1.SetError(emailText, "Invalied 'Email Address'");
                //}
                //else
                //{
                //    errorProvider1.SetError(emailText, string.Empty);
                //}

                return retVal;
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
                return false;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (User user = new User(true))
                {
                    user.Name = fNameTxt.Text;
                    user.UserName = usernameTxt.Text;
                    user.Mobile = mobileTPTxt.Text;
                    user.PermissionLevel = userLevelCombo.SelectedIndex;
                    user.Email = emailText.Text;

                    DataTable ds = user.SelectFind();
                    frmSearch frmSub = new frmSearch(ds, this.Text, 3);
                    frmSub.Width = 600;

                    HandleSearch(frmSub);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        // hadle operations after search
        private void HandleSearch(frmSearch frmSub)
        {
            //ApplicationSettings.ChildFormView(this.MdiParent, ref frmSub);
            if (frmSub.ShowDialog() == DialogResult.OK)
            {
                FillSearchFilds(frmSub.DataRowValues);

                deleteBtn.Enabled = true;
                addbtn.Text = "Update";
            }
            frmSub.Dispose();
        }


        public void FillSearchFilds(Hashtable hashtable)
        {
            try
            {
                userID = (int)hashtable["ID"];

                using (User user = new User(true))
                {
                    user.SelectUser(userID);



                    fNameTxt.Text = user.Name;
                    emailText.Text = user.Email;
                    usernameTxt.Text = user.UserName;
                    //rePasswordTxt.Text = passwordTxt.Text = hashtable["Pwd"].ToString();
                    mobileTPTxt.Text = user.Mobile;
                    userLevelCombo.SelectedIndex = user.PermissionLevel;
                    emailText.Text = user.Email;

                    rePasswordTxt.Enabled = passwordTxt.Enabled = false;
                    changePwsCheckbox.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (User cus = new User(true))
                {
                    cus.ID = userID;

                    if (MessageView.ShowQuestionMsg("Delete Customer " + fNameTxt.Text + "'") == DialogResult.OK)
                    {
                        cus.Delete();
                        clear();
                        MessageView.ShowMsg("Sucessfully Deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            userLevelCombo.SelectedIndex = -1;
        }

        private void changePwsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            rePasswordTxt.Enabled = passwordTxt.Enabled = changePwsCheckbox.Checked;
        }

    }
}
