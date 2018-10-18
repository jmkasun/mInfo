using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MahamewnawaInfo.Classes;

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class ChangeListReport : DevComponents.DotNetBar.Office2007Form
    {
        public ChangeListReport()
        {
            InitializeComponent();
        }

        private void ChangeListReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }


        public void AddData(List<ChangeListReportData> data, string sinhalaDate)
        {
            string bhikkuName = "";
            string AsapuwaName = string.Empty;
            int number = 1;

            mahamevnainfoDataSet.Util.Rows.Add(sinhalaDate);

            foreach (ChangeListReportData d in data)
            {
                bhikkuName = d.BhikkuName;

                if (!string.IsNullOrEmpty(d.Post))
                {
                    bhikkuName = string.Concat(d.BhikkuName, "    (", d.Post, ")");
                }

                if (AsapuwaName != d.AsapuwaName)
                {
                    if (AsapuwaName != string.Empty)
                    {
                        mahamevnainfoDataSet.ChangeListReport.Rows.Add(string.Empty, string.Empty, 0);
                    }
                    mahamevnainfoDataSet.ChangeListReport.Rows.Add(d.AsapuwaName, d.AsapuwaName, -1);
                   
                    AsapuwaName = d.AsapuwaName;
                    number = 0;
                }

                mahamevnainfoDataSet.ChangeListReport.Rows.Add(string.Empty, bhikkuName, number++);
            }
        }

    }
}
