using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class OfficePay : Form
    {

        public bool isUpdate = false;

        public OfficePay()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (payer.Text == "")
            {
                MessageBox.Show("Payer can not be empty");
                return;

            }
            if (!isUpdate)
            {
                SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                scn.Open();
                SQLiteCommand sq;
                

                sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks,officevoucher,title) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                   dateTimePicker1.Text,
                   payer.Text,
                   fileno.Text,
                   slipno.Text,
                   amount.Text,
                   receive.Text,
                   remarks.Text,
                   "1",
                   titleBox.Text), scn);

                sq.ExecuteNonQuery();

                MessageBox.Show("Data Saved successfully");
                Close();
            }
            else
            {
                SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                scn.Open();
                SQLiteCommand sq;

                sq = new SQLiteCommand("delete from pay where fileno = '" + fileno.Text + "' and payer = '" + payer.Text + "' and officevoucher = '1'", scn);
                sq.ExecuteNonQuery();


                sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks,officevoucher,title) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                   dateTimePicker1.Text,
                   payer.Text,
                   fileno.Text,
                   slipno.Text,
                   amount.Text,
                   receive.Text,
                   remarks.Text,
                   "1",
                   titleBox.Text), scn);

                sq.ExecuteNonQuery();

                MessageBox.Show("Data Saved successfully");
                Close();
            }
        }

        private void OfficePay_Load(object sender, EventArgs e)
        {
            payer.Items.AddRange(Sqlite.LoadClients());
            fileno.Items.AddRange(Sqlite.LoadFiles());
        }
    }
}
