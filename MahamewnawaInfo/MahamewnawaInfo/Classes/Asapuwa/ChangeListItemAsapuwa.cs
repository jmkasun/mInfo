using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MahamewnawaInfo.Common;
using DBCore;
using DBCore.Classes;
using MahamewnawaInfo.Forms;

namespace MahamewnawaInfo.Classes
{
    class ChangeListItemAsapuwa : DevComponents.DotNetBar.PanelEx
    {
        public int asapuwaID = 0;
        public string asapuwaName = string.Empty;
        public string asapuwaShortName = "";

        public ChangeListToolstriptItem RClickItem;
        bool allowResize;

        Control actcontrol;
        Point preloc;
        private Button closeButton;


        Label nameLabel;
        public List<ChangeListItemBhikku> bhikkuList;

        private PictureBox pictureBox1;
        DevComponents.DotNetBar.PanelEx namelIstPanel;

        Label sangaUpasthayakaCount;
        Label anuSangaUpasthayakaCount;
        Label upasampadaCount;
        Label samaneraCount;

        private Panel captionPanel;
        private Panel statusPanel;
        private Button minimizeButton;

        public MinimizedAsapuwa minimizedAsapuwa = null;
        private UpdateChangeItemFinalizeAsapuwa UpdateChangeItemFinalizeAsapuwa;

        public bool isFinalize;

        public ChangeListItemAsapuwa()
        {
        }

        public ChangeListItemAsapuwa(int asapuwaID, string asapuwaName, string asapuwaShortName, Control.ControlCollection contralls, bool visible, int maxNameLength, UpdateChangeItemFinalizeAsapuwa updateChangeItemFinalizeAsapuwa)
        {
            UpdateChangeItemFinalizeAsapuwa = updateChangeItemFinalizeAsapuwa;
            this.asapuwaID = asapuwaID;
            this.asapuwaName = asapuwaName;
            this.asapuwaShortName = asapuwaShortName;

            this.Size = new Size(maxNameLength, 250);


            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(panel_DragEnter);
            this.DragDrop += new DragEventHandler(panel_DragDrop);

            this.Visible = false;

            if (contralls != null)
            {
                contralls.Add(this);
            }

            //SetLocation();
            this.Visible = visible;

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
            nameLabel.MouseClick += new MouseEventHandler(nameLabel_MouseClick);

            if (this.Width < nameLabel.PreferredWidth + 50)
            {
                this.Width = nameLabel.PreferredWidth + 50;
            }

            bhikkuList = new List<ChangeListItemBhikku>();

            // list panel
            namelIstPanel = new DevComponents.DotNetBar.PanelEx();

            namelIstPanel.Location = new Point(10, 15);
            namelIstPanel.AutoScroll = true;
            namelIstPanel.Size = new Size(this.Size.Width - 8, this.Size.Height - 10 - namelIstPanel.Location.Y - 10);
            namelIstPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));

            namelIstPanel.MouseEnter += new EventHandler(namelIstPanel_MouseEnter);
            namelIstPanel.MouseClick += new MouseEventHandler(nameLabel_MouseClick);

            this.Controls.Add(namelIstPanel);



            // Rclick
            MenuItem bringToFront = new MenuItem("Bring To Front");
            bringToFront.Click += new EventHandler(bringToFront_Click);

            MenuItem currentBhikkuDetails = new MenuItem("වර්තමානයේ වැඩසිටින ස්වාමින් වහන්සේලා");
            currentBhikkuDetails.Click += new EventHandler(currentBhikkuDetails_Click);

            

            MenuItem closePanel = new MenuItem("Close");
            closePanel.Click += new EventHandler(closeMenue_Click);

            MenuItem newBhikkuDetails = new MenuItem("අලුතින් තෝරාගත් ස්වාමින් වහන්සේලා");
            newBhikkuDetails.Click += new EventHandler(newBhikkuDetails_Click);

            MenuItem finalizeDetails = new MenuItem("ස්වාමින් වහන්සේලා තෝරාගෙන අවසන්");
            finalizeDetails.Click += new EventHandler(finalize_Click);

            this.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[] { bringToFront, currentBhikkuDetails,newBhikkuDetails,finalizeDetails, closePanel, });

            // last
            InitializeComponent();
            this.closeButton.Location = new Point(this.Size.Width - this.closeButton.Size.Width - 3, 1);
            this.minimizeButton.Location = new Point(this.Size.Width - this.closeButton.Size.Width - this.minimizeButton.Width - 3, 1);

            this.pictureBox1.Location = new System.Drawing.Point(statusPanel.Size.Width - this.pictureBox1.Size.Width, statusPanel.Size.Height - this.pictureBox1.Size.Height);
            this.captionPanel.Width = this.Width;
            this.captionPanel.MouseClick += new MouseEventHandler(nameLabel_MouseClick);

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
            this.minimizeButton.BringToFront();
            this.pictureBox1.BringToFront();

            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;

            SetMinimizesItem();
            creatreBhikkuCountLabels();
        }


        private void SetMinimizesItem()
        {
            minimizedAsapuwa = new MinimizedAsapuwa(asapuwaName);

            minimizedAsapuwa.DragEnter += new DragEventHandler(panel_DragEnter);
            minimizedAsapuwa.DragDrop += new DragEventHandler(panel_DragDrop);
            minimizedAsapuwa.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);

            minimizedAsapuwa.Width = this.Width;
            minimizedAsapuwa.Height = 40;


        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.Location = minimizedAsapuwa.Location;
            minimizedAsapuwa.Parent.Controls.Add(this);
            this.BringToFront();
            this.Visible = true;

        }

        void nameLabel_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }


        void namelIstPanel_MouseEnter(object sender, EventArgs e)
        {
            namelIstPanel.Focus();
        }

        public void SetLocation()
        {
            SplitContainer spt = (SplitContainer)this.Parent.Parent.Parent;
            this.Location = new Point(MousePosition.X - this.Parent.Location.X - this.Parent.Parent.Parent.Parent.Location.X - spt.SplitterDistance - 10, MousePosition.Y - this.Parent.Location.Y - this.Parent.Parent.Parent.Parent.Location.Y - 80);
            // this.Location = new Point(MousePosition.X, MousePosition.Y );
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            minimizedAsapuwa.Style.BorderColor.Color = Color.Khaki;

            // MessageBox.Show(((Label)sender).Text);
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

            ChangeListItemBhikku lbl = (ChangeListItemBhikku)e.Data.GetData(typeof(ChangeListItemBhikku));

            if (lbl.ParentLbl == null)
            {
                AddBhikku(lbl);
            }
            else
            {
                lbl.ParentLbl.Reset();
                AddBhikku(lbl.ParentLbl);
            }

            this.BringToFront();
            this.minimizedAsapuwa.MinimizedAsapuwa_DragLeave(sender, e);
        }

        public void AddBhikku(ChangeListItemBhikku lbl)
        {

            AddBhikkuList(lbl);
            PrepareNamelist();
            lbl.DoDrag(this);

        }


        public void PrepareNamelist()
        {
            namelIstPanel.Controls.Clear();
            bhikkuList.Sort(new ChangeListItemBhikku());

            List<ChangeListItemBhikku> samaneraBhikkuList = new List<ChangeListItemBhikku>();

            foreach (ChangeListItemBhikku bhikku in bhikkuList)
            {
                if (bhikku.CloneLabel == null)
                {
                    bhikku.SetClone();
                }

                if (bhikku.bInfo.IsUpasampanna)
                {
                    bhikku.CloneLabel.Location = getNamepanelLocation(namelIstPanel.Controls.Count, bhikku.CloneLabel.Height);
                    namelIstPanel.Controls.Add(bhikku.CloneLabel);
                }
                else
                {
                    samaneraBhikkuList.Add(bhikku.CloneLabel);
                }
            }

            foreach (ChangeListItemBhikku bhikku in samaneraBhikkuList)
            {
                bhikku.Location = getNamepanelLocation(namelIstPanel.Controls.Count, bhikku.Height);
                namelIstPanel.Controls.Add(bhikku);
            }
        }

        private Point getNamepanelLocation(int count, int height)
        {
            return new System.Drawing.Point(5, (count * height) + (count * 2) + 20);
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
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();
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
            this.closeButton.Location = new System.Drawing.Point(0, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 0;
            this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // minimizeButton
            // 
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
            this.minimizeButton.Click += new System.EventHandler(this.minizeButton_Click);
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
            this.captionPanel.Size = new System.Drawing.Size(200, 25);
            this.captionPanel.TabIndex = 0;
            this.captionPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            this.captionPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.captionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.captionPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::MahamewnawaInfo.Properties.Resources.xf_resize_icon;
            this.pictureBox1.Location = new System.Drawing.Point(178, 146);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // statusPanel
            // 
            this.statusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusPanel.BackColor = System.Drawing.Color.Transparent;
            this.statusPanel.BackgroundImage = global::MahamewnawaInfo.Properties.Resources.StatusBarImage1;
            this.statusPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusPanel.Controls.Add(this.pictureBox1);
            this.statusPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.statusPanel.Location = new System.Drawing.Point(0, 0);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(200, 15);
            this.statusPanel.TabIndex = 0;
            this.statusPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            this.statusPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.statusPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.statusPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // ChangeListItemAsapuwa
            // 
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.captionPanel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.ResumeLayout(false);

        }


        private void closeMenue_Click(object sender, EventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Close Window") == DialogResult.OK)
            {
                ResetBhikkuList();
                this.RClickItem.Reset();
                this.Hide();
            }

        }


        private void closeButton_Click(object sender, EventArgs e)
        {
            if (this.RClickItem != null)
            {
                this.RClickItem.Reset();
            }


            this.Hide();
        }

        private void ResetBhikkuList()
        {
            while (bhikkuList.Count > 0)
            {
                bhikkuList[0].Reset();
            }
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
                this.Height = pictureBox1.Top + pictureBox1.Parent.Top + e.Y + 5;
                this.Width = pictureBox1.Left + pictureBox1.Parent.Left + e.X + 5;
            }
        }

        void bringToFront_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }




        public void currentBhikkuDetails_Click(object sender, EventArgs e)
        {
            using (Asapuwa a = new Asapuwa(true))
            {
                a.ID = this.asapuwaID;
                ChangeListItemAsapuwaDetails cad = new ChangeListItemAsapuwaDetails(this.asapuwaName, a.SelectCurrentBhikkuList(), this.Parent.Controls, new Point(100, 100),false);
            }
        }


        public void newBhikkuDetails_Click(object sender, EventArgs e)
        {
            List<AsapuwaHistryCurrentBhikku> binfoList = new List<AsapuwaHistryCurrentBhikku>();

            foreach (ChangeListItemBhikku b in bhikkuList)
            {
                binfoList.Add(new AsapuwaHistryCurrentBhikku(b.bInfo.NameAssumedAtRobing, "", b.bInfo.Post));
            }

            Control.ControlCollection cnt = this.Parent.Controls;

            if (this.minimizedAsapuwa.Parent == this.Parent)
            {
                cnt = this.Parent.Parent.Parent.Controls;
            }

            ChangeListItemAsapuwaDetails cad = new ChangeListItemAsapuwaDetails(this.asapuwaName, binfoList, cnt, new Point(100, 100),true);
        }


        public void finalize_Click(object sender, EventArgs e)
        {

            isFinalize = !isFinalize;
            SetFinalizedsettings();
            UpdateChangeItemFinalizeAsapuwa(asapuwaID, isFinalize);
        }


        public void SetFinalizedsettings()
        {
            if (isFinalize)
            {
                this.minimizedAsapuwa.AllowDrop = this.AllowDrop = false;
                this.minimizedAsapuwa.nameLabel.ForeColor = nameLabel.ForeColor = Color.Red;
                this.minimizedAsapuwa.nameLabel.BackColor = nameLabel.BackColor = Color.Yellow;

                
            }
            else
            {
                this.minimizedAsapuwa.AllowDrop = this.AllowDrop = true;
                this.minimizedAsapuwa.nameLabel.ForeColor = nameLabel.ForeColor = Color.Black;
                this.minimizedAsapuwa.nameLabel.BackColor = nameLabel.BackColor = Color.Transparent;
            }
        }



        private void creatreBhikkuCountLabels()
        {

            this.statusPanel.Location = new Point(0, this.Height - 18);
            int LabelLocationy = 2;
            this.Controls.Add(statusPanel);
            statusPanel.Width = captionPanel.Width;

            // sanga upasthayaka
            sangaUpasthayakaCount = new Label();
            minimizedAsapuwa.sangaUpasthayakaCount.BackColor = sangaUpasthayakaCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.SangaUpasthayaka);
            minimizedAsapuwa.sangaUpasthayakaCount.Text = sangaUpasthayakaCount.Text = "0";
            minimizedAsapuwa.sangaUpasthayakaCount.AutoSize = sangaUpasthayakaCount.AutoSize = true;
            sangaUpasthayakaCount.Location = new Point(3, LabelLocationy);
            //
            minimizedAsapuwa.sangaUpasthayakaCount.Location = new Point(3, 26);
            //
            minimizedAsapuwa.sangaUpasthayakaCount.Anchor = sangaUpasthayakaCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(sangaUpasthayakaCount);

            anuSangaUpasthayakaCount = new Label();
            minimizedAsapuwa.anuSangaUpasthayakaCount.BackColor = anuSangaUpasthayakaCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.AnusangaUpasthayaka);
            minimizedAsapuwa.anuSangaUpasthayakaCount.Text = anuSangaUpasthayakaCount.Text = "0";
            minimizedAsapuwa.anuSangaUpasthayakaCount.AutoSize = anuSangaUpasthayakaCount.AutoSize = true;
            anuSangaUpasthayakaCount.Location = new Point(sangaUpasthayakaCount.Location.X + sangaUpasthayakaCount.Width + 15, LabelLocationy);
            //
            minimizedAsapuwa.anuSangaUpasthayakaCount.Location = new Point(sangaUpasthayakaCount.Location.X + sangaUpasthayakaCount.Width + 15, 26);
            //
            minimizedAsapuwa.anuSangaUpasthayakaCount.Anchor = anuSangaUpasthayakaCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(anuSangaUpasthayakaCount);

            upasampadaCount = new Label();
            minimizedAsapuwa.upasampadaCount.BackColor = upasampadaCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.Upasampada);
            minimizedAsapuwa.upasampadaCount.Text = upasampadaCount.Text = "0";
            minimizedAsapuwa.upasampadaCount.AutoSize = upasampadaCount.AutoSize = true;
            upasampadaCount.Location = new Point(anuSangaUpasthayakaCount.Location.X + anuSangaUpasthayakaCount.Width + 15, LabelLocationy);
            //
            minimizedAsapuwa.upasampadaCount.Location = new Point(anuSangaUpasthayakaCount.Location.X + anuSangaUpasthayakaCount.Width + 15, 26);
            //
            minimizedAsapuwa.upasampadaCount.Anchor = upasampadaCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(upasampadaCount);

            samaneraCount = new Label();
            minimizedAsapuwa.samaneraCount.BackColor = samaneraCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.Samanera);
            minimizedAsapuwa.samaneraCount.Text = samaneraCount.Text = "0";
            minimizedAsapuwa.samaneraCount.AutoSize = samaneraCount.AutoSize = true;
            samaneraCount.Location = new Point(upasampadaCount.Location.X + upasampadaCount.Width + 15, LabelLocationy);
            //
            minimizedAsapuwa.samaneraCount.Location = new Point(upasampadaCount.Location.X + upasampadaCount.Width + 15, 26);
            //
            minimizedAsapuwa.samaneraCount.Anchor = samaneraCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(samaneraCount);
        }

        private void AddBhikkuList(ChangeListItemBhikku bhikku)
        {
            bhikkuList.Add(bhikku);
            updateBhikkuCountLabel(1, bhikku.bInfo.BhikkuType);
        }

        public void RemoveBhikkuList(ChangeListItemBhikku bhikku)
        {
            bhikkuList.Remove(bhikku);
            updateBhikkuCountLabel(-1, bhikku.bInfo.BhikkuType);
        }

        public void RecalculateCounts()
        {
            sangaUpasthayakaCount.Text = anuSangaUpasthayakaCount.Text = upasampadaCount.Text = samaneraCount.Text = "0";

            foreach (ChangeListItemBhikku b in bhikkuList)
            {
                updateBhikkuCountLabel(1, b.bInfo.BhikkuType);
            }
        }

        private void updateBhikkuCountLabel(int increment, BhikkuType type)
        {
            switch (type)
            {
                case BhikkuType.SangaUpasthayaka:
                    {
                        int currentCount = Int32.Parse(sangaUpasthayakaCount.Text);
                        minimizedAsapuwa.sangaUpasthayakaCount.Text = sangaUpasthayakaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.AnusangaUpasthayaka:
                    {
                        int currentCount = Int32.Parse(anuSangaUpasthayakaCount.Text);
                        minimizedAsapuwa.anuSangaUpasthayakaCount.Text = anuSangaUpasthayakaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.Upasampada:
                    {
                        int currentCount = Int32.Parse(upasampadaCount.Text);
                        minimizedAsapuwa.upasampadaCount.Text = upasampadaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.Samanera:
                    {
                        int currentCount = Int32.Parse(samaneraCount.Text);
                        minimizedAsapuwa.samaneraCount.Text = samaneraCount.Text = (currentCount + increment).ToString();
                        return;
                    }
            }
        }

        private void minizeButton_Click(object sender, EventArgs e)
        {
            if (this.Parent == this.minimizedAsapuwa.Parent)
            {
                this.Hide();
            }
            else
            {
                if (this.Height > 41)
                {
                    this.Height = 41;
                }
                else
                {
                    this.Height = 250;
                }
            }
        }


    }
}
