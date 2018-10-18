namespace MahamewnawaInfo.Forms
{
    partial class frmChangelistParams
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.changelistStatBtn = new DevComponents.DotNetBar.ButtonX();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.toDateDatetime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.fromDateDatetime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.histryGroup = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toDateDatetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateDatetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.changelistStatBtn);
            this.groupPanel1.Controls.Add(this.label50);
            this.groupPanel1.Controls.Add(this.label48);
            this.groupPanel1.Controls.Add(this.toDateDatetime);
            this.groupPanel1.Controls.Add(this.fromDateDatetime);
            this.groupPanel1.Location = new System.Drawing.Point(268, 5);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(269, 245);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "New";
            // 
            // changelistStatBtn
            // 
            this.changelistStatBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.changelistStatBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.changelistStatBtn.Location = new System.Drawing.Point(131, 112);
            this.changelistStatBtn.Name = "changelistStatBtn";
            this.changelistStatBtn.Size = new System.Drawing.Size(104, 39);
            this.changelistStatBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.changelistStatBtn.TabIndex = 55;
            this.changelistStatBtn.Text = "Start";
            this.changelistStatBtn.Click += new System.EventHandler(this.changelistStatBtn_Click);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(5, 63);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(44, 20);
            this.label50.TabIndex = 53;
            this.label50.Text = "දක්වා";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(5, 19);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(30, 20);
            this.label48.TabIndex = 52;
            this.label48.Text = "සිට";
            // 
            // toDateDatetime
            // 
            // 
            // 
            // 
            this.toDateDatetime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.toDateDatetime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.toDateDatetime.ButtonDropDown.Visible = true;
            this.toDateDatetime.CustomFormat = "yyyy-MMM-dd";
            this.toDateDatetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateDatetime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.toDateDatetime.Location = new System.Drawing.Point(55, 63);
            // 
            // 
            // 
            this.toDateDatetime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.toDateDatetime.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.toDateDatetime.MonthCalendar.BackgroundStyle.Class = "";
            this.toDateDatetime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.toDateDatetime.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.toDateDatetime.MonthCalendar.DisplayMonth = new System.DateTime(2011, 11, 1, 0, 0, 0, 0);
            this.toDateDatetime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.toDateDatetime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.toDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.toDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.toDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.toDateDatetime.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.toDateDatetime.MonthCalendar.TodayButtonVisible = true;
            this.toDateDatetime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.toDateDatetime.Name = "toDateDatetime";
            this.toDateDatetime.Size = new System.Drawing.Size(180, 26);
            this.toDateDatetime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.toDateDatetime.TabIndex = 51;
            // 
            // fromDateDatetime
            // 
            // 
            // 
            // 
            this.fromDateDatetime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.fromDateDatetime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.fromDateDatetime.ButtonDropDown.Visible = true;
            this.fromDateDatetime.CustomFormat = "yyyy-MMM-dd";
            this.fromDateDatetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateDatetime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.fromDateDatetime.Location = new System.Drawing.Point(55, 19);
            // 
            // 
            // 
            this.fromDateDatetime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fromDateDatetime.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.fromDateDatetime.MonthCalendar.BackgroundStyle.Class = "";
            this.fromDateDatetime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.fromDateDatetime.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.fromDateDatetime.MonthCalendar.DisplayMonth = new System.DateTime(2011, 11, 1, 0, 0, 0, 0);
            this.fromDateDatetime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.fromDateDatetime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fromDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.fromDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.fromDateDatetime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.fromDateDatetime.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.fromDateDatetime.MonthCalendar.TodayButtonVisible = true;
            this.fromDateDatetime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.fromDateDatetime.Name = "fromDateDatetime";
            this.fromDateDatetime.Size = new System.Drawing.Size(180, 26);
            this.fromDateDatetime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.fromDateDatetime.TabIndex = 50;
            // 
            // histryGroup
            // 
            this.histryGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.histryGroup.AutoScroll = true;
            this.histryGroup.CanvasColor = System.Drawing.SystemColors.Control;
            this.histryGroup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.histryGroup.Location = new System.Drawing.Point(2, 5);
            this.histryGroup.Name = "histryGroup";
            this.histryGroup.Size = new System.Drawing.Size(262, 245);
            // 
            // 
            // 
            this.histryGroup.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.histryGroup.Style.BackColorGradientAngle = 90;
            this.histryGroup.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.histryGroup.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.histryGroup.Style.BorderBottomWidth = 1;
            this.histryGroup.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.histryGroup.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.histryGroup.Style.BorderLeftWidth = 1;
            this.histryGroup.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.histryGroup.Style.BorderRightWidth = 1;
            this.histryGroup.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.histryGroup.Style.BorderTopWidth = 1;
            this.histryGroup.Style.Class = "";
            this.histryGroup.Style.CornerDiameter = 4;
            this.histryGroup.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.histryGroup.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.histryGroup.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.histryGroup.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.histryGroup.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.histryGroup.StyleMouseOver.Class = "";
            this.histryGroup.TabIndex = 1;
            this.histryGroup.Text = "Histry";
            this.histryGroup.MouseEnter += new System.EventHandler(this.histryGroup_MouseEnter);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmChangelistParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 252);
            this.Controls.Add(this.histryGroup);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.Name = "frmChangelistParams";
            this.Text = "Changelist Params";
            this.Load += new System.EventHandler(this.frmChangelistParams_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toDateDatetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateDatetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX changelistStatBtn;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label48;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput toDateDatetime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput fromDateDatetime;
        private DevComponents.DotNetBar.Controls.GroupPanel histryGroup;
        private System.Windows.Forms.ErrorProvider errorProvider1;

    }
}