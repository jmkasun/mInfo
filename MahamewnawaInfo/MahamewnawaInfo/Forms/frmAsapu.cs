using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MahamewnawaInfo.Common;
using DBCore.Classes;
using System.Collections;

namespace MahamewnawaInfo.Forms
{
    public partial class frmAsapu : DevComponents.DotNetBar.Office2007Form
    {
        int asapuwaID = 0;

        public frmAsapu()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBeforeAdd())
                {
                    using (Asapuwa asapuwa = new Asapuwa(true))
                    {
                        asapuwa.AsapuwaName = nameTextBoxX.Text;
                        asapuwa.ContactNumber1 = tp1textBox.Text;
                        asapuwa.ContactNumber2 = tp2TextBox.Text;
                        asapuwa.Address = addressTextBox.Text;
                        asapuwa.SangaUpasthayakahimi = 0;// sangaUpastayakaTheroComboBox.SelectedValue != null ? (int)sangaUpastayakaTheroComboBox.SelectedValue : 0;
                        asapuwa.OpeningDate = dateOfOpenDtm.Value.Date;
                        asapuwa.HeldUpasampada = false; // upasampadaCheckBox.Checked;
                        asapuwa.District = districtCombo.SelectedValue == null ? 0 : (int)districtCombo.SelectedValue;
                        asapuwa.PostalCode = postalcodeTextbox.Text;
                        asapuwa.Country = countryCombo.SelectedValue == null ? 0 : countryCombo.Text == "ශ්‍රී ලංකාව" ? 0 : (int)countryCombo.SelectedValue;

                        if (asapuwaID == 0)
                        {
                            if (asapuwa.Add() == 1)
                            {
                                MessageView.ShowMsg("Sucessfully Added");

                                //errorProvider1.SetError(idTxt, string.Empty);
                                //errorProvider1.SetError(nameTxt, string.Empty);

                                clear();
                            }

                        }
                        else
                        {
                            asapuwa.ID = asapuwaID;

                            if (MessageView.ShowQuestionMsg("Update '"+nameTextBoxX.Text+"' Asapuwa") == DialogResult.OK)
                            {
                                if (asapuwa.Update() == 1)
                                {
                                    MessageView.ShowMsg("Sucessfully Updated");

                                    //errorProvider1.SetError(idTxt, string.Empty);
                                    //errorProvider1.SetError(nameTxt, string.Empty);

                                    clear();
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message);
            }
        }

        private void clear()
        {
            asapuwaID = 0;
            nameTextBoxX.Clear();
            tp1textBox.Clear();
            tp2TextBox.Clear();
            addressTextBox.Clear();
            //upasampadaCheckBox.Checked = false;
            dateOfOpenDtm.Value = new DateTime();
            districtCombo.SelectedIndex = -1;


            deleteBtn.Enabled = false;
            addbtn.Text = "Insert";
            nameTextBoxX.Select();
            postalcodeTextbox.Clear();
        }

        private bool ValidateBeforeAdd()
        {

            try
            {
                bool result = true;

                // name
                if (string.IsNullOrEmpty(nameTextBoxX.Text))
                {
                    errorProvider1.SetError(nameTextBoxX, "Please Enter Name");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(nameTextBoxX, string.Empty);
                }

                return result;

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
                return false;
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (Asapuwa asapuwa = new Asapuwa(true))
                {
                    asapuwa.AsapuwaName = nameTextBoxX.Text;
                    asapuwa.District = districtCombo.SelectedValue != null ? (int)districtCombo.SelectedValue : 0;
                    asapuwa.ContactNumber1 = tp1textBox.Text;

                    DataTable ds = asapuwa.SelectFind();
                    frmSearch frmSub = new frmSearch(ds, this.Text, 4);
                    frmSub.Width = 800;
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
                asapuwaID = (int)hashtable["ID"];

                nameTextBoxX.Text = hashtable["AsapuwaName"].ToString();
                addressTextBox.Text = hashtable["Address"].ToString();
                tp1textBox.Text = hashtable["ContactNumber1"].ToString();
                tp2TextBox.Text = hashtable["ContactNumber2"].ToString();
                districtCombo.SelectedValue = (int)hashtable["DistrictID"];
                dateOfOpenDtm.Value = (DateTime)hashtable["OpeningDate"];
                postalcodeTextbox.Text = hashtable["PostalCode"].ToString();

                int country = (int)hashtable["Country"];
                if (country == 0)
                {
                    countryCombo.Text = "";
                    countryCombo.SelectedText = "ශ්‍රී ලංකාව";
                }
                else
                {
                    countryCombo.SelectedValue = country;
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            using (Asapuwa asapuwa = new Asapuwa(true))
            {
                asapuwa.ID = asapuwaID;

                if (MessageView.ShowQuestionMsg("Delete Details for '" + nameTextBoxX.Text + "' Asapuwa") == DialogResult.OK)
                {
                    asapuwa.Delete();
                    clear();
                    MessageView.ShowMsg("Sucessfully Deleted");
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void frmAsapu_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                //// Sanga Upasthayaka thero
                //using (BikkuInfo bInfo = new BikkuInfo(true))
                //{
                //    bInfo.BindToComboSangaUpsThero(sangaUpastayakaTheroComboBox);
                //}
                //sangaUpastayakaTheroComboBox.SelectedIndex = -1;

                // Sanga Upasthayaka thero
                using (District dis = new District(true))
                {
                    dis.BindToCombo(districtCombo);
                }
                districtCombo.SelectedIndex = -1;

                UpdateCountry();
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message, this.Location);
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData(1);
            ut.ShowForm((int)DBCore.UtilityDataName.Country, "Country");
          //  UpdateUtility((int)DBCore.UtilityDataName.Country, countryCombo);
        }

        private void UpdateCountry()
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(countryCombo, (int)DBCore.UtilityDataName.Country);
            }

            countryCombo.SelectedIndex = 0;
        }
    }
}