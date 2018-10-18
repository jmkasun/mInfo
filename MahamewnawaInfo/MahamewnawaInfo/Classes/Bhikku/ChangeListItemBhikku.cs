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

namespace MahamewnawaInfo.Classes
{
    [Serializable]
    public class ChangeListItemBhikku : Panel, IComparer<ChangeListItemBhikku>
    {
        public bool isDraged = false;

        public BikkuInfo bInfo;

        Image assignedImage;
        internal ChangeListItemAsapuwa Asapuwa;
        Label asapuwaLabel;

        public ChangeListItemBhikku CloneLabel;
        public ChangeListItemBhikku ParentLbl;

        public Button HeadButton;
        public Button BodyButton;
        public Button RearButton;

        public AddChangeItem AddChangeItemTable;

        public int ChanageListID; // ID of changelist table

        public ChangeListItemBhikku(string text, Control.ControlCollection contralls, BikkuInfo bInfo, int width,AddChangeItem addChangeItemTable)
        {
            try
            {
                this.bInfo = bInfo;

                AddChangeItemTable = addChangeItemTable;

                this.Size = new System.Drawing.Size(width, 28);
                this.Location = new System.Drawing.Point(5, (contralls.Count * this.Height) + (contralls.Count * 2) + 8);
                this.Font = new System.Drawing.Font(this.Font.FontFamily, 11);
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

                SetOriginalColor();

                this.Controls.Add(BodyButton);
                this.Controls.Add(HeadButton);
                this.Controls.Add(RearButton);

                HeadButton.Location = new Point(0, 0);
                BodyButton.Location = new Point(30, 0);
                RearButton.Location = new Point(this.Width - 18, 0);

                BodyButton.Size = new System.Drawing.Size(this.Width - HeadButton.Width - RearButton.Width + 3, 28);
                BodyButton.Text = text;
                contralls.Add(this);
            }
            catch (Exception ex)
            {
                MessageView.ShowErrorMsg(ex.Message);
            }
        }

        void ChangeListItemBhikku_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadBhikkuDetails();
        }

        private void LoadBhikkuDetails()
        {
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bInfo.SelectBhikkuChangeList(this.bInfo.ID);

                ChangeListItemBhikkuDetails bDetails = new ChangeListItemBhikkuDetails(bInfo, this.Parent.Parent.Parent.Parent.Controls, new Point(MousePosition.X, MousePosition.Y - 45));
            }
        }

        public ChangeListItemBhikku()
        {
            // TODO: Complete member initialization
        }

        void ChangeListLabel_MouseLeave(object sender, EventArgs e)
        {
            setOriginalImage(false,isDraged);
        }

        void ChangeListLabel_MouseEnter(object sender, EventArgs e)
        {
            this.Parent.Focus();
            setOriginalImage(true,isDraged);
        }

        public void resetMenu_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void suMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.SangaUpasthayaka;
            SetOriginalColor();
            ReAssignProperies();
        }

        private void ReAssignProperies()
        {
            if (this.Asapuwa != null)
                this.Asapuwa.RecalculateCounts();

            if (this.CloneLabel != null)
            {
                //this.CloneLabel.BackColor = Utility.GetBhikkuLabelColor(this.bInfo.BhikkuType);
                List<Image> list = Utility.GetBhikkuLabelImageList(this.bInfo.BhikkuType,isDraged,false);
                this.CloneLabel.HeadButton.Image = list[2];
                this.CloneLabel.BodyButton.Image = list[0];
                this.CloneLabel.RearButton.Image = list[1];
            }
        }

        public void asuMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.AnusangaUpasthayaka;
            SetOriginalColor();
            ReAssignProperies();
        }

        public void resetPostMenu_Click(object sender, EventArgs e)
        {
            this.bInfo.Post = DBCore.BhikkuPost.NAN;
            SetOriginalColor();
            ReAssignProperies();
        }

        public void bhikkuDetailsMenu_Click(object sender, EventArgs e)
        {
            LoadBhikkuDetails();
        }

        public void Reset()
        {
            isDraged = false;
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

            SetOriginalColor();


            using (ChangeList change = new ChangeList(true))
            {
                change.DeleteBhikkuAsapuwa(ChanageListID);
            }
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
            setOriginalImage(false, isDraged);
            this.Asapuwa = asapuwa;
            CreateAsapuwaLabel();

            if (ChanageListID == 0)
            {
                ChanageListID = AddChangeItemTable(asapuwa.asapuwaID, bInfo.ID);
            }

        }

        private void CreateAsapuwaLabel()
        {
            asapuwaLabel = new Label();
            asapuwaLabel.Text = this.Asapuwa.asapuwaShortName;
            asapuwaLabel.Location = new Point(this.Location.X + this.Width + 2, this.Location.Y+4);
            asapuwaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            asapuwaLabel.Font = new System.Drawing.Font(asapuwaLabel.Font.FontFamily, 10);
            asapuwaLabel.AutoSize = true;

            this.Parent.Controls.Add(asapuwaLabel);
        }

        internal void SetOriginalColor()
        {
            assignedImage = this.BackgroundImage;
            setOriginalImage(false,isDraged);
        }


        public void setOriginalImage(bool hover, bool isdraged)
        {
            List<Image> list = Utility.GetBhikkuLabelImageList(this.bInfo.BhikkuType, isdraged, hover);
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

            CloneLabel.Controls.Add(CloneLabel.BodyButton);
            CloneLabel.Controls.Add(CloneLabel.HeadButton);
            CloneLabel.Controls.Add(CloneLabel.RearButton);
            CloneLabel.ParentLbl = this;

            this.CloneLabel.ContextMenu = CreateContextMenuItems();
        }

        public Button GetCloneButton(Button original,ChangeListItemBhikku CloneLabel)
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
            this.HeadButton.Size = new System.Drawing.Size(33, 28);
            this.HeadButton.TabIndex = 0;
            this.HeadButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.HeadButton.UseVisualStyleBackColor = false;
            // 
            // BodyButton
            // 
            this.BodyButton.BackColor = System.Drawing.Color.Transparent;
            this.BodyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BodyButton.FlatAppearance.BorderSize = 0;
            this.BodyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BodyButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BodyButton.Location = new System.Drawing.Point(0, 0);
            this.BodyButton.Name = "BodyButton";
            this.BodyButton.Size = new System.Drawing.Size(75, 23);
            this.BodyButton.TabIndex = 0;
            this.BodyButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BodyButton.UseVisualStyleBackColor = false;
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
            this.RearButton.Size = new System.Drawing.Size(18, 28);
            this.RearButton.TabIndex = 0;
            this.RearButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.RearButton.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

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
