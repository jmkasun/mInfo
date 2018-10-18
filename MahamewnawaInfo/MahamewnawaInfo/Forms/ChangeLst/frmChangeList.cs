using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MahamewnawaInfo.Classes;
using MahamewnawaInfo.Common;
using DBCore.Classes;
using MahamewnawaInfo.Reporting.Viwer;

namespace MahamewnawaInfo.Forms
{
    public delegate int AddChangeItem(int asapuwaID, int bhikkuID);
    public delegate void UpdateChangeItemFinalizeAsapuwa(int asapuwaID, bool isAdd);

    public partial class frmChangeList : DevComponents.DotNetBar.Office2007Form
    {
        Control actcontrol;
        Point preloc;
        Dictionary<string, Asapuwa> asapuwaList;
        Dictionary<string, BikkuInfo> bhikkuList;
        Dictionary<int, ChangeListItemBhikku> BhikkuDict;
        Dictionary<int, ChangeListItemAsapuwa> AsapuDict;
        List<int> finalizedAsp;

        int ChangeListID = 0;
        DateTime FromDate;
        DateTime ToDate;

        ChangeListItemBhikku searchBhikku;
        ChangeListItemAsapuwa searchAsapuwa;

        int searchMode = 0; // 1 = bhikku , 2 = asapuwa

        int searchIntervel = 0;

        public frmChangeList()
        {
            finalizedAsp = new List<int>();
            AsapuDict = new Dictionary<int, ChangeListItemAsapuwa>();
            InitializeComponent();
        }

        public frmChangeList(int changeListID, DateTime fromDate, DateTime toDate)
            : base()
        {
            finalizedAsp = new List<int>();
            this.ChangeListID = changeListID;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            AsapuDict = new Dictionary<int, ChangeListItemAsapuwa>();
            InitializeComponent();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (actcontrol == null || actcontrol != sender)
                return;
            var location = actcontrol.Location;
            location.Offset(e.Location.X - preloc.X, e.Location.Y - preloc.Y);
            actcontrol.Location = location;

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            actcontrol = sender as Control;
            preloc = e.Location;
            Cursor = Cursors.Default;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            actcontrol = null;
            Cursor = Cursors.Default;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            // MessageBox.Show(((Label)sender).Text);
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            ChangeListItemBhikku lbl = (ChangeListItemBhikku)e.Data.GetData(typeof(ChangeListItemBhikku));
            //lbl.DoDrag();

            MessageBox.Show(lbl.Text);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DoDragDrop(sender, DragDropEffects.Copy);
            }
        }

        private void frmChangeList_Load(object sender, EventArgs e)
        {
            int maxNameLength = 0;
            using (BikkuInfo bInfo = new BikkuInfo(true))
            {
                bhikkuList = bInfo.SelectAllDictionary(ref maxNameLength);
                bInfo.BindToCombo(nameOfAssumedAtRobinCombo);
            }

            AddBhikkLabels((int)(maxNameLength * 9.5));

            using (Asapuwa asp = new Asapuwa(true))
            {
                asapuwaList = asp.SelectAllDictionary(ref maxNameLength);
                asp.BindToCombo(asapuHistrAsapu);
            }

            AddAsapuListRclick((int)(maxNameLength * 9));



            this.WindowState = FormWindowState.Maximized;
            this.nameOfAssumedAtRobinCombo.SelectedValueChanged += new System.EventHandler(this.nameOfAssumedAtRobinCombo_SelectedValueChanged);
        }


        private void AddBhikkLabels(int maxNamelength)
        {
            BhikkuDict = new Dictionary<int, ChangeListItemBhikku>();

            foreach (string name in bhikkuList.Keys)
            {
                BikkuInfo bInfo = bhikkuList[name];
                BhikkuDict.Add(bInfo.ID, new ChangeListItemBhikku(name, bhikkuNamelistPanel.Controls, bInfo, maxNamelength, new AddChangeItem(AddChangeItem)));
            }
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            //this.ContextMenu.Show(label2, new Point(0, 0));
        }

        // handle RClick
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (asapuwaList.ContainsKey(e.ClickedItem.Text))
            {
                ChangeListToolstriptItem changeItem = (ChangeListToolstriptItem)e.ClickedItem;


                AddAsapuwa(e.ClickedItem.Text, changeItem, true);
            }
        }

        private void AddAsapuwa(string text, ChangeListToolstriptItem changeItem, bool visible)
        {
           

            if (changeItem.AddedToChangeList)
            {
                changeItem.asapuChangeListItem.Visible = false;
                changeItem.asapuChangeListItem.Location = new Point(MousePosition.X - splitContainer1.SplitterDistance, MousePosition.Y - 60);
                changeItem.asapuChangeListItem.BringToFront();
            }
            else
            {
                changeItem.AddedToChangeList = true;
                Asapuwa asp = asapuwaList[text];
                changeItem.ForeColor = Color.Gray;
                changeItem.asapuwa = asp;
                changeItem.asapuChangeListItem = AsapuDict[asp.ID]; // new ChangeListItemAsapuwa(asp.ID, asp.AsapuwaName, asp.AsapuwaNameKey, asapuwaListPanel.Controls, visible);
                changeItem.asapuChangeListItem.Visible = false;
                changeItem.asapuChangeListItem.Location = new Point(MousePosition.X - splitContainer1.SplitterDistance, MousePosition.Y - 60);
                changeItem.asapuChangeListItem.RClickItem = changeItem;
                
            }

            if (changeItem.asapuChangeListItem.Parent == changeItem.asapuChangeListItem.minimizedAsapuwa.Parent)
            {
                asapuwaListPanel.Controls.Add(changeItem.asapuChangeListItem);
            }

            changeItem.asapuChangeListItem.Visible = true;
        }


        private void AddAllAsapuwaToPanel(Asapuwa asp, int maxNameLength)
        {
            ChangeListItemAsapuwa asapuwaClist = new ChangeListItemAsapuwa(asp.ID, asp.AsapuwaName, asp.AsapuwaNameKey, asapuwaListPanel.Controls, false,maxNameLength,new UpdateChangeItemFinalizeAsapuwa(UpdateChangeItemFinalizeAsapuwa));
            AsapuDict[asp.ID] = asapuwaClist;

            asapuwaClist.minimizedAsapuwa.CanvasColor = Color.Red;
            asapuwaClist.minimizedAsapuwa.Location = new Point(3, (allAsapuwaInnerPanel.Controls.Count * 45)+5);
            asapuwaClist.minimizedAsapuwa.Visible = true;
            allAsapuwaInnerPanel.Controls.Add(asapuwaClist.minimizedAsapuwa);
            asapuwaClist.minimizedAsapuwa.BringToFront();

            MenuItem currentBhikkuDetails = new MenuItem("වර්තමානයේ වැඩසිටින ස්වාමින් වහන්සේලා");
            currentBhikkuDetails.Click += new EventHandler(asapuwaClist.currentBhikkuDetails_Click);

            MenuItem newBhikkuDetails = new MenuItem("අලුතින් තෝරාගත් ස්වාමින් වහන්සේලා");
            newBhikkuDetails.Click += new EventHandler(asapuwaClist.newBhikkuDetails_Click);

            MenuItem finalizeDetails = new MenuItem("ස්වාමින් වහන්සේලා තෝරාගෙන අවසන්");
            finalizeDetails.Click += new EventHandler(asapuwaClist.finalize_Click);


            asapuwaClist.minimizedAsapuwa.nameLabel.MouseEnter += new EventHandler(allAsapuwaInnerPanel_MouseEnter);
            asapuwaClist.minimizedAsapuwa.captionPanel.MouseEnter += new EventHandler(allAsapuwaInnerPanel_MouseEnter);
            asapuwaClist.minimizedAsapuwa.MouseEnter += new EventHandler(allAsapuwaInnerPanel_MouseEnter);

            asapuwaClist.minimizedAsapuwa.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[] { currentBhikkuDetails, newBhikkuDetails, finalizeDetails });
        }




        // add asapuList to RClick menu
        private void AddAsapuListRclick(int maxNameLength)
        {
            bool addedForignSeparator = false;

            allAsapuwaListPanel.Width = maxNameLength+30;
            allAsapuwaListPanel.Location = new Point(this.TopLevelControl.Width - splitContainer1.SplitterDistance - maxNameLength - 30, 2);

            foreach (string name in asapuwaList.Keys)
            {
                Asapuwa asp = asapuwaList[name];

                if (asp.Country > 0 && !addedForignSeparator)
                {
                    addedForignSeparator = true;
                    ChangeListToolstriptItem forignSep = new ChangeListToolstriptItem("---------------");
                    forignSep.Enabled = false;

                    contextMenuStrip1.Items.Add(forignSep);
                }

                contextMenuStrip1.Items.Add(new ChangeListToolstriptItem(asp.AsapuwaNameKey));
                AddAllAsapuwaToPanel(asp, maxNameLength);
            }
        }

        private void bhikkuNamelistPanel_MouseEnter(object sender, EventArgs e)
        {
            bhikkuNamelistPanel.Focus();
        }

        private void asapuwaListPanel_MouseEnter(object sender, EventArgs e)
        {
            asapuwaListPanel.Focus();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            SaveChangeList();
            MessageView.ShowMsg("Sucessfully Saved");
        }

        private void SaveChangeList()
        {
            using (ChangeList change = new ChangeList(true))
            {
                change.FromDate = FromDate;
                change.Todate = ToDate;

                if (ChangeListID == 0)
                {
                    ChangeListID = change.Add();
                }

                change.Clear(ChangeListID);

                foreach (ChangeListToolstriptItem RmenuItem in contextMenuStrip1.Items)
                {
                    if (RmenuItem.asapuChangeListItem != null)
                    {
                        foreach (ChangeListItemBhikku bhikku in RmenuItem.asapuChangeListItem.bhikkuList)
                        {
                            change.AddBhikkuAsapuwa(ChangeListID, RmenuItem.asapuChangeListItem.asapuwaID, bhikku.bInfo.ID);
                        }
                    }
                }
            }
        }



        public int AddChangeItem(int asapuwaID, int bhikkuID)
        {

            using (ChangeList change = new ChangeList(true))
            {
                if (ChangeListID == 0)
                {
                    change.FromDate = FromDate;
                    change.Todate = ToDate;

                    ChangeListID = change.Add();
                }

                return change.AddBhikkuAsapuwa(ChangeListID, asapuwaID, bhikkuID);
            }
        }


        internal void LoadForm(int changeListID, List<ChangeListBhikku> list, List<int> _finalizedAsp)
        {
            finalizedAsp = _finalizedAsp;
            this.ChangeListID = changeListID;
            this.Show();
            int x = 5;
            int y = 5;


            Dictionary<int, ChangeListItemAsapuwa> addedAsapu = new Dictionary<int, ChangeListItemAsapuwa>();
            foreach (ChangeListBhikku ListBhikku in list)
            {
                // load bhikku
                if (BhikkuDict.ContainsKey(ListBhikku.BhikkuID))
                {
                    ChangeListItemBhikku cb = BhikkuDict[ListBhikku.BhikkuID];
                    cb.ChanageListID = ListBhikku.ID;
                    AsapuDict[ListBhikku.AsapuwaID].AddBhikku(cb);
                }
            }

            foreach (int aspID in finalizedAsp)
            {
                if (AsapuDict.ContainsKey(aspID))
                {
                    AsapuDict[aspID].isFinalize = true;
                    AsapuDict[aspID].SetFinalizedsettings();
                }
            }
        }

        private void frmChangeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageView.ShowQuestionMsg("Close Window") == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void finalizeBtn_Click(object sender, EventArgs e)
        {


        }

        private void GenarateReport()
        {
            List<ChangeListReportData> data = new List<ChangeListReportData>();

            foreach (int key in AsapuDict.Keys)
            {
                ChangeListItemAsapuwa a = AsapuDict[key];

                foreach (ChangeListItemBhikku b in a.bhikkuList)
                {
                    data.Add(new ChangeListReportData(a.asapuwaName, b.bInfo.NameAssumedAtRobing, Utility.GetPostString(b.bInfo.Post)));
                }
            }

            // foreach(

            ChangeListReport rep = new ChangeListReport();
            rep.MdiParent = this.MdiParent;
            rep.AddData(data);
            rep.Show();
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            GenarateReport();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            searchIntervel++;

            if (searchIntervel == 2)
            {
                if (searchMode == 1)
                {
                    searchIntervel = 0;
                    searchMode = 0;
                    searchBhikku.SetOriginalColor();
                    timer1.Enabled = false;
                    searchBhikku = null;
                }
                else if (searchMode == 2)
                {
                    searchIntervel = 0;
                    searchMode = 0;
                    timer1.Enabled = false;
                    searchAsapuwa.minimizedAsapuwa.nameLabel.ForeColor = Color.Black;
                    searchAsapuwa.minimizedAsapuwa.nameLabel.BackColor = Color.Transparent;

                    searchAsapuwa = null;
                }

            }
        }

        private void nameOfAssumedAtRobinCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (nameOfAssumedAtRobinCombo.SelectedValue != null && nameOfAssumedAtRobinCombo.SelectedValue is Int32)
            {
                int selectedIndex = (int)nameOfAssumedAtRobinCombo.SelectedValue;

                if (BhikkuDict.ContainsKey(selectedIndex))
                {
                    searchMode = 1;
                    searchBhikku = BhikkuDict[selectedIndex];
                    searchBhikku.Select();
                    timer1.Enabled = true;
                    searchBhikku.setOriginalImage(true, false);
                }
            }
        }

        private void showAllAsapuwa_Click(object sender, EventArgs e)
        {
            allAsapuwaListPanel.Visible = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            allAsapuwaListPanel.Visible = false;
        }

        private void allAsapuwaInnerPanel_MouseEnter(object sender, EventArgs e)
        {
            allAsapuwaInnerPanel.Focus();
        }

        private void allAsapuwaListPanel_MouseClick(object sender, MouseEventArgs e)
        {
            allAsapuwaListPanel.BringToFront();
        }

        private void asapuHistrAsapu_SelectedValueChanged(object sender, EventArgs e)
        {
            if (asapuHistrAsapu.SelectedValue != null && asapuHistrAsapu.SelectedValue is Int32)
            {
                int selectedIndex = (int)asapuHistrAsapu.SelectedValue;

                if (AsapuDict.ContainsKey(selectedIndex))
                {
                    searchMode = 2;
                    searchAsapuwa = AsapuDict[selectedIndex];
                    searchAsapuwa.minimizedAsapuwa.Select();
                    timer1.Enabled = true;
                    searchAsapuwa.minimizedAsapuwa.nameLabel.ForeColor = Color.Red;
                    searchAsapuwa.minimizedAsapuwa.nameLabel.BackColor = Color.RoyalBlue;

                    //searchBhikku.setOriginalImage(true, false);
                }
            }
        }

        private void UpdateChangeItemFinalizeAsapuwa(int asapuwaiD, bool isAdd)
        {
            using (ChangeList c = new ChangeList(true))
            {
                c.ID = ChangeListID;
                c.UpdateFinalizedAsapuList(string.Concat(",", asapuwaiD), isAdd);
            }
        }
    }
}




