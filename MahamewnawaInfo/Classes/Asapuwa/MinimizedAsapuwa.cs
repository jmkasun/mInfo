using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar;

namespace MahamewnawaInfo.Classes
{
    public class MinimizedAsapuwa : DevComponents.DotNetBar.PanelEx
    {
        public System.Windows.Forms.Panel captionPanel;
        public Label nameLabel;

        public Label sangaUpasthayakaCount;
        public Label anuSangaUpasthayakaCount;
        public Label upasampadaCount;
        public Button minimizeButton;
        public Label samaneraCount;
        public string labelText;
        public Label allCount;
        public Label numberOfKutiLbl;
        public ChangeListItemAsapuwa maximizedAsapuwa;
        private ButtonX printIcon;
        private ButtonItem printPreview;
        private ButtonItem printWithImage;
        private ButtonItem printImagePreview;
        private LabelItem labelItem1;

        public int originalWidth;

        public MinimizedAsapuwa(string asapuwaName, Color statusColor, Color captionColor, int numberOfKuti)
        {
            InitializeComponent();
            labelText = asapuwaName;
            numberOfKutiLbl.Text = numberOfKuti.ToString();
            nameLabel.Text = asapuwaName;

            this.captionPanel.Controls.Add(minimizeButton);
            this.captionPanel.Controls.Add(printIcon);
            this.captionPanel.Controls.Add(nameLabel);


            this.minimizeButton.Location = new System.Drawing.Point(captionPanel.Width - 20, 0);
            this.printIcon.Location = new Point(this.Size.Width - (this.minimizeButton.Size.Width + this.printIcon.Size.Width) - 5, 1);

            minimizeButton.BackColor = captionPanel.BackColor = captionColor;
            this.Style.BackColor1.Color = statusColor;
            this.Style.BackColor2.Color = statusColor;

            

        }

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.sangaUpasthayakaCount = new System.Windows.Forms.Label();
            this.anuSangaUpasthayakaCount = new System.Windows.Forms.Label();
            this.upasampadaCount = new System.Windows.Forms.Label();
            this.samaneraCount = new System.Windows.Forms.Label();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.allCount = new System.Windows.Forms.Label();
            this.numberOfKutiLbl = new System.Windows.Forms.Label();
            this.printIcon = new DevComponents.DotNetBar.ButtonX();
            this.printPreview = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.printWithImage = new DevComponents.DotNetBar.ButtonItem();
            this.printImagePreview = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.Transparent;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(3, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(100, 23);
            this.nameLabel.TabIndex = 0;
            // 
            // sangaUpasthayakaCount
            // 
            this.sangaUpasthayakaCount.Location = new System.Drawing.Point(0, 0);
            this.sangaUpasthayakaCount.Name = "sangaUpasthayakaCount";
            this.sangaUpasthayakaCount.Size = new System.Drawing.Size(100, 23);
            this.sangaUpasthayakaCount.TabIndex = 0;
            // 
            // anuSangaUpasthayakaCount
            // 
            this.anuSangaUpasthayakaCount.Location = new System.Drawing.Point(0, 0);
            this.anuSangaUpasthayakaCount.Name = "anuSangaUpasthayakaCount";
            this.anuSangaUpasthayakaCount.Size = new System.Drawing.Size(100, 23);
            this.anuSangaUpasthayakaCount.TabIndex = 1;
            // 
            // upasampadaCount
            // 
            this.upasampadaCount.Location = new System.Drawing.Point(0, 0);
            this.upasampadaCount.Name = "upasampadaCount";
            this.upasampadaCount.Size = new System.Drawing.Size(100, 23);
            this.upasampadaCount.TabIndex = 2;
            // 
            // samaneraCount
            // 
            this.samaneraCount.Location = new System.Drawing.Point(0, 0);
            this.samaneraCount.Name = "samaneraCount";
            this.samaneraCount.Size = new System.Drawing.Size(100, 23);
            this.samaneraCount.TabIndex = 3;
            // 
            // captionPanel
            // 
            this.captionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.captionPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.captionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.captionPanel.Location = new System.Drawing.Point(1, 1);
            this.captionPanel.Name = "captionPanel";
            this.captionPanel.Size = new System.Drawing.Size(298, 25);
            this.captionPanel.TabIndex = 0;
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.LightBlue;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Perpetua Titling MT", 12F);
            this.minimizeButton.ForeColor = System.Drawing.Color.Black;
            this.minimizeButton.Location = new System.Drawing.Point(0, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(20, 20);
            this.minimizeButton.TabIndex = 0;
            this.minimizeButton.Text = "-";
            this.minimizeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.minimizeButton.UseVisualStyleBackColor = false;
            // 
            // allCount
            // 
            this.allCount.BackColor = System.Drawing.Color.LightYellow;
            this.allCount.Location = new System.Drawing.Point(0, 0);
            this.allCount.Name = "allCount";
            this.allCount.Size = new System.Drawing.Size(100, 23);
            this.allCount.TabIndex = 2;
            // 
            // numberOfKutiLbl
            // 
            this.numberOfKutiLbl.AutoSize = true;
            this.numberOfKutiLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfKutiLbl.ForeColor = System.Drawing.Color.Maroon;
            this.numberOfKutiLbl.Location = new System.Drawing.Point(0, 0);
            this.numberOfKutiLbl.Name = "numberOfKutiLbl";
            this.numberOfKutiLbl.Size = new System.Drawing.Size(0, 13);
            this.numberOfKutiLbl.TabIndex = 3;
            // 
            // printIcon
            // 
            this.printIcon.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.printIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printIcon.BackColor = System.Drawing.Color.Maroon;
            this.printIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.printIcon.Image = global::MahamewnawaInfo.Properties.Resources.print1;
            this.printIcon.Location = new System.Drawing.Point(0, 0);
            this.printIcon.Name = "printIcon";
            this.printIcon.Size = new System.Drawing.Size(45, 20);
            this.printIcon.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.printIcon.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.printPreview,
            this.labelItem1,
            this.printWithImage,
            this.printImagePreview});
            this.printIcon.TabIndex = 0;
            this.printIcon.Click += new System.EventHandler(this.printIcon_Click_1);
            // 
            // printPreview
            // 
            this.printPreview.Name = "printPreview";
            this.printPreview.Text = "No Image";
            this.printPreview.Click += new System.EventHandler(this.printPreview_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "- - - - - - - - - - - - - - - - - - -   ";
            // 
            // printWithImage
            // 
            this.printWithImage.Enabled = false;
            this.printWithImage.Name = "printWithImage";
            this.printWithImage.Text = "Print image";
            this.printWithImage.Click += new System.EventHandler(this.printWithImage_Click);
            // 
            // printImagePreview
            // 
            this.printImagePreview.Enabled = false;
            this.printImagePreview.Name = "printImagePreview";
            this.printImagePreview.Text = "Print image preview";
            this.printImagePreview.Click += new System.EventHandler(this.printImagePreview_Click);
            // 
            // MinimizedAsapuwa
            // 
            this.AllowDrop = true;
            this.CanvasColor = System.Drawing.SystemColors.Control;
            this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Controls.Add(this.sangaUpasthayakaCount);
            this.Controls.Add(this.anuSangaUpasthayakaCount);
            this.Controls.Add(this.upasampadaCount);
            this.Controls.Add(this.samaneraCount);
            this.Controls.Add(this.captionPanel);
            this.Controls.Add(this.numberOfKutiLbl);
            this.Controls.Add(this.allCount);
            this.Size = new System.Drawing.Size(300, 100);
            this.Style.BackColor1.Color = System.Drawing.Color.SkyBlue;
            this.Style.BackColor2.Color = System.Drawing.Color.SkyBlue;
            this.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Style.BorderColor.Color = System.Drawing.Color.SaddleBrown;
            this.Style.CornerDiameter = 2;
            this.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Style.GradientAngle = 90;
            this.Text = "-";
            this.DragLeave += new System.EventHandler(this.MinimizedAsapuwa_DragLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void printImagePreview_Click(object sender, EventArgs e)
        {
            maximizedAsapuwa.printWithImagePreview_Click(sender, e);
        }

        void printWithImage_Click(object sender, EventArgs e)
        {
            maximizedAsapuwa.printWithImage_Click(sender, e);
        }

        void printPreview_Click(object sender, EventArgs e)
        {
            maximizedAsapuwa.printPreview_Click(sender, e);
        }

        public void MinimizedAsapuwa_DragLeave(object sender, EventArgs e)
        {
            this.Style.BorderColor.Color = System.Drawing.Color.SaddleBrown;
        }

           

        private void printIcon_Click_1(object sender, EventArgs e)
        {
            this.maximizedAsapuwa.printIcon_Click(sender, e);
        }

    }
}
