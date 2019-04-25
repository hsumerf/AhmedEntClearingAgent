using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class ProductKey : Form
    {
        public ProductKey()
        {
            InitializeComponent();
        }
        

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            if (ProductKeyTxtBox.Text.EndsWith(DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()))
            {
                StartPage startPage = new StartPage();
                startPage.Show();
                Properties.Settings.Default.first = false;
                Properties.Settings.Default.Save();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Product Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ProdKeyLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contact: 03222832789", "Get Product Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int length = textBox3.TextLength;
            if (length == textBox3.MaxLength)
                textBox2.Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int length = textBox2.TextLength;
            if (length == textBox2.MaxLength)
                textBox1.Select();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int length = textBox1.TextLength;
            if (length == textBox1.MaxLength)
                ProductKeyTxtBox.Select();
        }

        private void ProductKeyTxtBox_TextChanged(object sender, EventArgs e)
        {
            int length = ProductKeyTxtBox.TextLength;
            if (length == ProductKeyTxtBox.MaxLength)
                VerifyBtn.Select();
        }

        private void ProductKeyTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                VerifyBtn_Click(this, new EventArgs());
            }
        }
    }
}
