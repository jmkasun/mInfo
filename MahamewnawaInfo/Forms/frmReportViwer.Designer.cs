namespace MahamewnawaInfo.Forms
{
    partial class frmReportViwer
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
            this.reportParamPanel = new DevComponents.DotNetBar.PanelEx();
            this.reportViwerPanel = new DevComponents.DotNetBar.PanelEx();
            this.SuspendLayout();
            // 
            // reportParamPanel
            // 
            this.reportParamPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.reportParamPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.reportParamPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.reportParamPanel.Location = new System.Drawing.Point(2, 2);
            this.reportParamPanel.Name = "reportParamPanel";
            this.reportParamPanel.Size = new System.Drawing.Size(189, 526);
            this.reportParamPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.reportParamPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.reportParamPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.reportParamPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.reportParamPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.reportParamPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.reportParamPanel.Style.GradientAngle = 90;
            this.reportParamPanel.TabIndex = 0;
            this.reportParamPanel.Text = "panelEx1";
            // 
            // reportViwerPanel
            // 
            this.reportViwerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViwerPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.reportViwerPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.reportViwerPanel.Location = new System.Drawing.Point(197, 2);
            this.reportViwerPanel.Name = "reportViwerPanel";
            this.reportViwerPanel.Size = new System.Drawing.Size(579, 526);
            this.reportViwerPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.reportViwerPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.reportViwerPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.reportViwerPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.reportViwerPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.reportViwerPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.reportViwerPanel.Style.GradientAngle = 90;
            this.reportViwerPanel.TabIndex = 1;
            this.reportViwerPanel.Text = "panelEx1";
            // 
            // frmReportViwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 530);
            this.Controls.Add(this.reportViwerPanel);
            this.Controls.Add(this.reportParamPanel);
            this.Name = "frmReportViwer";
            this.Text = "ReportViwer";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx reportParamPanel;
        private DevComponents.DotNetBar.PanelEx reportViwerPanel;
    }
}