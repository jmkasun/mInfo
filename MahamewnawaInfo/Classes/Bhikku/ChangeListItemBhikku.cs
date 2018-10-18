using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using MahamewnawaInfo.Common;
using DBCore.Classes;
using System.Threading;
using MahamewnawaInfo.Forms;
using DevComponents.DotNetBar;

namespace MahamewnawaInfo.Classes
{
    [Serializable]
    public class ChangeListItemBhikku : Panel, IComparer<ChangeListItemBhikku>
    {
        public bool isDraged = false;

        public BikkuInfo bInfo;

        internal ChangeListItemAsapuwa Asapuwa;
        Label asapuwaLabel;

        public ChangeListItemBhikku CloneLabel;
        public ChangeListItemBhikku ParentLbl;

        public Button HeadButton;
        public Button BodyButton;
        public Button RearButton;

        public AddDeleteChangeItem AddChangeItemTable;
        public PictureBox imagePicbox;
        private Panel asapuwaNamepanel;

        public int ChanageListID; // ID of changelist table

        public ChangeListItemBhikku(string text, Panel panel, BikkuInfo bInfo, int width, AddDeleteChangeItem addChangeItemTable)
        {
            try
            {
                this.bInfo = bInfo;

                AddChangeItemTable = addChangeItemTable;

                this.Size = new System.Drawing.Size(width + 80, 68);
                this.Location = new System.Drawing.Point(5, (panel.Controls.Count * this.Height) + (panel.Controls.Count * 2) + 20);
                this.Font = new System.Drawing.Font(this.Font.FontFamily, 13, FontStyle.Bold);
                //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                this.ContextMenu = CreateContextMenuItems();



                InitializeComponent();

                this.BodyButton.MouseEnter += new EventHandler(ChangeListLabel_MouseEnter);
                this.BodyButton.MouseLeave += new EventHandler(ChangeListLabel_MouseLeave);
                this.BodyButton.MouseDown += new MouseEventHandler(label_MouseDown);

                this.HeadButton.MouseEnter += new EventHandler(ChangeListLabel_MouseEnter);
                this.HeadButton.MouseLeave += new EventHandler(ChangeListLabel_MouseLeave);
                this.HeadButton.MouseDown += new MouseEventHandler(label_MouseDown);

                this.RearButton.MouseEnter += new EventHandler(ChangeListLabel_MouseEnter);
                this.RearButton.MouseLeave += new EventHandler(ChangeListLabel_MouseLeave);
                this.RearButton.MouseDown += new MouseEventHandler(label_MouseDown);

                if (string.IsNullOrEmpty(bInfo.ImageData))
                {
                    imagePicbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                else
                {
                    imagePicbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    Utility.setBhikkuPictureFromByteArray(DBCore.Utility.GetByteFrom64String(bInfo.ImageData), imagePicbox);
                }


                setOriginalImage(bInfo.BhikkuType, false, isDraged);

                this.Controls.Add(imagePicbox);
                this.Controls.Add(BodyButton);
                this.Controls.Add(HeadButton);
                this.Controls.Add(RearButton);


                HeadButton.Location = new Point(0, 20);
                imagePicbox.Location = new Point(30, 0);
                BodyButton.Location = new Point(98, 20);

                BodyButton.Size = new System.Drawing.Size(this.Width - HeadButton.Width - RearButton.Width - imagePicbox.Width + 3, 30);

                RearButton.Location = new Point(98 + BodyButton.Width, 20);
                RearButton.BringToFront();

                BodyButton.Text = text;

                AddObjectTopanel(panel, this);

            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message);
            }
        }



        private void AddObjectTopanel(Panel panel, ChangeListItemBhikku bhikku)
        {
            if (panel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddObjectTopanel);
                this.Invoke(d, new object[] { panel.Controls, bhikku });
            }
            else
            {
                panel.Controls.Add(this);
            }
        }

        delegate void SetTextCallback(PanelEx panel, ChangeListItemBhikku bhikku);    


        void ChangeListItemBhikku_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadBhikkuDetails();
        }

        private void LoadBhikkuDetails()
        {
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bInfo.SelectBhikkuChangeList(this.bInfo.ID);

                ChangeListItemBhikkuDetails bDetails = new ChangeListItemBhikkuDetails(bInfo, this.Parent.Parent.Parent.Parent.Parent.Parent.Controls, new Point(MousePosition.X, MousePosition.Y - 45));
            }
        }

        public ChangeListItemBhikku()
        {
            // TODO: Complete member initialization
        }

        void ChangeListLabel_MouseLeave(object sender, EventArgs e)
        {
            setOriginalImage(bInfo.BhikkuType, false, isDraged);
        }

        void ChangeListLabel_MouseEnter(object sender, EventArgs e)
        {
            this.Parent.Focus();
            setOriginalImage(bInfo.BhikkuType, true, isDraged);
        }

        public void resetMenu_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void suMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.SangaUpasthayaka;
            ReAssignProperies();
        }

        private void ReAssignProperies()
        {
            if (this.Asapuwa != null)
                this.Asapuwa.RecalculateCounts();

            setOriginalImage(bInfo.BhikkuType, false, isDraged);

            if (this.CloneLabel != null)
            {
                this.CloneLabel.setOriginalImage(bInfo.BhikkuType, false, false);
            }

            if (isDraged)
            {
                AddChangeItemTable(ChanageListID, Asapuwa.asapuwaID, bInfo.ID, bInfo.Post, bInfo.IsUpasampanna);
            }

        }

        public void asuMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.AnusangaUpasthayaka;
            ReAssignProperies();
        }

        public void resetPostMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.NAN;
            ReAssignProperies();
        }

        public void bhikkuDetailsMenu_Click(object sender, EventArgs e)
        {
            LoadBhikkuDetails();
        }

        public void Reset()
        {
            isDraged = false;

            //using (ChangeList change = new ChangeList(true))
            //{
            //    change.DeleteBhikkuAsapuwa(ChanageListID);
            //}

            // delete
            AddChangeItemTable(ChanageListID, -1, -1, DBCore.BhikkuPost.NAN, bInfo.IsUpasampanna);

            ChanageListID = 0;

            if (asapuwaLabel != null)
                asapuwaLabel.Dispose();

            if (CloneLabel != null)
            {

                Asapuwa.RemoveBhikkuList(this);


                if (CloneLabel != null)
                    CloneLabel.Dispose();

                CloneLabel = null;
                Asapuwa.PrepareNamelist();
            }

            setOriginalImage(bInfo.BhikkuType, false, isDraged);
        }

        void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDraged && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Copy);
            }
        }

        internal void DoDrag(ChangeListItemAsapuwa asapuwa)
        {

            this.isDraged = true;
            setOriginalImage(bInfo.BhikkuType, false, isDraged);
            this.Asapuwa = asapuwa;
            CreateAsapuwaLabel();

            if (ChanageListID == 0)
            {
                ChanageListID = AddChangeItemTable(0, asapuwa.asapuwaID, bInfo.ID, bInfo.Post, bInfo.IsUpasampanna);
            }
        }

        private void CreateAsapuwaLabel()
        {
             asapuwaLabel = new Label();
             asapuwaLabel.Text = this.Asapuwa.asapuwaShortName;
             asapuwaLabel.Location = new Point(1,3);
            //// asapuwaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // asapuwaLabel.Font = new System.Drawing.Font(asapuwaLabel.Font.FontFamily, 10);
             asapuwaLabel.AutoSize = true;
             asapuwaLabel.Click += new EventHandler(HeadButton_Click);
            // asapuwaLabel.Style.CornerType = eCornerType.Rounded;
            // asapuwaLabel.Size = new Size(100, 15);

            //this.Parent.Controls.Add(asapuwaLabel);

           // asapuwaNamepanel.Text = this.Asapuwa.asapuwaShortName;
            asapuwaNamepanel.Location = new Point(this.Location.X + this.Width + 2, this.Location.Y + 20);
            asapuwaNamepanel.Size = new Size(asapuwaLabel.PreferredSize.Width + 20, asapuwaLabel.PreferredSize.Height + 15);

            asapuwaNamepanel.Controls.Add(asapuwaLabel);
            this.Parent.Controls.Add(asapuwaNamepanel);
        }

              //public void SetOriginalColor()
        //{
        //    assignedImage = this.BackgroundImage;
        //    setOriginalImage(false,isDraged);
        //}


        public void setOriginalImage(DBCore.BhikkuType bType, bool hover, bool isdraged)
        {
            List<Image> list = Utility.GetBhikkuLabelImageList(bType, isdraged, hover);
            this.HeadButton.BackgroundImage = list[2];
            this.BodyButton.BackgroundImage = list[0];
            this.RearButton.BackgroundImage = list[1];
        }


        public void SetClone()
        {
            CloneLabel = new ChangeListItemBhikku();
            CloneLabel.Size = this.Size;
            CloneLabel.HeadButton = GetCloneButton(HeadButton, CloneLabel);
            CloneLabel.BodyButton = GetCloneButton(BodyButton, CloneLabel);
            CloneLabel.RearButton = GetCloneButton(RearButton, CloneLabel);

            CloneLabel.imagePicbox = new PictureBox();
            CloneLabel.imagePicbox.Image = imagePicbox.Image;
            CloneLabel.imagePicbox.BackgroundImage = imagePicbox.BackgroundImage;
            CloneLabel.imagePicbox.BorderStyle = imagePicbox.BorderStyle;
            CloneLabel.imagePicbox.Location = imagePicbox.Location;
            CloneLabel.imagePicbox.Size = imagePicbox.Size;

            CloneLabel.Controls.Add(CloneLabel.imagePicbox);
            CloneLabel.Controls.Add(CloneLabel.BodyButton);
            CloneLabel.Controls.Add(CloneLabel.HeadButton);
            CloneLabel.Controls.Add(CloneLabel.RearButton);


            CloneLabel.ParentLbl = this;

            this.CloneLabel.ContextMenu = CreateContextMenuItems();
        }

        public Button GetCloneButton(Button original, ChangeListItemBhikku CloneLabel)
        {
            Button b = new Button();
            b.Location = original.Location;
            b.Size = original.Size;
            b.Text = original.Text;
            b.Location = original.Location;
            b.BackgroundImage = original.BackgroundImage;
            b.BackgroundImageLayout = original.BackgroundImageLayout;
            b.FlatStyle = original.FlatStyle;
            b.FlatAppearance.BorderSize = original.FlatAppearance.BorderSize;
            b.Font = original.Font;
            b.TextAlign = original.TextAlign;
            b.MouseDown += new MouseEventHandler(CloneLabel.label_MouseDown);
            b.ImageAlign = original.ImageAlign;
            b.ForeColor = original.ForeColor;
            b.UseCompatibleTextRendering = original.UseCompatibleTextRendering;
            return b;
        }

        private ContextMenu CreateContextMenuItems()
        {
            MenuItem resetMenu = new MenuItem("Reset");
            resetMenu.Click += new EventHandler(resetMenu_Click);

            MenuItem suMenu = new MenuItem("සංඝ උපස්ථායක ස්වාමීන්වහන්සේ");
            suMenu.Click += new EventHandler(suMenu_Click);

            MenuItem asuMenu = new MenuItem("අනු සංඝ උපස්ථායක ස්වාමීන්වහන්සේ");
            asuMenu.Click += new EventHandler(asuMenu_Click);

            MenuItem resetPostMenu = new MenuItem("Reset Post");
            resetPostMenu.Click += new EventHandler(resetPostMenu_Click);

            MenuItem emptyPostMenu = new MenuItem("------------------");
            emptyPostMenu.Enabled = false;


            MenuItem bhikkuDetailsMenu = new MenuItem("විස්තර");
            bhikkuDetailsMenu.Click += new EventHandler(bhikkuDetailsMenu_Click);


            return new ContextMenu(new MenuItem[] { bhikkuDetailsMenu, resetMenu, emptyPostMenu, suMenu, asuMenu, resetPostMenu });
        }


        #region IComparer<ChangeListItemBhikku> Members

        public int Compare(ChangeListItemBhikku x, ChangeListItemBhikku y)
        {
            return x.bInfo.SortListOrdeNumber.CompareTo(y.bInfo.SortListOrdeNumber);
        }

        #endregion

        private void InitializeComponent()
        {
            this.HeadButton = new System.Windows.Forms.Button();
            this.BodyButton = new System.Windows.Forms.Button();
            this.RearButton = new System.Windows.Forms.Button();
            this.asapuwaNamepanel = new System.Windows.Forms.Panel();
            this.imagePicbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imagePicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // HeadButton
            // 
            this.HeadButton.BackColor = System.Drawing.Color.Transparent;
            this.HeadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HeadButton.FlatAppearance.BorderSize = 0;
            this.HeadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HeadButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.HeadButton.Location = new System.Drawing.Point(0, 0);
            this.HeadButton.Name = "HeadButton";
            this.HeadButton.Size = new System.Drawing.Size(33, 30);
            this.HeadButton.TabIndex = 0;
            this.HeadButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.HeadButton.UseVisualStyleBackColor = false;
            this.HeadButton.Click += new System.EventHandler(this.HeadButton_Click);
            // 
            // BodyButton
            // 
            this.BodyButton.BackColor = System.Drawing.Color.Transparent;
            this.BodyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BodyButton.FlatAppearance.BorderSize = 0;
            this.BodyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BodyButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BodyButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BodyButton.Location = new System.Drawing.Point(0, 0);
            this.BodyButton.Name = "BodyButton";
            this.BodyButton.Size = new System.Drawing.Size(75, 28);
            this.BodyButton.TabIndex = 0;
            this.BodyButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BodyButton.UseVisualStyleBackColor = true;
            // 
            // RearButton
            // 
            this.RearButton.BackColor = System.Drawing.Color.Transparent;
            this.RearButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RearButton.FlatAppearance.BorderSize = 0;
            this.RearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RearButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.RearButton.Location = new System.Drawing.Point(0, 0);
            this.RearButton.Name = "RearButton";
            this.RearButton.Size = new System.Drawing.Size(18, 30);
            this.RearButton.TabIndex = 0;
            this.RearButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.RearButton.UseVisualStyleBackColor = false;
            // 
            // asapuwaNamepanel
            // 
            this.asapuwaNamepanel.BackColor = System.Drawing.Color.PowderBlue;
            this.asapuwaNamepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.asapuwaNamepanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asapuwaNamepanel.Location = new System.Drawing.Point(0, 0);
            this.asapuwaNamepanel.Name = "asapuwaNamepanel";
            this.asapuwaNamepanel.Size = new System.Drawing.Size(200, 100);
            this.asapuwaNamepanel.TabIndex = 0;
            this.asapuwaNamepanel.Click += new System.EventHandler(this.HeadButton_Click);
            // 
            // imagePicbox
            // 
            this.imagePicbox.BackColor = System.Drawing.Color.Transparent;
            this.imagePicbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imagePicbox.Location = new System.Drawing.Point(0, 0);
            this.imagePicbox.Name = "imagePicbox";
            this.imagePicbox.Size = new System.Drawing.Size(68, 68);
            this.imagePicbox.TabIndex = 0;
            this.imagePicbox.TabStop = false;
            // 
            // ChangeListItemBhikku
            // 
            ((System.ComponentModel.ISupportInitialize)(this.imagePicbox)).EndInit();
            this.ResumeLayout(false);

        }

        private void HeadButton_Click(object sender, EventArgs e)
        {

            Asapuwa.minimizedAsapuwa.Select();
            Asapuwa.timer1.Enabled = true;
            Asapuwa.minimizedAsapuwa.nameLabel.ForeColor = Color.Red;
            Asapuwa.minimizedAsapuwa.nameLabel.BackColor = Color.LightGreen;
        }

       
    }

    public struct ChangeListReportData
    {
        public string AsapuwaName;
        public string BhikkuName;
        public string Post;

        public ChangeListReportData(string aspName, string bhikkuName, string post)
        {
            AsapuwaName = aspName;
            BhikkuName = bhikkuName;
            Post = post;
        }
    }
}
