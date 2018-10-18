using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MahamewnawaInfo.Common;
using DBCore.Classes;
using System.IO;
using MahamewnawaInfo.Reporting.Viwer;

namespace MahamewnawaInfo.Classes
{
    class ChangeListItemAsapuwaDetails : DevComponents.DotNetBar.PanelEx
    {

        Control actcontrol;
        Point preloc;
        bool allowResize;
        private Panel captionPanel;
        private PictureBox pictureBox1;
        private Button closeButton;
        private DataGridView bhikkuDataGrid;
        private Button reportBtn;
        private Label label1;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn duration;

        Label nameLabel;
        List<AsapuwaHistryCurrentBhikku> binfoList;

        public ChangeListItemAsapuwaDetails(string asapuwaName, List<AsapuwaHistryCurrentBhikku> binfoList, Control.ControlCollection Control, Point location,bool isNewList)
        {

            this.binfoList = binfoList;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Visible = true;

            // name label
            nameLabel = new Label();
            nameLabel.Text = asapuwaName;
            nameLabel.Location = new Point(5, 2);
            nameLabel.Font = new System.Drawing.Font(nameLabel.Font.FontFamily, 10, FontStyle.Bold);
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.Transparent;
            this.nameLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.nameLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.nameLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            this.nameLabel.MouseClick += new MouseEventHandler(bhikkuDataGrid_Click);

            if (this.Width < nameLabel.PreferredWidth + 30)
            {
                this.Width = nameLabel.PreferredWidth + 30;
            }

            // last
            InitializeComponent();
            this.closeButton.Location = new System.Drawing.Point(this.Size.Width - this.closeButton.Size.Width - 3, 1);
            this.pictureBox1.Location = new System.Drawing.Point(this.Size.Width - this.pictureBox1.Size.Width, this.Size.Height - this.pictureBox1.Size.Height);
            this.captionPanel.Width = this.Width;

            this.CanvasColor = System.Drawing.SystemColors.Control;
            this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Style.GradientAngle = 90;

            this.captionPanel.Controls.Add(nameLabel);
            this.captionPanel.BringToFront();
            this.closeButton.BringToFront();
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.Location = location;
            Control.Add(this);
            this.BringToFront();

            this.Controls.Add(bhikkuDataGrid);
            this.bhikkuDataGrid.Location = new Point(2, 55);
            AddBhikkuList();
            bhikkuDataGrid.BringToFront();

            if (isNewList)
            {
                label1.Text = "අලුතින් තෝරාගත් ස්වාමින් වහන්සේලා";
                bhikkuDataGrid.Columns[1].Visible = false;
            }

            reportBtn.Location = new Point(closeButton.Location.X - 25, closeButton.Location.Y);
            this.Controls.Add(reportBtn);
            reportBtn.BringToFront();

            this.Controls.Add(label1);
            label1.Location = new Point(2, 35);
        }

        private void AddBhikkuList()
        {
            int count = 0;
            foreach (AsapuwaHistryCurrentBhikku b in binfoList)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                nameCell.Value = b.BhikkuName;
                row.Cells.Add(nameCell);

                DataGridViewTextBoxCell durationCell = new DataGridViewTextBoxCell();
                durationCell.Value = b.Duration;
                row.Cells.Add(durationCell);

                row.HeaderCell.Value = (++count).ToString();
                bhikkuDataGrid.Rows.Add(row);

            }
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (actcontrol == null || actcontrol != this)
                return;
            var location = actcontrol.Location;
            location.Offset(e.Location.X - preloc.X, e.Location.Y - preloc.Y);

            if (location.X < 0 || location.Y < 0)
                return;

            actcontrol.Location = location;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            actcontrol = this as Control;
            preloc = e.Location;
            //Cursor = Cursors.;
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            actcontrol = null;
            //Cursor = Cursors.Default;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bhikkuDataGrid = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.reportBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bhikkuDataGrid
            // 
            this.bhikkuDataGrid.AllowUserToAddRows = false;
            this.bhikkuDataGrid.AllowUserToDeleteRows = false;
            this.bhikkuDataGrid.AllowUserToOrderColumns = true;
            this.bhikkuDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bhikkuDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.bhikkuDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bhikkuDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.duration});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bhikkuDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.bhikkuDataGrid.Location = new System.Drawing.Point(0, 0);
            this.bhikkuDataGrid.Name = "bhikkuDataGrid";
            this.bhikkuDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.bhikkuDataGrid.Size = new System.Drawing.Size(492, 538);
            this.bhikkuDataGrid.TabIndex = 0;
            this.bhikkuDataGrid.Click += new System.EventHandler(this.bhikkuDataGrid_Click);
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Name.HeaderText = "ස්වාමින් වහන්සේගේ නම";
            this.Name.Name = "Name";
            // 
            // duration
            // 
            this.duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.duration.HeaderText = "කාලය";
            this.duration.Name = "duration";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.delete_icon;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.closeButton.Location = new System.Drawing.Point(300, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(22, 22);
            this.closeButton.TabIndex = 0;
            this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // captionPanel
            // 
            this.captionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.captionPanel.BackColor = System.Drawing.Color.Transparent;
            this.captionPanel.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.captionImage1;
            this.captionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.captionPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.captionPanel.Location = new System.Drawing.Point(0, 0);
            this.captionPanel.Name = "captionPanel";
            this.captionPanel.Size = new System.Drawing.Size(500, 25);
            this.captionPanel.TabIndex = 0;
            this.captionPanel.Click += new System.EventHandler(this.bhikkuDataGrid_Click);
            this.captionPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.captionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.captionPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::MahamewnawaInfo.Properties.Resources.xf_resize_icon;
            this.pictureBox1.Location = new System.Drawing.Point(478, 646);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // reportBtn
            // 
            this.reportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reportBtn.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.report_icon;
            this.reportBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.reportBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.reportBtn.FlatAppearance.BorderSize = 0;
            this.reportBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.reportBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.reportBtn.Location = new System.Drawing.Point(300, 0);
            this.reportBtn.Name = "reportBtn";
            this.reportBtn.Size = new System.Drawing.Size(22, 22);
            this.reportBtn.TabIndex = 0;
            this.reportBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.reportBtn.UseVisualStyleBackColor = true;
            this.reportBtn.Click += new System.EventHandler(this.reportBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "වර්තමානයේ වැඩසිටින ස්වාමින් වහන්සේලා";
            this.label1.Click += new System.EventHandler(this.bhikkuDataGrid_Click);
            // 
            // ChangeListItemAsapuwaDetails
            // 
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.captionPanel);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size = new System.Drawing.Size(500, 600);
            this.Click += new System.EventHandler(this.bhikkuDataGrid_Click);
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            allowResize = true;
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            allowResize = false;
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (allowResize)
            {
                this.Height = pictureBox1.Top + e.Y + 5;
                this.Width = pictureBox1.Left + e.X + 5;
            }
        }

        void bringToFront_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            GenarateReport();
        }


        private void GenarateReport()
        {
            List<ChangeListReportData> data = new List<ChangeListReportData>();

            foreach (AsapuwaHistryCurrentBhikku b in binfoList)
            {
                data.Add(new ChangeListReportData(nameLabel.Text, b.BhikkuName, Utility.GetPostString(b.Post)));
            }

            // foreach(

            ChangeListReport rep = new ChangeListReport();
            rep.MdiParent = (Form)this.TopLevelControl;
            rep.AddData(data);
            rep.Show();
        }

        private void bhikkuDataGrid_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

    }
}

