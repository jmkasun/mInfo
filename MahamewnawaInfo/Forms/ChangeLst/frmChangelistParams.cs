using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using MahamewnawaInfo.Classes;
using MahamewnawaInfo.Common;

namespace MahamewnawaInfo.Forms
{
    public partial class frmChangelistParams : DevComponents.DotNetBar.Office2007Form
    {
        int ChangeListID = 0;
        public frmChangelistParams()
        {
            InitializeComponent();
        }

        private void changelistStatBtn_Click(object sender, EventArgs e)
        {
            if (validateBeforeAdd())
            {
                frmChangeList cng = new frmChangeList(ChangeListID, fromDateDatetime.Value, toDateDatetime.Value);
                cng.MdiParent = this.MdiParent;
                cng.Show();
                this.Close();
            }
        }

        private bool validateBeforeAdd()
        {
            bool retVal = true;

            if (fromDateDatetime.Value > toDateDatetime.Value)
            {
                retVal = false;
                errorProvider1.SetError(toDateDatetime, "මෙය 'සිට' දිනයට වඩා විශාල විය යුතුයි");
            }
            else
            {
                retVal = true;
                errorProvider1.SetError(toDateDatetime, "");
            }

            return retVal;
        }


        private void AddChangeListHistry(List<ChangeList> list)
        {
            int y = 5;

            foreach (ChangeList c in list)
            {
                ChangeListHistryLabel lbl = new ChangeListHistryLabel();
                lbl.Text = string.Concat(c.FromDate.ToString("yyyy-MMM-dd"), " සිට ", c.Todate.ToString("yyyy-MMM-dd"), " ", c.ForignCountry ? "පිටරට" : "");
                lbl.Location = new Point(5, y);
                lbl.BackColor = Color.Transparent;

                lbl.ChangeList = c;

                y += lbl.Height + 3;
                this.histryGroup.Controls.Add(lbl);

                //lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                lbl.MouseDoubleClick += new MouseEventHandler(lbl_MouseDoubleClick);
                lbl.MouseMove += new MouseEventHandler(lbl_MouseMove);
                lbl.MouseClick += new MouseEventHandler(lbl_MouseClick);

            }
        }

        void lbl_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeListHistryLabel lbl = ((ChangeListHistryLabel)sender);

            if (e.X > lbl.Width - 20)
            {
                if (MessageView.ShowQuestionMsg("Delete Item") == DialogResult.OK)
                {
                    using (ChangeList c = new ChangeList(true))
                    {
                        c.ID = lbl.ChangeList.ID;
                        c.Delete();
                        ReloadhistryLabels();
                    }
                }
            }
        }

        void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            Label lbl = ((Label)sender);
            lbl.BackColor = Color.Gainsboro;

            if (e.X > lbl.Width-20)
            {
                lbl.Image = global::MahamewnawaInfo.Properties.Resources.delete_icon_h;
            }
            else
            {
                lbl.Image = global::MahamewnawaInfo.Properties.Resources.delete_icon;
            }
        }

        void lbl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangeListHistryLabel lbl = ((ChangeListHistryLabel)sender);

            LoadChangeHistry(lbl.ChangeList);
        }

        private void LoadChangeHistry(ChangeList changeList)
        {
            using (ChangeList cLIst = new ChangeList(true))
            {
                //LoadChangeListHistry(changeListID,cLIst.SelectChangeList(changeListID));

                frmChangeList cng = new frmChangeList();
                cng.MdiParent = this.MdiParent;
                cng.LoadForm(changeList, cLIst.SelectChangeList(changeList.ID));
                this.Close();
            }
        }

        private void LoadChangeListHistry(int changeListID,bool forign, List<ChangeListBhikku> list)
        {
           
        }

        void lbl_MouseLeave(object sender, EventArgs e)
        {
            Label lbl =  ((Label)sender);
           lbl.BackColor = Color.Transparent;
           lbl.Image = null;
        }

        void lbl_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = ((Label)sender);
            lbl.BackColor = Color.Gainsboro;

            
            lbl.Image = global::MahamewnawaInfo.Properties.Resources.delete_icon;
        }

        private void frmChangelistParams_Load(object sender, EventArgs e)
        {
            ReloadhistryLabels();
        }

        private void ReloadhistryLabels()
        {
            histryGroup.Controls.Clear();

            using (ChangeList cLIst = new ChangeList(true))
            {
                AddChangeListHistry(cLIst.SelectAllList());
            }
        }

        private void histryGroup_MouseEnter(object sender, EventArgs e)
        {
            histryGroup.Select();
        }

        
    }
}
