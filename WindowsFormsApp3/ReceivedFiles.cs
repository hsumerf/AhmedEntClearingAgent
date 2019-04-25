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
    public partial class ReceivedFiles : Form
    {
        public ReceivedFiles()
        {
            InitializeComponent();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (!StartPage.Admin)
            {
                MessageBox.Show("You don't have rights to Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Pay receivings = new Pay();
                receivings.dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                receivings.payer.Text = listView1.SelectedItems[0].SubItems[3].Text;
                receivings.fileno.Text = listView1.SelectedItems[0].SubItems[1].Text;
                receivings.slipno.Text = listView1.SelectedItems[0].SubItems[9].Text;
                receivings.receive.Text = listView1.SelectedItems[0].SubItems[7].Text;
                receivings.remarks.Text = listView1.SelectedItems[0].SubItems[10].Text;
                receivings.Show();
                this.Close();
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            // float totalAmount = 0, total = 0;
            String[] report = new String[11];
            List<String[]> reportList = new List<String[]>();

            SQLiteDataReader dr;

            if (nameBox.Text == "")
            {
                MessageBox.Show("Account Name can not be empty");
                return;

            }
            else
            {
                sq = new SQLiteCommand("select files.date,files.fileno,files.invoiceno,files.name,files.customerrefno,files.qtycontainer,files.invamount,pay.received as receive,pay.slipno,pay.remarks from files inner join pay on files.fileno = pay.fileno  where pay.payer = '" + nameBox.Text + "' and files.name = '" + nameBox.Text + "' and receiveformcheck = '1'", scn);
                dr = sq.ExecuteReader();
                // String tot ="10.5", rec = "5.5";
                while (dr.Read())
                {
                    //total = Convert.ToSingle(dr["total"].ToString());

                    report[0] = dr["date"].ToString();
                    report[1] = dr["fileno"].ToString();
                    report[2] = dr["invoiceno"].ToString();
                    report[3] = dr["name"].ToString();
                    report[4] = dr["customerrefno"].ToString();
                    report[5] = dr["qtycontainer"].ToString();
                    report[6] = Math.Round(Convert.ToDecimal(dr["invamount"])).ToString();
                    report[7] = Math.Round(Convert.ToDecimal(dr["receive"])).ToString();
                    report[8] = Math.Round((float.Parse(report[6]) - float.Parse(report[7]))).ToString();
                    report[9] = dr["slipno"].ToString();
                    report[10] = dr["remarks"].ToString();
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
                                                            report[9],
                                                            report[10]}));

                    // dr["total"].ToString()}));
                    // totalAmount += float.Parse(dr["total"].ToString());
                    // listView1.Items.Add(new ListViewItem(new[] { reportList[i][0], reportList[i][1], reportList[i][2], reportList[i][3], reportList[i][4], reportList[i][5] }));

                }
            }
        }

        private void ReceivedFiles_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }
    }
}
