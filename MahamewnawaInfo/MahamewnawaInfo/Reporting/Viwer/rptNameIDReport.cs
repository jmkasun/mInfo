using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using DBCore;

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class rptNameIDReport : DevComponents.DotNetBar.Office2007Form
    {
        public rptNameIDReport()
        {
            InitializeComponent();
        }

        private void rptNameIDReport_Load(object sender, EventArgs e)
        {
           

        }


        public void AddData(List<BikkuInfo> data)
        {
            mahamevnainfoDataSet.BhikkuReportNameID.Rows.Clear();
            foreach (BikkuInfo b in data)
            {
                mahamevnainfoDataSet.BhikkuReportNameID.Rows.Add(b.Number, b.NameAssumedAtRobing, b.IsUpasampanna ? "උපසම්පදා" : "සාමනේර",b.SortListOrdeNumber);
            }
        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            using (BikkuInfo b = new BikkuInfo(true))
            {

                CurrenStatus sts = sitiRadio.Checked ? CurrenStatus.Siti : otherPlaceRadio.Checked ? CurrenStatus.OtherPlace : CurrenStatus.Upavidi;

                AddData(b.SelectAllList(sts));

            }
            this.reportViewer1.RefreshReport();
        }

        private void label45_Click(object sender, EventArgs e)
        {
            sitiRadio.Checked = true;
        }

        private void label46_Click(object sender, EventArgs e)
        {
            otherPlaceRadio.Checked = true;
        }

        private void label47_Click(object sender, EventArgs e)
        {
            upavidiRadio.Checked=true;
        }
    }
}
