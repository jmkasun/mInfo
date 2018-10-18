using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class BhikkuInfoReport : UserControl
    {
        public BhikkuInfoReport()
        {
            InitializeComponent();
        }

        private void BhikkuInfoReport_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            rep_Sele_BhikkuTableAdapter.Fill(mahamevnainfoDataSet.Rep_Sele_Bhikku, 2);
            reportViewer1.RefreshReport();
        }
    }
}
