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
    public partial class rptAllBhikkuSummary : DevComponents.DotNetBar.Office2007Form
    {
        public rptAllBhikkuSummary()
        {
            InitializeComponent();
        }

        private void rptNameIDReport_Load(object sender, EventArgs e)
        {

            using (BikkuInfo b = new BikkuInfo(true))
            {

                //CurrenStatus sts = sitiRadio.Checked ? CurrenStatus.Siti : otherPlaceRadio.Checked ? CurrenStatus.OtherPlace : CurrenStatus.Upavidi;

                AddData(b.SelectAllList(CurrenStatus.Siti));

            }
            this.reportViewer1.RefreshReport();
        }


        public void AddData(List<BikkuInfo> data)
        {
            mahamevnainfoDataSet.BhikkuReportNameID.Rows.Clear();
            foreach (BikkuInfo b in data)
            {
                mahamevnainfoDataSet.BhikkuReportNameID.Rows.Add(b.Number, b.NameAssumedAtRobing, b.DateOfRobing == new DateTime() ? "" : b.DateOfRobing.ToString("yyyy-MMM-dd"), b.SortListOrdeNumber, b.NIC, b.SamaneraNumber, b.UpasampadaNumber);
            }
        }
    }
}
