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

namespace MahamewnawaInfo.Forms
{
    public partial class frmSamaneraBikku : DevComponents.DotNetBar.Office2007Form
    {
        public int bhikkuID = 0;
        byte[] bikkuImage = null;

        int TempleOfResidenceBeforeChange = 0;
        string TempleOfResidenceBeforeChangeName = "";

        public frmSamaneraBikku()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {

                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    if (ValidateBeforeAdd(bInfo))
                    {

                        bInfo.NIC = nicTextBoxX.Text;
                        bInfo.SamaneraNumber = samaneraNumberTextBoxX.Text;
                        bInfo.PassportNumber = passportNumbrtTextBoxX.Text;
                        bInfo.PlaceOfBirth = placeOfBirthtxt.Text;
                        bInfo.LayNameInFull = layNameinFullTxt.Text;
                        bInfo.DateOfBirth = dobDtm.Value.Date;
                        bInfo.NameOfFatherInFull = nameOfFatheinFullTxt.Text;
                        bInfo.DateOfRobing = dateOfRobingDtm.Value.Date;
                        bInfo.NameAssumedAtRobing = nameOfAssumedAtRobinTxt.Text;
                        bInfo.NameOfRobingTutor = nameOfRobingTutorCombo.SelectedValue == null ? 0 : (int)nameOfRobingTutorCombo.SelectedValue;
                        bInfo.TempleRobing = templaRobingTookCombo.SelectedValue == null ? 0 : (int)templaRobingTookCombo.SelectedValue;
                        bInfo.TempleOfResidence = templaResidenceCombo.SelectedValue == null ? 0 : (int)templaResidenceCombo.SelectedValue;
                        bInfo.NameOfViharadhipathi = nameOfViharadhipathiCombo.SelectedValue == null ? 0 : (int)nameOfViharadhipathiCombo.SelectedValue;

                        bInfo.IsUpasampanna = isUpasampannaCheckBox.Checked;
                        bInfo.PlaceOfHigherOrdination = upasampannaPlaceComboBoxEx.SelectedValue == null ? 0 : (int)upasampannaPlaceComboBoxEx.SelectedValue;
                        bInfo.DateOfHigherOrdination = upasampannaDatetp.Value.Date;
                        bInfo.NameOfUpadyaAtHigherOrdination = nameofUpadyaComboBoxEx.SelectedValue == null ? 0 : (int)nameofUpadyaComboBoxEx.SelectedValue;
                        bInfo.IsUpadyaThero = isUpadyaCheckBox.Checked;
                        //bInfo.Post = isSangaUpasCheckBox.Checked;
                        bInfo.District = districtcomboBox.SelectedValue == null ? 0 : (int)districtcomboBox.SelectedValue;
                        bInfo.DateOfCame = dateofCameDtm.Value; // == new DateTime()
                        bInfo.HomeAddress = homeaddressText.Text;
                        bInfo.HomeTP = hometpText.Text;

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
                            bInfo.ImageData = Utility.Get64String(bikkuImage);
                        }

                        if (bhikkuID == 0)
                        {
                            setActivitiesToObject(bInfo);
                            setAsapuHistryToObject(bInfo);
                            setOtherDataToObject(bInfo);

                            if (bInfo.Add() == 1)
                            {
                                MessageView.ShowMsg("Sucessfully Added");

                                //errorProvider1.SetError(idTxt, string.Empty);
                                //errorProvider1.SetError(nameTxt, string.Empty);

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


                                if (bInfo.Update() == 1)
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

                    bInfo.AsapuHistry.Add(new BhikkuAsapuHistry(0, (int)row.Cells[1].Value, fromDate, toDate, "", DBCore.BhikkuPost.NAN, ""));
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
                    //DateTime fromDate = DateTime.Parse(row.Cells[3].Value.ToString());
                    //DateTime toDate = DateTime.Parse(row.Cells[4].Value.ToString());

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

            nameOfFatheinFullTxt.Clear();
            nameOfAssumedAtRobinTxt.Clear();
            isSangaUpasCheckBox.Checked = false;
            isUpadyaCheckBox.Checked = false;

            upasampannaPlaceComboBoxEx.SelectedIndex = -1;
            nameOfRobingTutorCombo.SelectedIndex = -1;
            nameofUpadyaComboBoxEx.SelectedIndex = -1;
            nameOfViharadhipathiCombo.SelectedIndex = -1;
            districtcomboBox.SelectedIndex = -1;
            templaResidenceCombo.SelectedIndex = -1;
            templaRobingTookCombo.SelectedValue = -1;

            dateOfRobingDtm.Value = new DateTime();
            upasampannaDatetp.Value = new DateTime();
            dobDtm.Value = new DateTime();
            dateofCameDtm.Value = new DateTime();

            pictureBox.Image = null;
            deleteBtn.Enabled = false;

            addbtn.Text = "Insert";

            activityDatagrid.Rows.Clear();

            asapuHistryFrom.Value = new DateTime();
            asapuHistryTo.Value = new DateTime();
            asapuHistrAsapu.SelectedIndex = -1;

            otherDatagrid.Rows.Clear();
            descriptionTxt.Clear();
            imageName.Clear();
        }

        private bool ValidateBeforeAdd(BikkuInfo binfo)
        {
            try
            {
                bool retVal = true;

                int nicIndexValidator = binfo.ValidateIndexNICNumbers(nicTextBoxX.Text, samaneraNumberTextBoxX.Text, bhikkuID);


                if (string.IsNullOrEmpty(samaneraNumberTextBoxX.Text))
                {
                    errorProvider1.SetError(samaneraNumberTextBoxX, "Samanera Number cannot be empty");
                    retVal = false;
                    changeTabCaptionColor(0, true);
                }
                else
                {
                    if ((nicIndexValidator == 4 || nicIndexValidator == 3))
                    {
                        errorProvider1.SetError(samaneraNumberTextBoxX, "Duplicate Samanera number");
                        retVal = false;
                        changeTabCaptionColor(0, true);
                    }
                    else
                    {
                        errorProvider1.SetError(samaneraNumberTextBoxX, string.Empty);
                        changeTabCaptionColor(0, false);
                    }
                }


                // name
                if (string.IsNullOrEmpty(nameOfAssumedAtRobinTxt.Text))
                {
                    errorProvider1.SetError(nameOfAssumedAtRobinTxt, "Please Enter Name");
                    retVal = false;
                    changeTabCaptionColor(0, true);
                }
                else
                {
                    errorProvider1.SetError(nameOfAssumedAtRobinTxt, string.Empty);
                    changeTabCaptionColor(0, false);
                }


                if (!string.IsNullOrEmpty(nicTextBoxX.Text) && nicTextBoxX.Text.Length != 10)
                {
                    errorProvider1.SetError(nicTextBoxX, "Invalied 'NIC'");
                    changeTabCaptionColor(2, true);
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
                                changeTabCaptionColor(2, true);
                            }
                            else
                            {

                                errorProvider1.SetError(nicTextBoxX, string.Empty);
                                changeTabCaptionColor(2, false);
                            }
                        }
                        else
                        {

                            errorProvider1.SetError(nicTextBoxX, "Invalied 'NIC'");
                            retVal = false;
                            changeTabCaptionColor(2, true);
                        }
                    }
                    else
                    {

                        errorProvider1.SetError(nicTextBoxX, string.Empty);
                        changeTabCaptionColor(2, false);
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


        private void changeTabCaptionColor(int tabIndex, bool isError)
        {
            if (isError)
            {
                tabControl1.Tabs[tabIndex].TextColor = Color.Red;
            }
            else
            {
                tabControl1.Tabs[tabIndex].TextColor = Color.Black;
            }
        }


        private void findButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.NIC = nicTextBoxX.Text;
                    bInfo.NameAssumedAtRobing = nameOfAssumedAtRobinTxt.Text;

                    DataTable ds = bInfo.SelectFind();
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
                bhikkuID = (int)hashtable["ID"];

                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.SelectBhikku(bhikkuID);


                    nicTextBoxX.Text = bInfo.NIC;
                    samaneraNumberTextBoxX.Text = bInfo.SamaneraNumber;
                    passportNumbrtTextBoxX.Text = bInfo.PassportNumber;
                    placeOfBirthtxt.Text = bInfo.PlaceOfBirth;
                    layNameinFullTxt.Text = bInfo.LayNameInFull;
                    dobDtm.Value = bInfo.DateOfBirth;
                    nameOfFatheinFullTxt.Text = bInfo.NameOfFatherInFull;
                    dateOfRobingDtm.Value = bInfo.DateOfRobing;
                    nameOfAssumedAtRobinTxt.Text = bInfo.NameAssumedAtRobing;
                    isUpasampannaCheckBox.Checked = bInfo.IsUpasampanna;
                    upasampannaDatetp.Value = bInfo.DateOfHigherOrdination;
                    //isSangaUpasCheckBox.Checked = bInfo.Post;
                    isUpadyaCheckBox.Checked = bInfo.IsUpadyaThero;
                    districtcomboBox.SelectedValue = bInfo.District;
                    dateofCameDtm.Value = bInfo.DateOfCame;
                    upasampannaPlaceComboBoxEx.SelectedValue = bInfo.PlaceOfHigherOrdination;
                    nameOfRobingTutorCombo.SelectedValue = bInfo.NameOfRobingTutor;
                    templaRobingTookCombo.SelectedValue = bInfo.TempleRobing;
                    templaResidenceCombo.SelectedValue = TempleOfResidenceBeforeChange = bInfo.TempleOfResidence;
                    TempleOfResidenceBeforeChangeName = templaResidenceCombo.Text;
                    nameOfViharadhipathiCombo.SelectedValue = bInfo.NameOfViharadhipathi;
                    nameofUpadyaComboBoxEx.SelectedValue = bInfo.NameOfUpadyaAtHigherOrdination;

                    if (!string.IsNullOrEmpty(bInfo.ImageData))
                        setDriverPictureFromByteArray(Utility.GetByte64String(bInfo.ImageData));

                    setActivities(bInfo.Activities);
                    setAsapuHistry(bInfo.AsapuHistry);
                    setOtherData(bInfo.OtherData);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
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
                AddAsapuhistryRow(his.ID, his.AsapuID, his.AsapuName, his.FromDate, his.ToDate);
            }
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

                if (MessageView.ShowQuestionMsg("Delete Details for '" + nameOfAssumedAtRobinTxt.Text + "' thero") == DialogResult.OK)
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

                // robing
                using (Asapuwa asapuwa = new Asapuwa(true))
                {
                    asapuwa.BindToComboHeldUpasampada(templaRobingTookCombo);
                }
                templaRobingTookCombo.SelectedIndex = -1;


                // upasampanna
                using (Asapuwa asapuwa = new Asapuwa(true))
                {
                    asapuwa.BindToComboHeldUpasampada(upasampannaPlaceComboBoxEx);
                }
                upasampannaPlaceComboBoxEx.SelectedIndex = -1;

                // Upadya thero
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.BindToComboUpadyaThero(nameofUpadyaComboBoxEx);
                }
                nameofUpadyaComboBoxEx.SelectedIndex = -1;

                // Tutor thero
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.BindToComboUpadyaThero(nameOfRobingTutorCombo);
                }
                nameOfRobingTutorCombo.SelectedIndex = -1;


                // viharadhipathi thero
                using (BikkuInfo bInfo = new BikkuInfo(true))
                {
                    bInfo.BindToComboUpadyaThero(nameOfViharadhipathiCombo);
                }
                nameOfViharadhipathiCombo.SelectedIndex = -1;

                // District thero
                using (District dis = new District(true))
                {
                    dis.BindToCombo(districtcomboBox);
                }
                districtcomboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message, this.Location);
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void EnableUpasampannaProperties(bool p)
        {
            upasampannaPlaceComboBoxEx.Enabled = p;
            nameofUpadyaComboBoxEx.Enabled = p;
            upasampannaDatetp.Enabled = p;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnableUpasampannaProperties(isUpasampannaCheckBox.Checked);
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
                else
                {
                    pictureBox.Image = null;
                    bikkuImage = null;

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

                // set byte array
                MemoryStream mem = new MemoryStream();
                //  picDriverImage.Image.Save(mem, ImageFormat.Png);

                img.Save(mem, ImageFormat.Jpeg);
                bikkuImage = mem.ToArray();

                //if (img.Width >5000 || img.Height > 5000)
                //{
                //    MessageView.ShowErrorMsg("Image is too large,Maximum scale is 5000 X 5000");
                //    bikkuImage = null;
                //    return;
                //}


                pictureBox.Image = getThumbImage(img);

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private Image getThumbImage(Image image)
        {
            Image.GetThumbnailImageAbort del = new Image.GetThumbnailImageAbort(ThumbCallback);
            int thumbWidth = pictureBox.Width;
            int thumbHeight = pictureBox.Height;

            // set thumb images with and height, by considering actual image with and height ratio
            if (image.Width > image.Height)
            {
                thumbWidth = pictureBox.Width;
                thumbHeight = (int)Math.Round((image.Height / (float)image.Width) * pictureBox.Width);
            }
            else
            {
                thumbHeight = pictureBox.Height;
                thumbWidth = (int)Math.Round((image.Width / (float)image.Height) * pictureBox.Height);
            }

            return image.GetThumbnailImage(thumbWidth, thumbHeight, del, IntPtr.Zero);
        }

        // use in SetImageData, for delegate
        private static bool ThumbCallback()
        {
            return false;
        }

        private void setDriverPictureFromByteArray(byte[] picData)
        {
            try
            {
                if (picData == null)
                {
                    pictureBox.Image = null;
                    bikkuImage = null;
                }
                else
                {
                    // set byte array
                    MemoryStream mem = new MemoryStream(picData);
                    bikkuImage = picData;

                    Image img = Image.FromStream(mem);
                    pictureBox.Image = getThumbImage(img);
                }
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
                if (bhikkuID != 0)
                {
                    using (BikkuInfo info = new BikkuInfo(true))
                    {
                        info.ID = bhikkuID;
                        info.AddAsapuHistry((int)asapuHistrAsapu.SelectedValue, asapuHistryFrom.Value.Date, asapuHistryTo.Value.Date, DBCore.BhikkuPost.NAN, "");
                    }
                }

                AddAsapuhistryRow(0, (int)asapuHistrAsapu.SelectedValue, asapuHistrAsapu.Text, asapuHistryFrom.Value.Date, asapuHistryTo.Value.Date);

                asapuHistrAsapu.SelectedIndex = -1;
                asapuHistryFrom.Value = DateTime.Now;
                asapuHistryTo.Value = DateTime.Now;
            }
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


        private void AddAsapuhistryRow(int id, int asapuId, string asapuName, DateTime fromDate, DateTime toDate)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();

            cell1.Value = id;
            cell2.Value = asapuId;
            cell3.Value = asapuName;
            cell4.Value = fromDate.ToString("yyy-MMM-dd");
            cell5.Value = toDate.ToString("yyy-MMM-dd");


            row.HeaderCell.Value = (activityDatagrid.Rows.Count + 1).ToString();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);

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


                        info.AddOtherData(descriptionTxt.Text, fileName, Utility.GetFileByteCompress(fileName));
                    }
                }

                AddOtherDataRow(0, descriptionTxt.Text, fileName, Utility.GetFileByteCompress(fileName));

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
                            File.WriteAllBytes(saveFileDialog1.FileName, Utility.DecompressGZip(fileData));
                        }
                    }
                }
            }
        }

        //private void templaResidenceCombo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (bhikkuID != 0 && MessageView.ShowQuestionMsg("Add histry record? ") == System.Windows.Forms.DialogResult.OK)
        //    {
                
        //        MessageView.ShowMsg(sender.GetType().Name+" "+e.GetType().ToString());
        //    }
        //}
    }
}

