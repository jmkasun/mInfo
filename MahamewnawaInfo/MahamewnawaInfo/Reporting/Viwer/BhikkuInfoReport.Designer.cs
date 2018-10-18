namespace MahamewnawaInfo.Reporting.Viwer
{
    partial class BhikkuInfoReport
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.repSeleBhikkuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rep_Sele_BhikkuTableAdapter = new MahamewnawaInfo.mahamevnainfoDataSetTableAdapters.Rep_Sele_BhikkuTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSeleBhikkuBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.repSeleBhikkuBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.BhikkuInfi.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(626, 406);
            this.reportViewer1.TabIndex = 0;
            // 
            // mahamevnainfoDataSet
            // 
            this.mahamevnainfoDataSet.DataSetName = "mahamevnainfoDataSet";
            this.mahamevnainfoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // repSeleBhikkuBindingSource
            // 
            this.repSeleBhikkuBindingSource.DataMember = "Rep_Sele_Bhikku";
            this.repSeleBhikkuBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // rep_Sele_BhikkuTableAdapter
            // 
            this.rep_Sele_BhikkuTableAdapter.ClearBeforeFill = true;
            // 
            // BhikkuInfoReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "BhikkuInfoReport";
            this.Size = new System.Drawing.Size(626, 406);
            this.Load += new System.EventHandler(this.BhikkuInfoReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSeleBhikkuBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource repSeleBhikkuBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private mahamevnainfoDataSetTableAdapters.Rep_Sele_BhikkuTableAdapter rep_Sele_BhikkuTableAdapter;
    }
}
