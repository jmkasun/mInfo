using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DBCore;
using DBCore.Classes;
using MahamewnawaInfo.Forms;
using System.Drawing.Printing;
using DevComponents.DotNetBar;
using MahamewnawaInfo.Reporting.Viwer;

namespace MahamewnawaInfo.Classes
{
    public class ChangeListItemAsapuwa : DevComponents.DotNetBar.PanelEx
    {
        int printIndex = 0;
        public int asapuwaID = 0;
        public string asapuwaName = string.Empty;
        public string asapuwaShortName = "";
        int searchIntervel = 0;

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
        Label allCount;

        private Panel captionPanel;
        private Panel statusPanel;
        private Button minimizeButton;

        public MinimizedAsapuwa minimizedAsapuwa = null;
        private UpdateChangeItemFinalizeAsapuwa UpdateChangeItemFinalizeAsapuwa;
        public Timer timer1;
        private System.ComponentModel.IContainer components;

        public bool isFinalize;
        public Label numberOfKutiLbl;
        private ButtonX printIcon;
        private PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        public int NumberOfKuti;
        private PrintPreviewDialog printPreviewDialog1;
        private ButtonItem printPreview;
        private string fromToString;
        private ButtonItem printWithImage;
        private ButtonItem printWithImagePreview;
        private Panel bhikkuListReportPnl;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        public bool printImage = false;

  
        private System.Windows.Forms.BindingSource changeListReportBindingSource;
        private mahamevnainfoDataSet mahamevnainfoDataSet;
        private ButtonItem NoImage;
        private System.Windows.Forms.BindingSource UtilBindingSource;

        public ChangeListItemAsapuwa()
        {
        }

        public ChangeListItemAsapuwa(int asapuwaID, string asapuwaName, string asapuwaShortName, Control.ControlCollection contralls, bool visible, int maxNameLength, UpdateChangeItemFinalizeAsapuwa updateChangeItemFinalizeAsapuwa, Color statusColor, Color captionColor, int numberOfKuti, string fromToString)
        {
            UpdateChangeItemFinalizeAsapuwa = updateChangeItemFinalizeAsapuwa;
            this.asapuwaID = asapuwaID;
            this.asapuwaName = asapuwaName;
            this.asapuwaShortName = asapuwaShortName;
            this.NumberOfKuti = numberOfKuti;
            //this.Size = new Size(maxNameLength + 200, 800);
            this.fromToString = fromToString;

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
            nameLabel.Text = asapuwaShortName;
            nameLabel.Location = new Point(5, 2);
            nameLabel.Font = new System.Drawing.Font(nameLabel.Font.FontFamily, 10, FontStyle.Bold);
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.Transparent;
            this.nameLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.nameLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.nameLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            nameLabel.MouseClick += new MouseEventHandler(nameLabel_MouseClick);

            //if (this.Width < nameLabel.PreferredWidth + 40)
            //{
            //    this.Width = nameLabel.PreferredWidth + 40;
            //}

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

            this.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[] { bringToFront, currentBhikkuDetails, newBhikkuDetails, finalizeDetails, closePanel, });

            // last
            InitializeComponent();
            this.closeButton.Location = new Point(this.Size.Width - this.closeButton.Size.Width - 3, 1);
            this.printIcon.Location = new Point(this.Size.Width - (this.closeButton.Size.Width + this.printIcon.Size.Width) - 5, 1);

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
            this.captionPanel.Controls.Add(printIcon);
            this.printIcon.BringToFront();
            this.printIcon.Cursor = System.Windows.Forms.Cursors.Default;

            this.minimizeButton.BringToFront();
            this.pictureBox1.BringToFront();

            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;

            SetMinimizesItem(statusColor, captionColor);
            creatreBhikkuCountLabels();

            statusPanel.BackColor = statusColor;
            this.closeButton.BackColor = this.minimizeButton.BackColor = captionPanel.BackColor = captionColor;

            this.Size = new Size(maxNameLength + 200, this.Parent.Height - 125);

            //printIcon.BackColor = captionPanel.BackColor;
        }


        private void SetMinimizesItem(Color statusColor, Color captionColor)
        {
            minimizedAsapuwa = new MinimizedAsapuwa(asapuwaShortName, statusColor, captionColor, NumberOfKuti);


            minimizedAsapuwa.DragEnter += new DragEventHandler(panel_DragEnter);
            minimizedAsapuwa.DragDrop += new DragEventHandler(panel_DragDrop);
            minimizedAsapuwa.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);

            minimizedAsapuwa.Width = 220;
            try
            {
                minimizedAsapuwa.Width = Int32.Parse(Utility.GetAppsetting(AppSetting.LabelLength));
            }
            catch { }


            minimizedAsapuwa.originalWidth = minimizedAsapuwa.Width;
            minimizedAsapuwa.Height = 40;
            minimizedAsapuwa.maximizedAsapuwa = this;

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(100, 25); // minimizedAsapuwa.Location.Y + minimizedAsapuwa.Parent.Location.Y);

            //minimizedAsapuwa.Parent.Controls.Add(this);

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

            DoDragnDrop(lbl);

            this.BringToFront();
            this.minimizedAsapuwa.MinimizedAsapuwa_DragLeave(sender, e);
        }

        public void DoDragnDrop(ChangeListItemBhikku lbl)
        {
            if (lbl.ParentLbl == null)
            {
                AddBhikku(lbl);
            }
            else
            {
                lbl.ParentLbl.Reset();
                AddBhikku(lbl.ParentLbl);
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeListItemAsapuwa));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.changeListReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mahamevnainfoDataSet = new MahamewnawaInfo.mahamevnainfoDataSet();
            this.UtilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numberOfKutiLbl = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.bhikkuListReportPnl = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.printIcon = new DevComponents.DotNetBar.ButtonX();
            this.NoImage = new DevComponents.DotNetBar.ButtonItem();
            this.printWithImage = new DevComponents.DotNetBar.ButtonItem();
            this.printWithImagePreview = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UtilBindingSource)).BeginInit();
            this.statusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // changeListReportBindingSource
            // 
            this.changeListReportBindingSource.DataMember = "ChangeListReport";
            this.changeListReportBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // mahamevnainfoDataSet
            // 
            this.mahamevnainfoDataSet.DataSetName = "mahamevnainfoDataSet";
            this.mahamevnainfoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // UtilBindingSource
            // 
            this.UtilBindingSource.DataMember = "Util";
            this.UtilBindingSource.DataSource = this.mahamevnainfoDataSet;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.LightBlue;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(0, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "X";
            this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
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
            this.minimizeButton.Font = new System.Drawing.Font("Perpetua Titling MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.Location = new System.Drawing.Point(0, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(20, 20);
            this.minimizeButton.TabIndex = 0;
            this.minimizeButton.Text = "-";
            this.minimizeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Visible = false;
            this.minimizeButton.Click += new System.EventHandler(this.minizeButton_Click);
            // 
            // captionPanel
            // 
            this.captionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.captionPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.captionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.captionPanel.Location = new System.Drawing.Point(0, 0);
            this.captionPanel.Name = "captionPanel";
            this.captionPanel.Size = new System.Drawing.Size(200, 25);
            this.captionPanel.TabIndex = 0;
            this.captionPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            this.captionPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.captionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.captionPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // statusPanel
            // 
            this.statusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.statusPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusPanel.Controls.Add(this.pictureBox1);
            this.statusPanel.Location = new System.Drawing.Point(0, 0);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(200, 15);
            this.statusPanel.TabIndex = 0;
            this.statusPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            this.statusPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.statusPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.statusPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
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
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numberOfKutiLbl
            // 
            this.numberOfKutiLbl.Location = new System.Drawing.Point(0, 0);
            this.numberOfKutiLbl.Name = "numberOfKutiLbl";
            this.numberOfKutiLbl.Size = new System.Drawing.Size(100, 23);
            this.numberOfKutiLbl.TabIndex = 0;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // bhikkuListReportPnl
            // 
            this.bhikkuListReportPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bhikkuListReportPnl.Location = new System.Drawing.Point(0, 0);
            this.bhikkuListReportPnl.Name = "bhikkuListReportPnl";
            this.bhikkuListReportPnl.Size = new System.Drawing.Size(200, 100);
            this.bhikkuListReportPnl.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.changeListReportBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.UtilBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MahamewnawaInfo.Reporting.Reports.AsapuBhikkuWithImage.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(737, 752);
            this.reportViewer1.TabIndex = 0;
            // 
            // printIcon
            // 
            this.printIcon.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.printIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printIcon.BackColor = System.Drawing.Color.Black;
            this.printIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.printIcon.Image = global::MahamewnawaInfo.Properties.Resources.print1;
            this.printIcon.Location = new System.Drawing.Point(0, 0);
            this.printIcon.Name = "printIcon";
            this.printIcon.Size = new System.Drawing.Size(45, 20);
            this.printIcon.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.printIcon.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.NoImage,
            this.printWithImage,
            this.printWithImagePreview});
            this.printIcon.TabIndex = 0;
            this.printIcon.Click += new System.EventHandler(this.printIcon_Click);
            // 
            // NoImage
            // 
            this.NoImage.Name = "NoImage";
            this.NoImage.Text = "No Image";
            this.NoImage.Click += new System.EventHandler(this.printPreview_Click);
            // 
            // printWithImage
            // 
            this.printWithImage.Enabled = false;
            this.printWithImage.Name = "printWithImage";
            this.printWithImage.Text = "Print with image";
            this.printWithImage.Click += new System.EventHandler(this.printWithImage_Click);
            // 
            // printWithImagePreview
            // 
            this.printWithImagePreview.Enabled = false;
            this.printWithImagePreview.Name = "printWithImagePreview";
            this.printWithImagePreview.Text = "Print Image Preview";
            this.printWithImagePreview.Click += new System.EventHandler(this.printWithImagePreview_Click);
            // 
            // ChangeListItemAsapuwa
            // 
            this.CanvasColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.captionPanel);
            this.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Style.BorderWidth = 5;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.changeListReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mahamevnainfoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UtilBindingSource)).EndInit();
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        public void printWithImagePreview_Click(object sender, EventArgs e)
        {
            printImage = true;
            printPreviewDialog1.Width = 700;
            printPreviewDialog1.Height = (int)(this.Parent.Height);
            printPreviewDialog1.ShowDialog();
        }

       public void printWithImage_Click(object sender, EventArgs e)
        {
            printImage = true;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.Document = printDocument1;
                printDocument1.Print();
            }
        }

        public void printPreview_Click(object sender, EventArgs e)
        {
            //printImage = false;
            //printPreviewDialog1.Width = 700;
            //printPreviewDialog1.Height = (int)(this.Parent.Height);
            //printPreviewDialog1.ShowDialog();

            AsapuBhikkuNoImage rptAllBhikkunoImg = new AsapuBhikkuNoImage();
            rptAllBhikkunoImg.ShowReport(bhikkuList, this.fromToString, this.asapuwaName);

            }


        private void closeMenue_Click(object sender, EventArgs e)
        {
            if (MahamewnawaInfo.Common.MessageView.ShowQuestionMsg("Close Window") == DialogResult.OK)
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
                ChangeListItemAsapuwaDetails cad = new ChangeListItemAsapuwaDetails(this.asapuwaName, a.SelectCurrentBhikkuList(), this.Parent.Controls, new Point(100, 100), false);
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

            ChangeListItemAsapuwaDetails cad = new ChangeListItemAsapuwaDetails(this.asapuwaName, binfoList, cnt, new Point(100, 100), true);
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
            int labelGap = 7;
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
            anuSangaUpasthayakaCount.Location = new Point(sangaUpasthayakaCount.Location.X + sangaUpasthayakaCount.Width + labelGap, LabelLocationy);
            //
            minimizedAsapuwa.anuSangaUpasthayakaCount.Location = new Point(sangaUpasthayakaCount.Location.X + sangaUpasthayakaCount.Width + labelGap, 26);
            //
            minimizedAsapuwa.anuSangaUpasthayakaCount.Anchor = anuSangaUpasthayakaCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(anuSangaUpasthayakaCount);

            upasampadaCount = new Label();
            minimizedAsapuwa.upasampadaCount.BackColor = upasampadaCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.Upasampada);
            minimizedAsapuwa.upasampadaCount.Text = upasampadaCount.Text = "0";
            minimizedAsapuwa.upasampadaCount.AutoSize = upasampadaCount.AutoSize = true;
            upasampadaCount.Location = new Point(anuSangaUpasthayakaCount.Location.X + anuSangaUpasthayakaCount.Width + labelGap, LabelLocationy);
            //
            minimizedAsapuwa.upasampadaCount.Location = new Point(anuSangaUpasthayakaCount.Location.X + anuSangaUpasthayakaCount.Width + labelGap, 26);
            //
            minimizedAsapuwa.upasampadaCount.Anchor = upasampadaCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(upasampadaCount);

            samaneraCount = new Label();
            minimizedAsapuwa.samaneraCount.BackColor = samaneraCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.Samanera);
            minimizedAsapuwa.samaneraCount.Text = samaneraCount.Text = "0";
            minimizedAsapuwa.samaneraCount.AutoSize = samaneraCount.AutoSize = true;
            samaneraCount.Location = new Point(upasampadaCount.Location.X + upasampadaCount.Width + labelGap, LabelLocationy);
            //
            minimizedAsapuwa.samaneraCount.Location = new Point(upasampadaCount.Location.X + upasampadaCount.Width + labelGap, 26);
            //
            minimizedAsapuwa.samaneraCount.Anchor = samaneraCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(samaneraCount);


            ///

            ///

            allCount = new Label();
            minimizedAsapuwa.allCount.BackColor = allCount.BackColor = Common.Utility.GetBhikkuLabelColor(BhikkuType.all);
            minimizedAsapuwa.allCount.Text = allCount.Text = "0";
            minimizedAsapuwa.allCount.AutoSize = allCount.AutoSize = true;
            allCount.Location = new Point(samaneraCount.Location.X + samaneraCount.Width + labelGap, LabelLocationy);
            //
            minimizedAsapuwa.allCount.Location = new Point(samaneraCount.Location.X + samaneraCount.Width + labelGap, 26);
            //
            minimizedAsapuwa.allCount.Anchor = allCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            statusPanel.Controls.Add(allCount);

            //

            this.numberOfKutiLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfKutiLbl.ForeColor = System.Drawing.Color.Maroon;
            numberOfKutiLbl.AutoSize = true;
            numberOfKutiLbl.Text = NumberOfKuti.ToString();

            numberOfKutiLbl.Location = new Point(statusPanel.Width - 30, LabelLocationy);
            numberOfKutiLbl.BringToFront();

            minimizedAsapuwa.numberOfKutiLbl.Location = new Point(minimizedAsapuwa.Width - minimizedAsapuwa.numberOfKutiLbl.Width, 26);
            numberOfKutiLbl.Anchor = minimizedAsapuwa.numberOfKutiLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));

            statusPanel.Controls.Add(numberOfKutiLbl);
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
            minimizedAsapuwa.sangaUpasthayakaCount.Text = minimizedAsapuwa.anuSangaUpasthayakaCount.Text = minimizedAsapuwa.upasampadaCount.Text = minimizedAsapuwa.samaneraCount.Text = "0";

            foreach (ChangeListItemBhikku b in bhikkuList)
            {
                updateBhikkuCountLabel(1, b.bInfo.BhikkuType);
            }
        }

        private void updateBhikkuCountLabel(int increment, BhikkuType type)
        {
            int currentCount = Int32.Parse(allCount.Text);
            minimizedAsapuwa.allCount.Text = allCount.Text = (currentCount + increment).ToString();

            switch (type)
            {
                case BhikkuType.SangaUpasthayaka:
                    {
                        currentCount = Int32.Parse(sangaUpasthayakaCount.Text);
                        minimizedAsapuwa.sangaUpasthayakaCount.Text = sangaUpasthayakaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.AnusangaUpasthayaka:
                    {
                        currentCount = Int32.Parse(anuSangaUpasthayakaCount.Text);
                        minimizedAsapuwa.anuSangaUpasthayakaCount.Text = anuSangaUpasthayakaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.Upasampada:
                    {
                        currentCount = Int32.Parse(upasampadaCount.Text);
                        minimizedAsapuwa.upasampadaCount.Text = upasampadaCount.Text = (currentCount + increment).ToString();
                        return;
                    }

                case BhikkuType.Samanera:
                    {
                        currentCount = Int32.Parse(samaneraCount.Text);
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
                    this.Height = 800;
                    //this.Height = 400;
                }
            }
        }



        internal void ChangeCaptioColor(Color clr)
        {
            this.closeButton.BackColor = this.minimizeButton.BackColor = this.minimizedAsapuwa.minimizeButton.BackColor = this.captionPanel.BackColor = this.minimizedAsapuwa.captionPanel.BackColor = clr;
        }


        internal void ChangeStatusColor(Color color)
        {
            this.statusPanel.BackColor = this.minimizedAsapuwa.Style.BackColor1.Color = this.minimizedAsapuwa.Style.BackColor2.Color = color;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            searchIntervel++;
            if (searchIntervel == 2)
            {
                searchIntervel = 0;
                timer1.Enabled = false;
                SetFinalizedsettings();
            }
        }

        public void printIcon_Click(object sender, EventArgs e)
        {

            AsapuBhikkuImage rptAllBhikkuImg = new AsapuBhikkuImage();
            rptAllBhikkuImg.ShowReport(bhikkuList, this.fromToString, this.asapuwaName);
        }

        private void ShowReportPanel()
        {

            AsapuBhikkuImagePanel pnl = new AsapuBhikkuImagePanel();
            this.Parent.Controls.Add(pnl);
            pnl.Show();
            pnl.Width = 600;
            pnl.Height = 800;
            pnl.Location = new Point(200, 30);
            pnl.BringToFront();
            pnl.ShowReport(bhikkuList, this.fromToString, this.asapuwaName);
        }


        // drow print document
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (bhikkuList.Count == 0)
                return;

            //e.Graphics.PageUnit = GraphicsUnit.Display;

            int imgeWidthHeight = bhikkuList[0].imagePicbox.Height;
            int y = 80;
            int labelX = 150 + (printImage ? imgeWidthHeight : -30);

            // add heading and date for first page only
            if (printIndex == 0)
            {
                y = 215;

                //Font fB = new Font("Iskoola Pota", 120, FontStyle.Bold);

                TextRenderer.DrawText(e.Graphics, "නමෝ බුද්ධාය !", new Font("Iskoola Pota", 20 * 6, FontStyle.Regular), new Rectangle(480, 60*6, 4000, 200), SystemColors.ControlText, TextFormatFlags.HorizontalCenter);
                TextRenderer.DrawText(e.Graphics, "පින්වත් ස්වාමීන් වහන්සේලාගේ අසපු මාරුවීමේ නාම ලේඛනය", new Font("Iskoola Pota", 15 * 6, FontStyle.Bold), new Rectangle(480, 100*6, 4000, 200), SystemColors.ControlText, TextFormatFlags.HorizontalCenter);
                TextRenderer.DrawText(e.Graphics, fromToString, new Font("Iskoola Pota", 13 * 6), new Rectangle(480, 130*6, 4000, 200), SystemColors.ControlText, TextFormatFlags.HorizontalCenter);
                e.Graphics.DrawLine(new Pen(Brushes.DarkGray,2), new Point(10, 158), new Point(e.PageBounds.Width - 10, 158));

                TextRenderer.DrawText(e.Graphics, this.asapuwaName, new Font("Iskoola Pota", 16 * 6, FontStyle.Bold), new Rectangle(480, 165*6, 4000, 200), SystemColors.ControlText, TextFormatFlags.Left);
               
                // e.Graphics.DrawString(this.asapuwaName+" a", new Font("Iskoola Pota", 18, FontStyle.Bold), Brushes.Black, 100, 80);
                //e.Graphics.DrawString(fromToString, new Font("Iskoola Pota", 12), Brushes.Black, 100, 120);

              
            }

            int labelY = y; //  +imgeWidthHeight / 2;

            if (printImage)
            {
                labelY = y - 16 + imgeWidthHeight / 2;
            }

            // print footer
            TextRenderer.DrawText(e.Graphics, string.Concat(this.asapuwaName, "  (", fromToString, ")"), new Font("Iskoola Pota", 9 * 6, FontStyle.Bold), new Rectangle(10 * 6, (e.PageBounds.Height - 50) * 6, 8000, 200), SystemColors.ControlText, TextFormatFlags.Left);


            while (printIndex < bhikkuList.Count)
            {

                // page breaker 
                if (y > e.MarginBounds.Height + 65)
                {
                    y = 100;
                    e.HasMorePages = true;
                    return;
                }

                ChangeListItemBhikku bhikku = bhikkuList[printIndex++];

                e.Graphics.DrawString(printIndex + ".", new Font("Iskoola Pota", 16), Brushes.Black, 80, labelY);

                if (printImage && bhikku.imagePicbox.Image!=null)
                {
                    e.Graphics.DrawImageUnscaled(bhikku.imagePicbox.Image, new Point(130, y));
                }

                string postrString = MahamewnawaInfo.Common.Utility.GetPostString(bhikku.bInfo.Post);

                if (!string.IsNullOrEmpty(postrString))
                {
                    postrString = string.Concat("(", MahamewnawaInfo.Common.Utility.GetPostString(bhikku.bInfo.Post), ")");
                }


                // e.Graphics.DrawString(bhikku.bInfo.NameAssumedAtRobing, new Font("Iskoola Pota", 16), Brushes.Black, labelX, labelY);
                TextRenderer.DrawText(e.Graphics, bhikku.bInfo.NameAssumedAtRobing + (printImage ? "" : "  "+postrString), new Font("Iskoola Pota", 16 * 6), new Rectangle(labelX * 6, labelY * 6, 5000, 200), SystemColors.ControlText, TextFormatFlags.Left);

                // add post
                if (printImage)
                {
                    //e.Graphics.DrawString(MahamewnawaInfo.Common.Utility.GetPostStringLong(bhikku.bInfo.Post), new Font("Iskoola Pota", 12), Brushes.Black, labelX, labelY + 25);
                    TextRenderer.DrawText(e.Graphics, MahamewnawaInfo.Common.Utility.GetPostStringLong(bhikku.bInfo.Post), new Font("Iskoola Pota", 9 * 6), new Rectangle(labelX * 6, (labelY + 25) * 6, 5000, 200), SystemColors.ControlText, TextFormatFlags.Left);
                }

                if (printImage)
                {
                    y += imgeWidthHeight + 5;
                    labelY = y - 16 + imgeWidthHeight / 2;
                }
                else
                {
                    y += 30;
                    labelY = y;
                }
            }

            printIndex = 0;
        }
    }
}
