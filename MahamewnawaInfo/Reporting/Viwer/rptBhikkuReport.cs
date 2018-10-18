using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;

namespace MahamewnawaInfo.Reporting.Viwer
{
    public partial class rptBhikkuReport : DevComponents.DotNetBar.Office2007Form
    {
        public rptBhikkuReport()
        {
            InitializeComponent();
        }

        private void rptBhikkuReport_Load(object sender, EventArgs e)
        {
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bInfo.BindToComboNameSeparate(nameOfAssumedAtRobinCombo);
            }
            nameOfAssumedAtRobinCombo.SelectedIndex = -1;

            //this.reportViewer1.RefreshReport();
        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            if (nameOfAssumedAtRobinCombo.SelectedValue == null)
                return;

            using (BikkuInfo b = new BikkuInfo(true))
            {
                b.ID = (int)nameOfAssumedAtRobinCombo.SelectedValue;
                AddData(b.SelectBhikkuReport());

            }
            this.reportViewer1.RefreshReport();
        }
          
                 

        public void AddData(BikkuInfo b)
        {
            mahamevnainfoDataSet.BhikkuReport.Rows.Clear();
           
                mahamevnainfoDataSet.BhikkuReport.Rows.Add(b.NameAssumedAtRobing,b.NIC, b.SamaneraNumber,
                    b.UpasampadaNumber, b.BloodGroup, b.HomeAddress, b.HomeTP + (string.IsNullOrEmpty(b.HomeTP2) ? "" : " / " + b.HomeTP2), b.PassportNumber,
                    b.DateOfBirth == new DateTime() ? "" : b.DateOfBirth.ToString("yyy-MMM-dd"), 
                    b.DateOfRobing == new DateTime() ? "" : b.DateOfRobing.ToString("yyy-MMM-dd"), 
                    "", b.ImageData);
           
        }
    }
}
