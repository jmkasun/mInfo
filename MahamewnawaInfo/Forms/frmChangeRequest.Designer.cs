namespace MahamewnawaInfo.Forms
{
    partial class frmChangeRequest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeRequest));
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.requestAsapuwa1 = new System.Windows.Forms.ComboBox();
            this.requestAsapuwa3 = new System.Windows.Forms.ComboBox();
            this.requestAsapuwa2 = new System.Windows.Forms.ComboBox();
            this.cmbChangeList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asapuwa1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asapuwa2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asapuwa3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbName
            // 
            this.cmbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(12, 81);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(303, 26);
            this.cmbName.TabIndex = 1;
            // 
            // requestAsapuwa1
            // 
            this.requestAsapuwa1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.requestAsapuwa1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.requestAsapuwa1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestAsapuwa1.FormattingEnabled = true;
            this.requestAsapuwa1.Location = new System.Drawing.Point(366, 12);
            this.requestAsapuwa1.Name = "requestAsapuwa1";
            this.requestAsapuwa1.Size = new System.Drawing.Size(303, 26);
            this.requestAsapuwa1.TabIndex = 2;
            // 
            // requestAsapuwa3
            // 
            this.requestAsapuwa3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.requestAsapuwa3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.requestAsapuwa3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestAsapuwa3.FormattingEnabled = true;
            this.requestAsapuwa3.Location = new System.Drawing.Point(366, 81);
            this.requestAsapuwa3.Name = "requestAsapuwa3";
            this.requestAsapuwa3.Size = new System.Drawing.Size(303, 26);
            this.requestAsapuwa3.TabIndex = 4;
            // 
            // requestAsapuwa2
            // 
            this.requestAsapuwa2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.requestAsapuwa2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.requestAsapuwa2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestAsapuwa2.FormattingEnabled = true;
            this.requestAsapuwa2.Location = new System.Drawing.Point(366, 46);
            this.requestAsapuwa2.Name = "requestAsapuwa2";
            this.requestAsapuwa2.Size = new System.Drawing.Size(303, 26);
            this.requestAsapuwa2.TabIndex = 3;
            // 
            // cmbChangeList
            // 
            this.cmbChangeList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbChangeList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbChangeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChangeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChangeList.FormattingEnabled = true;
            this.cmbChangeList.Location = new System.Drawing.Point(12, 28);
            this.cmbChangeList.Name = "cmbChangeList";
            this.cmbChangeList.Size = new System.Drawing.Size(303, 26);
            this.cmbChangeList.TabIndex = 11;
            this.cmbChangeList.TabStop = false;
            this.cmbChangeList.SelectedIndexChanged += new System.EventHandler(this.cmbChangeList_SelectedIndexChanged);
            this.cmbChangeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cmbChangeList_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "අසපු මාරුවීමේ ලේඛණය";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "ස්වාමින්වහන්සේගේ නම";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.name,
            this.asapuwa1,
            this.asapuwa2,
            this.asapuwa3});
            this.dataGridView1.Location = new System.Drawing.Point(10, 114);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(943, 373);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(716, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 53);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "NameAssumedAtRobing";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.DefaultCellStyle = dataGridViewCellStyle2;
            this.name.FillWeight = 135.6378F;
            this.name.HeaderText = "ස්වාමින්වහන්සේගේ නම";
            this.name.MinimumWidth = 300;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // asapuwa1
            // 
            this.asapuwa1.DataPropertyName = "AsapuwaName1";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asapuwa1.DefaultCellStyle = dataGridViewCellStyle3;
            this.asapuwa1.FillWeight = 176.6497F;
            this.asapuwa1.HeaderText = "ඉල්ලීම් කළ අසපුව 1";
            this.asapuwa1.MinimumWidth = 200;
            this.asapuwa1.Name = "asapuwa1";
            this.asapuwa1.ReadOnly = true;
            // 
            // asapuwa2
            // 
            this.asapuwa2.DataPropertyName = "AsapuwaName2";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asapuwa2.DefaultCellStyle = dataGridViewCellStyle4;
            this.asapuwa2.FillWeight = 43.85622F;
            this.asapuwa2.HeaderText = "ඉල්ලීම් කළ අසපුව 2";
            this.asapuwa2.MinimumWidth = 200;
            this.asapuwa2.Name = "asapuwa2";
            this.asapuwa2.ReadOnly = true;
            // 
            // asapuwa3
            // 
            this.asapuwa3.DataPropertyName = "AsapuwaName3";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asapuwa3.DefaultCellStyle = dataGridViewCellStyle5;
            this.asapuwa3.FillWeight = 43.85622F;
            this.asapuwa3.HeaderText = "ඉල්ලීම් කළ අසපුව 3";
            this.asapuwa3.MinimumWidth = 200;
            this.asapuwa3.Name = "asapuwa3";
            this.asapuwa3.ReadOnly = true;
            // 
            // frmChangeRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 491);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbChangeList);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.requestAsapuwa2);
            this.Controls.Add(this.requestAsapuwa3);
            this.Controls.Add(this.requestAsapuwa1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChangeRequest";
            this.Text = "අසපු මාරුවීමේ ඉල්ලීම්";
            this.Load += new System.EventHandler(this.frmChangeRequest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.ComboBox requestAsapuwa1;
        private System.Windows.Forms.ComboBox requestAsapuwa3;
        private System.Windows.Forms.ComboBox requestAsapuwa2;
        private System.Windows.Forms.ComboBox cmbChangeList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn asapuwa1;
        private System.Windows.Forms.DataGridViewTextBoxColumn asapuwa2;
        private System.Windows.Forms.DataGridViewTextBoxColumn asapuwa3;
    }
}