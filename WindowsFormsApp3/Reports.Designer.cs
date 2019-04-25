namespace WindowsFormsApp3
{
    partial class Reports
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
            this.total_Rec = new System.Windows.Forms.Label();
            this.total_amount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.DailyReports = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.total_bal = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // total_Rec
            // 
            this.total_Rec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_Rec.ForeColor = System.Drawing.Color.MidnightBlue;
            this.total_Rec.Location = new System.Drawing.Point(1011, 569);
            this.total_Rec.Name = "total_Rec";
            this.total_Rec.Size = new System.Drawing.Size(113, 15);
            this.total_Rec.TabIndex = 59;
            this.total_Rec.Text = "0";
            this.total_Rec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // total_amount
            // 
            this.total_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_amount.ForeColor = System.Drawing.Color.MidnightBlue;
            this.total_amount.Location = new System.Drawing.Point(884, 569);
            this.total_amount.Name = "total_amount";
            this.total_amount.Size = new System.Drawing.Size(109, 15);
            this.total_amount.TabIndex = 58;
            this.total_amount.Text = "0";
            this.total_amount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.total_amount.Click += new System.EventHandler(this.total_amount_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(948, 602);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 57;
            this.label3.Text = "Total";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(167, 569);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 32);
            this.button3.TabIndex = 5;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(701, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 54;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(452, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 53;
            this.label1.Text = "From";
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.CustomFormat = "yyyy-MM-dd";
            this.dateTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeTo.Location = new System.Drawing.Point(751, 57);
            this.dateTimeTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(167, 22);
            this.dateTimeTo.TabIndex = 2;
            this.dateTimeTo.Value = new System.DateTime(2019, 4, 16, 0, 0, 0, 0);
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.CustomFormat = "yyyy-MM-dd";
            this.dateTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeFrom.Location = new System.Drawing.Point(499, 57);
            this.dateTimeFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(167, 22);
            this.dateTimeFrom.TabIndex = 1;
            this.dateTimeFrom.Value = new System.DateTime(2019, 4, 16, 0, 0, 0, 0);
            // 
            // DailyReports
            // 
            this.DailyReports.Location = new System.Drawing.Point(947, 57);
            this.DailyReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DailyReports.Name = "DailyReports";
            this.DailyReports.Size = new System.Drawing.Size(87, 27);
            this.DailyReports.TabIndex = 3;
            this.DailyReports.Text = "Filter";
            this.DailyReports.UseVisualStyleBackColor = true;
            this.DailyReports.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 51;
            this.label4.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.nameBox.FormattingEnabled = true;
            this.nameBox.Location = new System.Drawing.Point(124, 58);
            this.nameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(313, 24);
            this.nameBox.Sorted = true;
            this.nameBox.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader10,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(75, 100);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1217, 462);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 145;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "File No./Job No.";
            this.columnHeader10.Width = 128;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Slip No/Cheque No";
            this.columnHeader1.Width = 142;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Remarks";
            this.columnHeader3.Width = 202;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Amount";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 95;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Received";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader8.Width = 108;
            // 
            // total_bal
            // 
            this.total_bal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_bal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.total_bal.Location = new System.Drawing.Point(1155, 569);
            this.total_bal.Name = "total_bal";
            this.total_bal.Size = new System.Drawing.Size(109, 15);
            this.total_bal.TabIndex = 63;
            this.total_bal.Text = "0";
            this.total_bal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Location = new System.Drawing.Point(1215, 602);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 18);
            this.label6.TabIndex = 64;
            this.label6.Text = "Balance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label7.Location = new System.Drawing.Point(1083, 602);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 18);
            this.label7.TabIndex = 65;
            this.label7.Text = "Receive";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 569);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Print Preview";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(379, 569);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(75, 569);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 32);
            this.button4.TabIndex = 66;
            this.button4.Text = "Remove";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 657);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.total_bal);
            this.Controls.Add(this.total_Rec);
            this.Controls.Add(this.total_amount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimeTo);
            this.Controls.Add(this.dateTimeFrom);
            this.Controls.Add(this.DailyReports);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label total_Rec;
        private System.Windows.Forms.Label total_amount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Button DailyReports;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox nameBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label total_bal;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}