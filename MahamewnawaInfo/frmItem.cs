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
using MahamewnawaInfo.Common;

namespace ShopMannager.Forms
{
    public partial class frmItem : DevComponents.DotNetBar.Office2007Form
    {
        public int itemID = 0;
        public frmItem()
        {
            InitializeComponent();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                using (Category cat = new Category(true))
                {
                    cat.BindToCombo(categoryCombo);
                }

                categoryCombo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                frmCategory frmCat = new frmCategory();
                frmCat.currentCategories = (DataTable)categoryCombo.DataSource;
                frmCat.ShowFromAdd(new CallbackAdd(AddCategoryBtn));

                using (Category cat = new Category(true))
                {
                    cat.BindToCombo(categoryCombo);
                }
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSubCategory subCat = new frmSubCategory();

                subCat.currentCategories = (DataTable)subcategoryCombo.DataSource;

                subCat.mainCatSelectedIndex = categoryCombo.SelectedIndex;

                subCat.ShowFromAdd(new CallbackAdd(AddSubCategoryBtn));
                RefreshSubCategory();
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        public void AddCategoryBtn(int categoryIndex)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        public void AddSubCategoryBtn(int SubcategoryIndex)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        private void categoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshSubCategory();
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        private void RefreshSubCategory()
        {
            try
            {
                subcategoryCombo.Text = string.Empty;
                if (categoryCombo.SelectedValue != null && categoryCombo.SelectedValue is int)
                {
                    using (Category cat = new Category(true))
                    {
                        cat.mainCategoryID = (int)categoryCombo.SelectedValue;
                        cat.BindToCombo_sub(subcategoryCombo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message,this.Location);
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBeforeAdd())
                {
                    using (Item item = new Item(true))
                    {


                        item.Name = nameTxt.Text;
                        item.Code = idTxt.Text;
                        item.Cost = (double)costNud.Value;
                        item.Price = (double)priceNUD.Value;
                        item.InStock = (int)inStockNUD.Value;
                        item.WarnLimit = (int)warnLimitNUD.Value;
                        item.MainCategory = (int)categoryCombo.SelectedValue;
                        item.SubCategory = subcategoryCombo.SelectedValue == null ? 0 : (int)subcategoryCombo.SelectedValue;
                        item.CustomPrice = customPriceCheckbox.Checked;

                        if (itemID == 0)
                        {
                            int status = item.SelectExists();

                            switch (status)
                            {
                                case 0:
                                    if (item.Add() == 1)
                                    {
                                        MessageView.ShowMsg("Sucessfully Added");

                                        errorProvider1.SetError(idTxt, string.Empty);
                                        errorProvider1.SetError(nameTxt, string.Empty);

                                        clear();
                                    }
                                    break;

                                case 1:
                                    errorProvider1.SetError(idTxt, "'Item Code' Already Exists");
                                    break;

                                case 2:
                                    errorProvider1.SetError(nameTxt, "'Name' Already Exists");

                                    break;

                                case 3:
                                    errorProvider1.SetError(nameTxt, "'Name' Already Exists");
                                    errorProvider1.SetError(idTxt, "'Item Code' Already Exists");
                                    break;

                            }
                        }
                        else
                        {
                            item.ID = itemID;

                            if (MessageView.ShowQuestionMsg("Update Current Item") == DialogResult.OK)
                            {
                                if (item.Update() == 1)
                                {
                                    MessageView.ShowMsg("Sucessfully Updated");

                                    errorProvider1.SetError(idTxt, string.Empty);
                                    errorProvider1.SetError(nameTxt, string.Empty);

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
            itemID = 0;
            nameTxt.Text = string.Empty;
            idTxt.Text = string.Empty;

            costNud.Value = 0;
            priceNUD.Value = 0;
            inStockNUD.Value = 0;

            categoryCombo.SelectedIndex = -1;

            deleteBtn.Enabled = false;
            addbtn.Text = "Insert";

            customPriceCheckbox.Checked = false;
        }


        private bool ValidateBeforeAdd()
        {
            try
            {
                bool result = true;

                // name
                if (string.IsNullOrEmpty(nameTxt.Text))
                {
                    errorProvider1.SetError(nameTxt, "Please Enter Name");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(nameTxt, string.Empty);
                }

                //code
                if (string.IsNullOrEmpty(idTxt.Text))
                {
                    errorProvider1.SetError(idTxt, "Please Enter 'Item Code'");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(idTxt, string.Empty);
                }


                // Cost
                if (costNud.Value == 0)
                {
                    errorProvider1.SetError(costNud, "Please Enter Cost");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(costNud, string.Empty);
                }

                // price
                if (priceNUD.Value == 0)
                {
                    errorProvider1.SetError(priceNUD, "Please Enter Price");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(priceNUD, string.Empty);
                }

                // price
                if (categoryCombo.SelectedValue == null)
                {
                    errorProvider1.SetError(categoryCombo, "Please Enter Category");
                    result = false;
                }
                else
                {
                    errorProvider1.SetError(categoryCombo, string.Empty);
                }

                return result;

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
                return false;
            }
        }

        private void costNud_Enter(object sender, EventArgs e)
        {
            try
            {
                ((NumericUpDown)sender).Select(0, costNud.Value.ToString().Length + 3);
            }
            catch { }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                using (Item item = new Item(true))
                {


                    item.Name = nameTxt.Text;
                    item.Code = idTxt.Text == null ? string.Empty : idTxt.Text;
                    //item.Cost = (double)costNud.Value;
                    //item.Price = (double)priceNUD.Value;
                    //item.InStock = (int)inStockNUD.Value;
                    item.MainCategory = categoryCombo.SelectedValue == null ? -1 : (int)categoryCombo.SelectedValue;
                    //item.SubCategory = subcategoryCombo.SelectedValue == null ? 0 : (int)subcategoryCombo.SelectedValue;

                    DataTable ds = item.SelectFind();
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
                itemID = (int)hashtable["ID"];
                nameTxt.Text = hashtable["ItemName"].ToString();
                idTxt.Text = hashtable["ItemCode"].ToString();
                costNud.Value = Convert.ToDecimal(hashtable["Cost"]);
                priceNUD.Value = Convert.ToDecimal(hashtable["Price"]);
                inStockNUD.Value = Convert.ToDecimal(hashtable["InStock"]);
                categoryCombo.SelectedValue = (int)hashtable["Category"];
                subcategoryCombo.SelectedValue = (int)hashtable["SubCategory"];
                customPriceCheckbox.Checked = (bool)hashtable["CustomPrice"];
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (Item item = new Item(true))
                {
                    item.ID = itemID;

                    if (MessageView.ShowQuestionMsg("Delete Item " + nameTxt.Text + "'") == DialogResult.OK)
                    {
                        item.Delete();
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

        internal void ShowFromOther()
        {
            throw new NotImplementedException();
        }

        //private void customPriceCheckbox_CheckedChanged(object sender, EventArgs e)
        //{
        //    priceNUD.Enabled = costNud.Enabled = !customPriceCheckbox.Checked;
        //}
    }
}
