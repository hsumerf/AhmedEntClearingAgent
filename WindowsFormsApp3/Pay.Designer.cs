namespace WindowsFormsApp3
{
    partial class Pay
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
            this.payer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.remarks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.slipno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.receive = new System.Windows.Forms.TextBox();
            this.fileno = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // payer
            // 
            this.payer.FormattingEnabled = true;
            this.payer.Location = new System.Drawing.Point(185, 79);
            this.payer.Name = "payer";
            this.payer.Size = new System.Drawing.Size(185, 21);
            this.payer.Sorted = true;
            this.payer.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(185, 36);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(185, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2019, 4, 15, 0, 0, 0, 0);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(294, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Remarks";
            // 
            // remarks
            // 
            this.remarks.Location = new System.Drawing.Point(184, 221);
            this.remarks.Multiline = true;
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(185, 53);
            this.remarks.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 81;
            this.label6.Text = "File No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "Slip No./Cheque No.";
            // 
            // slipno
            // 
            this.slipno.Location = new System.Drawing.Point(182, 153);
            this.slipno.Name = "slipno";
            this.slipno.Size = new System.Drawing.Size(185, 20);
            this.slipno.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Receive";
            // 
            // receive
            // 
            this.receive.Location = new System.Drawing.Point(182, 189);
            this.receive.Name = "receive";
            this.receive.Size = new System.Drawing.Size(185, 20);
            this.receive.TabIndex = 4;
            this.receive.Text = "0";
            // 
            // fileno
            // 
            this.fileno.FormattingEnabled = true;
            this.fileno.Location = new System.Drawing.Point(185, 118);
            this.fileno.Name = "fileno";
            this.fileno.Size = new System.Drawing.Size(185, 21);
            this.fileno.Sorted = true;
            this.fileno.TabIndex = 83;
            // 
            // Pay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 415);
            this.Controls.Add(this.fileno);
            this.Controls.Add(this.receive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.slipno);
            this.Controls.Add(this.payer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarks);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Pay";
            this.Text = "Receivings";
            this.Load += new System.EventHandler(this.Pay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox remarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox slipno;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox receive;
        public System.Windows.Forms.ComboBox payer;
        public System.Windows.Forms.ComboBox fileno;
    }
}