namespace WindowsFormsApp3
{
    partial class ProductKey
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
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ProdKeyLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.VerifyBtn = new System.Windows.Forms.Button();
            this.ProductKeyTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(19, 33);
            this.textBox3.MaxLength = 4;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(60, 26);
            this.textBox3.TabIndex = 1;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(85, 33);
            this.textBox2.MaxLength = 4;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(60, 26);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(151, 33);
            this.textBox1.MaxLength = 4;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ProdKeyLabel
            // 
            this.ProdKeyLabel.AutoSize = true;
            this.ProdKeyLabel.Location = new System.Drawing.Point(15, 98);
            this.ProdKeyLabel.Name = "ProdKeyLabel";
            this.ProdKeyLabel.Size = new System.Drawing.Size(133, 13);
            this.ProdKeyLabel.TabIndex = 6;
            this.ProdKeyLabel.TabStop = true;
            this.ProdKeyLabel.Text = "Don\'t have a product key?";
            this.ProdKeyLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ProdKeyLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter Prodct Key";
            // 
            // VerifyBtn
            // 
            this.VerifyBtn.BackColor = System.Drawing.Color.Transparent;
            this.VerifyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VerifyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyBtn.Location = new System.Drawing.Point(217, 65);
            this.VerifyBtn.Name = "VerifyBtn";
            this.VerifyBtn.Size = new System.Drawing.Size(60, 31);
            this.VerifyBtn.TabIndex = 5;
            this.VerifyBtn.Text = "Verify";
            this.VerifyBtn.UseVisualStyleBackColor = false;
            this.VerifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // ProductKeyTxtBox
            // 
            this.ProductKeyTxtBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ProductKeyTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductKeyTxtBox.Location = new System.Drawing.Point(217, 33);
            this.ProductKeyTxtBox.MaxLength = 4;
            this.ProductKeyTxtBox.Name = "ProductKeyTxtBox";
            this.ProductKeyTxtBox.Size = new System.Drawing.Size(60, 26);
            this.ProductKeyTxtBox.TabIndex = 4;
            this.ProductKeyTxtBox.TextChanged += new System.EventHandler(this.ProductKeyTxtBox_TextChanged);
            this.ProductKeyTxtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductKeyTxtBox_KeyDown);
            // 
            // ProductKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 117);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ProdKeyLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VerifyBtn);
            this.Controls.Add(this.ProductKeyTxtBox);
            this.Name = "ProductKey";
            this.Text = "ProductKey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductKey_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel ProdKeyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button VerifyBtn;
        private System.Windows.Forms.TextBox ProductKeyTxtBox;
    }
}