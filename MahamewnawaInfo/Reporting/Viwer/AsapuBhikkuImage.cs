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

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class AsapuBhikkuImage : DevComponents.DotNetBar.Office2007Form
    {
        public AsapuBhikkuImage()
        {
            InitializeComponent();
        }

    
        public void ShowReport(List<ChangeListItemBhikku> bhikkuList,string fromDate, string asapuwaName)
        {
            //mahamevnainfoDataSet.BhikkuReportNameID.Rows.Clear();
            //mahamevnainfoDataSet.Util.Rows.Clear();
            mahamevnainfoDataSet.Util.Rows.Add(fromDate, asapuwaName);
            int index = 1;
            foreach (ChangeListItemBhikku b in bhikkuList)
            {
                mahamevnainfoDataSet.ChangeListReport.Rows.Add(b.bInfo.ImageData, b.bInfo.NameAssumedAtRobing, index++,(int)b.bInfo.Post);
            }

            reportViewer1.RefreshReport();

            this.ShowDialog();
            
        }
    }
}