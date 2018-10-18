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
    public partial class rpt_AllbhikkuAsapuwa : DevComponents.DotNetBar.Office2007Form
    {
        public rpt_AllbhikkuAsapuwa()
        {
            InitializeComponent();
        }

        private void rptNameIDReport_Load(object sender, EventArgs e)
        {

            using (BikkuInfo b = new BikkuInfo(true))
            {

                //CurrenStatus sts = sitiRadio.Checked ? CurrenStatus.Siti : otherPlaceRadio.Checked ? CurrenStatus.OtherPlace : CurrenStatus.Upavidi;

                AddData(b.SelectAllListWithAsapuwa());

            }
            this.reportViewer1.RefreshReport();
        }


        public void AddData(List<BikkuInfo> data)
        {
            mahamevnainfoDataSet.BhikkuReportNameID.Rows.Clear();
            foreach (BikkuInfo b in data)
            {
                mahamevnainfoDataSet.BhikkuReportNameID.Rows.Add(b.Number, b.NameAssumedAtRobing,"", b.SortListOrdeNumber,b.NIC,b.SamaneraNumber,b.UpasampadaNumber);
            }
        }
    }
}
