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
    public partial class Receivings : Form
    {
        public Receivings()
        {
            InitializeComponent();
        }
        string tempFileNo;
        string tempPayer;

        public bool isUpdate = false;
        private void Pay_Load(object sender, EventArgs e)
        {


             tempFileNo = fileno.Text;
             tempPayer = payer.Text;

            payer.Items.AddRange(Sqlite.LoadClients());
            fileno.Items.AddRange(Sqlite.LoadFiles(payer.Text));

            //SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            //scn.Open();

            //SQLiteCommand sq = new SQLiteCommand("select received, remarks from pay where fileno = '" + fileno.Text + "' and receivecheck = '1'", scn);
            //SQLiteDataReader dr = sq.ExecuteReader();

            //while (dr.Read())
            //{
            //    receive.Text = dr["received"].ToString();
            //    remarks.Text = dr["remarks"].ToString();
            //}

            //scn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (payer.Text == "")
            {
                MessageBox.Show("Payer can not be empty");
                return;
            }
            else
            {
                if (!isUpdate)
                {
                    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                    scn.Open();
                    SQLiteCommand sq;
                    sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks, receiveformcheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                       dateTimePicker1.Text,
                       payer.Text,
                       fileno.Text,
                       slipno.Text,
                       "0",
                       receive.Text,
                       remarks.Text,
                       "1"), scn);

                    
                    sq.ExecuteNonQuery();

                    MessageBox.Show("Data Saved successfully");
                    scn.Close();
                    this.Close();
                }
                else if (isUpdate)
                {
                    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                    scn.Open();
                    SQLiteCommand sq;

                    sq = new SQLiteCommand(String.Format("delete from pay where fileno = '" + tempFileNo + "' and payer = '" + tempPayer +"' and receiveformcheck = '1'"), scn);
                    sq.ExecuteNonQuery();

                    sq = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks, receiveformcheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                       dateTimePicker1.Text,
                       payer.Text,
                       fileno.Text,
                       slipno.Text,
                       "0",
                       receive.Text,
                       remarks.Text,
                       "1"), scn);

                    sq.ExecuteNonQuery();

                    MessageBox.Show("Data Saved successfully");
                    scn.Close();
                    this.Close();

                }
            }
        }

        private void fileno_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void payer_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileno.Items.AddRange(Sqlite.LoadFiles(payer.Text));
        }
    }
}
