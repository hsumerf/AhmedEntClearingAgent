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
    public partial class DebitNote : Form
    {
        public DebitNote()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            String[] report = new String[10];
            List<String[]> reportList = new List<String[]>();

            try
            {

                SQLiteDataReader dr;

                if (nameBox.Text == "")
                {
                    MessageBox.Show("Account Name can not be empty");
                    return;

                }
                else
                    sq = new SQLiteCommand("select fileno,customerrefno,date,servicename,payer,received,chequeno,chequedate,amount,remarks from pay where payer = '" + nameBox.Text + "' and debitnote='1'", scn);
                dr = sq.ExecuteReader();
                while (dr.Read())
                {
                    //total = Convert.ToSingle(dr["total"].ToString());
                    report[0] = dr["fileno"].ToString();
                    report[1] = dr["customerrefno"].ToString();
                    report[2] = dr["date"].ToString();
                    report[3] = dr["servicename"].ToString();
                    report[4] = dr["payer"].ToString();
                    report[5] = dr["received"].ToString();
                    report[6] = dr["chequeno"].ToString();
                    report[7] = dr["chequedate"].ToString();
                    report[8] = dr["amount"].ToString();
                    report[9] = dr["remarks"].ToString();
                    // reportList.Add(new String[] { report[0], report[1], report[2], report[3], report[4], report[5], report[6], report[7], report[8], report[9], report[10] });
                    listView1.Items.Add(new ListViewItem(new[] {report[0],
                                                            report[1],
                                                            report[2],
                                                            report[3],
                                                            report[4],
                                                            report[5],
                                                            report[6],
                                                            report[7],
                                                            report[8],
                                                            report[9]}));
                }
                sq = new SQLiteCommand("select sum(received) as totaldebit,sum(amount) as totalcredit from pay where payer = '" + nameBox.Text + "' and debitnote = '1'", scn);
                dr = sq.ExecuteReader();
                // float tot = "10.5f", rec = "5.5f";
                while (dr.Read())
                {
                    //total = Convert.ToSingle(dr["total"].ToString());
                    totaldebit.Text = dr["totaldebit"].ToString();
                    totalcredit.Text = dr["totalcredit"].ToString();
                    balance.Text = Convert.ToString(Convert.ToInt32(totaldebit.Text) - Convert.ToInt32(totalcredit.Text));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There are no services provided by this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(listView1.SelectedItems[0].SubItems[2].Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.SelectedItems[0].ToString());
            if(listView1.SelectedItems.Count > 0)
            {
               
                ReceivingServices receivingservices = new ReceivingServices();
                receivingservices.dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                receivingservices.payer.Text = listView1.SelectedItems[0].SubItems[4].Text;
                receivingservices.fileno.Text = listView1.SelectedItems[0].SubItems[0].Text;
                receivingservices.CustomerRefNo.Text= listView1.SelectedItems[0].SubItems[1].Text;
                receivingservices.ServiceName.Text = listView1.SelectedItems[0].SubItems[3].Text;
                receivingservices.DebitAmount.Text= listView1.SelectedItems[0].SubItems[5].Text;
                receivingservices.ChequeNo.Text= listView1.SelectedItems[0].SubItems[6].Text;
                receivingservices.ChequeDate.Text = listView1.SelectedItems[0].SubItems[7].Text;
                receivingservices.CreditAmount.Text= listView1.SelectedItems[0].SubItems[8].Text;
                receivingservices.Remarks.Text= listView1.SelectedItems[0].SubItems[9].Text;
                
                receivingservices.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sqlite.ExportToExcel("DebitNote.csv", listView1);
        }

        private void DebitNote_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                scn.Open();
                SQLiteCommand sq;
                sq = new SQLiteCommand("delete from pay where fileno = '" + listView1.SelectedItems[0].SubItems[0].Text + "' and payer = '" + listView1.SelectedItems[0].SubItems[4].Text + "' and debitnote='1' ", scn);
                // sq = new SQLiteCommand("delete from pay where fileno = '" + fileno.Text + "" Ref"' " AND payer = '"payer, scn);
                sq.ExecuteNonQuery();
            }
        }
    }
}
