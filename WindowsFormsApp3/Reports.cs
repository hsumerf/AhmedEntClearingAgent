using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Reports : Form
    {
        List<int> id = new List<int>();
        public Reports()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            SQLiteDataReader dr;
            if (nameBox.Text == "")
                sq = new SQLiteCommand("select * from pay where date BETWEEN '" + dateTimeFrom.Text + "' and '" + dateTimeTo.Text + "' AND officevoucher='1' ", scn);
            else
                sq = new SQLiteCommand("select * from pay where date BETWEEN '" + dateTimeFrom.Text + "' and '" + dateTimeTo.Text + "' AND payer='" + nameBox.Text + "' AND officevoucher='1' ", scn);
            dr = sq.ExecuteReader();
            // MessageBox.Show(dateTimeTo.Text);
            float totalAmount = 0, totalReceived = 0, totalBalance = 0, total = 0, received = 0;
            while (dr.Read())
            {
                total = Convert.ToSingle(dr["amount"].ToString());
                received = Convert.ToSingle(dr["received"].ToString());
                id.Add(Convert.ToInt32(dr["id"]));
                listView1.Items.Add(new ListViewItem(new[] { dr["date"].ToString(),
                                                             dr["payer"].ToString(),
                                                             dr["fileno"].ToString(),
                                                             dr["slipno"].ToString(),
                                                             dr["remarks"].ToString(),
                                                             dr["amount"].ToString(),
                                                             dr["received"].ToString() }));
                totalAmount += float.Parse(dr["amount"].ToString());
                totalReceived += float.Parse(dr["received"].ToString());
                totalBalance += total - received;
            }
            total_amount.Text = totalAmount.ToString();
            total_Rec.Text = totalReceived.ToString();
            total_bal.Text = (totalAmount - totalReceived).ToString();
            //receive table
            //if (nameBox.Text == "")
            //    sq = new SQLiteCommand("select * from receive where Date BETWEEN '" + dateTimeFrom.Text + "' and '" + dateTimeTo.Text + "'", scn);
            //else
            //    sq = new SQLiteCommand("select * from receive where Date BETWEEN '" + dateTimeFrom.Text + "' and '" + dateTimeTo.Text + "' AND payer='" + nameBox.Text + "'", scn);
            //dr = sq.ExecuteReader();
            //while (dr.Read())
            //{

            //    listView1.Items.Add(new ListViewItem(new[] { dr["date"].ToString(),
            //                                                 dr["payer"].ToString(),
            //                                                 dr["fileno"].ToString(),
            //                                                 dr["slipno"].ToString(),
            //                                                 dr["amount"].ToString(),
            //                                                 dr["remarks"].ToString()}));

            //}

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            nameBox.Items.AddRange(Sqlite.LoadClients());
        }

        private void total_amount_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if(nameBox.Text == "")
            {
                MessageBox.Show("Please select name first");
                this.Enabled = true;
                listView1.Items.Clear();
                return;
            }
            if (File.Exists(Path.Combine(".\\Outstanding_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Outstanding_Report.pdf"));
            }

            ReportDataSet outstandingDataSet = new ReportDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = outstandingDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("from");
            tempDataTable.Columns.Add("to");
            tempDataTable.Columns.Add("date");
            tempDataTable.Columns.Add("name");
            tempDataTable.Columns.Add("file_no");
            tempDataTable.Columns.Add("slip_no");
            tempDataTable.Columns.Add("remarks");
            tempDataTable.Columns.Add("amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_received", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_balance", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("received", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("companyntnno");
            tempDataTable.Columns.Add("companystrno");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["from"] = dateTimeFrom.Text;
            row["to"] = dateTimeTo.Text;
            row["name"] = nameBox.Text;
            row["companyntnno"] = Sqlite.ntnnumber;
            row["companystrno"] = Sqlite.strnumber;

            int j = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (j == 0)
                    j++;
                else
                    row = tempDataTable.NewRow();
                row["date"] = listView1.Items[i].SubItems[0].Text;
                row["file_no"] = listView1.Items[i].SubItems[2].Text;
                row["slip_no"] = listView1.Items[i].SubItems[3].Text;
                row["remarks"] = listView1.Items[i].SubItems[4].Text;
                row["amount"] = listView1.Items[i].SubItems[5].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                row["received"] = listView1.Items[i].SubItems[6].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[6].Text);
                row["total_amount"] = Convert.ToDouble(total_amount.Text);
                row["total_received"] = Convert.ToDouble(total_Rec.Text);
                row["total_balance"] = Convert.ToDouble(total_bal.Text);

                tempDataTable.Rows.Add(row);
            }

            OutstandingCrystalReport crystalReport = new OutstandingCrystalReport();
            crystalReport.SetDataSource(outstandingDataSet.Tables[1]);

            //if we are dealing with databaser
            //crystalReport.Load("C:\\Users\\moheb\\Desktop\\labaratory_data\\labaratory_data\\CrystalReport1.rpt");

            //if want to show view crystal viewer
            //crystalReportViewer1.ReportSource = crystalReport;
            //crystalReportViewer1.Refresh();

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = ".\\Outstanding_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                //opens the pdf stored
                System.Diagnostics.Process.Start(@".\\Outstanding_Report.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Enabled = true;
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (nameBox.Text == "")
            {
                MessageBox.Show("Please select name first");
                this.Enabled = true;
                listView1.Items.Clear();
                return;
            }
            if (File.Exists(Path.Combine(".\\Outstanding_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Outstanding_Report.pdf"));
            }

            ReportDataSet outstandingDataSet = new ReportDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = outstandingDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("from");
            tempDataTable.Columns.Add("to");
            tempDataTable.Columns.Add("date");
            tempDataTable.Columns.Add("name");
            tempDataTable.Columns.Add("file_no");
            tempDataTable.Columns.Add("slip_no");
            tempDataTable.Columns.Add("remarks");
            tempDataTable.Columns.Add("amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_received", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("total_balance", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("received", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("companyntnno");
            tempDataTable.Columns.Add("companystrno");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["from"] = dateTimeFrom.Text;
            row["to"] = dateTimeTo.Text;
            row["name"] = nameBox.Text;
            row["companyntnno"] = Sqlite.ntnnumber;
            row["companystrno"] = Sqlite.strnumber;

            int j = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (j == 0)
                    j++;
                else
                    row = tempDataTable.NewRow();
                row["date"] = listView1.Items[i].SubItems[0].Text;
                row["file_no"] = listView1.Items[i].SubItems[2].Text;
                row["slip_no"] = listView1.Items[i].SubItems[3].Text;
                row["remarks"] = listView1.Items[i].SubItems[4].Text;
                row["amount"] = listView1.Items[i].SubItems[5].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                row["received"] = listView1.Items[i].SubItems[6].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[6].Text);
                row["total_amount"] = Convert.ToDouble(total_amount.Text);
                row["total_received"] = Convert.ToDouble(total_Rec.Text);
                row["total_balance"] = Convert.ToDouble(total_bal.Text);

                tempDataTable.Rows.Add(row);
            }

            OutstandingCrystalReport crystalReport = new OutstandingCrystalReport();
            crystalReport.SetDataSource(outstandingDataSet.Tables[1]);

            //if we are dealing with databaser
            //crystalReport.Load("C:\\Users\\moheb\\Desktop\\labaratory_data\\labaratory_data\\CrystalReport1.rpt");

            //if want to show view crystal viewer
            //crystalReportViewer1.ReportSource = crystalReport;
            //crystalReportViewer1.Refresh();

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = ".\\Outstanding_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                //opens the pdf stored
                SendToPrinter(".\\Outstanding_Report.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Enabled = true;
            }
            this.Enabled = true;
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
            ExportToExcel("Outstanding.csv", listView1);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                if (StartPage.Admin)
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                        scn.Open();
                        SQLiteCommand sq;

                        sq = new SQLiteCommand("delete from pay where id = " + id[index], scn);

                        sq.ExecuteNonQuery();
                        listView1.Items[index].Remove();
                    }               
                }
                else
                    MessageBox.Show("You don't have right to remove this", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Please select any particular", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            OfficePay officePayVoucher = new OfficePay();

            officePayVoucher.dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            officePayVoucher.payer.Text = listView1.SelectedItems[0].SubItems[1].Text;
            officePayVoucher.fileno.Text = listView1.SelectedItems[0].SubItems[2].Text;
            officePayVoucher.slipno.Text = listView1.SelectedItems[0].SubItems[3].Text;
            officePayVoucher.remarks.Text = listView1.SelectedItems[0].SubItems[4].Text;
            officePayVoucher.amount.Text = listView1.SelectedItems[0].SubItems[5].Text;
            officePayVoucher.receive.Text = listView1.SelectedItems[0].SubItems[6].Text;


            officePayVoucher.ShowDialog();
        }
    }
}

