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

namespace MahamewnawaInfo.Classes
{
    [Serializable]
    public class ChangeListItemBhikkuPlanel : Button,IComparer<ChangeListItemBhikku>
    {
        public bool isDraged = false;

        public BikkuInfo bInfo;
       
        Color assignedBackColor;
        internal ChangeListItemAsapuwa Asapuwa;
        Label asapuwaLabel;

        public ChangeListItemBhikku CloneLabel;

        public ChangeListItemBhikkuPlanel(string text, Control.ControlCollection contralls,BikkuInfo bInfo,int width )
        {
            this.bInfo = bInfo;


            this.Text = "       "+text;
            this.Size = new System.Drawing.Size(width, 25);
            this.Location = new System.Drawing.Point(5, (contralls.Count * this.Height) + (contralls.Count * 2) + 8);
            this.Font = new System.Drawing.Font(this.Font.FontFamily, 11);
            //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            contralls.Add(this);

            this.MouseEnter += new EventHandler(ChangeListLabel_MouseEnter);
            this.MouseLeave += new EventHandler(ChangeListLabel_MouseLeave);
            this.MouseDown += new MouseEventHandler(label_MouseDown);

            this.ContextMenu = CreateContextMenuItems();

            SetOriginalColor();
            InitializeComponent();
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

                ChangeListItemBhikkuDetails bDetails = new ChangeListItemBhikkuDetails(bInfo, this.Parent.Parent.Parent.Parent.Controls, new Point(MousePosition.X, MousePosition.Y-45)/*new Point(this.Location.X+this.Width,this.Location.Y + this.Height)*/);
            }
        }

        public ChangeListItemBhikkuPlanel()
        {
            // TODO: Complete member initialization
        }

        void ChangeListLabel_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = assignedBackColor;
        }

        void ChangeListLabel_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ActiveCaption;
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
                //this.CloneLabel.Image = Utility.GetBhikkuLabelImage(this.bInfo.BhikkuType);
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

                ////Asapuwa.RemoveBhikkuList(this);


                if (CloneLabel != null)
                    CloneLabel.Dispose();

                CloneLabel = null;
                Asapuwa.PrepareNamelist();
            }

            SetOriginalColor();
        }

        void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDraged && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DoDragDrop(sender, DragDropEffects.Copy);
            }
        }

        internal void DoDrag(ChangeListItemAsapuwa asapuwa)
        {
            this.isDraged = true;
            assignedBackColor = this.BackColor = Color.LightGray;

            this.Asapuwa = asapuwa;
            CreateAsapuwaLabel();
        }

        private void CreateAsapuwaLabel()
        {
            asapuwaLabel = new Label();
            asapuwaLabel.Text = this.Asapuwa.asapuwaShortName;
            asapuwaLabel.Location = new Point(this.Location.X + this.Width + 2, this.Location.Y);
            asapuwaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;            
            asapuwaLabel.Font = new System.Drawing.Font(asapuwaLabel.Font.FontFamily, 10);
            asapuwaLabel.AutoSize = true;

            this.Parent.Controls.Add(asapuwaLabel);
        }

        internal void SetOriginalColor()
        {
            if (this.isDraged)
            {
                this.BackColor = Color.Gray;
            }
            else
            {
                this.BackColor = Color.Transparent;

                //this.BackColor = Common.Utility.GetBhikkuLabelColor(bInfo.BhikkuType);

            }

            assignedBackColor = this.BackColor;
            setOriginalImage();
        }


        private void setOriginalImage()
        {
            this.BackgroundImage = Common.Utility.GetBhikkuLabelImage(bInfo.BhikkuType);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
       

        public void SetClone()
        {
            CloneLabel = new ChangeListItemBhikku();
            CloneLabel.Text = this.Text;
            CloneLabel.Size = this.Size;
            CloneLabel.Font = this.Font;
            //CloneLabel.BorderStyle = this.BorderStyle;
            CloneLabel.BackColor = this.BackColor;
            CloneLabel.BackgroundImage = this.BackgroundImage;
            CloneLabel.BackgroundImageLayout = this.BackgroundImageLayout;
            //CloneLabel.ImageAlign = this.ImageAlign;
            //CloneLabel.TextAlign = this.TextAlign;
            //CloneLabel.FlatStyle = this.FlatStyle;
            //CloneLabel.FlatAppearance.BorderSize = this.FlatAppearance.BorderSize;
            this.CloneLabel.ContextMenu = CreateContextMenuItems();
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


            return new ContextMenu(new MenuItem[] {bhikkuDetailsMenu, resetMenu, emptyPostMenu, suMenu, asuMenu, resetPostMenu });
        }
     

        #region IComparer<ChangeListItemBhikku> Members

        public int Compare(ChangeListItemBhikku x, ChangeListItemBhikku y)
        {
            return x.bInfo.SortListOrdeNumber.CompareTo(y.bInfo.SortListOrdeNumber);
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChangeListItemBhikku
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }
    }   
}
