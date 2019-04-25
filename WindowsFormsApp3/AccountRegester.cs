using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp3
{
    public partial class AccountRegester : Form
    {
        public AccountRegester()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subComboBox_SelectedIndexChanged(this, e);

            /*listView1.Items.Clear();
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
                sq = new SQLiteCommand("select files.date,files.fileno,files.invoiceno,files.name,files.customerrefno,files.qtycontainer,files.invamount,sum(pay.received) as receive,pay.slipno,pay.remarks from files inner join pay on files.fileno = pay.fileno  where pay.payer = '" + nameBox.Text + "' and files.name = '" + nameBox.Text + "' group by files.fileno", scn);
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
                    report[6] = dr["invamount"].ToString();
                    report[7] = dr["receive"].ToString();
                    report[8] = (float.Parse(report[6]) - float.Parse(report[7])).ToString();
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
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sqlite.ExportToExcel("AccountRegester.csv", listView1);

        }

        private void AccountRegester_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }

        private void mainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(@"data source = main.db");
            SQLiteCommand sqliteCommand;
            SQLiteDataReader sqliteDataReader;

            if (mainComboBox.Text == "Customer Ref No")
            {
                subComboBox.Items.Clear();
                subComboBox.Text = "";

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select customerrefno from pay", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                    subComboBox.Items.Add(sqliteDataReader["customerrefno"]);

                deleteEmptySpacesFromComboBox();

                sqliteConnection.Close();


            }

            else if (mainComboBox.Text == "File No")
            {
                subComboBox.Items.Clear();
                subComboBox.Text = "";

                //sqliteConnection.Open();

                //sqliteCommand = new SQLiteCommand("select fileno from pay", sqliteConnection);
                //sqliteDataReader = sqliteCommand.ExecuteReader();

                //while (sqliteDataReader.Read())
                //    subComboBox.Items.Add(sqliteDataReader["fileno"]);

                //deleteEmptySpacesFromComboBox();

                subComboBox.Items.AddRange(Sqlite.LoadFiles());

                sqliteConnection.Close();
            }

            else if (mainComboBox.Text == "Client Name")
            {
                subComboBox.Items.Clear();
                subComboBox.Text = "";

                //sqliteConnection.Open();

                //sqliteCommand = new SQLiteCommand("select payer from pay", sqliteConnection);
                //sqliteDataReader = sqliteCommand.ExecuteReader();

                //while (sqliteDataReader.Read())
                //    subComboBox.Items.Add(sqliteDataReader["payer"]);

                //deleteEmptySpacesFromComboBox();

                subComboBox.Items.AddRange(Sqlite.LoadClients());

                sqliteConnection.Close();
            }
        }

        private void deleteEmptySpacesFromComboBox()
        {
            int i = 0;
            while (i < subComboBox.Items.Count)
            {
                if (subComboBox.Items[i].Equals(""))
                    subComboBox.Items.RemoveAt(i);
                i++;
            }
        }

        private void subComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(@"data source = main.db");
            SQLiteCommand sqliteCommand;
            SQLiteDataReader sqliteDataReader;

            if (mainComboBox.Text == "Customer Ref No")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select pay.date, pay.fileno, files.invoiceno as invoiceno, pay.payer, pay.customerrefno, files.qtycontainer as qtycontainer, files.invamount as invamount, sum(pay.received) as receive, pay.chequeno, pay.remarks  from pay inner join files on pay.fileno = pay.fileno where pay.customerrefno = '" + subComboBox.Text + "' group by pay.customerrefno", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                listView1.Items.Clear();

                while (sqliteDataReader.Read())
                {
                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["date"].ToString(),
                        sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["payer"].ToString(),
                        sqliteDataReader["customerrefno"].ToString(),
                        sqliteDataReader["qtycontainer"].ToString(),
                        sqliteDataReader["invamount"].ToString(),
                        sqliteDataReader["receive"].ToString(),
                        Convert.ToString(Convert.ToInt32(sqliteDataReader["invamount"]) + Convert.ToInt32(sqliteDataReader["receive"])),
                        sqliteDataReader["chequeno"].ToString(),
                        sqliteDataReader["remarks"].ToString()}));
                }


                sqliteConnection.Close();
            }

            if (mainComboBox.Text == "File No")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select pay.date, pay.fileno, files.invoiceno as invoiceno, pay.payer, pay.customerrefno, files.qtycontainer as qtycontainer, files.invamount as invamount, sum(pay.received) as receive, pay.chequeno, pay.remarks  from pay inner join files on pay.fileno = pay.fileno where pay.fileno = '" + subComboBox.Text + "' group by pay.fileno", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                listView1.Items.Clear();

                while (sqliteDataReader.Read())
                {

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["date"].ToString(),
                        sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["payer"].ToString(),
                        sqliteDataReader["customerrefno"].ToString(),
                        sqliteDataReader["qtycontainer"].ToString(),
                        sqliteDataReader["invamount"].ToString(),
                        sqliteDataReader["receive"].ToString(),
                        Convert.ToString(Convert.ToInt32(sqliteDataReader["invamount"]) + Convert.ToInt32(sqliteDataReader["receive"])),
                        sqliteDataReader["chequeno"].ToString(),
                        sqliteDataReader["remarks"].ToString()}));

                }


                sqliteConnection.Close();
            }

            if (mainComboBox.Text == "Client Name")
            {
                string[] report = new string[subComboBox.Items.Count];

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select pay.date, pay.fileno, files.invoiceno as invoiceno, pay.payer, pay.customerrefno, files.qtycontainer as qtycontainer, files.invamount as invamount, sum(pay.received) as receive, pay.chequeno, pay.remarks  from pay inner join files on files.fileno = pay.fileno where pay.payer = '" + subComboBox.Text + "' group by pay.fileno", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                listView1.Items.Clear();

                SQLiteCommand comm = new SQLiteCommand("select chequeno from pay inner join files on files.fileno = pay.fileno where payer = '" + subComboBox.Text + "' and files.fileno = pay.fileno and receiveformcheck = '1'", sqliteConnection);
                SQLiteDataReader dr = comm.ExecuteReader();

                while (sqliteDataReader.Read())
                {

                    string tempStr = "";

                    while (dr.Read())
                    {
                        tempStr = dr["chequeno"].ToString();

                        if (tempStr != "")
                        {
                            break;
                        }
                    }

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["date"].ToString(),
                        sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["payer"].ToString(),
                        sqliteDataReader["customerrefno"].ToString(),
                        sqliteDataReader["qtycontainer"].ToString(),
                        sqliteDataReader["invamount"].ToString(),
                        sqliteDataReader["receive"].ToString(),
                        Convert.ToString(Convert.ToInt32(sqliteDataReader["invamount"]) + Convert.ToInt32(sqliteDataReader["receive"])),
                        //sqliteDataReader["chequeno"].ToString(),
                        tempStr,
                        sqliteDataReader["remarks"].ToString()}));
                }
                sqliteConnection.Close();
            }

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sorter sorter = listView1.ListViewItemSorter as sorter;
            if (sorter == null)
            {
                sorter = new sorter(8);
                listView1.ListViewItemSorter = sorter;
            }
            else
            {
                sorter.Column = 8;
            }
            listView1.Sort();
            listView1.ListViewItemSorter = null;
        }
    }
}

