using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using MahamewnawaInfo.Common;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using DevComponents.Editors;
using MySql.Data.MySqlClient;

namespace MahamewnawaInfo.Forms
{
    public partial class frmBikkuInfo : DevComponents.DotNetBar.Office2007Form
    {
        public int bhikkuID = 0;
        byte[] bikkuImage = null;
        List<OtherLang> otherLanguages = new List<OtherLang>();
        string currentStatusComment = string.Empty;
        List<int> findIDList = new List<int>();
        int findListSelectedIndex = -1;

        InputLanguage lan = InputLanguage.CurrentInputLanguage;
        int TempleOfResidenceBeforeChange = 0;
        string TempleOfResidenceBeforeChangeName = "";
        bool isSearch = false;
        int histryID = 0;
        DataGridViewRow histryRow = null;
        int nextNumber = 0;
        BikkuInfo nextPrevbInfo;
        bool bhikkuLoading = false;

        DBCore.UserLevel userLevel = DBCore.UserLevel.SystemUser;

        public frmBikkuInfo(int permissionLevel)
        {
            userLevel = (DBCore.UserLevel)permissionLevel;
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                int ret = 0;
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    if (ValidateBeforeAdd(bInfo))
                    {

                        SetBInfoFromFields(bInfo);

                        if (bhikkuID == 0)
                        {
                            setActivitiesToObject(bInfo);
                            setAsapuHistryToObject(bInfo);
                            setOtherDataToObject(bInfo);


                            try
                            {
                                ret = bInfo.Add(false);

                            }
                            catch (MySqlException ex)
                            {
                                if (ex.Message.EndsWith("'number_UNIQUE'"))
                                {
                                    if (MessageView.ShowQuestionMsg("Duplicate 'Number' Found\n\nInsert Anyway") == System.Windows.Forms.DialogResult.OK)
                                    {
                                        ret = bInfo.Add(true);
                                    }
                                }
                                else
                                {
                                    throw;
                                }
                            }

                            if (ret == 1)
                            {
                                statusLbl.Text = "Sucessfully Added";
                                timer1.Enabled = true;
                                clear();
                            }


                        }
                        else
                        {
                            bInfo.ID = bhikkuID;

                            if (MessageView.ShowQuestionMsg("Update Current Item") == DialogResult.OK)
                            {


                                // if templae changed, ask for add record to histry
                                if (TempleOfResidenceBeforeChange != bInfo.TempleOfResidence)
                                {
                                    string message = string.Concat("<br/><br/><b>Asapuwa&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;</b>", TempleOfResidenceBeforeChangeName, "\r\n<br/><b>From Date&nbsp;&nbsp;:&nbsp;</b>", histryDatagrid.Rows[histryDatagrid.RowCount - 1].Cells[4].Value, "\r\n<br/><b>To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;</b>", DateTime.Now.ToString("yyyy-MMM-dd"));
                                    if (MessageView.ShowQuestionMsg("Residence temple has edited, Add histry record? \n\n" + message) == System.Windows.Forms.DialogResult.OK)
                                    {

                                    }
                                }


                                try
                                {
                                    ret = bInfo.Update(false); ;

                                }
                                catch (MySqlException ex)
                                {
                                    if (ex.Message.EndsWith("'number_UNIQUE'"))
                                    {
                                        if (MessageView.ShowQuestionMsg("Duplicate 'Number' Found\n\nUpdate Anyway") == System.Windows.Forms.DialogResult.OK)
                                        {
                                            ret = bInfo.Update(true);
                                        }
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }

                                if (ret == 1 && sender.ToString() != nextBtn.Text)
                                {
                                    statusLbl.Text = "Sucessfully Updated";
                                    timer1.Enabled = true;

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

        private void SetBInfoFromFields(BikkuInfo bInfo)
        {
            bInfo.NIC = nicTextBoxX.Text;
            bInfo.SamaneraNumber = samaneraNumberTextBoxX.Text;
            bInfo.PassportNumber = passportNumbrtTextBoxX.Text;
            bInfo.PlaceOfBirth = placeOfBirthtxt.Text;
            bInfo.LayNameInFull = layNameinFullTxt.Text;
            bInfo.DateOfBirth = dobDtm.Value.Date;
            bInfo.NameOfFatherInFull = nameOfFatheinFullTxt.Text;
            bInfo.DateOfRobing = dateOfRobingDtm.Value.Date;
            bInfo.NameAssumedAtRobing = nameOfAssumedAtRobinCombo.Text;
            bInfo.TempleRobing = templaRobingTookCombo.SelectedValue == null ? 0 : (int)templaRobingTookCombo.SelectedValue;
            bInfo.TempleOfResidence = templaResidenceCombo.SelectedValue == null ? 0 : (int)templaResidenceCombo.SelectedValue;

            bInfo.IsUpasampanna = upasampadaCheckbox.Checked;
            bInfo.PlaceOfHigherOrdination = upasampannaPlaceComboBoxEx.SelectedValue == null ? 0 : (int)upasampannaPlaceComboBoxEx.SelectedValue;
            bInfo.DateOfHigherOrdination = upasampannaDatetp.Value.Date;
            bInfo.NameOfUpadyaAtHigherOrdination = nameofUpadyaComboBoxEx.SelectedValue == null ? 0 : (int)nameofUpadyaComboBoxEx.SelectedValue;
            bInfo.District = districtcomboBox.SelectedValue == null ? 0 : (int)districtcomboBox.SelectedValue;
            bInfo.DateOfCame = dateofCameDtm.Value; // == new DateTime()
            bInfo.HomeAddress = homeaddressText.Text;
            bInfo.HomeTP = hometpText.Text;

            bInfo.Dharmadeshanaa = dharmaDeshanaCheckbox.CheckState == CheckState.Checked;
            bInfo.Vandanaa = vandanaCheckbox.CheckState == CheckState.Checked;
            bInfo.Sajjayana = sajjayanaCheckbox.CheckState == CheckState.Checked;
            bInfo.Sinhala = sinhalaCheckbox.CheckState == CheckState.Checked;
            bInfo.Tamil = tamilCheckbox.CheckState == CheckState.Checked;
            bInfo.Hindhi = hindiCheckbox.CheckState == CheckState.Checked;
            bInfo.English = englishCheckbox.CheckState == CheckState.Checked;

            // add other languages
            foreach (OtherLang lang in otherLanguages)
            {
                bInfo.OtheLanguage += string.Concat(lang.ID, ",");
            }


            if (bInfo.IsUpasampanna)
            {
                bInfo.UpasampadaNumber = upasampadaNumberTextbox.Text;
                bInfo.UpasampadaRegDate = upasampadaRegDate.Value;

                bInfo.KarmacharyaHimi1 = karmacharyaHimi1.Text; // == null ? 0 : (int)karmacharyaHimi1.SelectedValue;
                bInfo.KarmacharyaHimi2 = karmacharyaHimi2.Text; //.SelectedValue == null ? 0 : (int)karmacharyaHimi2.SelectedValue;
                bInfo.KarmacharyaHimi3 = karmacharyaHimi3.Text; //.SelectedValue == null ? 0 : (int)karmacharyaHimi3.SelectedValue;

                bInfo.UpasampadaMahaNayakaHimidetails = upasampadaMahanayakaHimiCombo.SelectedValue == null ? 0 : (int)upasampadaMahanayakaHimiCombo.SelectedValue;
                bInfo.UpasampadaAcharyaHimiDetails = upasampadaAcharyaHimiCombo.SelectedValue == null ? 0 : (int)upasampadaAcharyaHimiCombo.SelectedValue;
                bInfo.UpasampadaNikaya = upasampadaNikayaCombo.SelectedValue == null ? 0 : (int)upasampadaNikayaCombo.SelectedValue;
                bInfo.UpadyaTheroName = upadyaHimiCombo.SelectedValue == null ? 0 : (int)upadyaHimiCombo.SelectedValue;
            }

            bInfo.MahaNayakaHimidetails = mahanayakaHimiCombo.SelectedValue == null ? 0 : (int)mahanayakaHimiCombo.SelectedValue;
            bInfo.AcharyaHimiDetails = acharyaHimiCombo.SelectedValue == null ? 0 : (int)acharyaHimiCombo.SelectedValue;
            bInfo.Nikaya = nikayaHimiCombo.SelectedValue == null ? 0 : (int)nikayaHimiCombo.SelectedValue;

            bInfo.Country = countryCombo.SelectedValue == null ? 0 : (int)countryCombo.SelectedValue;
            bInfo.Number = (int)numberNumericUpDown.Value;

            bInfo.CurrentStatus = (DBCore.CurrenStatus)(sitiRadio.Checked ? 1 : otherPlaceRadio.Checked ? 2 : upavidiRadio.Checked ? 3 : otherPlaceResignStdRadio.Checked ? 6 : 5);
            bInfo.CurrentStatusComment = bInfo.CurrentStatus == DBCore.CurrenStatus.Siti ? string.Empty : currentStatusComment;
            bInfo.HomeTP2 = hometp2Text.Text;

            if (bloodGroupCombo.SelectedItem != null)
            {
                ComboItem item = (ComboItem)bloodGroupCombo.SelectedItem;
                bInfo.BloodGroup = item.Text;
            }

            if (bikkuImage == null)
            {
                bInfo.ImageData = string.Empty;
            }
            else
            {
                bInfo.ImageData = DBCore.Utility.Get64String(bikkuImage);
            }
        }

        private void setActivitiesToObject(BikkuInfo bInfo)
        {
            bInfo.Activities = new List<Activity>();

            if (activityDatagrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in activityDatagrid.Rows)
                {
                    bInfo.Activities.Add(new Activity(0, row.Cells[0].Value.ToString()));
                }
            }
        }


        private void setAsapuHistryToObject(BikkuInfo bInfo)
        {
            bInfo.AsapuHistry = new List<BhikkuAsapuHistry>();

            if (histryDatagrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in histryDatagrid.Rows)
                {
                    DateTime fromDate = DateTime.Parse(row.Cells[3].Value.ToString());
                    DateTime toDate = DateTime.Parse(row.Cells[4].Value.ToString());
                    DBCore.BhikkuPost post = (DBCore.BhikkuPost)(row.Cells[5].Value.ToString() == "ස: උ:" ? 1 : row.Cells[5].Value.ToString() == "අනු ස: උ:" ? 2 : 0);

                    bInfo.AsapuHistry.Add(new BhikkuAsapuHistry(0, (int)row.Cells[1].Value, fromDate, toDate, "", post, row.Cells[6].Value.ToString()));
                }
            }
        }

        private void setOtherDataToObject(BikkuInfo bInfo)
        {
            bInfo.OtherData = new List<OtherData>();

            if (otherDatagrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in otherDatagrid.Rows)
                {
                    bInfo.OtherData.Add(new OtherData(0, row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), (byte[])row.Cells[4].Value));
                }
            }
        }

        private void clear()
        {
            bhikkuID = 0;
            nicTextBoxX.Clear();
            samaneraNumberTextBoxX.Clear();
            passportNumbrtTextBoxX.Clear();
            placeOfBirthtxt.Clear();
            layNameinFullTxt.Clear();
            captionName.Text = string.Empty;

            nameOfFatheinFullTxt.Clear();
            nameOfAssumedAtRobinCombo.Text = "";
            nameOfAssumedAtRobinCombo.SelectedIndex = -1;

            upasampannaPlaceComboBoxEx.SelectedIndex = -1;
            nameofUpadyaComboBoxEx.SelectedIndex = -1;
            districtcomboBox.SelectedIndex = -1;
            templaResidenceCombo.SelectedIndex = -1;
            templaRobingTookCombo.SelectedValue = -1;

            dateOfRobingDtm.Value = new DateTime();
            dateOfRobingBYDtm.Value = new DateTime();

            upasampannaDatetp.Value = new DateTime();
            upasampannaBYDatetp.Value = new DateTime();

            dobDtm.Value = new DateTime();
            dateofCameDtm.Value = new DateTime();

            pictureBox.Image = null;
            bikkuImage = null;
            deleteBtn.Enabled = false;

            addbtn.Text = "Insert";

            activityDatagrid.Rows.Clear();

            asapuHistryFrom.Value = new DateTime();
            asapuHistryTo.Value = new DateTime();
            asapuHistrAsapu.SelectedIndex = -1;

            otherDatagrid.Rows.Clear();
            descriptionTxt.Clear();
            imageName.Clear();
            hometp2Text.Clear();

            otherLangPanel.Controls.Clear();

            homeaddressText.Clear();
            hometpText.Clear();
            bloodGroupCombo.SelectedIndex = -1;
            countryCombo.SelectedIndex = 0;

            changeCheckboxState(CheckState.Indeterminate);

            SetNextNumber();


            mahanayakaHimiCombo.SelectedIndex = 0;
            acharyaHimiCombo.SelectedIndex = 0;
            upadyaHimiCombo.SelectedIndex = 0;
            nikayaHimiCombo.SelectedIndex = 0;

            upasampadaMahanayakaHimiCombo.SelectedIndex = 0;
            upasampadaAcharyaHimiCombo.SelectedIndex = 0;
            upasampadaNikayaCombo.SelectedIndex = 0;
            upasampadaCheckbox.CheckState = CheckState.Unchecked;
            currentStatusComment = string.Empty;

            histryID = 0;
            histryDatagrid.Rows.Clear();
            histryRow = null;

            findIDList.Clear();
            findListSelectedIndex = -1;
            nextBtn.Visible = false;
            prevBtn.Visible = false;
            nextPrevbInfo = null;
            sitiRadio.Checked = true;
            addNoteBtn.Visible = false;
        }

        private void SetNextNumber()
        {
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bInfo.GetNextNumber();

                orderNumberText.Text = bInfo.OrderNumber.ToString();
                numberNumericUpDown.Value = nextNumber = bInfo.Number;
            }
        }

        private void changeCheckboxState(CheckState state)
        {
            dharmaDeshanaCheckbox.CheckState = vandanaCheckbox.CheckState = sajjayanaCheckbox.CheckState
            = englishCheckbox.CheckState = tamilCheckbox.CheckState = hindiCheckbox.CheckState
            = state;

            sinhalaCheckbox.CheckState = CheckState.Checked;
        }

        private bool ValidateBeforeAdd(BikkuInfo binfo)
        {
            try
            {
                bool retVal = true;

                int nicIndexValidator = binfo.ValidateIndexNICNumbers(nicTextBoxX.Text, samaneraNumberTextBoxX.Text, bhikkuID);


                if (!string.IsNullOrEmpty(samaneraNumberTextBoxX.Text))
                {
                    if ((nicIndexValidator == 4 || nicIndexValidator == 3))
                    {
                        errorProvider1.SetError(samaneraNumberTextBoxX, "Duplicate Samanera number");
                        retVal = false;
                        //changeTabCaptionColor(0, true);
                    }
                    else
                    {
                        errorProvider1.SetError(samaneraNumberTextBoxX, string.Empty);
                        //changeTabCaptionColor(0, false);
                    }
                }


                // name
                if (string.IsNullOrEmpty(nameOfAssumedAtRobinCombo.Text))
                {
                    errorProvider1.SetError(nameOfAssumedAtRobinCombo, "Please Enter Name");
                    retVal = false;
                    //changeTabCaptionColor(0, true);
                }
                else
                {
                    errorProvider1.SetError(nameOfAssumedAtRobinCombo, string.Empty);
                    //changeTabCaptionColor(0, false);
                }


                if (!string.IsNullOrEmpty(nicTextBoxX.Text) && nicTextBoxX.Text.Length != 10)
                {
                    errorProvider1.SetError(nicTextBoxX, "Invalied 'NIC'");
                    //changeTabCaptionColor(2, true);
                    retVal = false;
                }
                else
                {
                    double nicInt = 0;

                    if (!string.IsNullOrEmpty(nicTextBoxX.Text) && nicTextBoxX.Text.Length == 10)
                    {
                        if (double.TryParse(nicTextBoxX.Text.Substring(0, 9), out nicInt) && !double.TryParse(nicTextBoxX.Text.Substring(9, 1), out nicInt))
                        {

                            if (!string.IsNullOrEmpty(nicTextBoxX.Text) && (nicIndexValidator == 4 || nicIndexValidator == 2))
                            {
                                errorProvider1.SetError(nicTextBoxX, "Duplicate 'NIC'");
                                retVal = false;
                                //changeTabCaptionColor(2, true);
                            }
                            else
                            {

                                errorProvider1.SetError(nicTextBoxX, string.Empty);
                                //changeTabCaptionColor(2, false);
                            }
                        }
                        else
                        {

                            errorProvider1.SetError(nicTextBoxX, "Invalied 'NIC'");
                            retVal = false;
                            //changeTabCaptionColor(2, true);
                        }
                    }
                    else
                    {

                        errorProvider1.SetError(nicTextBoxX, string.Empty);
                        //changeTabCaptionColor(2, false);
                    }
                }

                return retVal;

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
                return false;
            }
        }


        //private void changeTabCaptionColor(int tabIndex, bool isError)
        //{
        //    if (isError)
        //    {
        //        tabControl1.Tabs[tabIndex].TextColor = Color.Red;
        //    }
        //    else
        //    {
        //        tabControl1.Tabs[tabIndex].TextColor = Color.Black;
        //    }
        //}


        private void findButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.NIC = nicTextBoxX.Text;
                    bInfo.NameAssumedAtRobing = nameOfAssumedAtRobinCombo.Text;

                    DataTable ds = bInfo.SelectFind();
                    frmSearch frmSub = new frmSearch(ds, this.Text, 4, 70, 250);
                    frmSub.Width = 800;

                    // if select a member
                    if (HandleSearch(frmSub))
                    {
                        AddFindIDsToList(ds, 4);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void AddFindIDsToList(DataTable ds, int IDCell)
        {
            try
            {
                foreach (DataRow row in ds.Rows)
                {
                    int ID = Convert.ToInt32(row[IDCell]);
                    findIDList.Add(ID);

                    if (ID == bhikkuID)
                        findListSelectedIndex = findIDList.Count - 1;
                }
            }
            catch { }
        }

        // hadle operations after search
        private bool HandleSearch(frmSearch frmSub)
        {
            //ApplicationSettings.ChildFormView(this.MdiParent, ref frmSub);
            if (frmSub.ShowDialog() == DialogResult.OK)
            {
                isSearch = true;
                FillSearchFilds(frmSub.DataRowValues);

                if (userLevel == DBCore.UserLevel.SystemAdmin || userLevel == DBCore.UserLevel.SystemUser_IUD)
                {
                    deleteBtn.Enabled = true;
                    addbtn.Text = "Update";
                }

                isSearch = false; ;
                nextBtn.Visible = true;
                prevBtn.Visible = true;

                return true;
            }
            frmSub.Dispose();
            return false;
        }

        public void FillSearchFilds(int bhikkuID, object sender, EventArgs e)
        {
            try
            {
                isSearch = true;

                CheckUpdateCurrentBhikku(sender, e);



                this.bhikkuID = bhikkuID;
                using (nextPrevbInfo = new BikkuInfo(true))
                {
                    nextPrevbInfo.SelectBhikku(this.bhikkuID);

                    SetBhikkuFields(nextPrevbInfo);
                }
                isSearch = false;
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void CheckUpdateCurrentBhikku(object sender, EventArgs e)
        {
            BikkuInfo binfo = new BikkuInfo();
            SetBInfoFromFields(binfo);

            if (!binfo.IsIdenticle(nextPrevbInfo))
            {
                addbtn_Click(nextBtn.Text, e);
            }


        }


        public void FillSearchFilds(Hashtable hashtable)
        {
            try
            {
                bhikkuID = (int)hashtable["ID"];
                changeCheckboxState(CheckState.Unchecked);

                using (nextPrevbInfo = new BikkuInfo(true))
                {
                    nextPrevbInfo.SelectBhikku(bhikkuID);

                    SetBhikkuFields(nextPrevbInfo);

                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void SetBhikkuFields(BikkuInfo bInfo)
        {

            nicTextBoxX.Text = bInfo.NIC;
            samaneraNumberTextBoxX.Text = bInfo.SamaneraNumber;
            passportNumbrtTextBoxX.Text = bInfo.PassportNumber;
            placeOfBirthtxt.Text = bInfo.PlaceOfBirth;
            layNameinFullTxt.Text = bInfo.LayNameInFull;
            dobDtm.Value = bInfo.DateOfBirth;
            nameOfFatheinFullTxt.Text = bInfo.NameOfFatherInFull;
            dateOfRobingDtm.Value = bInfo.DateOfRobing;
            nameOfAssumedAtRobinCombo.Text = bInfo.NameAssumedAtRobing;
            upasampadaCheckbox.Checked = bInfo.IsUpasampanna;
            upasampannaDatetp.Value = bInfo.DateOfHigherOrdination;
            districtcomboBox.SelectedValue = bInfo.District;
            dateofCameDtm.Value = bInfo.DateOfCame;
            upasampannaPlaceComboBoxEx.SelectedValue = bInfo.PlaceOfHigherOrdination;
            templaRobingTookCombo.SelectedValue = bInfo.TempleRobing;
            templaResidenceCombo.SelectedValue = TempleOfResidenceBeforeChange = bInfo.TempleOfResidence;
            TempleOfResidenceBeforeChangeName = templaResidenceCombo.Text;
            nameofUpadyaComboBoxEx.SelectedValue = bInfo.NameOfUpadyaAtHigherOrdination;

            homeaddressText.Text = bInfo.HomeAddress;
            hometpText.Text = bInfo.HomeTP;
            bloodGroupCombo.Text = bInfo.BloodGroup;

            if (string.IsNullOrEmpty(bInfo.ImageData))
            {
                pictureBox.Image = null;
                bikkuImage = null;
            }
            else
            {
                bikkuImage = DBCore.Utility.GetByteFrom64String(bInfo.ImageData);

                Utility.setBhikkuPictureFromByteArray(bikkuImage, pictureBox);
            }



            setActivities(bInfo.Activities);
            setAsapuHistry(bInfo.AsapuHistry);
            setOtherData(bInfo.OtherData);



            dharmaDeshanaCheckbox.Checked = bInfo.Dharmadeshanaa;
            vandanaCheckbox.Checked = bInfo.Vandanaa;
            sajjayanaCheckbox.Checked = bInfo.Sajjayana;
            sinhalaCheckbox.Checked = bInfo.Sinhala;
            tamilCheckbox.Checked = bInfo.Tamil;
            hindiCheckbox.Checked = bInfo.Hindhi;
            englishCheckbox.Checked = bInfo.English;

            // add other languages
            otherLanguages.Clear();
            otherLangPanel.Controls.Clear();

            foreach (UtilityData data in bInfo.OtherLanguages)
            {
                addOtherLangLabel(data.Value, data.ID);
            }



            upasampadaNumberTextbox.Text = bInfo.UpasampadaNumber;
            upasampadaRegDate.Value = bInfo.UpasampadaRegDate;

            karmacharyaHimi1.Text = bInfo.KarmacharyaHimi1;
            karmacharyaHimi2.Text = bInfo.KarmacharyaHimi2;
            karmacharyaHimi3.Text = bInfo.KarmacharyaHimi3;

            mahanayakaHimiCombo.SelectedValue = bInfo.MahaNayakaHimidetails;
            acharyaHimiCombo.SelectedValue = bInfo.AcharyaHimiDetails;
            upadyaHimiCombo.SelectedValue = bInfo.UpadyaTheroName;
            nikayaHimiCombo.SelectedValue = bInfo.Nikaya;

            upasampadaMahanayakaHimiCombo.SelectedValue = bInfo.UpasampadaMahaNayakaHimidetails;
            upasampadaAcharyaHimiCombo.SelectedValue = bInfo.UpasampadaAcharyaHimiDetails;
            upasampadaNikayaCombo.SelectedValue = bInfo.UpasampadaNikaya;

            countryCombo.SelectedValue = bInfo.Country;
            orderNumberText.Text = bInfo.OrderNumber.ToString();
            numberNumericUpDown.Value = bInfo.Number;
            hometp2Text.Text = bInfo.HomeTP2;

            switch (bInfo.CurrentStatus)
            {
                case DBCore.CurrenStatus.Siti:
                    {
                        sitiRadio.Checked = true;
                        break;
                    }

                case DBCore.CurrenStatus.OtherPlace:
                    {
                        otherPlaceRadio.Checked = true;
                        break;
                    }

                case DBCore.CurrenStatus.Upavidi:
                    {
                        upavidiRadio.Checked = true;
                        break;
                    }
                case DBCore.CurrenStatus.Apawath:
                    {
                        apawathRadio.Checked = true;
                        break;
                    }
                case DBCore.CurrenStatus.OtherPlaceResignStudent:
                    {
                        otherPlaceResignStdRadio.Checked = true;
                        break;
                    }
            }
            currentStatusComment = bInfo.CurrentStatusComment;
            SetCaption();
        }

        private void setActivities(List<Activity> list)
        {
            activityDatagrid.Rows.Clear();

            foreach (Activity act in list)
            {
                AddActivityRow(act.ID, act.ActivityInfo);
            }
        }

        private void setAsapuHistry(List<BhikkuAsapuHistry> list)
        {
            histryDatagrid.Rows.Clear();

            foreach (BhikkuAsapuHistry his in list)
            {
                AddAsapuhistryRow(his.ID, his.AsapuID, his.AsapuName, his.FromDate, his.ToDate, his.Post, his.Note, his.DateDiff);
            }
            histryDatagrid.Sort(new HistryDatagridSort());
        }

        private void setOtherData(List<OtherData> list)
        {
            otherDatagrid.Rows.Clear();

            foreach (OtherData oth in list)
            {
                AddOtherDataRow(oth.ID, oth.Description, oth.FileName, oth.Data);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bInfo.ID = bhikkuID;

                if (MessageView.ShowQuestionMsg("Delete Details for '" + nameOfAssumedAtRobinCombo.Text + "' thero") == DialogResult.OK)
                {
                    bInfo.Delete();
                    clear();
                    MessageView.ShowMsg("Sucessfully Deleted");
                }
            }
        }

        private void frmSamaneraBikku_Load(object sender, EventArgs e)
        {
            RefreshForm();

            setUserPermissions();
        }

        private void RefreshForm()
        {
            try
            {
                // recidence
                using (Asapuwa asapuwa = new Asapuwa(true))
                {
                    asapuwa.BindToCombo(templaResidenceCombo);
                    asapuwa.BindToCombo(asapuHistrAsapu);
                }
                templaResidenceCombo.SelectedIndex = -1;
                asapuHistrAsapu.SelectedIndex = -1;


                // upasampanna
                using (Asapuwa asapuwa = new Asapuwa(true))
                {
                    asapuwa.BindToComboHeldUpasampada(upasampannaPlaceComboBoxEx);
                }
                upasampannaPlaceComboBoxEx.SelectedIndex = -1;


                // District thero
                using (District dis = new District(true))
                {
                    dis.BindToCombo(districtcomboBox);
                }
                districtcomboBox.SelectedIndex = -1;

                UpdateUtilityAll();


                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.BindToCombo_KarmacharyaBhikkuName(new ComboBox[] { karmacharyaHimi1, karmacharyaHimi2, karmacharyaHimi3 });
                }

                karmacharyaHimi1.SelectedIndex = karmacharyaHimi2.SelectedIndex = karmacharyaHimi3.SelectedIndex = -1;

                bhikkuLoading = true;
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.BindToComboNameSeparate(nameOfAssumedAtRobinCombo);
                }
                nameOfAssumedAtRobinCombo.SelectedIndex = -1;
                bhikkuLoading = false;

                SetNextNumber();
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message, this.Location);
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Clear form") == System.Windows.Forms.DialogResult.OK)
            {
                clear();
            }
        }


        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                //  fd.Filter = "Image files|*.jpeg|*.jpg|*.png";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    setPictureFromFile(fd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void setPictureFromFile(string fileName)
        {
            try
            {
                Image img = Image.FromFile(fileName);
                img = DBCore.Utility.getThumbImage(img, pictureBox.Width, pictureBox.Height);


                bikkuImage = DBCore.Utility.GetByteFromImage(img);


                pictureBox.Image = img; //DBCore.Utility.getThumbImage(img, pictureBox.Width, pictureBox.Height);

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }




        private void activityButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(activityTextbox.Text))
            {
                if (bhikkuID != 0)
                {
                    using (BikkuInfo info = new BikkuInfo(true))
                    {
                        info.ID = bhikkuID;

                        info.AddActivity(activityTextbox.Text);
                    }
                }

                AddActivityRow(0, activityTextbox.Text);

                activityTextbox.Clear();

            }
        }

        private void activityDatagrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && activityDatagrid.CurrentRow.Index > -1)
            {
                if (MessageView.ShowQuestionMsg("Delete Current Activity ?") == System.Windows.Forms.DialogResult.OK)
                {
                    int id = (int)activityDatagrid.Rows[activityDatagrid.CurrentRow.Index].Cells[1].Value;
                    using (BikkuInfo bInfo = new BikkuInfo(true))
                    {
                        bInfo.RemoveActivity(id);
                    }

                    activityDatagrid.Rows.RemoveAt(activityDatagrid.CurrentRow.Index);

                    Utility.SetDatagridViewRow(activityDatagrid);
                }
            }
        }

        private void AddActivityRow(int id, string activity)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();

            cell1.Value = activity;
            cell2.Value = id;

            row.HeaderCell.Value = (activityDatagrid.Rows.Count + 1).ToString();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);

            activityDatagrid.Rows.Add(row);
        }

        private void addHistryButton_Click(object sender, EventArgs e)
        {

            if (validateAsapuHistry())
            {
                BhikkuAsapuHistry histry = new BhikkuAsapuHistry();
                histry.AsapuID = (int)asapuHistrAsapu.SelectedValue;
                histry.FromDate = asapuHistryFrom.Value.Date;
                histry.ToDate = asapuHistryTo.Value.Date;
                histry.Post = postCheckbox.Checked ? (DBCore.BhikkuPost)postCombo.SelectedIndex + 1 : DBCore.BhikkuPost.NAN;
                histry.Note = noteTextbox.Text;

                if (isValiedFroToDate(histry))
                {

                    if (bhikkuID != 0)
                    {
                        using (BikkuInfo info = new BikkuInfo(true))
                        {
                            info.ID = bhikkuID;

                            if (histryID == 0)
                            {
                                info.AddAsapuHistry(histry.AsapuID, histry.FromDate, histry.ToDate, histry.Post, histry.Note, 0);
                            }
                            else
                            {
                                if (MessageView.ShowQuestionMsg("Update Histry") == System.Windows.Forms.DialogResult.OK)
                                {
                                    info.UpdateAsapuHistry(histryID, histry.AsapuID, histry.FromDate, histry.ToDate, histry.Post, histry.Note);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }

                    AddAsapuhistryRow(0, histry.AsapuID, asapuHistrAsapu.Text, histry.FromDate, histry.ToDate, histry.Post, histry.Note, histry.DateDiff);

                    HistryClear();
                    histryDatagrid.Sort(new HistryDatagridSort());
                }
            }
        }


        private void HistryClear()
        {
            asapuHistrAsapu.SelectedIndex = -1;

            if (asapuHistryFrom.Value != new DateTime())
            {
                asapuHistryTo.Value = asapuHistryFrom.Value;
                asapuHistryFrom.Value = asapuHistryTo.Value.AddMonths(-6);
            }
            noteTextbox.Clear();
            postCombo.SelectedIndex = -1;
            postCheckbox.CheckState = CheckState.Unchecked;
            addHistryButton.Text = "Add";
            histryID = 0;
            histryRow = null;
            numberOfMonths.Value = 6;
            numberOfMonths.Focus();
        }

        private bool validateAsapuHistry()
        {
            try
            {
                bool result = true;

                // asapuwa
                if (asapuHistrAsapu.SelectedValue == null)
                {
                    errorProvider1.SetError(asapuHistrAsapu, "Please Select Asapuwa");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(asapuHistrAsapu, string.Empty);
                }


                if (asapuHistryFrom.Value == new DateTime())
                {
                    errorProvider1.SetError(asapuHistryFrom, "Please Select 'From Date'");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(asapuHistryFrom, string.Empty);
                }

                if (asapuHistryTo.Value == new DateTime())
                {
                    errorProvider1.SetError(asapuHistryTo, "Please Select 'To Date'");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(asapuHistryTo, string.Empty);

                    if (asapuHistryTo.Value < asapuHistryFrom.Value)
                    {
                        errorProvider1.SetError(asapuHistryTo, "'To Date' Must greater Than 'From Date'");
                        result = false;
                    }
                    else
                    {
                        errorProvider1.SetError(asapuHistryTo, string.Empty);
                    }
                }



                return result;

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
                return false;
            }
        }

        private bool isValiedFroToDate(BhikkuAsapuHistry histry)
        {
            foreach (DataGridViewRow row in histryDatagrid.Rows)
            {
                row.DefaultCellStyle.BackColor = SystemColors.Window;

                if (histryRow == row)
                {
                    continue;
                }

                DateTime fromDate = DateTime.Parse(row.Cells[3].Value.ToString());
                DateTime toDate = DateTime.Parse(row.Cells[4].Value.ToString());

                if (histry.FromDate > fromDate && histry.FromDate < toDate)
                {
                    errorProvider1.SetError(asapuHistryFrom, "මෙම දිනයට වෙනත් දත්තයක් ඇතුලත් කර ඇත");
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                    return false;
                }

                if (histry.ToDate > fromDate && histry.ToDate < toDate)
                {
                    errorProvider1.SetError(asapuHistryTo, "මෙම දිනයට වෙනත් දත්තයක් ඇතුලත් කර ඇත");
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                    return false;
                }


                if (fromDate > histry.FromDate && fromDate < histry.ToDate)
                {
                    errorProvider1.SetError(asapuHistryFrom, "මෙම දිනයට වෙනත් දත්තයක් ඇතුලත් කර ඇත");
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                    return false;

                }
            }

            return true;
        }


        private void AddAsapuhistryRow(int id, int asapuId, string asapuName, DateTime fromDate, DateTime toDate, DBCore.BhikkuPost post, string note, string totalDays)
        {
            string cel6Value = "";

            cel6Value = Utility.GetPostString(post);

            if (histryRow != null)
            {
                histryRow.Cells[1].Value = asapuId;
                histryRow.Cells[2].Value = asapuName;
                histryRow.Cells[3].Value = fromDate.ToString("yyy-MMM-dd");
                histryRow.Cells[4].Value = toDate.ToString("yyy-MMM-dd");
                histryRow.Cells[5].Value = cel6Value;
                histryRow.Cells[6].Value = note;
                histryRow.Cells[7].Value = totalDays;
                return;
            }

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();

            cell1.Value = id;
            cell2.Value = asapuId;
            cell3.Value = asapuName;
            cell4.Value = fromDate.ToString("yyy-MMM-dd");
            cell5.Value = toDate.ToString("yyy-MMM-dd");
            cell6.Value = cel6Value;
            cell7.Value = note;
            cell8.Value = totalDays;

            row.HeaderCell.Value = (histryDatagrid.Rows.Count + 1).ToString();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            row.Cells.Add(cell7);
            row.Cells.Add(cell8);

            histryDatagrid.Rows.Add(row);
        }


        private void histryDatagrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && histryDatagrid.CurrentRow.Index > -1)
            {
                if (MessageView.ShowQuestionMsg("Delete Current Histry ?") == System.Windows.Forms.DialogResult.OK)
                {
                    int id = (int)histryDatagrid.Rows[histryDatagrid.CurrentRow.Index].Cells[0].Value;
                    using (BikkuInfo bInfo = new BikkuInfo(true))
                    {
                        bInfo.RemoveAsapuHistry(id);
                    }

                    histryDatagrid.Rows.RemoveAt(histryDatagrid.CurrentRow.Index);

                    Utility.SetDatagridViewRow(histryDatagrid);
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.InitialDirectory = Directory.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageName.Text = openFileDialog1.FileName;
            }
            else
            {
                imageName.Clear();

            }
        }

        private void addInfoBtn_Click(object sender, EventArgs e)
        {
            if (validateOtherData())
            {
                string fileName = Path.GetFileName(imageName.Text);
                if (bhikkuID != 0)
                {
                    using (BikkuInfo info = new BikkuInfo(true))
                    {
                        info.ID = bhikkuID;


                        info.AddOtherData(descriptionTxt.Text, fileName, DBCore.Utility.GetFileByteCompress(fileName));
                    }
                }

                AddOtherDataRow(0, descriptionTxt.Text, fileName, DBCore.Utility.GetFileByteCompress(imageName.Text));

                imageName.Clear();
                descriptionTxt.Clear();
            }
        }

        private void AddOtherDataRow(int id, string description, string fileName, byte[] Data)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
            DataGridViewImageCell cell4 = new DataGridViewImageCell();
            DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();

            cell1.Value = id;
            cell2.Value = description;
            cell3.Value = fileName;
            //cell4.Value = fromDate.ToString("yyy-MMM-dd");
            cell5.Value = Data;


            row.HeaderCell.Value = (otherDatagrid.Rows.Count + 1).ToString();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);

            otherDatagrid.Rows.Add(row);
        }

        private bool validateOtherData()
        {
            return true;
        }

        private void otherDatagrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && otherDatagrid.CurrentRow.Index > -1)
            {
                if (MessageView.ShowQuestionMsg("Delete Current Data ?") == System.Windows.Forms.DialogResult.OK)
                {
                    int id = (int)otherDatagrid.Rows[otherDatagrid.CurrentRow.Index].Cells[0].Value;
                    using (BikkuInfo bInfo = new BikkuInfo(true))
                    {
                        bInfo.RemoveOtherData(id);
                    }

                    otherDatagrid.Rows.RemoveAt(otherDatagrid.CurrentRow.Index);

                    Utility.SetDatagridViewRow(otherDatagrid);
                }
            }
        }

        private void otherDatagrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (otherDatagrid.CurrentRow.Index > -1)
            {

                byte[] fileData = null;
                string fileName = otherDatagrid.Rows[otherDatagrid.CurrentRow.Index].Cells[2].Value.ToString();

                if (MessageView.ShowQuestionMsg("Download File '" + fileName + "' ?") == System.Windows.Forms.DialogResult.OK)
                {

                    saveFileDialog1.FileName = fileName;
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        int id = (int)otherDatagrid.Rows[otherDatagrid.CurrentRow.Index].Cells[0].Value;
                        using (BikkuInfo bInfo = new BikkuInfo(true))
                        {
                            fileData = bInfo.GetOtherDatafile(id);
                        }


                        if (fileData != null)
                        {
                            File.WriteAllBytes(saveFileDialog1.FileName, DBCore.Utility.DecompressGZip(fileData));
                        }
                    }
                }
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.MahanayakaHimi, label2.Text);
            UpdateUtility((int)DBCore.UtilityDataName.MahanayakaHimi, mahanayakaHimiCombo);

            upasampadaMahanayakaHimiCombo.DataSource = mahanayakaHimiCombo.DataSource;
            setUtilityDataComboProperty(upasampadaMahanayakaHimiCombo);
        }


        private void UpdateUtilityAll()
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(mahanayakaHimiCombo, (int)DBCore.UtilityDataName.MahanayakaHimi);
                mahanayakaHimiCombo.SelectedIndex = 0;
                upasampadaMahanayakaHimiCombo.DataSource = mahanayakaHimiCombo.DataSource;
                setUtilityDataComboProperty(upasampadaMahanayakaHimiCombo);

                ut.BindToCombo(acharyaHimiCombo, (int)DBCore.UtilityDataName.acharyaHimi);
                acharyaHimiCombo.SelectedIndex = 0;
                upasampadaAcharyaHimiCombo.DataSource = acharyaHimiCombo.DataSource;
                setUtilityDataComboProperty(upasampadaAcharyaHimiCombo);

                ut.BindToCombo(nikayaHimiCombo, (int)DBCore.UtilityDataName.Nikaya);
                nikayaHimiCombo.SelectedIndex = 0;
                upasampadaNikayaCombo.DataSource = nikayaHimiCombo.DataSource;
                setUtilityDataComboProperty(upasampadaNikayaCombo);

                //ut.BindToCombo(nikayaHimiCombo, (int)DBCore.UtilityDataName.Nikaya);
                // mahanayakaHimiCombo.SelectedIndex = 0;
                //upasampadaNikayaCombo.DataSource = nikayaHimiCombo.DataSource;
                //setUtilityDataComboProperty(upasampadaNikayaCombo);

                ut.BindToCombo(upadyaHimiCombo, (int)DBCore.UtilityDataName.UpadyaHimi);
                upadyaHimiCombo.SelectedIndex = 0;

                ut.BindToCombo(otherLangCombo, (int)DBCore.UtilityDataName.OtherLang);
                otherLangCombo.SelectedIndex = -1;

                ut.BindToCombo(upasampannaPlaceComboBoxEx, (int)DBCore.UtilityDataName.PlaceUpasampada);
                upasampannaPlaceComboBoxEx.SelectedIndex = -1;

                ut.BindToCombo(templaRobingTookCombo, (int)DBCore.UtilityDataName.placeRobing);
                templaRobingTookCombo.SelectedIndex = -1;

                ut.BindToCombo(countryCombo, (int)DBCore.UtilityDataName.Country);
                countryCombo.SelectedIndex = 0;
            }
        }


        private void setUtilityDataComboProperty(ComboBox combo)
        {
            combo.DisplayMember = "value";
            combo.ValueMember = "ID";
            combo.SelectedIndex = 0;
        }

        private void UpdateUtility(int nameID, ComboBox combo)
        {
            using (UtilityData ut = new UtilityData(true))
            {
                ut.BindToCombo(combo, nameID);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.acharyaHimi, label21.Text);
            UpdateUtility((int)DBCore.UtilityDataName.acharyaHimi, acharyaHimiCombo);

            upasampadaAcharyaHimiCombo.DataSource = acharyaHimiCombo.DataSource;
            setUtilityDataComboProperty(upasampadaAcharyaHimiCombo);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.Nikaya, label3.Text);
            UpdateUtility((int)DBCore.UtilityDataName.Nikaya, nikayaHimiCombo);

            upasampadaNikayaCombo.DataSource = nikayaHimiCombo.DataSource;
            setUtilityDataComboProperty(upasampadaNikayaCombo);
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            buttonX5_Click(sender, e);
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            buttonX4_Click(sender, e);
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            buttonX3_Click(sender, e);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.UpadyaHimi, label39.Text);
            UpdateUtility((int)DBCore.UtilityDataName.UpadyaHimi, upadyaHimiCombo);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (otherLangCombo.SelectedItem != null && getOtherlangLabelIndex(otherLangCombo.Text) == -1)
            {
                addOtherLangLabel(otherLangCombo.Text, (int)otherLangCombo.SelectedValue);
            }
        }

        private void addOtherLangLabel(string labelText, int selectedValue)
        {
            Label lbl = new Label();
            lbl.Text = labelText;
            lbl.Font = label32.Font;
            lbl.DoubleClick += new System.EventHandler(this.OthelLangLabel_Delete);
            lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
            lbl.AutoSize = true;

            otherLanguages.Add(new OtherLang(selectedValue, labelText, lbl));
            setLanguageLabels();
        }

        void lbl_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Transparent;
        }

        void lbl_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.LightGray;
        }

        private void addLangButton_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.OtherLang, "වෙනත් භාෂා");
            UpdateUtility((int)DBCore.UtilityDataName.OtherLang, otherLangCombo);
        }

        private void setLanguageLabels()
        {

            int locationY = -20;

            foreach (OtherLang lang in otherLanguages)
            {
                lang.label.Location = new Point(5, locationY += 20);
                otherLangPanel.Controls.Add(lang.label);
            }
        }

        private void OthelLangLabel_Delete(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;

            if (MessageView.ShowQuestionMsg("Delete '" + lbl.Text + "'?") == System.Windows.Forms.DialogResult.OK)
            {
                otherLangPanel.Controls.RemoveAt(lbl.Location.Y / 20);

                int labelIndex = getOtherlangLabelIndex(lbl.Text);

                if (labelIndex > -1)
                {
                    otherLanguages.RemoveAt(labelIndex);
                }

                setLanguageLabels();
            }
        }

        private int getOtherlangLabelIndex(string langName)
        {
            for (int i = 0; i < otherLanguages.Count; i++)
            {
                OtherLang lang = otherLanguages[i];
                if (lang.label.Text == langName)
                {
                    return i;
                }
            }

            return -1;
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.PlaceUpasampada, label28.Text);
            UpdateUtility((int)DBCore.UtilityDataName.PlaceUpasampada, upasampannaPlaceComboBoxEx);
        }


        private void upasampadaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = groupBox5.Enabled = groupBox7.Enabled = upasampadaNumberTextbox.Enabled = upasampadaRegDate.Enabled = upasampannaPlaceComboBoxEx.Enabled = buttonX10.Enabled = upasampadaCheckbox.CheckState == CheckState.Checked;
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.placeRobing, label19.Text);
            UpdateUtility((int)DBCore.UtilityDataName.placeRobing, templaRobingTookCombo);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Delete Image") == DialogResult.OK)
            {
                pictureBox.Image = null;
                bikkuImage = null;
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            frmUtilityData ut = new frmUtilityData((int)userLevel);
            ut.ShowForm((int)DBCore.UtilityDataName.Country, label43.Text);
            UpdateUtility((int)DBCore.UtilityDataName.Country, countryCombo);
        }

        private void dateOfRobingDtm_ValueChanged(object sender, EventArgs e)
        {

            dateOfRobingBYDtm.Value = Utility.GetBYDate(dateOfRobingDtm.Value);
        }

        private void dateOfRobingBYDtm_ValueChanged(object sender, EventArgs e)
        {
            dateOfRobingDtm.Value = Utility.GetCYDate(dateOfRobingBYDtm.Value);
        }

        private void upasampannaDatetp_ValueChanged(object sender, EventArgs e)
        {
            upasampannaBYDatetp.Value = Utility.GetBYDate(upasampannaDatetp.Value);
        }

        private void upasampannaBYDatetp_ValueChanged(object sender, EventArgs e)
        {
            upasampannaDatetp.Value = Utility.GetCYDate(upasampannaBYDatetp.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        //private void frmBikkuInfo_KeyUp(object sender, KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Enter && !numberNumericUpDown.Focused )
        //    {
        //        addbtn_Click(sender, e);
        //    }
        //}

        private void upavidiRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!upavidiRadio.Checked || isSearch)
            {
                return;
            }

            frmComment comment = new frmComment(currentStatusComment);
            if (comment.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentStatusComment = comment.Comment;
            }
            isSearch = false;
        }

        private void otherPlaceRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if ((otherPlaceRadio.Checked || otherPlaceResignStdRadio.Checked) && !isSearch)
                {
                    frmComment comment = new frmComment(currentStatusComment);
                    if (comment.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        currentStatusComment = comment.Comment;
                    }
                }
            }

            isSearch = false;
        }

        private void label46_DoubleClick(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (otherPlaceResignStdRadio.Checked || otherPlaceRadio.Checked || upavidiRadio.Checked)
                {
                    addNoteBtn.Visible = true;
                }
            }else
            {
                addNoteBtn.Visible = false;
            }
        }

        private void postCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            postCombo.Enabled = postCheckbox.Checked;
        }



        private void histryDatagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = histryDatagrid.Rows[e.RowIndex];

            histryID = (int)row.Cells[0].Value;
            histryRow = row;

            DateTime fromDate = DateTime.Parse(row.Cells[3].Value.ToString());
            DateTime toDate = DateTime.Parse(row.Cells[4].Value.ToString());
            DBCore.BhikkuPost post = (DBCore.BhikkuPost)(string.IsNullOrEmpty(row.Cells[5].Value.ToString()) ? 0 : row.Cells[5].Value.ToString() == "ස: උ:" ? 1 : 2);


            asapuHistryFrom.Value = fromDate;
            asapuHistryTo.Value = toDate;
            asapuHistrAsapu.SelectedValue = (int)row.Cells[1].Value;
            noteTextbox.Text = row.Cells[6].Value.ToString();

            if ((int)post > 0)
            {
                postCheckbox.Checked = true;
                postCombo.SelectedIndex = (int)post - 1;
            }
            else
            {
                postCheckbox.Checked = false;
                postCombo.SelectedIndex = -1;
            }

            addHistryButton.Text = "Update";
        }

        private void histryClear_Click(object sender, EventArgs e)
        {
            HistryClear();
        }

        private void numberNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    if (numberNumericUpDown.Value != nextNumber)
                    {
                        bInfo.SelectBhikkuFromNumber((int)numberNumericUpDown.Value);

                        if (bInfo.Number == numberNumericUpDown.Value)
                        {

                            this.bhikkuID = bInfo.ID;
                            deleteBtn.Enabled = true;
                            addbtn.Text = "Update";

                            SetBhikkuFields(bInfo);


                        }
                        else
                        {
                            int tempNumber = (int)numberNumericUpDown.Value;
                            clear();
                            numberNumericUpDown.Value = tempNumber;
                        }
                    }
                }
                //numberNumericUpDown.SelectionStar = 15;
            }
        }

        private void nicTextBoxX_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = lan;
        }

        private void nicTextBoxX_Enter(object sender, EventArgs e)
        {
            lan = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[0];
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (findListSelectedIndex + 1 > -1 && findIDList.Count > findListSelectedIndex + 1)
            {
                FillSearchFilds(findIDList[++findListSelectedIndex], sender, e);
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (findListSelectedIndex - 1 > -1 && findIDList.Count > findListSelectedIndex - 1)
            {
                FillSearchFilds(findIDList[--findListSelectedIndex], sender, e);
            }
        }

        private void frmBikkuInfo_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 39 && nextBtn.Enabled && tabControl2.SelectedIndex == 0)
            {
                nextBtn.Focus();
                nextBtn_Click(sender, e);
            }
            else if (e.KeyValue == 37 && prevBtn.Enabled && tabControl2.SelectedIndex == 0)
            {
                prevBtn.Focus();
                prevBtn_Click(sender, e);
            }
        }

        private void nameOfAssumedAtRobinCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!bhikkuLoading && nameOfAssumedAtRobinCombo.SelectedValue != null && nameOfAssumedAtRobinCombo.SelectedValue is Int32)
            {
                isSearch = true;
                using (BikkuInfo binfo = new BikkuInfo(true))
                {
                    bhikkuID = (int)nameOfAssumedAtRobinCombo.SelectedValue;
                    binfo.SelectBhikku(bhikkuID);
                    deleteBtn.Enabled = true;
                    addbtn.Text = "Update";

                    SetBhikkuFields(binfo);

                }
                isSearch = false;
            }
        }

        private void asapuHistryFrom_ValueChanged(object sender, EventArgs e)
        {
            if (asapuHistryTo.Value > asapuHistryFrom.Value)
            {
                setNumberOfDaysLabel(DBCore.Utility.GetDateDiff(asapuHistryFrom.Value, asapuHistryTo.Value));
            }
        }

        private void setNumberOfDaysLabel(string timeSpan)
        {

            numberofDatesLabel.Text = string.Concat("දින ගණන : ", timeSpan);
        }

        private void asapuHistryTo_ValueChanged(object sender, EventArgs e)
        {
            if (asapuHistryTo.Value > asapuHistryFrom.Value)
            {
                setNumberOfDaysLabel(DBCore.Utility.GetDateDiff(asapuHistryFrom.Value, asapuHistryTo.Value));
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numberOfMonths_Leave(object sender, EventArgs e)
        {
            if (asapuHistryTo.Value != new DateTime())
                asapuHistryFrom.Value = asapuHistryTo.Value.AddMonths(-1 * (int)numberOfMonths.Value);

            if (numberOfMonths.Value == 0)
            {
                asapuHistryFrom.TabStop = true;
            }
            else
            {
                asapuHistryFrom.TabStop = false;
            }
        }

        private void nameOfAssumedAtRobinCombo_Leave(object sender, EventArgs e)
        {
            SetCaption();
        }

        private void SetCaption()
        {
            captionName.Text = nameOfAssumedAtRobinCombo.Text;
            captionName.Location = new Point(panelEx2.Width - captionName.PreferredWidth - 100, 5);
        }

        private void setUserPermissions()
        {
            if (userLevel == DBCore.UserLevel.SystemUser)
            {
                addbtn.Enabled = deleteBtn.Enabled = false;
            }

            if (userLevel == DBCore.UserLevel.SystemUser_I)
            {
                addbtn.Enabled = true;
                deleteBtn.Enabled = false;
            }

            if (userLevel == DBCore.UserLevel.SystemUser_IUD || userLevel == DBCore.UserLevel.SystemAdmin)
            {
                addbtn.Enabled = true;
            }
        }

        private void label45_Click(object sender, EventArgs e)
        {
            sitiRadio.Checked = true;

        }

        private void label46_Click(object sender, EventArgs e)
        {
            otherPlaceRadio.Checked = true;
        }

        private void label461_Click(object sender, EventArgs e)
        {
            otherPlaceResignStdRadio.Checked = true;
        }

        private void label47_Click(object sender, EventArgs e)
        {
            upavidiRadio.Checked = true;
        }

        private void label54_Click(object sender, EventArgs e)
        {
            apawathRadio.Checked = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusLbl.Visible = false;
            timer1.Enabled = false;
        }

        private void addNoteBtn_Click(object sender, EventArgs e)
        {
            frmComment comment = new frmComment(currentStatusComment);
            if (comment.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentStatusComment = comment.Comment;
            }
        }
    }
}

