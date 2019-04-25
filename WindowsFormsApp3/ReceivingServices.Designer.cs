namespace WindowsFormsApp3
{
    partial class ReceivingServices
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
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CustomerRefNo = new System.Windows.Forms.TextBox();
            this.payer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DebitAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ChequeNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CreditAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Remarks = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ServiceName = new System.Windows.Forms.TextBox();
            this.fileno = new System.Windows.Forms.ComboBox();
            this.ChequeDate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 91;
            this.label6.Text = "File No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 90;
            this.label4.Text = "Customer Ref no.";
            // 
            // CustomerRefNo
            // 
            this.CustomerRefNo.Location = new System.Drawing.Point(318, 150);
            this.CustomerRefNo.Margin = new System.Windows.Forms.Padding(4);
            this.CustomerRefNo.Name = "CustomerRefNo";
            this.CustomerRefNo.Size = new System.Drawing.Size(245, 22);
            this.CustomerRefNo.TabIndex = 81;
            // 
            // payer
            // 
            this.payer.FormattingEnabled = true;
            this.payer.Location = new System.Drawing.Point(318, 72);
            this.payer.Margin = new System.Windows.Forms.Padding(4);
            this.payer.Name = "payer";
            this.payer.Size = new System.Drawing.Size(245, 24);
            this.payer.Sorted = true;
            this.payer.TabIndex = 78;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 89;
            this.label5.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(318, 35);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(245, 22);
            this.dateTimePicker1.TabIndex = 79;
            this.dateTimePicker1.Value = new System.DateTime(2019, 3, 30, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 88;
            this.label3.Text = "BL/AWS NO.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 465);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 84;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(467, 465);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 85;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 227);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 87;
            this.label1.Text = "Amount Debit";
            // 
            // DebitAmount
            // 
            this.DebitAmount.Location = new System.Drawing.Point(318, 224);
            this.DebitAmount.Margin = new System.Windows.Forms.Padding(4);
            this.DebitAmount.Name = "DebitAmount";
            this.DebitAmount.Size = new System.Drawing.Size(245, 22);
            this.DebitAmount.TabIndex = 83;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 86;
            this.label2.Text = "Account Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 272);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 121;
            this.label7.Text = "Cheque No.";
            // 
            // ChequeNo
            // 
            this.ChequeNo.Location = new System.Drawing.Point(318, 261);
            this.ChequeNo.Margin = new System.Windows.Forms.Padding(4);
            this.ChequeNo.Name = "ChequeNo";
            this.ChequeNo.Size = new System.Drawing.Size(245, 22);
            this.ChequeNo.TabIndex = 120;
            this.ChequeNo.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(170, 338);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 17);
            this.label8.TabIndex = 119;
            this.label8.Text = "Credit Amount:";
            // 
            // CreditAmount
            // 
            this.CreditAmount.Location = new System.Drawing.Point(318, 335);
            this.CreditAmount.Margin = new System.Windows.Forms.Padding(4);
            this.CreditAmount.Name = "CreditAmount";
            this.CreditAmount.Size = new System.Drawing.Size(245, 22);
            this.CreditAmount.TabIndex = 116;
            this.CreditAmount.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 375);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 17);
            this.label9.TabIndex = 118;
            this.label9.Text = "Remarks";
            // 
            // Remarks
            // 
            this.Remarks.Location = new System.Drawing.Point(318, 372);
            this.Remarks.Margin = new System.Windows.Forms.Padding(4);
            this.Remarks.Multiline = true;
            this.Remarks.Name = "Remarks";
            this.Remarks.Size = new System.Drawing.Size(245, 64);
            this.Remarks.TabIndex = 117;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(170, 307);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 17);
            this.label10.TabIndex = 123;
            this.label10.Text = "Date";
            // 
            // ServiceName
            // 
            this.ServiceName.Location = new System.Drawing.Point(318, 187);
            this.ServiceName.Margin = new System.Windows.Forms.Padding(4);
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.Size = new System.Drawing.Size(245, 22);
            this.ServiceName.TabIndex = 124;
            // 
            // fileno
            // 
            this.fileno.FormattingEnabled = true;
            this.fileno.Location = new System.Drawing.Point(318, 111);
            this.fileno.Margin = new System.Windows.Forms.Padding(4);
            this.fileno.Name = "fileno";
            this.fileno.Size = new System.Drawing.Size(245, 24);
            this.fileno.Sorted = true;
            this.fileno.TabIndex = 125;
            // 
            // ChequeDate
            // 
            this.ChequeDate.Location = new System.Drawing.Point(318, 298);
            this.ChequeDate.Margin = new System.Windows.Forms.Padding(4);
            this.ChequeDate.Name = "ChequeDate";
            this.ChequeDate.Size = new System.Drawing.Size(245, 22);
            this.ChequeDate.TabIndex = 126;
            // 
            // ReceivingServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 534);
            this.Controls.Add(this.ChequeDate);
            this.Controls.Add(this.fileno);
            this.Controls.Add(this.ServiceName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ChequeNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CreditAmount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Remarks);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CustomerRefNo);
            this.Controls.Add(this.payer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DebitAmount);
            this.Controls.Add(this.label2);
            this.Name = "ReceivingServices";
            this.Text = "ReceivingServices";
            this.Load += new System.EventHandler(this.ReceivingServices_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox CustomerRefNo;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox DebitAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox ChequeNo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox CreditAmount;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox Remarks;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox ServiceName;
        public System.Windows.Forms.ComboBox payer;
        public System.Windows.Forms.ComboBox fileno;
        public System.Windows.Forms.TextBox ChequeDate;
    }
}