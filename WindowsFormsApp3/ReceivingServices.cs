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
    public partial class ReceivingServices : Form
    {
        public ReceivingServices()
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
            sq = new SQLiteCommand("delete from pay where fileno = '" + fileno.Text + "' and payer = '" + payer.Text + "' and debitnote='1' ", scn);
           // sq = new SQLiteCommand("delete from pay where fileno = '" + fileno.Text + "" Ref"' " AND payer = '"payer, scn);
            sq.ExecuteNonQuery();
            sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,customerrefno,servicename,received,chequeno,chequedate,amount,remarks,debitnote) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
               dateTimePicker1.Text,
               payer.Text,
               fileno.Text,
               CustomerRefNo.Text,
               ServiceName.Text,
               DebitAmount.Text,
               ChequeNo.Text,
               ChequeDate.Text,
               CreditAmount.Text,
               Remarks.Text,
               "1"), scn);
            

            sq.ExecuteNonQuery();

            MessageBox.Show("Data Saved successfully");
            Close();
        }

        private void ReceivingServices_Load(object sender, EventArgs e)
        {
            fileno.Items.AddRange(Sqlite.LoadFiles());
            payer.Items.AddRange(Sqlite.LoadClients());
        }
    }
}
