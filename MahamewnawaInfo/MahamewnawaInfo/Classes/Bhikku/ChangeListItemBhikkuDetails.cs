using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MahamewnawaInfo.Common;
using DBCore.Classes;
using System.IO;

namespace MahamewnawaInfo.Classes
{
    class ChangeListItemBhikkuDetails : DevComponents.DotNetBar.PanelEx
    {

        Control actcontrol;
        Point preloc;
        bool allowResize;
        private Panel captionPanel;
        private PictureBox pictureBox1;
        private Button closeButton;
        private PictureBox bhikkuImagePicBox;
        private Label numberLabel;
        private Label idLabel;
        private Label samaneranUmberLabel;
        private Label numberValueLabel;
        private Label idValueLabel;
        private Label samaneraNumberValueLabel;
        private DataGridView histryHrid;
        private Label langCaption;
        private Label langCaptionValue;
        private DataGridViewTextBoxColumn namhe;
        private DataGridViewTextBoxColumn from;
        private DataGridViewTextBoxColumn to;
        private DataGridViewTextBoxColumn post;
        private DataGridViewTextBoxColumn days;
        private DataGridView activityGrid;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

        Label nameLabel;

        public ChangeListItemBhikkuDetails(BikkuInfo binfo, Control.ControlCollection Control, Point location)
        {


            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(panel_DragEnter);

            this.Visible = false;
            this.Visible = true;

            // name label
            nameLabel = new Label();
            nameLabel.Text = binfo.NameAssumedAtRobing;
            nameLabel.Location = new Point(5, 2);
            nameLabel.Font = new System.Drawing.Font(nameLabel.Font.FontFamily, 10, FontStyle.Bold);
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.Transparent;
            this.nameLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.nameLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.nameLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);


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

            Control.Add(this);

            if (location.Y + this.Height > this.TopLevelControl.Height)
            {
                this.Location = new Point(location.X, this.TopLevelControl.Height - this.Height);
            }
            else
            {
                this.Location = location;
            }

            this.BringToFront();

            setBhikkuPictureFromByteArray(binfo.ImageData);
            this.Controls.Add(bhikkuImagePicBox);
            this.bhikkuImagePicBox.Location = new Point(this.Width - bhikkuImagePicBox.Width - 5, 28);

            numberLabel.Location = new Point(5, 40);
            numberValueLabel.Location = new Point(140, 40);
            numberValueLabel.Text = string.Concat(":     ", binfo.Number.ToString());

            langCaption.Location = new Point(5, 70);
            langCaptionValue.Location = new Point(80, 70);

            string langs = "";

            if (binfo.Hindhi)
                langs = string.Concat(langs, ", හින්දි");
            if (binfo.English)
                langs = string.Concat(langs, ", ඉංග්‍රීසි");
            if (binfo.Tamil)
                langs = string.Concat(langs, ", දෙමළ");

            foreach (UtilityData ud in binfo.OtherLanguages)
            {
                langs = string.Concat(langs, ", ", ud.Value);
            }

            if (langs.Length > 1)
            {
                langCaptionValue.Text = langs.Substring(1);
            }

            //idLabel.Location = new Point(5, 100);
            //idValueLabel.Location = new Point(140, 100);
            //idValueLabel.Text = string.Concat(":     ", binfo.NIC);

            this.Controls.Add(numberLabel);
            this.Controls.Add(numberValueLabel);
            this.Controls.Add(samaneranUmberLabel);
            this.Controls.Add(samaneraNumberValueLabel);
            this.Controls.Add(idLabel);
            this.Controls.Add(idValueLabel);
            this.Controls.Add(langCaption);
            this.Controls.Add(langCaptionValue);

            SetHistryGrid(binfo.AsapuHistry);
            this.Controls.Add(histryHrid);
            this.histryHrid.Location = new Point(2, 190);
            this.histryHrid.BringToFront();

            this.Controls.Add(activityGrid);
            this.activityGrid.Location = new Point(2, 90);
            SetActivityGrid(binfo.Activities);

        }

        private void SetActivityGrid(List<Activity> list)
        {
            foreach (Activity act in list)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                nameCell.Value = act.ActivityInfo;
                row.Cells.Add(nameCell);

                activityGrid.Rows.Add(row);

            }
        }

        private void SetHistryGrid(List<BhikkuAsapuHistry> list)
        {
            foreach (BhikkuAsapuHistry his in list)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                nameCell.Value = his.AsapuName;

                DataGridViewTextBoxCell from = new DataGridViewTextBoxCell();
                from.Value = his.FromDate.ToString("yyy-MMM-dd");

                DataGridViewTextBoxCell to = new DataGridViewTextBoxCell();
                to.Value = his.ToDate.ToString("yyy-MMM-dd");

                DataGridViewTextBoxCell post = new DataGridViewTextBoxCell();
                post.Value = Utility.GetPostString(his.Post);

                DataGridViewTextBoxCell total = new DataGridViewTextBoxCell();
                total.Value = his.DateDiff.ToString();

                row.Cells.Add(nameCell);
                row.Cells.Add(from);
                row.Cells.Add(to);
                row.Cells.Add(post);
                row.Cells.Add(total);

                histryHrid.Rows.Add(row);
            }
        }


        private void setBhikkuPictureFromByteArray(string imageData)
        {
            try
            {

                if (string.IsNullOrEmpty(imageData))
                {
                    bhikkuImagePicBox.Image = null;
                }
                else
                {

                    // set byte array
                    MemoryStream mem = new MemoryStream(Utility.GetByte64String(imageData));

                    Image img = Image.FromStream(mem);
                    bhikkuImagePicBox.Image = Utility.getThumbImage(img, bhikkuImagePicBox.Width, bhikkuImagePicBox.Height);
                }
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            // MessageBox.Show(((Label)sender).Text);
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
            this.numberLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.samaneranUmberLabel = new System.Windows.Forms.Label();
            this.numberValueLabel = new System.Windows.Forms.Label();
            this.idValueLabel = new System.Windows.Forms.Label();
            this.samaneraNumberValueLabel = new System.Windows.Forms.Label();
            this.histryHrid = new System.Windows.Forms.DataGridView();
            this.namhe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.days = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bhikkuImagePicBox = new System.Windows.Forms.PictureBox();
            this.langCaption = new System.Windows.Forms.Label();
            this.langCaptionValue = new System.Windows.Forms.Label();
            this.activityGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.histryHrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuImagePicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberLabel.Location = new System.Drawing.Point(14, 11);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(233, 20);
            this.numberLabel.TabIndex = 90;
            this.numberLabel.Text = "ස්වාමින් වහන්සේගේ අංකය";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(14, 70);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(120, 20);
            this.idLabel.TabIndex = 67;
            this.idLabel.Text = "හැදුනුම්පත් අංකය";
            this.idLabel.Visible = false;
            // 
            // samaneranUmberLabel
            // 
            this.samaneranUmberLabel.AutoSize = true;
            this.samaneranUmberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samaneranUmberLabel.Location = new System.Drawing.Point(14, 220);
            this.samaneranUmberLabel.Name = "samaneranUmberLabel";
            this.samaneranUmberLabel.Size = new System.Drawing.Size(182, 20);
            this.samaneranUmberLabel.TabIndex = 35;
            this.samaneranUmberLabel.Text = "සාමනේර සහතිකයේ අංකය\r\n";
            this.samaneranUmberLabel.Visible = false;
            // 
            // numberValueLabel
            // 
            this.numberValueLabel.AutoSize = true;
            this.numberValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberValueLabel.Location = new System.Drawing.Point(14, 11);
            this.numberValueLabel.Name = "numberValueLabel";
            this.numberValueLabel.Size = new System.Drawing.Size(233, 20);
            this.numberValueLabel.TabIndex = 90;
            this.numberValueLabel.Tag = "";
            this.numberValueLabel.Text = "nu";
            // 
            // idValueLabel
            // 
            this.idValueLabel.AutoSize = true;
            this.idValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idValueLabel.Location = new System.Drawing.Point(14, 70);
            this.idValueLabel.Name = "idValueLabel";
            this.idValueLabel.Size = new System.Drawing.Size(120, 20);
            this.idValueLabel.TabIndex = 67;
            this.idValueLabel.Text = "id";
            this.idValueLabel.Visible = false;
            // 
            // samaneraNumberValueLabel
            // 
            this.samaneraNumberValueLabel.AutoSize = true;
            this.samaneraNumberValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samaneraNumberValueLabel.Location = new System.Drawing.Point(14, 220);
            this.samaneraNumberValueLabel.Name = "samaneraNumberValueLabel";
            this.samaneraNumberValueLabel.Size = new System.Drawing.Size(182, 20);
            this.samaneraNumberValueLabel.TabIndex = 35;
            this.samaneraNumberValueLabel.Text = "sa";
            // 
            // histryHrid
            // 
            this.histryHrid.AllowUserToDeleteRows = false;
            this.histryHrid.AllowUserToOrderColumns = true;
            this.histryHrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.histryHrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.histryHrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.histryHrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.namhe,
            this.from,
            this.to,
            this.post,
            this.days});
            this.histryHrid.Location = new System.Drawing.Point(0, 0);
            this.histryHrid.Name = "histryHrid";
            this.histryHrid.RowHeadersVisible = false;
            this.histryHrid.Size = new System.Drawing.Size(545, 205);
            this.histryHrid.TabIndex = 0;
            // 
            // namhe
            // 
            this.namhe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.namhe.HeaderText = "අසපුවේ නම";
            this.namhe.Name = "namhe";
            // 
            // from
            // 
            this.from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.from.HeaderText = "සිට";
            this.from.Name = "from";
            // 
            // to
            // 
            this.to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.to.HeaderText = "දක්වා";
            this.to.Name = "to";
            // 
            // post
            // 
            this.post.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.post.HeaderText = "තනතුර";
            this.post.Name = "post";
            this.post.Width = 50;
            // 
            // days
            // 
            this.days.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.days.HeaderText = "දින";
            this.days.Name = "days";
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
            this.closeButton.Location = new System.Drawing.Point(350, 0);
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
            this.captionPanel.Size = new System.Drawing.Size(550, 25);
            this.captionPanel.TabIndex = 0;
            this.captionPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.captionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.captionPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::MahamewnawaInfo.Properties.Resources.xf_resize_icon;
            this.pictureBox1.Location = new System.Drawing.Point(528, 446);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // bhikkuImagePicBox
            // 
            this.bhikkuImagePicBox.AccessibleName = "";
            this.bhikkuImagePicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bhikkuImagePicBox.BackColor = System.Drawing.Color.Transparent;
            this.bhikkuImagePicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bhikkuImagePicBox.Location = new System.Drawing.Point(822, 9);
            this.bhikkuImagePicBox.MaximumSize = new System.Drawing.Size(250, 250);
            this.bhikkuImagePicBox.Name = "bhikkuImagePicBox";
            this.bhikkuImagePicBox.Size = new System.Drawing.Size(150, 150);
            this.bhikkuImagePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.bhikkuImagePicBox.TabIndex = 36;
            this.bhikkuImagePicBox.TabStop = false;
            this.bhikkuImagePicBox.Tag = "";
            // 
            // langCaption
            // 
            this.langCaption.AutoSize = true;
            this.langCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.langCaption.Location = new System.Drawing.Point(14, 11);
            this.langCaption.Name = "langCaption";
            this.langCaption.Size = new System.Drawing.Size(233, 20);
            this.langCaption.TabIndex = 90;
            this.langCaption.Tag = "";
            this.langCaption.Text = "වෙනත් භාෂා :";
            // 
            // langCaptionValue
            // 
            this.langCaptionValue.AutoSize = true;
            this.langCaptionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.langCaptionValue.Location = new System.Drawing.Point(14, 11);
            this.langCaptionValue.Name = "langCaptionValue";
            this.langCaptionValue.Size = new System.Drawing.Size(233, 20);
            this.langCaptionValue.TabIndex = 90;
            this.langCaptionValue.Tag = "";
            // 
            // activityGrid
            // 
            this.activityGrid.AllowUserToDeleteRows = false;
            this.activityGrid.AllowUserToOrderColumns = true;
            this.activityGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.activityGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.activityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activityGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5});
            this.activityGrid.Location = new System.Drawing.Point(0, 0);
            this.activityGrid.Name = "activityGrid";
            this.activityGrid.RowHeadersVisible = false;
            this.activityGrid.Size = new System.Drawing.Size(390, 95);
            this.activityGrid.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "විශේෂ කරුණු";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // ChangeListItemBhikkuDetails
            // 
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.captionPanel);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size = new System.Drawing.Size(550, 400);
            ((System.ComponentModel.ISupportInitialize)(this.histryHrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bhikkuImagePicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            // this.ChangelistBhikku.Select();
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

        

    }
}

