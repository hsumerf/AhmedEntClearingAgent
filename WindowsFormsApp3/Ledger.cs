using CrystalDecisions.Shared;
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
    public partial class Ledger : Form
    {
        public Ledger()
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
            float totalReceived = 0, totalAmount = 0, totalBalance = 0, total = 0, received = 0; 
            
            SQLiteDataReader dr;
            if (nameBox.Text == "")
                sq = new SQLiteCommand("select payer,sum(amount) as total,sum(received) as received from pay group by payer", scn);
            else
                sq = new SQLiteCommand("select payer,sum(amount) as total,sum(received) as received from pay where payer='" + nameBox.Text + "' group by payer", scn);
            dr = sq.ExecuteReader();
            while (dr.Read())
            {
                total = Convert.ToSingle(dr["total"].ToString());
                received = Convert.ToSingle(dr["received"].ToString());
                listView1.Items.Add(new ListViewItem(new[] { dr["payer"].ToString(),
                                                             dr["total"].ToString(),
                                                             dr["received"].ToString(),
                                                             (total-received).ToString()}));
                totalAmount += float.Parse(dr["total"].ToString());
                totalReceived += float.Parse(dr["received"].ToString());
                totalBalance += total - received;
            }
            total_amount.Text = totalAmount.ToString();
            total_Rec.Text = totalReceived.ToString();
            total_balance.Text = (totalAmount - totalReceived).ToString();

            LedgerDataSet ledDataSet = new LedgerDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = ledDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("Name");
            tempDataTable.Columns.Add("Amount", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("Received", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("Balance", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("TotalAmount", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("TotalReceived", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("TotalBalance", Type.GetType("System.Int32"));

            //adding values
            DataRow row;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                row = tempDataTable.NewRow();
                row["Name"] = listView1.Items[i].SubItems[0].Text;
                row["Amount"] = Convert.ToInt32(listView1.Items[i].SubItems[1].Text);
                row["Received"] = Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                row["Balance"] = Convert.ToInt32(listView1.Items[i].SubItems[3].Text);
                row["TotalAmount"] = Convert.ToInt32(total_amount.Text);
                row["TotalReceived"] = Convert.ToInt32(total_Rec.Text);
                row["TotalBalance"] = Convert.ToInt32(total_balance.Text);
                tempDataTable.Rows.Add(row);
            }

            LedgerCrystalReport crystalReport = new LedgerCrystalReport();
            crystalReport.SetDataSource(ledDataSet.Tables[1]);

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = ".\\Ledger_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Ledger_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void total_balance_Click(object sender, EventArgs e)
        {

        }

        private void total_Rec_Click(object sender, EventArgs e)
        {

        }

        private void total_amount_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void nameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //opens the pdf stored
            System.Diagnostics.Process.Start(@".\\Ledger_Report.pdf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendToPrinter(".\\Ledger_Report.pdf");
        }

        private void SendToPrinter(String path)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = @path;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            //p.WaitForInputIdle();
            //System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExportToExcel("Ledger.csv", listView1);
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
    }
}
