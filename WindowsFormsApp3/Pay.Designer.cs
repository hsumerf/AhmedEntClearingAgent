namespace WindowsFormsApp3
{
    partial class Receivings
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
            this.payer.Location = new System.Drawing.Point(247, 97);
            this.payer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.payer.Name = "payer";
            this.payer.Size = new System.Drawing.Size(245, 24);
            this.payer.Sorted = true;
            this.payer.TabIndex = 1;
            this.payer.SelectedIndexChanged += new System.EventHandler(this.payer_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 72;
            this.label5.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(247, 44);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(245, 22);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2019, 4, 15, 0, 0, 0, 0);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(392, 361);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 7;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 272);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 66;
            this.label1.Text = "Remarks";
            // 
            // remarks
            // 
            this.remarks.Location = new System.Drawing.Point(245, 272);
            this.remarks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.remarks.Multiline = true;
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(245, 64);
            this.remarks.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 64;
            this.label2.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 145);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 81;
            this.label6.Text = "File No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 80;
            this.label4.Text = "Slip No./Cheque No.";
            // 
            // slipno
            // 
            this.slipno.Location = new System.Drawing.Point(243, 188);
            this.slipno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slipno.Name = "slipno";
            this.slipno.Size = new System.Drawing.Size(245, 22);
            this.slipno.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 233);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 82;
            this.label7.Text = "Receive";
            // 
            // receive
            // 
            this.receive.Location = new System.Drawing.Point(243, 233);
            this.receive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.receive.Name = "receive";
            this.receive.Size = new System.Drawing.Size(245, 22);
            this.receive.TabIndex = 4;
            this.receive.Text = "0";
            // 
            // fileno
            // 
            this.fileno.FormattingEnabled = true;
            this.fileno.Location = new System.Drawing.Point(247, 145);
            this.fileno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fileno.Name = "fileno";
            this.fileno.Size = new System.Drawing.Size(245, 24);
            this.fileno.Sorted = true;
            this.fileno.TabIndex = 83;
            this.fileno.SelectedIndexChanged += new System.EventHandler(this.fileno_SelectedIndexChanged);
            // 
            // Receivings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 511);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Receivings";
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