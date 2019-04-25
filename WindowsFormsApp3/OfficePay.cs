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
            
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks,officevoucher) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
               dateTimePicker1.Text,
               payer.Text,
               fileno.Text+" Ref",
               slipno.Text,
               amount.Text,
               receive.Text,
               remarks.Text,
               "1"), scn);

            sq.ExecuteNonQuery();

            MessageBox.Show("Data Saved successfully");
            Close();
        }

        private void OfficePay_Load(object sender, EventArgs e)
        {
            payer.Items.AddRange(Sqlite.LoadClients());
            fileno.Items.AddRange(Sqlite.LoadFiles());
        }
    }
}
