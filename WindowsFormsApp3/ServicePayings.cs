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
    public partial class ServicePayings : Form
    {
        public ServicePayings()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (payer.Text == "")
            {
                MessageBox.Show("Account Name can not be empty");
                return;

            }

            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            sq = new SQLiteCommand(String.Format("insert into debitnote (date,clientname,fileno,creditamount,chequeno,remarks) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
               dateTimePicker1.Text,
               payer.Text,
               fileno.Text + " Ref",
               CreditAmount.Text,
               ChequeNo.Text,
               Remarks.Text), scn);


            sq.ExecuteNonQuery();

            MessageBox.Show("Data Saved successfully");
            Close();
        }
    }
}
