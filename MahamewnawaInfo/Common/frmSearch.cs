using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MahamewnawaInfo.Common
{
    public partial class frmSearch : DevComponents.DotNetBar.Office2007Form
    {

        public Hashtable DataRowValues { get; private set; }

        object[] selectedRowData;
        DataTable dt = null;
        int maxColumnCount = 3;
        int firstColWidth = -1;
        int secondColWidth = -1;

        public frmSearch(DataTable dt, string text, int _maxColCount)
        {
            maxColumnCount = _maxColCount;

            Init(dt, text);
        }


        public frmSearch(DataTable dt, string text, int _maxColCount,int firstColumnWidth,int _secondColWidth=-1)
        {
            maxColumnCount = _maxColCount;
            firstColWidth = firstColumnWidth;
            secondColWidth = _secondColWidth;

            Init(dt, text);
        }

        private void Init(DataTable dt, string text)
        {

            InitializeComponent();


            try
            {
                //ned to remove
                // ds = new GeneralFunctions("dcb", "dcb").GetTestData();
                ///ned to remove
                ///
                int colCnt = dt.Columns.Count;

                selectedRowData = new object[colCnt];

                if (colCnt > 0)
                {
                    for (int i = 0; i < colCnt; i++)
                    {
                        string actText = dt.Columns[i].Caption;
                        selectedRowData[i] = actText;
                        string newText = getFormatedColumnName(actText);
                        dt.Columns[i].ColumnName = newText;
                    }
                }

                this.dt = dt;
                this.Text += " _ " + text;
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private string getFormatedColumnName(string colName)
        {
            try
            {
                StringBuilder newText = new StringBuilder();
                colName = colName.Trim();

                char[] charArr = colName.ToCharArray();

                newText.Append(char.ToUpper(charArr[0]).ToString());
                for (int i = 0; i < charArr.Length; i++)
                {

                    if (char.IsUpper(charArr[i]) && i > 0 && !char.IsUpper(charArr[i - 1]))
                    {
                        newText.Append(" ");
                        newText.Append(char.ToUpper(charArr[i]).ToString());
                    }
                    else if (char.Equals('_', charArr[i]))
                    {
                        newText.Append(" ");
                        newText.Append(char.ToUpper(charArr[i + 1]).ToString());
                        i += 1;
                    }
                    else if (char.Equals(' ', charArr[i]))
                    {
                        newText.Append(" ");
                        newText.Append(char.ToUpper(charArr[i + 1]).ToString());
                        i += 1;
                    }
                    else
                    {
                        if (i > 0)
                        {
                            newText.Append(charArr[i].ToString());
                        }
                    }
                }

                return newText.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count == 0)
                {
                    MessageView.ShowWarningMsg("No record(s) found");
                    this.Dispose();
                }
                dgvSearch.DataSource = dt;

                // hide extra fields

                for (int i = 0; i < dgvSearch.Columns.Count; i++)
                {
                    if (maxColumnCount < (i + 1))
                        dgvSearch.Columns[i].Visible = false;
                }

                Utility.SetDatagridViewRow(dgvSearch);

                if (firstColWidth > 0 && dgvSearch.Columns.Count > 0)
                {
                    dgvSearch.Columns[0].Width = firstColWidth;
                }

                if (secondColWidth > 0 && dgvSearch.Columns.Count > 1)
                {
                    dgvSearch.Columns[1].Width = secondColWidth;
                }

            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }      



        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSearch.SelectedRows.Count > 0)
                {
                    selectRow(dgvSearch.CurrentRow.Index);
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageView.ShowWarningMsg("Please select a row");
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }

        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }


        //private void dgvSearch_Sorted(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        new ApplicationSettings().SetColuredGridsRows(ref dgvSearch);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageView.ExceptionError(ex);
        //    }
        //}


        private void dgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    selectRow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void selectRow(int index)
        {
            try
            {
                this.DialogResult = DialogResult.OK;

                //set selected data
                Hashtable htbl = new Hashtable();
                for (int i = 0; i < selectedRowData.Length; i++)
                {
                    htbl.Add(selectedRowData[i].ToString(), dgvSearch[i, index].Value);
                }

                DataRowValues = htbl;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Return)
                {
                    selectRow(dgvSearch.CurrentRow.Index);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }

        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
