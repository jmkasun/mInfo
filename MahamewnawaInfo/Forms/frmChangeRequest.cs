using DBCore.Classes;
using MahamewnawaInfo.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahamewnawaInfo.Forms
{
    public partial class frmChangeRequest : Form
    {
        bool finishLoadData = false;
        public frmChangeRequest()
        {
            InitializeComponent();
        }

        private void frmChangeRequest_Load(object sender, EventArgs e)
        {
            using (ChangeList cLIst = new ChangeList(true))
            {
                cLIst.BindToCombo(cmbChangeList);
            }

            using (BikkuInfo b = new BikkuInfo(true))
            {
                b.BindToComboNameSeparate(cmbName);
                cmbName.SelectedIndex = -1;
            }

            using (Asapuwa a = new Asapuwa(true))
            {
                a.BindToCombo(new ComboBox[] { requestAsapuwa1, requestAsapuwa2, requestAsapuwa3 });
            }

            finishLoadData = true;

            refreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbName.SelectedValue != null && (int)cmbName.SelectedValue > 0)
                {
                    using (ChangelistRequest req = new ChangelistRequest(true))
                    {
                        req.BhikkuId = (int)cmbName.SelectedValue;
                        req.ChangelistId = (int)cmbChangeList.SelectedValue;
                        req.Asapuwa1Id = (int)(requestAsapuwa1.SelectedValue ?? -1);
                        req.Asapuwa2Id = (int)(requestAsapuwa2.SelectedValue ?? -1);
                        req.Asapuwa3Id = (int)(requestAsapuwa3.SelectedValue ?? -1);

                        if (!(req.Asapuwa1Id == -1 && req.Asapuwa2Id == -1 && req.Asapuwa3Id == -1))
                        {
                            req.Add();
                        }
                    }

                    clear();
                    cmbName.Select();
                    refreshData();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Name Already Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            cmbName.SelectedIndex = requestAsapuwa1.SelectedIndex = requestAsapuwa2.SelectedIndex = requestAsapuwa3.SelectedIndex = -1;
        }

        private void cmbChangeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (finishLoadData)
            {
                refreshData();
            }
        }

        private void cmbChangeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void refreshData()
        {
            if (cmbChangeList.SelectedIndex > -1)
            {
                using (ChangelistRequest r = new ChangelistRequest(true))
                {
                    var dataSource = r.SelectAll((int)cmbChangeList.SelectedValue);

                    dataSource.Columns.Add("image", typeof(Image));

                    foreach (DataRow rw in dataSource.Rows)
                    {
                        if (rw["ImageData"] != null)
                        {
                            rw["image"] = Utility.GetImageFromBase64(rw["ImageData"].ToString());

                        }
                    }
                    
                    dataGridView1.DataSource = dataSource;
                }
            }

            PrepareCells();
        }

        private void PrepareCells()
        {
            int rowIndex = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 60;
                row.HeaderCell.Value = (++rowIndex).ToString();

                ((DataGridViewImageCell)row.Cells["image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            dataGridView1.Refresh();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Delete Record") == DialogResult.OK)
            {
                using (ChangelistRequest r = new ChangelistRequest(true))
                {
                    if (r.Delete(Convert.ToInt32(e.Row.Cells[0].Value)) == 0)
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Utility.ExportToExcelFile(dataGridView1);
        }
    }
}
