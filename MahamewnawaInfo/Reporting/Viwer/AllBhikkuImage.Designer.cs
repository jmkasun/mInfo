namespace MahamewnawaInfo.Reporting.Viwer
{
    partial class AllBhikkuImage
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllBhikkuImage));
            this.changeListReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.apawathRadio = new System.Windows.Forms.RadioButton();
            this.label54 = new System.Windows.Forms.Label();
            this.otherPlaceRadioResignStd = new System.Windows.Forms.RadioButton();
            this.otherPlaceRadio = new System.Windows.Forms.RadioButton();
            this.upavidiRadio = new System.Windows.Forms.RadioButton();
            this.sitiRadio = new System.Windows.Forms.RadioButton();
            this.label45 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.showbtn = new DevComponents.DotNetBar.ButtonX();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.apawathRadio);
            this.groupBox2.Controls.Add(this.label54);
            this.groupBox2.Controls.Add(this.otherPlaceRadioResignStd);
            this.groupBox2.Controls.Add(this.otherPlaceRadio);
            this.groupBox2.Controls.Add(this.upavidiRadio);
            this.groupBox2.Controls.Add(this.sitiRadio);
            this.groupBox2.Controls.Add(this.label45);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label46);
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(696, 46);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "වර්තමානයේ";
            // 
            // apawathRadio
            // 
            this.apawathRadio.AutoSize = true;
            this.apawathRadio.Location = new System.Drawing.Point(571, 22);
            this.apawathRadio.Name = "apawathRadio";
            this.apawathRadio.Size = new System.Drawing.Size(14, 13);
            this.apawathRadio.TabIndex = 75;
            this.apawathRadio.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(586, 17);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(87, 20);
            this.label54.TabIndex = 74;
            this.label54.Text = "අපවත් වී ඇත";
            this.label54.Click += new System.EventHandler(this.label54_Click);
            // 
            // otherPlaceRadioResignStd
            // 
            this.otherPlaceRadioResignStd.AutoSize = true;
            this.otherPlaceRadioResignStd.Location = new System.Drawing.Point(250, 22);
            this.otherPlaceRadioResignStd.Name = "otherPlaceRadioResignStd";
            this.otherPlaceRadioResignStd.Size = new System.Drawing.Size(14, 13);
            this.otherPlaceRadioResignStd.TabIndex = 71;
            this.otherPlaceRadioResignStd.UseVisualStyleBackColor = true;
            // 
            // otherPlaceRadio
            // 
            this.otherPlaceRadio.AutoSize = true;
            this.otherPlaceRadio.Location = new System.Drawing.Point(86, 22);
            this.otherPlaceRadio.Name = "otherPlaceRadio";
            this.otherPlaceRadio.Size = new System.Drawing.Size(14, 13);
            this.otherPlaceRadio.TabIndex = 71;
            this.otherPlaceRadio.UseVisualStyleBackColor = true;
            // 
            // upavidiRadio
            // 
            this.upavidiRadio.AutoSize = true;
            this.upavidiRadio.Location = new System.Drawing.Point(435, 22);
            this.upavidiRadio.Name = "upavidiRadio";
            this.upavidiRadio.Size = new System.Drawing.Size(14, 13);
            this.upavidiRadio.TabIndex = 70;
            this.upavidiRadio.UseVisualStyleBackColor = true;
            // 
            // sitiRadio
            // 
            this.sitiRadio.AutoSize = true;
            this.sitiRadio.Checked = true;
            this.sitiRadio.Location = new System.Drawing.Point(12, 22);
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
            this.label45.Location = new System.Drawing.Point(23, 17);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(31, 20);
            this.label45.TabIndex = 68;
            this.label45.Text = " සිටී";
            this.label45.Click += new System.EventHandler(this.label45_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(263, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 68;
            this.label1.Text = "ශිෂ්‍ය භාවයෙන් ඉවත් වී සිටී";
            this.label1.Click += new System.EventHandler(this.label461_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(97, 17);
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
            this.label47.Location = new System.Drawing.Point(446, 17);
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
            this.showbtn.Location = new System.Drawing.Point(704, 8);
            this.showbtn.Name = "showbtn";
            this.showbtn.Size = new System.Drawing.Size(85, 35);
            this.showbtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.showbtn.TabIndex = 85;
            this.showbtn.Text = "Show";
            this.showbtn.Click += new System.EventHandler(this.showbtn_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.DocumentMapWidth = 8;
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.changeListReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.AllBhikkuWithImage.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 50);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1011, 540);
            this.reportViewer1.TabIndex = 0;
            // 
            // AllBhikkuImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 585);
            this.Controls.Add(this.showbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.reportViewer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AllBhikkuImage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AllBhikkuImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource changeListReportBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton apawathRadio;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.RadioButton otherPlaceRadio;
        private System.Windows.Forms.RadioButton upavidiRadio;
        private System.Windows.Forms.RadioButton sitiRadio;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private DevComponents.DotNetBar.ButtonX showbtn;
        private System.Windows.Forms.RadioButton otherPlaceRadioResignStd;
        private System.Windows.Forms.Label label1;
    }
}
