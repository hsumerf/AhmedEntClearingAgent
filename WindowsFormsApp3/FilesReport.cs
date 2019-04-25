using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class FilesReport : Form
    {
        public FilesReport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            float totalAmount = 0,  total = 0;
            String[] report = new String[6];
            List<String[]> reportList = new List<String[]>();

            SQLiteDataReader dr;
            if (nameBox.Text == "")
                sq = new SQLiteCommand("select payer,fileno,sum(amount) as total from pay where amountcheck='1' group by fileno", scn);
            else
                sq = new SQLiteCommand("select payer,fileno,sum(amount) as total from pay where payer='" + nameBox.Text + "' AND amountcheck='1' group by fileno", scn);
            dr = sq.ExecuteReader();
            while (dr.Read())
            {
                total = Convert.ToSingle(dr["total"].ToString());
                report[0] = dr["payer"].ToString();
                report[1] = dr["fileno"].ToString();
                report[5] = dr["total"].ToString();
                reportList.Add(new String[] { report[0], report[1], "", "","", report[5]});
                //listView1.Items.Add(new ListViewItem(new[] { dr["payer"].ToString(),
                //                                             dr["fileno"].ToString(),
                //                                             dr["total"].ToString()}));
                totalAmount += float.Parse(dr["total"].ToString());
                
            }

            
            for (int i = 0; i < reportList.Count; i++)
            {
                String invoice, qty = "", consigneename="";
                sq = new SQLiteCommand("select count(id) from exportfiledetails where fileno = '" + reportList[i][1]+"'", scn);
                int num = Convert.ToInt32(sq.ExecuteScalar());
                if(num == 1)
                {
                    sq = new SQLiteCommand("select qty, consigneename from exportfiledetails where fileno = '" + reportList[i][1]+"'", scn);
                    dr = sq.ExecuteReader();
                    while (dr.Read())
                    {
                        qty = dr["qty"].ToString();
                        consigneename = dr["consigneename"].ToString();
                    }
                }

                sq = new SQLiteCommand("select invoiceno from files where fileno = '" + reportList[i][1]+"'", scn);
                dr = sq.ExecuteReader();
             //   String invoice = "";
                while (dr.Read())
                {
                    invoice = dr["invoiceno"].ToString();
                    reportList[i][2] = invoice;
                    reportList[i][3] = consigneename;
                    reportList[i][4] = qty;
                    listView1.Items.Add(new ListViewItem(new[] { reportList[i][0], reportList[i][1], reportList[i][2], reportList[i][3], reportList[i][4], reportList[i][5] }));
                }


               
            }

            total_amount.Text = totalAmount.ToString();
            
        }

        private void FilesReport_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel("FilesReport.csv", listView1);
        }

        private void ExportToExcel(string path, ListView listsource)
        {
            StringBuilder CVS = new StringBuilder();
            for (int i = 0; i < listsource.Columns.Count; i++)
            {
                CVS.Append(listsource.Columns[i].Text + ",");
            }
            CVS.Append(Environment.NewLine);
            for (int i = 0; i < listsource.Items.Count; i++)
            {
                for (int j = 0; j < listsource.Columns.Count; j++)
                {
                    CVS.Append(listsource.Items[i].SubItems[j].Text + ",");
                }
                CVS.Append(Environment.NewLine);
            }
            System.IO.File.WriteAllText(path, CVS.ToString());
            Process.Start(path);
        }

        private void total_amount_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void nameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
