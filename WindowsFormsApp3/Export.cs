using CrystalDecisions.CrystalReports.Engine;
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
    public partial class Export : Form
    {
        int listCount; //for admin

        SQLiteConnection sqliteConnection;
        SQLiteCommand sqliteCommand;
        SQLiteDataReader sqliteDataReader;

        public Export()
        {
            InitializeComponent();

            sqliteConnection = new SQLiteConnection(@"data source = main.db");
        }

        private void FileTotalAmountcal()
        {
            string filetotalamount = totalamount.Text;

            
            sqliteCommand = new SQLiteCommand("delete from pay where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteCommand.ExecuteNonQuery();

            sqliteCommand = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks,advancepaymentname,amountcheck,receivecheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
               dateTimePicker1.Text, //date
               namebox.Text,//payer
               filenobox.Text,//fileno
               "", //slipno
               totalamount.Text, //amount
               "0", //received
               "", // remarks
               "",//advancepaymentname
               "1",//amountcheck
               ""), sqliteConnection); //receivecheck

            sqliteCommand.ExecuteNonQuery();

        }

        private void refreshlist()
        {
            listView1.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();

            //int id = 1;
            while (sqliteDataReader.Read())
            {

                listView1.Items.Add(new ListViewItem(new[] {  sqliteDataReader["particular"].ToString(),
                                                              sqliteDataReader["receiptno"].ToString(),
                                                              sqliteDataReader["quantity"].ToString(),
                                                              sqliteDataReader["rate"].ToString(),
                                                              sqliteDataReader["amount"].ToString(),
                                                              sqliteDataReader["remarks"].ToString()  }));
            }
            listCount = listView1.Items.Count;
            sqliteConnection.Close();
            TotalAmount();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (filenobox.Text == "")
            {
                button2.Enabled = true;
                updateBtn.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                updateBtn.Enabled = true;
            }
            filenobox.Items.AddRange(Sqlite.LoadExportFiles());
            namebox.Items.AddRange(Sqlite.LoadClients());

            listView1.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from exportfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();

            while (sqliteDataReader.Read())
            {
                namebox.Text = sqliteDataReader["name"].ToString();
                file_invoiceBox.Text = sqliteDataReader["file_invoice_no"].ToString() == null ? "" : sqliteDataReader["file_invoice_no"].ToString();
                dateTimePicker1.Text = sqliteDataReader["date"].ToString();
                msbox.Text = sqliteDataReader["ms"].ToString();
                descriptionbox.Text = sqliteDataReader["description"].ToString();
                qtybox.Text = sqliteDataReader["qty"].ToString();
                portofloadingbox.Text = sqliteDataReader["portofloading"].ToString();
                qtyofcontainerbox.Text = sqliteDataReader["qtyofcontainer"].ToString();
                containernobox.Text = sqliteDataReader["containerno"].ToString();
                sbillnobox.Text = sqliteDataReader["sbillno"].ToString();
                consigneenamebox.Text = sqliteDataReader["consigneename"].ToString();
                shippingcompanybox.Text = sqliteDataReader["shippingcompany"].ToString();
                portofdischargebox.Text = sqliteDataReader["portofdischarge"].ToString();
                vesselnamebox.Text = sqliteDataReader["vesselname"].ToString();
                formebox.Text = sqliteDataReader["forme"].ToString();
                customer_ref_no.Text = sqliteDataReader["invoiceno"].ToString();
                ntnnobox.Text = sqliteDataReader["clientntn"].ToString();
                strnobox.Text = sqliteDataReader["clientstr"].ToString();
            }

            sqliteConnection.Close();

            if (file_invoiceBox.Text == "")
                generateInvoice.Enabled = true;
            else
                generateInvoice.Enabled = false;

            refreshlist();

        }

        private void filenobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            sqliteConnection.Open();
            sqliteCommand = new SQLiteCommand("select * from exportfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                namebox.Text = sqliteDataReader["name"].ToString();
                dateTimePicker1.Text = sqliteDataReader["date"].ToString();
                msbox.Text = sqliteDataReader["ms"].ToString();
                descriptionbox.Text = sqliteDataReader["description"].ToString();
                qtybox.Text = sqliteDataReader["qty"].ToString();
                portofloadingbox.Text = sqliteDataReader["portofloading"].ToString();
                qtyofcontainerbox.Text = sqliteDataReader["qtyofcontainer"].ToString();
                containernobox.Text = sqliteDataReader["sbillno"].ToString();
                consigneenamebox.Text = sqliteDataReader["consigneename"].ToString();
                shippingcompanybox.Text = sqliteDataReader["shippingcompany"].ToString();
                portofdischargebox.Text = sqliteDataReader["portofdischarge"].ToString();
                vesselnamebox.Text = sqliteDataReader["vesselname"].ToString();
                formebox.Text = sqliteDataReader["forme"].ToString();
                customer_ref_no.Text = sqliteDataReader["invoiceno"].ToString();


            }
            //listview
            sqliteCommand = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                listView1.Items.Add(new ListViewItem(new[] { 
                                                             sqliteDataReader["particular"].ToString(),
                                                             sqliteDataReader["receiptno"].ToString(),
                                                             sqliteDataReader["amount"].ToString(),
                                                             sqliteDataReader["remarks"].ToString()}));
            }
            sqliteConnection.Close();
            TotalAmount();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewClient newclient = new NewClient();
            newclient.Show();
        }
        
        private void itemaddbutton_Click(object sender, EventArgs e)
        {
            if (particularsbox.Text == "")
            {
                MessageBox.Show("Please select particulars");
                return;
            }
            //SQLiteConnection sqliteConnection = new SQLiteConnection(@"data source = main.db");
            //sqliteConnection.Open();
            //SQLiteCommand sq;
            //sq = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
            //SQLiteDataReader dr = sq.ExecuteReader();
            //while (dr.Read())
            //{
            //    id++;
            //}
            int quant = Convert.ToInt32(quantityBox.Text);
            double rate = Convert.ToDouble(rateBox.Text);

            if(quant < 0)
            {
                MessageBox.Show("Invalid quantity");
                return;
            }
            else if(rate < 0.0)
            {
                MessageBox.Show("Invalid rate");
                return;
            }

            //for sales invoice

            ListViewItem itm = new ListViewItem();
            itm.Text = particularsbox.Text;
            //itm.SubItems.Add(particularsbox.Text);
            itm.SubItems.Add(receiptnobox.Text);
            itm.SubItems.Add((quant).ToString());
            itm.SubItems.Add((rate).ToString());
            itm.SubItems.Add((quant * rate).ToString());
            itm.SubItems.Add(remarksbox.Text);
            listView1.Items.Add(itm);
            //int total=0;
            //for (int i = 0; i < listView1.Items.Count; i++)
            //{

            //    //MessageBox.Show(listView1.Items[i].SubItems[2].Text);
            //    total += Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
            //}
            ////MessageBox.Show(total.ToString());
            //totalamount.Text = total.ToString();
            TotalAmount();
            particularsbox.Text = "";
            receiptnobox.Text = "";
            quantityBox.Text = "1";
            rateBox.Text = "0";
            remarksbox.Text = "";
        }

        private bool NoFile()
        {
            bool no_file = true;
            for (int i = 0; i < filenobox.Items.Count; i++)
            {
                String value = filenobox.GetItemText(filenobox.Items[i]);
                if (value == filenobox.Text)
                { no_file = false; }

            }
            return no_file;
        }

        private void TotalAmount()
        {
            float total=0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                
                //MessageBox.Show(listView1.Items[i].SubItems[2].Text);
                total += Convert.ToSingle(listView1.Items[i].SubItems[4].Text);
            }
            //MessageBox.Show(total.ToString());
            totalamount.Text = total.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string alreadyExistFileNo = "";

                if (namebox.Text == "")
                {
                    MessageBox.Show("Client name can not be empty");
                    return;
                }
                sqliteConnection.Open();
                sqliteCommand = new SQLiteCommand("select fileno from files", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    alreadyExistFileNo = sqliteDataReader["fileno"].ToString();

                    if (alreadyExistFileNo == filenobox.Text)
                    {
                        MessageBox.Show("You can not save an already existing file.", "File no " + filenobox.Text + " already exists.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

              

                if (alreadyExistFileNo != filenobox.Text)
                {
                    sqliteCommand = new SQLiteCommand(String.Format("insert into files (fileno,name,date,invoiceno,customerrefno,qtycontainer,invamount) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        filenobox.Text,
                        namebox.Text,
                        dateTimePicker1.Text,
                        file_invoiceBox.Text,
                        customer_ref_no.Text,
                        qtyofcontainerbox.Text,
                        totalamount.Text), sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();



                    if (file_invoiceBox.Text != "" && !generateInvoice.Enabled)
                    {
                        //for file invoice
                        sqliteCommand = new SQLiteCommand(String.Format("update sqlite_sequence set seq = '{0}' where name='invoiceno'", file_invoiceBox.Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();

                    }
                   
                    sqliteCommand = new SQLiteCommand(String.Format("insert into exportfiledetails  (fileno,name,ntnno,strno,date,ms,description,qty,portofloading,qtyofcontainer,containerno,sbillno,consigneename,shippingcompany,portofdischarge,vesselname,forme,invoiceno,file_invoice_no, clientntn, clientstr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                          filenobox.Text,
                          namebox.Text,
                          ntnnobox.Text,
                          strnobox.Text,
                          dateTimePicker1.Text,
                          msbox.Text,
                          descriptionbox.Text,
                          qtybox.Text,
                          portofloadingbox.Text,
                          qtyofcontainerbox.Text,
                          containernobox.Text,
                          sbillnobox.Text,
                          consigneenamebox.Text,
                          shippingcompanybox.Text,
                          portofdischargebox.Text,
                          vesselnamebox.Text,
                          formebox.Text,
                          customer_ref_no.Text,
                          file_invoiceBox.Text,
                          ntnnobox.Text,
                          strnobox.Text), sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        sqliteCommand = new SQLiteCommand(String.Format("insert into exportfileparticulars (fileno,particular,receiptno,quantity,rate,amount,remarks) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                           filenobox.Text,
                           listView1.Items[i].SubItems[0].Text,
                           listView1.Items[i].SubItems[1].Text,
                           listView1.Items[i].SubItems[2].Text,
                           listView1.Items[i].SubItems[3].Text,
                           listView1.Items[i].SubItems[4].Text,
                           listView1.Items[i].SubItems[5].Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();
                    }
                    FileTotalAmountcal();

                    sqliteConnection.Close();


                    MessageBox.Show("Data Saved Succsefully");
                    this.Close();
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //filenobox.Items.AddRange(Sqlite.LoadFiles());
            filenobox.Items.Clear();
            filenobox.Items.AddRange(Sqlite.LoadClients());
            
        }

        private void filenobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            updateBtn.Enabled = true;
            button2.Enabled = false;

            listView1.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from exportfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();

            while (sqliteDataReader.Read())
            {
                namebox.Text = sqliteDataReader["name"].ToString();
                file_invoiceBox.Text = sqliteDataReader["file_invoice_no"].ToString() == null ? "" : sqliteDataReader["file_invoice_no"].ToString();
                dateTimePicker1.Text = sqliteDataReader["date"].ToString();
                msbox.Text = sqliteDataReader["ms"].ToString();
                descriptionbox.Text = sqliteDataReader["description"].ToString();
                qtybox.Text = sqliteDataReader["qty"].ToString();
                portofloadingbox.Text = sqliteDataReader["portofloading"].ToString();
                qtyofcontainerbox.Text = sqliteDataReader["qtyofcontainer"].ToString();
                containernobox.Text = sqliteDataReader["containerno"].ToString();
                sbillnobox.Text = sqliteDataReader["sbillno"].ToString();
                consigneenamebox.Text = sqliteDataReader["consigneename"].ToString();
                shippingcompanybox.Text = sqliteDataReader["shippingcompany"].ToString();
                portofdischargebox.Text = sqliteDataReader["portofdischarge"].ToString();
                vesselnamebox.Text = sqliteDataReader["vesselname"].ToString();
                formebox.Text = sqliteDataReader["forme"].ToString();
                customer_ref_no.Text = sqliteDataReader["invoiceno"].ToString();
                ntnnobox.Text = sqliteDataReader["clientntn"].ToString();
                strnobox.Text = sqliteDataReader["clientstr"].ToString();
            }

            sqliteConnection.Close();

            if (file_invoiceBox.Text == "")
                generateInvoice.Enabled = true;
            else
                generateInvoice.Enabled = false;
            
            refreshlist();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                listView1.Items[index].Remove();
                TotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select any particular", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            commercial_report(true);
        }

        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX  
                bool isDone = false;//test if already translated  
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))  
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric  
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping  
                    String place = "";//digit grouping name:hundres,thousand,etc...  
                    switch (numDigits)
                    {
                        case 1://ones' range  

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range  
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range  
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range  
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range  
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range  
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...  
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)  
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros  
                        //if (beginsZero) word = " and " + word.Trim();  
                    }
                    //ignore digit grouping names  
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        private static String ConvertToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "Only";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents  
                        endStr = "Paisa " + endStr;//Cents  
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }

        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select seq from sqlite_sequence where name='invoiceno'", sqliteConnection);
            int lastid = Convert.ToInt32(sqliteCommand.ExecuteScalar());
            sqliteConnection.Close();
            file_invoiceBox.Text = (lastid + 1).ToString();
            generateInvoice.Enabled = false;
        }

        private void windowClose(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(Path.Combine(".\\Export_Report.pdf")))
                File.Delete(Path.Combine(".\\Export_Report.pdf"));
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            commercial_report(false);
        }

        private void commercial_report(bool print)
        {
            this.Enabled = false;
            if (File.Exists(Path.Combine(".\\Export_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Export_Report.pdf"));
            }

            ExportDataSet expDataSet = new ExportDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = expDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("id", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("fileno");
            tempDataTable.Columns.Add("name");
            tempDataTable.Columns.Add("companyntnno");
            tempDataTable.Columns.Add("companystrno");
            tempDataTable.Columns.Add("date");
            tempDataTable.Columns.Add("ms");
            tempDataTable.Columns.Add("description");
            tempDataTable.Columns.Add("qty");
            tempDataTable.Columns.Add("portofloading");
            tempDataTable.Columns.Add("qtyofcontainer");
            tempDataTable.Columns.Add("containerno");
            tempDataTable.Columns.Add("sbillno");
            tempDataTable.Columns.Add("consigneename");
            tempDataTable.Columns.Add("shippingcompany");
            tempDataTable.Columns.Add("portofdischarge");
            tempDataTable.Columns.Add("vesselname");
            tempDataTable.Columns.Add("forme");
            tempDataTable.Columns.Add("invoiceno");
            tempDataTable.Columns.Add("particular");
            tempDataTable.Columns.Add("receiptno");
            tempDataTable.Columns.Add("amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("remarks");
            tempDataTable.Columns.Add("totalNumberInWords");
            tempDataTable.Columns.Add("Sno", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("file_invoice_no");
            tempDataTable.Columns.Add("quantity", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("rate", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("companyaddress");
            tempDataTable.Columns.Add("companytel_cel_email");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["id"] = -1;
            row["fileno"] = filenobox.Text;
            row["name"] = namebox.Text;
            row["companyntnno"] = Sqlite.ntnnumber;
            row["companystrno"] = Sqlite.strnumber;
            row["date"] = dateTimePicker1.Text;
            row["ms"] = msbox.Text;
            row["description"] = descriptionbox.Text;
            row["qty"] = qtybox.Text;
            row["portofloading"] = portofloadingbox.Text;
            row["qtyofcontainer"] = qtyofcontainerbox.Text;
            row["containerno"] = containernobox.Text;
            row["sbillno"] = sbillnobox.Text;
            row["consigneename"] = consigneenamebox.Text;
            row["shippingcompany"] = shippingcompanybox.Text;
            row["portofdischarge"] = portofdischargebox.Text;
            row["vesselname"] = vesselnamebox.Text;
            row["forme"] = formebox.Text;
            row["invoiceno"] = customer_ref_no.Text;
            row["totalNumberInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(totalamount.Text)).ToString());
            row["file_invoice_no"] = file_invoiceBox.Text;



            int j = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (j == 0)
                    j++;
                else
                    row = tempDataTable.NewRow();
                row["particular"] = listView1.Items[i].SubItems[0].Text;
                row["receiptno"] = listView1.Items[i].SubItems[1].Text;
                row["quantity"] = listView1.Items[i].SubItems[2].Text == "" ? 0 : Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                row["rate"] = listView1.Items[i].SubItems[3].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                row["amount"] = listView1.Items[i].SubItems[4].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                row["remarks"] = listView1.Items[i].SubItems[5].Text;
                row["totalNumberInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(totalamount.Text)).ToString());
                row["Sno"] = (i + 1);
                row["companyaddress"] = Sqlite.address;
                row["companytel_cel_email"] = "Tel# " + Sqlite.telno + " Cell# " + Sqlite.cellno + " ,Email: " + Sqlite.emailaddress;
                tempDataTable.Rows.Add(row);
            }

            ExportCrystalReport crystalReport = new ExportCrystalReport();
            crystalReport.Load("..//ExportCrystalReport.rpt");
            crystalReport.DataSourceConnections.Clear();
            crystalReport.SetDataSource(expDataSet.Tables[1]);
            crystalReport.Subreports[0].DataSourceConnections.Clear();
            crystalReport.Subreports[0].SetDataSource(Sqlite.headerDataSet.Tables[1]);
            

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = ".\\Export_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                if(print)
                    SendToPrinter(".\\Export_Report.pdf");
                else
                    System.Diagnostics.Process.Start(@".\\Export_Report.pdf"); //opens the pdf stored
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Enabled = true;
            }
            this.Enabled = true;
        }

        private void File_Option_Pressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!filenobox.Items.Contains(filenobox.Text))
                {
                    listView1.Items.Clear();
                    namebox.Text = "";
                    ntnnobox.Text = "";
                    strnobox.Text = "";
                    sbillnobox.Text = "";
                    dateTimePicker1.Value = DateTime.Today;
                    msbox.Text = "";
                    descriptionbox.Text = "";
                    qtybox.Text = "";
                    portofloadingbox.Text = "";
                    qtyofcontainerbox.Text = "";
                    containernobox.Text = "";
                    consigneenamebox.Text = "";
                    shippingcompanybox.Text = "";
                    portofdischargebox.Text = "";
                    vesselnamebox.Text = "";
                    formebox.Text = "";
                    customer_ref_no.Text = "";
                    particularsbox.Text = "";
                    receiptnobox.Text = "";
                    quantityBox.Text = "1";
                    rateBox.Text = "0";
                    remarksbox.Text = "";
                    totalamount.Text = "0";
                    file_invoiceBox.Text = "";
                    generateInvoice.Enabled = true;
                }
            }
        }

        private void File_leave(object sender, EventArgs e)
        {
            if (!filenobox.Items.Contains(filenobox.Text))
            {
                listView1.Items.Clear();
                namebox.Text = "";
                ntnnobox.Text = "";
                strnobox.Text = "";
                sbillnobox.Text = "";
                dateTimePicker1.Value = DateTime.Today;
                msbox.Text = "";
                descriptionbox.Text = "";
                qtybox.Text = "";
                portofloadingbox.Text = "";
                qtyofcontainerbox.Text = "";
                containernobox.Text = "";
                consigneenamebox.Text = "";
                shippingcompanybox.Text = "";
                portofdischargebox.Text = "";
                vesselnamebox.Text = "";
                formebox.Text = "";
                customer_ref_no.Text = "";
                particularsbox.Text = "";
                receiptnobox.Text = "";
                quantityBox.Text = "1";
                rateBox.Text = "0";
                remarksbox.Text = "";
                totalamount.Text = "0";
                file_invoiceBox.Text = "";
                generateInvoice.Enabled = true;
            }
        }

        private void name_IndexedChanged(object sender, EventArgs e)
        {

            SQLiteConnection con = new SQLiteConnection(@"data source = main.db");
            con.Open();

            sqliteCommand = new SQLiteCommand("select * from clients where name = '" + namebox.Text + "'", con);
            SQLiteDataReader sqliteDataReader1 = sqliteCommand.ExecuteReader();
            while (sqliteDataReader1.Read())
            {
                ntnnobox.Text = sqliteDataReader1["ntnnumber"].ToString();
                strnobox.Text = sqliteDataReader1["strnumber"].ToString(); ;
            }
           con.Close();

        }

        private void Sales_Preview_Button_Click(object sender, EventArgs e)
        {
            sales_invoice_report(false);
        }

        private void Sales_Print_Button_Click(object sender, EventArgs e)
        {
            sales_invoice_report(true);
        }

        private void sales_invoice_report(bool print)
        {
            this.Enabled = false;
            if (File.Exists(Path.Combine(".\\Export_Sales_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Export_Sales_Report.pdf"));
            }

            SaleTaxInvoiceDataSet salesTaxDataSet = new SaleTaxInvoiceDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = salesTaxDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("fileno");
            tempDataTable.Columns.Add("date");
            tempDataTable.Columns.Add("companystrno");
            tempDataTable.Columns.Add("companyntnno");
            tempDataTable.Columns.Add("ntnno");
            tempDataTable.Columns.Add("strno");
            tempDataTable.Columns.Add("qty");
            tempDataTable.Columns.Add("description");
            tempDataTable.Columns.Add("sbno");
            tempDataTable.Columns.Add("igmno");
            tempDataTable.Columns.Add("vesselname");
            tempDataTable.Columns.Add("agencyservicecharges");
            tempDataTable.Columns.Add("salestax");
            tempDataTable.Columns.Add("total");
            tempDataTable.Columns.Add("totalinwords");
           
            tempDataTable.Columns.Add("companyaddress");
            tempDataTable.Columns.Add("companytel_cell_email");
            tempDataTable.Columns.Add("client_name");
            tempDataTable.Columns.Add("client_address");
            tempDataTable.Columns.Add("file_invoiceno");
            tempDataTable.Columns.Add("indexno");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["fileno"] = filenobox.Text;
            row["date"] = dateTimePicker1.Text;
            row["companystrno"] = Sqlite.strnumber;
            row["companyntnno"] = Sqlite.ntnnumber;

            row["ntnno"] = ntnnobox.Text;
            row["strno"] = strnobox.Text;
            row["qty"] = qtybox.Text;
            row["description"] = descriptionbox.Text;
            row["sbno"] = sbillnobox.Text;
            row["igmno"] = "";
            row["vesselname"] = vesselnamebox.Text;
            row["file_invoiceno"] = file_invoiceBox.Text;
            //row["indexno"] = ind.Text;

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select name, address from clients where name = '" + namebox.Text + "'", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();

            while (sqliteDataReader.Read())
            {

                row["client_name"] = sqliteDataReader["name"].ToString();
                row["client_address"] = sqliteDataReader["address"].ToString();
            }
            sqliteConnection.Close();

            string agencySalesCharges = "0", salesTax = "0";
            for (int i = 0; i < listView1.Items.Count; i++)
            {

                if (listView1.Items[i].SubItems[0].Text == "AGENCY AMOUNTS:")
                    agencySalesCharges = listView1.Items[i].SubItems[4].Text;
                if (listView1.Items[i].SubItems[0].Text == "SALES TAX @13 %")
                    salesTax = listView1.Items[i].SubItems[4].Text;
            }
            row["agencyservicecharges"] = agencySalesCharges;
            row["salestax"] = salesTax;
            double total = Convert.ToDouble(agencySalesCharges) + Convert.ToDouble(salesTax);
            row["total"] = total.ToString();
            row["totalinwords"] = ConvertToWords(Math.Abs(total).ToString());

            tempDataTable.Rows.Add(row);

            SalesTaxInvoiceCrystalReport crystalReport = new SalesTaxInvoiceCrystalReport();
            crystalReport.SetDataSource(salesTaxDataSet.Tables[1]);

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
                CrDiskFileDestinationOptions.DiskFileName = ".\\Export_Sales_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                if(print)
                    SendToPrinter(".\\Export_Sales_Report.pdf");
                else
                    System.Diagnostics.Process.Start(@".\\Export_Sales_Report.pdf");//opens the pdf stored
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Enabled = true;
            }
            this.Enabled = true;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!StartPage.Admin)
                    MessageBox.Show("You don't have right to update this", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                else
                {
                    sqliteConnection.Open();

                    sqliteCommand = new SQLiteCommand("delete from files where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand(String.Format("insert into files (fileno,name,date,invoiceno,customerrefno,qtycontainer,invamount) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        filenobox.Text,
                        namebox.Text,
                        dateTimePicker1.Text,
                        file_invoiceBox.Text,
                        customer_ref_no.Text,
                        qtyofcontainerbox.Text,
                        totalamount.Text), sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();



                    if (file_invoiceBox.Text != "" && !generateInvoice.Enabled)
                    {
                        //for file invoice
                        sqliteCommand = new SQLiteCommand(String.Format("update sqlite_sequence set seq = '{0}' where name='invoiceno'", file_invoiceBox.Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();

                    }
                    //sq = new SQLiteCommand(String.Format("insert into files  (fileno,name,ntnno,date) values ('{0}','{1}','{2}','{3}')",
                    //      filenobox.Text,
                    //      namebox.Text,
                    //      ntnnobox.Text,
                    //      dateTimePicker1.Text), sqliteConnection);
                    //sq.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand("delete from exportfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand(String.Format("insert into exportfiledetails  (fileno,name,ntnno,strno,date,ms,description,qty,portofloading,qtyofcontainer,containerno,sbillno,consigneename,shippingcompany,portofdischarge,vesselname,forme,invoiceno,file_invoice_no, clientntn, clientstr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                          filenobox.Text,
                          namebox.Text,
                          ntnnobox.Text,
                          strnobox.Text,
                          dateTimePicker1.Text,
                          msbox.Text,
                          descriptionbox.Text,
                          qtybox.Text,
                          portofloadingbox.Text,
                          qtyofcontainerbox.Text,
                          containernobox.Text,
                          sbillnobox.Text,
                          consigneenamebox.Text,
                          shippingcompanybox.Text,
                          portofdischargebox.Text,
                          vesselnamebox.Text,
                          formebox.Text,
                          customer_ref_no.Text,
                          file_invoiceBox.Text,
                          ntnnobox.Text,
                          strnobox.Text), sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();


                    sqliteCommand = new SQLiteCommand("delete from exportfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    for (int i = 0; i < listView1.Items.Count; i++)
                    {


                        sqliteCommand = new SQLiteCommand(String.Format("insert into exportfileparticulars (fileno,particular,receiptno,quantity,rate,amount,remarks) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                           filenobox.Text,
                           listView1.Items[i].SubItems[0].Text,
                           listView1.Items[i].SubItems[1].Text,
                           listView1.Items[i].SubItems[2].Text,
                           listView1.Items[i].SubItems[3].Text,
                           listView1.Items[i].SubItems[4].Text,
                           listView1.Items[i].SubItems[5].Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();
                    }
                    FileTotalAmountcal();

                    MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sqliteConnection.Close();
                    this.Close();
                }
            }

            catch(Exception exception)
            {
                MessageBox.Show(Convert.ToString(exception), "Exception found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
