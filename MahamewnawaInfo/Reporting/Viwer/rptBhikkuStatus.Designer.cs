namespace MahamewnawaInfo.Reporting.Viwer
{
    partial class rptBhikkuStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptBhikkuStatus));
            this.bhikkuReportNameIDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.apawathRadio = new System.Windows.Forms.RadioButton();
            this.label54 = new System.Windows.Forms.Label();
            this.otherPlaceRadio = new System.Windows.Forms.RadioButton();
            this.upavidiRadio = new System.Windows.Forms.RadioButton();
            this.sitiRadio = new System.Windows.Forms.RadioButton();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.showbtn = new DevComponents.DotNetBar.ButtonX();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuReportNameIDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bhikkuReportNameIDBindingSource
            // 
            this.bhikkuReportNameIDBindingSource.DataMember = "BhikkuReportNameID";
            this.bhikkuReportNameIDBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // mahamevnainfoDataSet
            // 
            this.mahamevnainfoDataSet.DataSetName = "mahamevnainfoDataSet";
            this.mahamevnainfoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.apawathRadio);
            this.groupBox2.Controls.Add(this.label54);
            this.groupBox2.Controls.Add(this.otherPlaceRadio);
            this.groupBox2.Controls.Add(this.upavidiRadio);
            this.groupBox2.Controls.Add(this.sitiRadio);
            this.groupBox2.Controls.Add(this.label45);
            this.groupBox2.Controls.Add(this.label46);
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 66);
            this.groupBox2.TabIndex = 83;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "වර්තමානයේ";
            // 
            // apawathRadio
            // 
            this.apawathRadio.AutoSize = true;
            this.apawathRadio.Location = new System.Drawing.Point(408, 33);
            this.apawathRadio.Name = "apawathRadio";
            this.apawathRadio.Size = new System.Drawing.Size(14, 13);
            this.apawathRadio.TabIndex = 75;
            this.apawathRadio.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(423, 30);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(87, 20);
            this.label54.TabIndex = 74;
            this.label54.Text = "අපවත් වී ඇත";
            this.label54.Click += new System.EventHandler(this.label54_Click);
            // 
            // otherPlaceRadio
            // 
            this.otherPlaceRadio.Location = new System.Drawing.Point(86, 33);
            this.otherPlaceRadio.Name = "otherPlaceRadio";
            this.otherPlaceRadio.Size = new System.Drawing.Size(14, 13);
            this.otherPlaceRadio.TabIndex = 71;
            this.otherPlaceRadio.UseVisualStyleBackColor = true;
            // 
            // upavidiRadio
            // 
            this.upavidiRadio.AutoSize = true;
            this.upavidiRadio.Location = new System.Drawing.Point(272, 33);
            this.upavidiRadio.Name = "upavidiRadio";
            this.upavidiRadio.Size = new System.Drawing.Size(14, 13);
            this.upavidiRadio.TabIndex = 70;
            this.upavidiRadio.UseVisualStyleBackColor = true;
            // 
            // sitiRadio
            // 
            this.sitiRadio.AutoSize = true;
            this.sitiRadio.Checked = true;
            this.sitiRadio.Location = new System.Drawing.Point(12, 33);
            this.sitiRadio.Name = "sitiRadio";
            this.sitiRadio.Size = new System.Drawing.Size(14, 13);
            this.sitiRadio.TabIndex = 69;
            this.sitiRadio.TabStop = true;
            this.sitiRadio.UseVisualStyleBackColor = true;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(23, 30);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(31, 20);
            this.label45.TabIndex = 68;
            this.label45.Text = " සිටී";
            this.label45.Click += new System.EventHandler(this.label45_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(97, 30);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(137, 20);
            this.label46.TabIndex = 68;
            this.label46.Text = " වෙනත් ස්ථානයක සිටී";
            this.label46.Click += new System.EventHandler(this.label46_Click);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(282, 30);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(90, 20);
            this.label47.TabIndex = 68;
            this.label47.Text = " උපැවිදි වී ඇත";
            this.label47.Click += new System.EventHandler(this.label47_Click);
            // 
            // showbtn
            // 
            this.showbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.showbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.showbtn.Location = new System.Drawing.Point(545, 11);
            this.showbtn.Name = "showbtn";
            this.showbtn.Size = new System.Drawing.Size(100, 54);
            this.showbtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.showbtn.TabIndex = 84;
            this.showbtn.Text = "Show";
            this.showbtn.Click += new System.EventHandler(this.showbtn_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.bhikkuReportNameIDBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.BhikkuInfoNameID.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 72);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1354, 677);
            this.reportViewer1.TabIndex = 0;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "ReportViewer";
            this.reportViewer2.Size = new System.Drawing.Size(396, 246);
            this.reportViewer2.TabIndex = 0;
            // 
            // rptBhikkuStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 749);
            this.Controls.Add(this.showbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.reportViewer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "rptBhikkuStatus";
            this.Text = "rptNameIDReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptNameIDReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuReportNameIDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource bhikkuReportNameIDBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton otherPlaceRadio;
        private System.Windows.Forms.RadioButton upavidiRadio;
        private System.Windows.Forms.RadioButton sitiRadio;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private DevComponents.DotNetBar.ButtonX showbtn;
        private System.Windows.Forms.RadioButton apawathRadio;
        private System.Windows.Forms.Label label54;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
    }
}