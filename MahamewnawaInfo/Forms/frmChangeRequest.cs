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
                    dataGridView1.DataSource = r.SelectAll((int)cmbChangeList.SelectedValue);
                }
            }
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
    }
}
