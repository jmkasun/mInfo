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
    public partial class AsapuBhikkuImagePanel : Panel
    {
        public AsapuBhikkuImagePanel()
        {
            InitializeComponent();
        }

    
        public void ShowReport(List<ChangeListItemBhikku> bhikkuList,string fromDate, string asapuwaName)
        {
            //mahamevnainfoDataSet.BhikkuReportNameID.Rows.Clear();
            //mahamevnainfoDataSet.Util.Rows.Clear();

            mahamevnainfoDataSet.Util.Rows.Add(fromDate, asapuwaName);

            foreach (ChangeListItemBhikku b in bhikkuList)
            {
                mahamevnainfoDataSet.ChangeListReport.Rows.Add(b.bInfo.ImageData, b.bInfo.NameAssumedAtRobing, b.bInfo.SortListOrdeNumber);
            }

            reportViewer1.RefreshReport();

         //   this.ShowDialog();

            
        }


        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.changeListReportBindingSource = new System.Windows.Forms.BindingSource();
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.UtilBindingSource = new System.Windows.Forms.BindingSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();

            ((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UtilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // changeListReportBindingSource
            // 
            this.changeListReportBindingSource.DataMember = "ChangeListReport";
            this.changeListReportBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // mahamevnainfoDataSet
            // 
            this.mahamevnainfoDataSet.DataSetName = "mahamevnainfoDataSet";
            this.mahamevnainfoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // UtilBindingSource
            // 
            this.UtilBindingSource.DataMember = "Util";
            this.UtilBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.changeListReportBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.UtilBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.AsapuBhikkuWithImage.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(737, 752);
            this.reportViewer1.TabIndex = 0;
            // 
            // AsapuBhikkuImage
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 752);
            this.Controls.Add(this.reportViewer1);
            //this.DoubleBuffered = true;
            this.Name = "AsapuBhikkuImage";
            //((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.UtilBindingSource)).EndInit();
            this.ResumeLayout(false);

        }



        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource changeListReportBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private System.Windows.Forms.BindingSource UtilBindingSource;
    }
}