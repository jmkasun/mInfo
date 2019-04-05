namespace MahamewnawaInfo.Reporting.Viwer
{
    partial class rptBhikkuReport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptBhikkuReport));
            this.bhikkuReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.nameOfAssumedAtRobinCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.showbtn = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // bhikkuReportBindingSource
            // 
            this.bhikkuReportBindingSource.DataMember = "BhikkuReport";
            this.bhikkuReportBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // mahamevnainfoDataSet
            // 
            this.mahamevnainfoDataSet.DataSetName = "mahamevnainfoDataSet";
            this.mahamevnainfoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "BhikkuReport";
            reportDataSource1.Value = this.bhikkuReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.BhikkuInfi.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-4, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(963, 578);
            this.reportViewer1.TabIndex = 0;
            // 
            // nameOfAssumedAtRobinCombo
            // 
            this.nameOfAssumedAtRobinCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.nameOfAssumedAtRobinCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.nameOfAssumedAtRobinCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameOfAssumedAtRobinCombo.FormattingEnabled = true;
            this.nameOfAssumedAtRobinCombo.Location = new System.Drawing.Point(234, 17);
            this.nameOfAssumedAtRobinCombo.Name = "nameOfAssumedAtRobinCombo";
            this.nameOfAssumedAtRobinCombo.Size = new System.Drawing.Size(404, 26);
            this.nameOfAssumedAtRobinCombo.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "පින්වත් ස්වාමින් වහන්සේගේ නම";
            // 
            // showbtn
            // 
            this.showbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.showbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.showbtn.Location = new System.Drawing.Point(644, 17);
            this.showbtn.Name = "showbtn";
            this.showbtn.Size = new System.Drawing.Size(77, 26);
            this.showbtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.showbtn.TabIndex = 85;
            this.showbtn.Text = "Show";
            this.showbtn.Click += new System.EventHandler(this.showbtn_Click);
            // 
            // rptBhikkuReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 636);
            this.Controls.Add(this.showbtn);
            this.Controls.Add(this.nameOfAssumedAtRobinCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "rptBhikkuReport";
            this.Text = "rptBhikkuReport";
            this.Load += new System.EventHandler(this.rptBhikkuReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource bhikkuReportBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private System.Windows.Forms.ComboBox nameOfAssumedAtRobinCombo;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX showbtn;
    }
}