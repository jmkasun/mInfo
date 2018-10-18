using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

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

        public MinimizedAsapuwa(string asapuwaName)
        {
            InitializeComponent();

            nameLabel.Text = asapuwaName;
            if (this.Width < nameLabel.PreferredWidth + 50)
            {
                this.Width = nameLabel.PreferredWidth + 50;
            }
            this.captionPanel.Controls.Add(nameLabel);
            this.captionPanel.Controls.Add(minimizeButton);

            this.minimizeButton.Location = new System.Drawing.Point(captionPanel.Width - 20, 0);

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
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.Transparent;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
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
            this.captionPanel.BackColor = System.Drawing.Color.Transparent;
            this.captionPanel.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.captionImage1;
            this.captionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.captionPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.captionPanel.Location = new System.Drawing.Point(1, 1);
            this.captionPanel.Name = "captionPanel";
            this.captionPanel.Size = new System.Drawing.Size(198, 25);
            this.captionPanel.TabIndex = 0;
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.minimize;
            this.minimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.minimizeButton.Location = new System.Drawing.Point(0, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(20, 20);
            this.minimizeButton.TabIndex = 0;
            this.minimizeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.minimizeButton.UseVisualStyleBackColor = true;
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
            this.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Style.BorderColor.Color = System.Drawing.Color.SaddleBrown;
            this.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Style.GradientAngle = 90;
            this.DragLeave += new System.EventHandler(this.MinimizedAsapuwa_DragLeave);
            this.ResumeLayout(false);

        }

        public void MinimizedAsapuwa_DragLeave(object sender, EventArgs e)
        {
            this.Style.BorderColor.Color = System.Drawing.Color.SaddleBrown;
        }

    }
}
