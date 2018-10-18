using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MahamewnawaInfo.Classes;
using DBCore.Classes;
using DBCore;

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class AllBhikkuImage : DevComponents.DotNetBar.Office2007Form
    {
        public AllBhikkuImage()
        {
            InitializeComponent();
        }

        private void AllBhikkuImage_Load(object sender, EventArgs e)
        {
           
        }


        public void AddData(List<BikkuInfo> data)
        {
            mahamevnainfoDataSet.ChangeListReport.Rows.Clear();

            foreach (BikkuInfo b in data)
            {
                mahamevnainfoDataSet.ChangeListReport.Rows.Add( b.ImageData, b.NameAssumedAtRobing,b.SortListOrdeNumber,"1");
            }
        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            CurrenStatus sts = sitiRadio.Checked ? CurrenStatus.Siti : otherPlaceRadio.Checked ? CurrenStatus.OtherPlace : upavidiRadio.Checked ? CurrenStatus.Upavidi : CurrenStatus.Apawath;

            using (BikkuInfo b = new BikkuInfo(true))
            {

                AddData(b.SelectAllImage(sts));

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
            upavidiRadio.Checked =  true;
        }

        private void label54_Click(object sender, EventArgs e)
        {
            apawathRadio.Checked = true;
        }
    }
}