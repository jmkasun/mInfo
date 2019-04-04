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
using System.Configuration;
using System.Threading;
using DevComponents.DotNetBar;

namespace MahamewnawaInfo.Forms
{
    public delegate int AddDeleteChangeItem(int changeListID, int asapuwaID, int bhikkuID, DBCore.BhikkuPost post, DBCore.BhikkuChangeType changetype, bool isUpasampanna);
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

        int currentPanelCount = 0;
        PanelEx bhikkuNamelistPanel;


        int ChangeListID = 0;
        DateTime FromDate;
        DateTime ToDate;
        bool AddedHistry;

        ChangeListItemBhikku searchBhikku;
        ChangeListItemAsapuwa searchAsapuwa;

        int searchMode = 0; // 1 = bhikku , 2 = asapuwa

        int searchIntervel = 0;

        int upasampadaCount = 0;
        int samaneraCount = 0;

        int upasampadaChangeCount = 0;

        int samaneraChangeCount = 0;
        int allAsapuwaListPanelOriginalWidth = 0;

        Color bgColor = Color.Silver;
        Color capColor = Color.SkyBlue;
        Color statusColor = Color.SkyBlue;
        string sinhalaDate = string.Empty;


        DevComponents.DotNetBar.TabControlPanel tabControlPanel1 = null;

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
                bInfo.BindToComboNameSeparate(nameOfAssumedAtRobinCombo);
            }           
            
           

            bhikkuNamelistPanel = createBhikkuPanel();

            


            AddBhikkLabels((int)(maxNameLength * 11));           

            using (Asapuwa asp = new Asapuwa(true))
            {
                asapuwaList = asp.SelectAllDictionary(ref maxNameLength);
                asp.BindToCombo(asapuHistrAsapu);
            }

            AddAsapuListRclick((int)(maxNameLength * 8));



            this.WindowState = FormWindowState.Maximized;
            this.nameOfAssumedAtRobinCombo.SelectedValueChanged += new System.EventHandler(this.nameOfAssumedAtRobinCombo_SelectedValueChanged);

            label5.BackColor = upasampadaountLb.BackColor = upasampadaChangeCountLbl.BackColor = upasampadaRemainCountLbl.BackColor = Utility.GetBhikkuLabelColor(DBCore.BhikkuType.Upasampada);
            label6.BackColor = samaneraCountLbl.BackColor = samaneraChangeCountLbl.BackColor = samaneraRemainCountLbl.BackColor = Utility.GetBhikkuLabelColor(DBCore.BhikkuType.Samanera);
        }

        private void setSummaryCounts()
        {
            upasampadaChangeCountLbl.Text = upasampadaChangeCount.ToString();
            upasampadaRemainCountLbl.Text = (upasampadaCount - upasampadaChangeCount).ToString();

            samaneraChangeCountLbl.Text = samaneraChangeCount.ToString();
            samaneraRemainCountLbl.Text = (samaneraCount - samaneraChangeCount).ToString();
        }


        private void AddBhikkLabels(object maxNamelength)
        {

            BhikkuDict = new Dictionary<int, ChangeListItemBhikku>();

            foreach (string name in bhikkuList.Keys)
            {
                BikkuInfo bInfo = bhikkuList[name];
                BhikkuDict.Add(bInfo.ID, new ChangeListItemBhikku(name, bhikkuNamelistPanel, bInfo, (int)maxNamelength, new AddDeleteChangeItem(AddDeleteChangeItem)));



                if (bInfo.IsUpasampanna)
                {
                    upasampadaCount++;
                }
                else
                {
                    samaneraCount++;
                }

                currentPanelCount++;

                if (currentPanelCount == 300)
                {
                    bhikkuNamelistPanel = createBhikkuPanel();
                    currentPanelCount = 0;
                }
            }

            upasampadaountLb.Text = upasampadaCount.ToString();
            samaneraCountLbl.Text = samaneraCount.ToString();


           
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


        private void AddAllAsapuwaToPanel(Asapuwa asp, int maxNameLength, Color statusColor, Color captionColor)
        {
            sinhalaDate = string.Concat(Utility.GetSinhalaDate(FromDate)," දින සිට  ",Utility.GetSinhalaDate(ToDate)," දින දක්වා");
            ChangeListItemAsapuwa asapuwaClist = new ChangeListItemAsapuwa(asp.ID, asp.AsapuwaName, asp.AsapuwaNameKey, asapuwaListPanel.Controls, false, maxNameLength, new UpdateChangeItemFinalizeAsapuwa(UpdateChangeItemFinalizeAsapuwa), statusColor, captionColor, asp.NumberOfKuti, sinhalaDate);

            AsapuDict[asp.ID] = asapuwaClist;

            asapuwaClist.minimizedAsapuwa.CanvasColor = Color.Red;
            asapuwaClist.minimizedAsapuwa.Location = new Point(3, (allAsapuwaPanel.Controls.Count * 45) + 5);
            asapuwaClist.minimizedAsapuwa.Visible = true;
            allAsapuwaPanel.Controls.Add(asapuwaClist.minimizedAsapuwa);
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

            allAsapuwaListPanel.Width = maxNameLength + 30;

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


                try
                {
                    bgColor = Color.FromArgb(Int32.Parse(ConfigurationManager.AppSettings["bgColor"]));
                    capColor = Color.FromArgb(Int32.Parse(ConfigurationManager.AppSettings["capColor"]));
                    statusColor = Color.FromArgb(Int32.Parse(ConfigurationManager.AppSettings["statusColor"]));
                }
                catch
                {
                }

                AddAllAsapuwaToPanel(asp, maxNameLength, statusColor, capColor);
            }
        }

        private void bhikkuNamelistPanel_MouseEnter(object sender, EventArgs e)
        {

            ((PanelEx)sender).Focus();

        }

        private void asapuwaListPanel_MouseEnter(object sender, EventArgs e)
        {
            asapuwaListPanel.Focus();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            SaveToHistry();

            using (ChangeList change = new ChangeList(true))
            {
                change.ID = ChangeListID;

                change.SetAddedHistry();
                AddedHistry = true;

                saveHistrybtn.Enabled = false;

            }

            MessageView.ShowMsg("Sucessfully Saved");
        }

        private void SaveToHistry()
        {
            using (ChangeList cList = new ChangeList(true))
            {
                cList.ID = ChangeListID;
                cList.DeleteBhikkuHistry();
            }


            using (BikkuInfo bInfo = new BikkuInfo(true))
            {

                foreach (int key in AsapuDict.Keys)
                {
                    ChangeListItemAsapuwa a = AsapuDict[key];

                    foreach (ChangeListItemBhikku b in a.bhikkuList)
                    {
                        bInfo.ID = b.bInfo.ID;
                        bInfo.AddAsapuHistry(a.asapuwaID, FromDate, ToDate, b.bInfo.Post, "", ChangeListID);
                    }
                }
            }
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
                            change.AddBhikkuAsapuwa(ChangeListID, RmenuItem.asapuChangeListItem.asapuwaID, bhikku.bInfo.ID, bhikku.bInfo.Post, bhikku.bInfo.ChangeType);
                        }
                    }
                }
            }
        }



        public int AddDeleteChangeItem(int bhikkuChangeListID, int asapuwaID, int bhikkuID, DBCore.BhikkuPost post, DBCore.BhikkuChangeType changeType, bool isUpasampanna)
        {

            using (ChangeList change = new ChangeList(true))
            {
                saveHistrybtn.Enabled = true;

                if (ChangeListID == 0)
                {
                    change.FromDate = FromDate;
                    change.Todate = ToDate;


                    ChangeListID = change.Add();
                }

                if (bhikkuChangeListID == 0)
                {
                    if (isUpasampanna)
                    {
                        upasampadaChangeCount++;
                    }
                    else
                    {
                        samaneraChangeCount++;
                    }

                    setSummaryCounts();

                    return change.AddBhikkuAsapuwa(ChangeListID, asapuwaID, bhikkuID, post, changeType);
                }
                else
                {
                    // delete bhikku
                    if (asapuwaID == -1)
                    {
                        if (isUpasampanna)
                        {
                            upasampadaChangeCount--;
                        }
                        else
                        {
                            samaneraChangeCount--;
                        }

                        change.DeleteBhikkuAsapuwa(bhikkuChangeListID);
                        setSummaryCounts();
                        return 0;
                    }
                    else
                    {
                        change.UpdateBhikkuAsapuwa(bhikkuChangeListID, post,changeType);
                        return bhikkuChangeListID;
                    }
                }

            }
        }


        internal void LoadForm(ChangeList changeList, List<ChangeListBhikku> list)
        {
            finalizedAsp = changeList.FinalizedAsapu; ;
            this.AddedHistry = changeList.AddedHistry;
            this.ChangeListID = changeList.ID;
            this.FromDate = changeList.FromDate;
            this.ToDate = changeList.Todate;

            this.Show();


            Dictionary<int, ChangeListItemAsapuwa> addedAsapu = new Dictionary<int, ChangeListItemAsapuwa>();

            foreach (ChangeListBhikku ListBhikku in list)
            {
                // load bhikku
                if (BhikkuDict.ContainsKey(ListBhikku.BhikkuID) && AsapuDict.ContainsKey(ListBhikku.AsapuwaID))
                {
                    ChangeListItemBhikku cb = BhikkuDict[ListBhikku.BhikkuID];
                    cb.ChanageListID = ListBhikku.ID;
                    cb.bInfo.Post = ListBhikku.Post;
                    cb.bInfo.ChangeType = ListBhikku.ChangeType;
                    cb.setOriginalImage(cb.bInfo.BhikkuType, cb.bInfo.ChangeType, false, false);
                    AsapuDict[ListBhikku.AsapuwaID].AddBhikku(cb);

                    if (cb.bInfo.IsUpasampanna)
                    {
                        upasampadaChangeCount++;
                    }
                    else
                    {
                        samaneraChangeCount++;
                    }
                }

                setSummaryCounts();
            }

            foreach (int aspID in finalizedAsp)
            {
                if (AsapuDict.ContainsKey(aspID))
                {
                    AsapuDict[aspID].isFinalize = true;
                    AsapuDict[aspID].SetFinalizedsettings();
                }
            }

            ShoallasapuwaPanel();

        }

        private void frmChangeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveHistrybtn.Enabled)
            {
                if (MessageView.ShowQuestionMsg("Changes are not saved to bhikku histry\nClose without saving histry?") == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {


                if (MessageView.ShowQuestionMsg("Close Window") == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
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

                if (a.bhikkuList.Count > 0)
                {
                    data.Add(new ChangeListReportData(a.asapuwaName, "(වැඩසිටිය හැකි ස්වාමින්වහන්සේලා ගණන - " + a.NumberOfKuti + " )", ""));

                    foreach (ChangeListItemBhikku b in a.bhikkuList)
                    {
                        data.Add(new ChangeListReportData(a.asapuwaName, b.bInfo.NameAssumedAtRobing, Utility.GetPostString(b.bInfo.Post)));
                    }
                }



            }

            // foreach(

            ChangeListReport rep = new ChangeListReport();
            rep.MdiParent = this.MdiParent;
            rep.AddData(data,sinhalaDate);
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
                    searchBhikku.setOriginalImage(searchBhikku.bInfo.BhikkuType, searchBhikku.bInfo.ChangeType, false, false);
                    timer1.Enabled = false;
                    searchBhikku = null;
                }
                else if (searchMode == 2)
                {
                    searchIntervel = 0;
                    searchMode = 0;
                    timer1.Enabled = false;
                    searchAsapuwa.SetFinalizedsettings();

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
                    searchBhikku.setOriginalImage(searchBhikku.bInfo.BhikkuType, searchBhikku.bInfo.ChangeType, true, false);
                }
            }
        }

        private void showAllAsapuwa_Click(object sender, EventArgs e)
        {
            ShoallasapuwaPanel();

        }

        private void ShoallasapuwaPanel()
        {
            //if (allAsapuwaPanel.Controls.Count > 0)
            //{
            //    allAsapuwaListPanelOriginalWidth = allAsapuwaPanel.Controls[0].Width + 50;
            //}
            //else
            //{
            //    allAsapuwaListPanelOriginalWidth = 250;
            //}

            //allAsapuwaListPanel.Width = allAsapuwaListPanelOriginalWidth;
            //allAsapuwaListPanel.Location = new Point(this.TopLevelControl.Width - splitContainer1.SplitterDistance - allAsapuwaListPanel.Width, 2);
            //allAsapuwaListPanel.Visible = true;
            //reArangeAsapuwalist(false, 0);


            allAsapuwaListPanel.Visible = true;
            allAsapuwaListPanel.Dock = DockStyle.Fill;
            reArangeAsapuwalist(false, 0);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            allAsapuwaListPanel.Visible = false;

            //if (allAsapuwaListPanel.Width - allAsapuwaListPanelOriginalWidth > allAsapuwaListPanelOriginalWidth)
            //{
            //    allAsapuwaListPanel.Width = allAsapuwaListPanel.Width - allAsapuwaListPanelOriginalWidth;
            //    allAsapuwaListPanel.Location = new Point(allAsapuwaListPanel.Location.X + allAsapuwaListPanelOriginalWidth, 2);

            //    reArangeAsapuwalist(false, 0);
            //}
            //else
            //{
            //    allAsapuwaListPanel.Visible = false;
            //}
        }

        private void allAsapuwaInnerPanel_MouseEnter(object sender, EventArgs e)
        {
            allAsapuwaPanel.Focus();
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
                    searchAsapuwa = AsapuDict[selectedIndex];

                    HilightAsapuwa(searchAsapuwa);

                    //searchBhikku.setOriginalImage(true, false);
                }
            }
        }

        private void HilightAsapuwa(ChangeListItemAsapuwa asp)
        {
            searchMode = 2;
            asp.minimizedAsapuwa.Select();
            timer1.Enabled = true;
            asp.minimizedAsapuwa.nameLabel.ForeColor = Color.Red;
            asp.minimizedAsapuwa.nameLabel.BackColor = Color.WhiteSmoke;
        }

        private void UpdateChangeItemFinalizeAsapuwa(int asapuwaiD, bool isAdd)
        {
            using (ChangeList c = new ChangeList(true))
            {
                c.ID = ChangeListID;
                c.UpdateFinalizedAsapuList(string.Concat(",", asapuwaiD), isAdd);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Dock = DockStyle.None;

            if (this.TopLevelControl.Width > allAsapuwaListPanel.Width + allAsapuwaListPanelOriginalWidth + 100)
            {
                allAsapuwaListPanel.Width = allAsapuwaListPanel.Width + allAsapuwaListPanelOriginalWidth;
                allAsapuwaListPanel.Location = new Point(allAsapuwaListPanel.Location.X - allAsapuwaListPanelOriginalWidth, 2);

                reArangeAsapuwalist(false, 0);
            }
        }


        private int reArangeAsapuwalist(bool changeSize, int change)
        {
            int finalWidth = 0;

            if (allAsapuwaPanel.Controls.Count > 1)
            {

                int itemWidth = allAsapuwaPanel.Controls[0].Width;

                if (changeSize)
                {
                    itemWidth = itemWidth + change;
                }

                int maxCol = itemWidth > allAsapuwaPanel.Width ? 1 : allAsapuwaPanel.Width / itemWidth;
                int curRow = 0;
                int curCol = 0;


                for (int i = 0; i < allAsapuwaPanel.Controls.Count; i++)
                {
                    curRow = i / maxCol;
                    curCol = i % maxCol;

                    MinimizedAsapuwa c = (MinimizedAsapuwa)allAsapuwaPanel.Controls[allAsapuwaPanel.Controls.Count - 1 - i];

                    if (changeSize)
                    {
                        c.Width = c.Width + change;
                        finalWidth = c.Width;
                        allAsapuwaListPanelOriginalWidth = c.Width;
                    }

                    c.Location = new Point(curCol * (c.Width + 3), curRow * (c.Height + 3));

                }
            }

            return finalWidth;
        }



        private void buttonX3_Click(object sender, EventArgs e)
        {
          int finalWidth =  reArangeAsapuwalist(true, 6);

            DBCore.Utility.SetAppsetingData(DBCore.AppSetting.LabelLength, finalWidth.ToString());
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            int finalWidth = reArangeAsapuwalist(true, -6);

            DBCore.Utility.SetAppsetingData(DBCore.AppSetting.LabelLength, finalWidth.ToString());
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (maximized)
            //{
            //    //allAsapuwaListPanel.Visible = false;
            //    maximized = false;

            //    allAsapuwaListPanel.Dock = DockStyle.None;
            //}
            //else
            //{
            //    //allAsapuwaListPanel.Width = asapuwaListPanel.Width;
            //    //allAsapuwaListPanel.Location = new Point(5, 0);
            //    //allAsapuwaListPanel.Visible = true;
            //    maximized = true;
            //    //reArangeAsapuwalist(false,0);


            //    allAsapuwaListPanel.Dock = DockStyle.Fill;
            //}
        }

        private void allAsapuwaListPanel_SizeChanged(object sender, EventArgs e)
        {
            reArangeAsapuwalist(false, 0);
        }

        private void colorPickerBtn_Click(object sender, EventArgs e)
        {
            //colorDialog1.AllowFullOpen = true;
            colorDialog1.FullOpen = true;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (backgroundColorRadio.Checked)
                {
                    bgColor = colorDialog1.Color;
                    ChangeBackgroundColor(colorDialog1.Color.ToArgb());
                    DBCore.Utility.SetAppsetingData(DBCore.AppSetting.bgColor, colorDialog1.Color.ToArgb().ToString());


                }
                else if (headerColorRadio.Checked)
                {
                    capColor = colorDialog1.Color;
                    ChangeCapColor(colorDialog1.Color.ToArgb());
                    DBCore.Utility.SetAppsetingData(DBCore.AppSetting.capColor, colorDialog1.Color.ToArgb().ToString());

                }
                else if (statusColorRadio.Checked)
                {
                    statusColor = colorDialog1.Color;
                    ChangeStatusColor(colorDialog1.Color.ToArgb());
                    DBCore.Utility.SetAppsetingData(DBCore.AppSetting.statusColor, colorDialog1.Color.ToArgb().ToString());
                }

                colorPickerBtn.BackColor = colorDialog1.Color;

            }
        }

        private void ChangeBackgroundColor(int color)
        {
            allAsapuwaPanel.Style.BackColor1.Color = Color.FromArgb(color);
            allAsapuwaPanel.Style.BackColor2.Color = Color.FromArgb(color);
        }

        private void ChangeStatusColor(int color)
        {
            foreach (Control ct in asapuwaListPanel.Controls)
            {
                if (ct is ChangeListItemAsapuwa)
                {
                    ((ChangeListItemAsapuwa)ct).ChangeStatusColor(Color.FromArgb(color));

                }
            }
        }


        private void ChangeCapColor(int color)
        {
            foreach (Control ct in asapuwaListPanel.Controls)
            {
                if (ct is ChangeListItemAsapuwa)
                {
                    ((ChangeListItemAsapuwa)ct).ChangeCaptioColor(Color.FromArgb(color));

                }
            }
        }

        private void headerColorRadio_MouseHover(object sender, EventArgs e)
        {
            balloonTip1.SetBalloonText(headerColorRadio, "Change Header back Color");
        }

        private void backgroundColorRadio_MouseHover(object sender, EventArgs e)
        {
            balloonTip1.SetBalloonText(backgroundColorRadio, "Change background Color");
        }

        private void statusColorRadio_MouseHover(object sender, EventArgs e)
        {
            balloonTip1.SetBalloonText(statusColorRadio, "Change status bar back Color");
        }

        private void headerColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (headerColorRadio.Checked)
            {
                colorDialog1.Color = colorPickerBtn.BackColor = capColor;
            }
        }

        private void statusColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (statusColorRadio.Checked)
            {
                colorDialog1.Color = colorPickerBtn.BackColor = statusColor;
            }
        }

        private void backgroundColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundColorRadio.Checked)
            {
                colorDialog1.Color = colorPickerBtn.BackColor = bgColor;
            }
        }

        private PanelEx createBhikkuPanel()
        {

            DevComponents.DotNetBar.TabItem tabItem1 = new DevComponents.DotNetBar.TabItem();
            tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            DevComponents.DotNetBar.PanelEx bhikkuNamelistPanel = new PanelEx();

            tabControlPanel1.SuspendLayout();
            tabControlPanel1.ResumeLayout(false);

            tabItem1.AttachedControl = tabControlPanel1;
            tabItem1.Name = "tabItem1";
            tabItem1.Text = "Page " + tabControl1.Controls.Count;


            tabControlPanel1.Controls.Add(bhikkuNamelistPanel);
            tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            tabControlPanel1.Name = "tabControlPanel1";
            tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            tabControlPanel1.Size = new System.Drawing.Size(320, 772);
            tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                   | DevComponents.DotNetBar.eBorderSide.Bottom)));
            tabControlPanel1.Style.GradientAngle = 90;
            tabControlPanel1.TabIndex = 1;
            tabControlPanel1.TabItem = tabItem1;

            this.tabControl1.Controls.Add(tabControlPanel1);
            this.tabControl1.Tabs.Add(tabItem1);

            // panel
            bhikkuNamelistPanel.AutoScroll = true;
            bhikkuNamelistPanel.CanvasColor = System.Drawing.SystemColors.Control;
            bhikkuNamelistPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            bhikkuNamelistPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            bhikkuNamelistPanel.Location = new System.Drawing.Point(1, 1);
            bhikkuNamelistPanel.Name = "bhikkuNamelistPanel";
            bhikkuNamelistPanel.Size = new System.Drawing.Size(318, 770);
            bhikkuNamelistPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            bhikkuNamelistPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            bhikkuNamelistPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            bhikkuNamelistPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            bhikkuNamelistPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            bhikkuNamelistPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            bhikkuNamelistPanel.Style.GradientAngle = 90;
            bhikkuNamelistPanel.TabIndex = 3;
            bhikkuNamelistPanel.MouseEnter += new System.EventHandler(this.bhikkuNamelistPanel_MouseEnter);

            return bhikkuNamelistPanel;
        }

       

        

    }
}




