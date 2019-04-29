namespace MahamewnawaInfo.Reporting
{
    partial class frm_CustomReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CustomReport));
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filter2Todate = new System.Windows.Forms.DateTimePicker();
            this.filter2FromDate = new System.Windows.Forms.DateTimePicker();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.filter2Combo = new System.Windows.Forms.ComboBox();
            this.filter2text = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filter1ToDate = new System.Windows.Forms.DateTimePicker();
            this.filter1FromDate = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.filter1Combo = new System.Windows.Forms.ComboBox();
            this.filter1Text = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(931, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.filter2Todate);
            this.groupBox2.Controls.Add(this.filter2FromDate);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.filter2Combo);
            this.groupBox2.Controls.Add(this.filter2text);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(465, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 65);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter 2";
            // 
            // filter2Todate
            // 
            this.filter2Todate.Location = new System.Drawing.Point(176, 41);
            this.filter2Todate.Name = "filter2Todate";
            this.filter2Todate.Size = new System.Drawing.Size(200, 21);
            this.filter2Todate.TabIndex = 2;
            this.filter2Todate.Visible = false;
            // 
            // filter2FromDate
            // 
            this.filter2FromDate.Location = new System.Drawing.Point(176, 17);
            this.filter2FromDate.Name = "filter2FromDate";
            this.filter2FromDate.Size = new System.Drawing.Size(200, 21);
            this.filter2FromDate.TabIndex = 2;
            this.filter2FromDate.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "-------- N/A --------",
            "ස්වාමින්වහන්සේගේ නම",
            "උපසම්පදා/සාමනේර",
            "පැවිදි වූ දිනය",
            "පැවිදි වූ විහාරයේ නම",
            "උපසම්පදා වූ දිනය",
            "උපසම්පදා කළ ස්ථානය",
            "තනතුර",
            "වැඩසිටින අසපුව",
            "උපන් දිනය",
            "උපන් රට",
            "ලේ වර්ගය",
            "භාෂා හැකියාවන්",
            "හැකියාවන්",
            "වර්තමානයේ ..."});
            this.comboBox2.Location = new System.Drawing.Point(6, 17);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(166, 21);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // filter2Combo
            // 
            this.filter2Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filter2Combo.FormattingEnabled = true;
            this.filter2Combo.Location = new System.Drawing.Point(176, 17);
            this.filter2Combo.Name = "filter2Combo";
            this.filter2Combo.Size = new System.Drawing.Size(282, 21);
            this.filter2Combo.TabIndex = 0;
            this.filter2Combo.Visible = false;
            // 
            // filter2text
            // 
            this.filter2text.Location = new System.Drawing.Point(176, 17);
            this.filter2text.Name = "filter2text";
            this.filter2text.Size = new System.Drawing.Size(282, 21);
            this.filter2text.TabIndex = 1;
            this.filter2text.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filter1ToDate);
            this.groupBox1.Controls.Add(this.filter1FromDate);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.filter1Combo);
            this.groupBox1.Controls.Add(this.filter1Text);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter 1";
            // 
            // filter1ToDate
            // 
            this.filter1ToDate.Location = new System.Drawing.Point(173, 41);
            this.filter1ToDate.Name = "filter1ToDate";
            this.filter1ToDate.Size = new System.Drawing.Size(200, 21);
            this.filter1ToDate.TabIndex = 2;
            this.filter1ToDate.Visible = false;
            // 
            // filter1FromDate
            // 
            this.filter1FromDate.Location = new System.Drawing.Point(173, 17);
            this.filter1FromDate.Name = "filter1FromDate";
            this.filter1FromDate.Size = new System.Drawing.Size(200, 21);
            this.filter1FromDate.TabIndex = 2;
            this.filter1FromDate.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "-------- N/A --------",
            "ස්වාමින්වහන්සේගේ නම",
            "උපසම්පදා/සාමනේර",
            "පැවිදි වූ දිනය",
            "පැවිදි වූ විහාරයේ නම",
            "උපසම්පදා වූ දිනය",
            "උපසම්පදා කළ ස්ථානය",
            "තනතුර",
            "වැඩසිටින අසපුව",
            "උපන් දිනය",
            "උපන් රට",
            "ලේ වර්ගය",
            "භාෂා හැකියාවන්",
            "හැකියාවන්",
            "වර්තමානයේ ..."});
            this.comboBox1.Location = new System.Drawing.Point(3, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(166, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // filter1Combo
            // 
            this.filter1Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filter1Combo.FormattingEnabled = true;
            this.filter1Combo.Location = new System.Drawing.Point(173, 17);
            this.filter1Combo.Name = "filter1Combo";
            this.filter1Combo.Size = new System.Drawing.Size(282, 21);
            this.filter1Combo.TabIndex = 0;
            this.filter1Combo.Visible = false;
            // 
            // filter1Text
            // 
            this.filter1Text.Location = new System.Drawing.Point(173, 17);
            this.filter1Text.Name = "filter1Text";
            this.filter1Text.Size = new System.Drawing.Size(282, 21);
            this.filter1Text.TabIndex = 1;
            this.filter1Text.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1056, 525);
            this.dataGridView1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(995, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 53);
            this.button2.TabIndex = 1;
            this.button2.Text = "Report";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frm_CustomReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1061, 611);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_CustomReport";
            this.Text = "Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DateTimePicker filter2Todate;
        private System.Windows.Forms.DateTimePicker filter2FromDate;
        private System.Windows.Forms.TextBox filter2text;
        private System.Windows.Forms.DateTimePicker filter1ToDate;
        private System.Windows.Forms.DateTimePicker filter1FromDate;
        private System.Windows.Forms.TextBox filter1Text;
        private System.Windows.Forms.ComboBox filter2Combo;
        private System.Windows.Forms.ComboBox filter1Combo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}