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
    public partial class FormImport : Form
    {
        int listCount;
        int listCount2;

        SQLiteConnection sqliteConnection;
        SQLiteCommand sqliteCommand;
        SQLiteDataReader sqliteDataReader;

        public FormImport()
        {
            InitializeComponent();

            sqliteConnection = new SQLiteConnection(@"data source = main.db");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filenobox.Items.AddRange(Sqlite.LoadImportFiles());
            namebox.Items.AddRange(Sqlite.LoadClients());

            if (filenobox.Text == "")
            {
                updateBtn.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                updateBtn.Enabled = true;
                button2.Enabled = false;
            }

            listView1.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from importfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
            SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                namebox.Text = sqliteDataReader["name"].ToString();
                dateTimePicker1.Text = sqliteDataReader["date"].ToString();
                ms.Text = sqliteDataReader["ms"].ToString();
                descriptionbox.Text = sqliteDataReader["description"].ToString();
                blawbnobox.Text = sqliteDataReader["blawbno"].ToString();
                countryio.Text = sqliteDataReader["countryio"].ToString();
                shippingco.Text = sqliteDataReader["shippingco"].ToString();
                containerno.Text = sqliteDataReader["containerno"].ToString();
                gdnodate.Text = sqliteDataReader["gdnodate"].ToString();
                lcno.Text = sqliteDataReader["lcno"].ToString();
                vesselname.Text = sqliteDataReader["vesselname"].ToString();
                customer_ref_no.Text = sqliteDataReader["invoiceno"].ToString();
                igmnodate.Text = sqliteDataReader["igmnodate"].ToString();
                indno.Text = sqliteDataReader["indno"].ToString();
                file_invoiceBox.Text = sqliteDataReader["file_invoice_no"].ToString();
                ntnnobox.Text = sqliteDataReader["clientntn"].ToString();
                strno.Text = sqliteDataReader["clientstr"].ToString();
            }
            sqliteDataReader.Close();


            sqliteConnection.Close();

            if (file_invoiceBox.Text == "")
                generateInvoice.Enabled = true;
            else
                generateInvoice.Enabled = false;

            refreshlist(); //PARTICULAR LIST
            refreshadvance();
        }

        private void FileTotalAmountcal()
        {
            string filetotalamount = TotalAmountLabel.Text;

            


            sqliteCommand = new SQLiteCommand("delete from pay where fileno = '" + filenobox.Text + "'", sqliteConnection);
            sqliteCommand.ExecuteNonQuery();

            sqliteCommand = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,amount,received,remarks,advancepaymentname,amountcheck,receivecheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
               dateTimePicker1.Text,
               namebox.Text, //payer
               filenobox.Text, //fileno
               "", //slipno
               TotalAmountLabel.Text, //amount
               "0", // received
               "", //remarks
               "", //advancepaymentname
               "1", // amountcheck
               ""), sqliteConnection);  //receivecheck

            sqliteCommand.ExecuteNonQuery();
            
        }

        private String TotalAdvance()
        {
            float total_advance = 0;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                //MessageBox.Show(total_advance);
                total_advance += Convert.ToSingle(listView2.Items[i].SubItems[2].Text);
            }
            return total_advance.ToString();
        }

        private void TotalAmount()
        {
            float total = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                total += Convert.ToSingle(listView1.Items[i].SubItems[4].Text);
            }
            TotalAmountLabel.Text = total.ToString();
        }

        private void refreshlist()
        {
            listView1.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from importfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
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
            sqliteDataReader.Close();
            listCount = listView1.Items.Count;
            TotalAmount();

            sqliteConnection.Close();
        }

        private void refreshadvance()
        {
            listView2.Items.Clear();

            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from pay where fileno = '" + filenobox.Text + "' and payer = '" + namebox.Text + "' and receivecheck='1' ", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();
            //int id = 1;
            while (sqliteDataReader.Read())
            {

                listView2.Items.Add(new ListViewItem(new[] {  sqliteDataReader["advancepaymentname"].ToString(),
                                                              sqliteDataReader["slipno"].ToString(),
                                                              sqliteDataReader["received"].ToString(),
                                                              sqliteDataReader["remarks"].ToString()}));
            }
            listCount2 = listView2.Items.Count;
            TotalAdvanceLabel.Text = TotalAdvance();


            sqliteConnection.Close();
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

                //If the file is already saved then it will become true
                bool error = false;

                while (sqliteDataReader.Read())
                {
                    alreadyExistFileNo = sqliteDataReader["fileno"].ToString();

                    if (alreadyExistFileNo == filenobox.Text)
                    {
                        MessageBox.Show("You can not save an already existing file.", "File no " + filenobox.Text + " already exists.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        error = true;
                        break;
                    }
                }
                sqliteDataReader.Close();
                if (!error)
                {
                    if (alreadyExistFileNo != filenobox.Text)
                    {
                        //CREATING NEW FILE 
                        sqliteCommand = new SQLiteCommand(String.Format("insert into files  (fileno,name,date,invoiceno,customerrefno,invamount) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                            filenobox.Text,
                            namebox.Text,
                            dateTimePicker1.Text,
                            file_invoiceBox.Text,
                            customer_ref_no.Text,
                            TotalAmountLabel.Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();

                        if (file_invoiceBox.Text != "" && !generateInvoice.Enabled)
                        {
                            //for file invoice
                            sqliteCommand = new SQLiteCommand(String.Format("update sqlite_sequence set seq = '{0}' where name='invoiceno'", file_invoiceBox.Text), sqliteConnection);
                            sqliteCommand.ExecuteNonQuery();
                        }

                        sqliteCommand = new SQLiteCommand(String.Format("insert into importfiledetails  (fileno,name,ntnno,strno,date,ms,description,blawbno,countryio,shippingco,containerno,gdnodate,lcno,vesselname,invoiceno,igmnodate,indno,file_invoice_no,clientntn,clientstr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",
                                filenobox.Text,
                                namebox.Text,
                                ntnnobox.Text,
                                strno.Text,
                                dateTimePicker1.Text,
                                ms.Text,
                                descriptionbox.Text,
                                blawbnobox.Text,
                                countryio.Text,
                                shippingco.Text,
                                containerno.Text,
                                gdnodate.Text,
                                lcno.Text,
                                vesselname.Text,
                                customer_ref_no.Text,
                                igmnodate.Text,
                                indno.Text,
                                file_invoiceBox.Text,
                                ntnnobox.Text,
                                strno.Text), sqliteConnection);

                        sqliteCommand.ExecuteNonQuery();

                        //FILE PARTCULERS INSERTION
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            sqliteCommand = new SQLiteCommand(String.Format("insert into importfileparticulars (fileno,particular,receiptno,quantity,rate,amount,remarks) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                                filenobox.Text,
                                listView1.Items[i].SubItems[0].Text,
                                listView1.Items[i].SubItems[1].Text,
                                listView1.Items[i].SubItems[2].Text,
                                listView1.Items[i].SubItems[3].Text,
                                listView1.Items[i].SubItems[4].Text,
                                listView1.Items[i].SubItems[5].Text), sqliteConnection);
                            sqliteCommand.ExecuteNonQuery();
                        }

                        //FILE PARTICULARS AMOUNT INSERTION
                        FileTotalAmountcal();
                        //FILE ADVANCE PAYMENT INSERTION
                        //      sq = new SQLiteCommand("delete from pay where fileno = '" + filenobox.Text + "' and payer = '" + namebox.Text + "' and receivecheck='1' ", scn);
                        //     sq.ExecuteNonQuery();
                        for (int i = 0; i < listView2.Items.Count; i++)
                        {
                            sqliteCommand = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,remarks,amount,received,advancepaymentname,receivecheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                dateTimePicker1.Text, //date
                                namebox.Text, //payer
                                filenobox.Text, //fileno
                                listView2.Items[i].SubItems[1].Text, //slip no.
                                listView2.Items[i].SubItems[3].Text, //remarks
                                "0", //AMOUNT
                                listView2.Items[i].SubItems[2].Text, //advance receive amount
                                listView2.Items[i].SubItems[0].Text, //advance payment name
                                "1"), sqliteConnection);
                            //dateTimePicker1.Text, //date
                            //namebox.Text, //payer
                            // filenobox.Text,//fileno
                            //listView2.Items[i].SubItems[1].Text, //slip no.
                            //listView2.Items[i].SubItems[3].Text, // remarks   
                            // "0", // amount
                            // listView2.Items[i].SubItems[2].Text, //received advance amount
                            // listView2.Items[i].SubItems[0].Text, scn)); //advance payment name
                            //  "",//amountcheck
                            //  "-"),scn); //receive check
                            sqliteCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Data Saved Succsefully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sqliteConnection.Close();

                        this.Close();
                    }
                }


            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
            sqliteConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (particularsbox.Text == "")
            {
                MessageBox.Show("Please select particulars");
                return;
            }

            int quant = Convert.ToInt32(quantityBox.Text);
            double rate = Convert.ToDouble(rateBox.Text);

            if (quant < 0)
            {
                MessageBox.Show("Invalid quantity");
                return;
            }
            else if (rate < 0.0)
            {
                MessageBox.Show("Invalid rate");
                return;
            }

            ListViewItem itm = new ListViewItem();
            itm.Text = particularsbox.Text;
            //itm.SubItems.Add(particularsbox.Text);
            itm.SubItems.Add(receiptnobox.Text);
            itm.SubItems.Add((quant).ToString());
            itm.SubItems.Add((rate).ToString());
            itm.SubItems.Add((quant * rate).ToString());
            itm.SubItems.Add(remarksbox.Text);
            listView1.Items.Add(itm);

            TotalAmount();
            particularsbox.Text = "";
            receiptnobox.Text = "";
            quantityBox.Text = "1";
            rateBox.Text = "0";
            remarksbox.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (paymentname.Text == "")
            {
                MessageBox.Show("Please select particulars");
                return;
            }

            ListViewItem itm = new ListViewItem();
            itm.Text = paymentname.Text;
            //itm.SubItems.Add(particularsbox.Text);
            itm.SubItems.Add(advancereceipt.Text);
            itm.SubItems.Add(advanceAmountBox.Text);
            itm.SubItems.Add(advanceremarksbox.Text);
            listView2.Items.Add(itm);
            TotalAdvanceLabel.Text = TotalAdvance();

            paymentname.Text = "";
            advancereceipt.Text = "";
            advanceAmountBox.Text = "0";
            advanceremarksbox.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NewClient newclient = new NewClient();
            newclient.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            namebox.Items.Clear();
            namebox.Items.AddRange(Sqlite.LoadClients());
        }

        private void filenobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            updateBtn.Enabled = true;

            listView1.Items.Clear();
            sqliteConnection.Open();
            sqliteCommand = new SQLiteCommand("select * from importfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
            SQLiteDataReader sqliteDataReader1 = sqliteCommand.ExecuteReader();
            while (sqliteDataReader1.Read())
            {
                namebox.Text = sqliteDataReader1["name"].ToString();
                dateTimePicker1.Text = sqliteDataReader1["date"].ToString();
                ms.Text = sqliteDataReader1["ms"].ToString();
                descriptionbox.Text = sqliteDataReader1["description"].ToString();
                blawbnobox.Text = sqliteDataReader1["blawbno"].ToString();
                countryio.Text = sqliteDataReader1["countryio"].ToString();
                shippingco.Text = sqliteDataReader1["shippingco"].ToString();
                containerno.Text = sqliteDataReader1["containerno"].ToString();
                gdnodate.Text = sqliteDataReader1["gdnodate"].ToString();
                lcno.Text = sqliteDataReader1["lcno"].ToString();
                vesselname.Text = sqliteDataReader1["vesselname"].ToString();
                customer_ref_no.Text = sqliteDataReader1["invoiceno"].ToString();
                igmnodate.Text = sqliteDataReader1["igmnodate"].ToString();
                indno.Text = sqliteDataReader1["indno"].ToString();
                file_invoiceBox.Text = sqliteDataReader1["file_invoice_no"].ToString();
                ntnnobox.Text = sqliteDataReader1["clientntn"].ToString();
                strno.Text = sqliteDataReader1["clientstr"].ToString();
            }
            sqliteDataReader.Close();

            sqliteConnection.Close();

            if (file_invoiceBox.Text == "")
                generateInvoice.Enabled = true;
            else
                generateInvoice.Enabled = false;

            //for admin
            //if (filenobox.Items.Contains(filenobox.Text) && !StartPage.Admin)
            //{
            //    namebox.Enabled = false;
            //    ntnnobox.Enabled = false;
            //    strno.Enabled = false;
            //    ms.Enabled = false;
            //    dateTimePicker1.Enabled = false;
            //    descriptionbox.Enabled = false;
            //    blawbnobox.Enabled = false;
            //    countryio.Enabled = false;
            //    shippingco.Enabled = false;
            //    gdnodate.Enabled = false;
            //    lcno.Enabled = false;
            //    vesselname.Enabled = false;
            //    igmnodate.Enabled = false;
            //    indno.Enabled = false;
            //    invoiceno.Enabled = false;
            //    containerno.Enabled = false;
            //}

            refreshlist(); //PARTICULAR LIST
            refreshadvance();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            commercial_invoice_report(true);
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

        private void generateInvoice_Click(object sender, EventArgs e)
        {
            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select seq from sqlite_sequence where name='invoiceno'", sqliteConnection);
            int lastid = Convert.ToInt32(sqliteCommand.ExecuteScalar());
            sqliteConnection.Close();
            file_invoiceBox.Text = (lastid + 1).ToString();
            generateInvoice.Enabled = false;

            sqliteConnection.Close();

        }

        private void namebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = main.db");
            con.Open();

            SQLiteCommand sqliteCommand1 = new SQLiteCommand("select * from clients where name = '" + namebox.Text + "'", con);
            sqliteDataReader = sqliteCommand1.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                ntnnobox.Text = sqliteDataReader["ntnnumber"].ToString();
                strno.Text = sqliteDataReader["strnumber"].ToString(); ;
            }
            sqliteDataReader.Close();
            con.Close();
            
        }

        private void printPreview_Click(object sender, EventArgs e)
        {
            commercial_invoice_report(false);
        }

        private void commercial_invoice_report(bool print)
        {
            this.Enabled = false;
            if (File.Exists(Path.Combine(".\\Import_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Import_Report.pdf"));
            }

            ImportDataSet impDataSet = new ImportDataSet();
            DataTable tempDataTable = impDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("id", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("fileno");
            tempDataTable.Columns.Add("name");
            tempDataTable.Columns.Add("companyntnno");
            tempDataTable.Columns.Add("companystrno");
            tempDataTable.Columns.Add("date");
            tempDataTable.Columns.Add("ms");
            tempDataTable.Columns.Add("description");
            tempDataTable.Columns.Add("blawbno");
            tempDataTable.Columns.Add("countryio");
            tempDataTable.Columns.Add("shippingco");
            tempDataTable.Columns.Add("containerno");
            tempDataTable.Columns.Add("gdnodate");
            tempDataTable.Columns.Add("lcno");
            tempDataTable.Columns.Add("vesselname");
            tempDataTable.Columns.Add("invoiceno");
            tempDataTable.Columns.Add("igmnodate");
            tempDataTable.Columns.Add("particular");
            tempDataTable.Columns.Add("receiptno");
            tempDataTable.Columns.Add("amount", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("remarks");
            tempDataTable.Columns.Add("totalBalanceInWords");
            tempDataTable.Columns.Add("Sno", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("indno");
            tempDataTable.Columns.Add("advancepaymentname");
            tempDataTable.Columns.Add("received", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("pay_remarks");
            tempDataTable.Columns.Add("pay_Sno", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("pay_totalNumberInWords");
            tempDataTable.Columns.Add("balance", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("file_invoice_no");
            tempDataTable.Columns.Add("quantity", Type.GetType("System.Int32"));
            tempDataTable.Columns.Add("rate", Type.GetType("System.Double"));
            tempDataTable.Columns.Add("companyaddress");
            tempDataTable.Columns.Add("companytel_cell_email");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["id"] = -1;
            row["fileno"] = filenobox.Text;
            row["name"] = namebox.Text;
            row["companyntnno"] = Sqlite.ntnnumber;
            row["companystrno"] = Sqlite.strnumber;
            row["date"] = dateTimePicker1.Text;
            row["ms"] = ms.Text;
            row["description"] = descriptionbox.Text;
            row["blawbno"] = blawbnobox.Text;
            row["countryio"] = countryio.Text;
            row["shippingco"] = shippingco.Text;
            row["containerno"] = containerno.Text;
            row["gdnodate"] = gdnodate.Text;
            row["lcno"] = lcno.Text;
            row["vesselname"] = vesselname.Text;
            row["invoiceno"] = customer_ref_no.Text;
            row["igmnodate"] = igmnodate.Text;
            row["indno"] = indno.Text;
            row["pay_totalNumberInWords"] = "";
            row["balance"] = Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text);
            row["file_invoice_no"] = file_invoiceBox.Text;
            row["totalBalanceInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text)).ToString());
            //row["balance"] = 2;
            //row["totalBalanceInWords"] = "sd";

            int i = 0, j = 0, k = 0;
            int m = listView1.Items.Count;
            int n = listView2.Items.Count;

            while (i < m && j < n)
            {
                if (k == 0)
                    k++;
                else
                    row = tempDataTable.NewRow();

                row["particular"] = listView1.Items[i].SubItems[0].Text;
                row["receiptno"] = listView1.Items[i].SubItems[1].Text;
                row["quantity"] = listView1.Items[i].SubItems[2].Text == "" ? 0 : Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                row["rate"] = listView1.Items[i].SubItems[3].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                row["amount"] = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                row["remarks"] = listView1.Items[i].SubItems[5].Text;
                row["balance"] = Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text);
                row["totalBalanceInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text)).ToString());
                row["Sno"] = (i++ + 1);

                row["advancepaymentname"] = listView2.Items[j].SubItems[0].Text;
                row["received"] = Convert.ToDouble(listView2.Items[j].SubItems[2].Text);
                row["pay_remarks"] = listView2.Items[j].SubItems[3].Text;
                row["pay_Sno"] = (j++ + 1);
                row["pay_totalNumberInWords"] = "";
                row["companyaddress"] = Sqlite.address;
                row["companytel_cell_email"] = "Tel# " + Sqlite.telno + " Cell# " + Sqlite.cellno + " ,Email: " + Sqlite.emailaddress;

                tempDataTable.Rows.Add(row);
            }

            while (i < m)
            {
                if (k == 0)
                    k++;
                else
                    row = tempDataTable.NewRow();

                row["particular"] = listView1.Items[i].SubItems[0].Text;
                row["receiptno"] = listView1.Items[i].SubItems[1].Text;
                row["quantity"] = listView1.Items[i].SubItems[2].Text == "" ? 0 : Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                row["rate"] = listView1.Items[i].SubItems[3].Text == "" ? 0 : Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                row["amount"] = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                row["remarks"] = listView1.Items[i].SubItems[5].Text;
                row["balance"] = Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text);
                row["totalBalanceInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text)).ToString());
                row["Sno"] = (i++ + 1);
                row["companyaddress"] = Sqlite.address;
                row["companytel_cell_email"] = "Tel# " + Sqlite.telno + " Cell# " + Sqlite.cellno + " ,Email: " + Sqlite.emailaddress;

                tempDataTable.Rows.Add(row);
            }

            while (j < n)
            {
                if (k == 0)
                    k++;
                else
                    row = tempDataTable.NewRow();

                row["balance"] = Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text);
                row["totalBalanceInWords"] = ConvertToWords(Math.Abs(Convert.ToDouble(TotalAmountLabel.Text) - Convert.ToDouble(TotalAdvanceLabel.Text)).ToString());

                row["advancepaymentname"] = listView2.Items[j].SubItems[0].Text; ;
                row["received"] = Convert.ToDouble(listView2.Items[j].SubItems[2].Text);
                row["pay_remarks"] = listView2.Items[j].SubItems[3].Text;
                row["pay_Sno"] = (j++ + 1); ;
                row["pay_totalNumberInWords"] = "";
                row["companyaddress"] = Sqlite.address;
                row["companytel_cell_email"] = "Tel# " + Sqlite.telno + " Cell# " + Sqlite.cellno + " ,Email: " + Sqlite.emailaddress;

                tempDataTable.Rows.Add(row);
            }



            ImportCrystalReport crystalReport = new ImportCrystalReport();
            crystalReport.Load("..//ImportCrystalReport.rpt");
            crystalReport.DataSourceConnections.Clear();
            crystalReport.SetDataSource(impDataSet.Tables[1]);
            crystalReport.Subreports[0].DataSourceConnections.Clear();
            crystalReport.Subreports[0].SetDataSource(Sqlite.headerDataSet.Tables[1]);



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
                CrDiskFileDestinationOptions.DiskFileName = ".\\Import_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                if (print)
                    SendToPrinter(".\\Import_Report.pdf");
                else
                    System.Diagnostics.Process.Start(@".\\Import_Report.pdf");//opens the pdf stored
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
                    listView2.Items.Clear();
                    namebox.Text = "";
                    ntnnobox.Text = "";
                    strno.Text = "";
                    ms.Text = "";
                    dateTimePicker1.Value = DateTime.Today;
                    descriptionbox.Text = "";
                    blawbnobox.Text = "";
                    countryio.Text = "";
                    shippingco.Text = "";
                    gdnodate.Text = "";
                    lcno.Text = "";
                    vesselname.Text = "";
                    igmnodate.Text = "";
                    indno.Text = "";
                    customer_ref_no.Text = "";
                    particularsbox.Text = "";
                    receiptnobox.Text = "";
                    quantityBox.Text = "1";
                    rateBox.Text = "0";
                    remarksbox.Text = "";
                    TotalAmountLabel.Text = "0";
                    paymentname.Text = "";
                    advancereceipt.Text = "";
                    advanceAmountBox.Text = "0";
                    advanceremarksbox.Text = "";
                    TotalAdvanceLabel.Text = "0";
                    file_invoiceBox.Text = "";
                    generateInvoice.Enabled = true;
                    containerno.Text = "";
                }
            }
        }

        private void File_leave(object sender, EventArgs e)
        {
            if (!filenobox.Items.Contains(filenobox.Text))
            {
                listView1.Items.Clear();
                listView2.Items.Clear();
                namebox.Text = "";
                ntnnobox.Text = "";
                strno.Text = "";
                ms.Text = "";
                dateTimePicker1.Value = DateTime.Today;
                descriptionbox.Text = "";
                blawbnobox.Text = "";
                countryio.Text = "";
                shippingco.Text = "";
                gdnodate.Text = "";
                lcno.Text = "";
                vesselname.Text = "";
                igmnodate.Text = "";
                indno.Text = "";
                customer_ref_no.Text = "";
                particularsbox.Text = "";
                receiptnobox.Text = "";
                quantityBox.Text = "1";
                rateBox.Text = "0";
                remarksbox.Text = "";
                TotalAmountLabel.Text = "0";
                paymentname.Text = "";
                advancereceipt.Text = "";
                advanceAmountBox.Text = "0";
                advanceremarksbox.Text = "";
                TotalAdvanceLabel.Text = "0";
                file_invoiceBox.Text = "";
                generateInvoice.Enabled = true;
                containerno.Text = "";
            }
        }

        private void Sales_Tax_Preview_Click(object sender, EventArgs e)
        {
            sales_invoice_report(false);
        }

        private void Sales_Tax_Print_Click(object sender, EventArgs e)
        {
            sales_invoice_report(true);
        }

        private void sales_invoice_report(bool print)
        {
            this.Enabled = false;
            if (File.Exists(Path.Combine(".\\Import_Sales_Report.pdf")))
            {
                File.Delete(Path.Combine(".\\Import_Sales_Report.pdf"));
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
            tempDataTable.Columns.Add("GDNo");

            //adding values
            DataRow row = tempDataTable.NewRow();
            row["fileno"] = filenobox.Text;
            row["date"] = dateTimePicker1.Text;
            row["companystrno"] = Sqlite.ntnnumber;
            row["ntnno"] = ntnnobox.Text;
            row["strno"] = strno.Text;
            row["qty"] = "";
            row["description"] = descriptionbox.Text;
            row["sbno"] = "";
            row["igmno"] = igmnodate.Text;
            row["vesselname"] = vesselname.Text;
            row["file_invoiceno"] = file_invoiceBox.Text;
            row["indexno"] = indno.Text;
            row["GDNo"] = gdnodate.Text;
            row["companystrno"] = Sqlite.strnumber;
            row["companyntnno"] = Sqlite.ntnnumber;


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
                CrDiskFileDestinationOptions.DiskFileName = ".\\Import_Sales_Report.pdf";
                CrExportOptions = crystalReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                crystalReport.Export();

                if (print)
                    SendToPrinter(".\\Import_Sales_Report.pdf");
                else
                    Process.Start(@".\\Import_Sales_Report.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Enabled = true;
            }
            this.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                if (!StartPage.Admin && index >= listCount)
                {
                    listView1.Items[index].Remove();
                    TotalAmount();
                }
                else if (StartPage.Admin)
                {
                    listView1.Items[index].Remove();
                    TotalAmount();
                }
                else
                    MessageBox.Show("You don't have right to remove this", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select any particular", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                int index = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                if (!StartPage.Admin && index >= listCount2)
                {
                    listView2.Items[index].Remove();
                    TotalAdvanceLabel.Text = TotalAdvance();
                }
                else if (StartPage.Admin)
                {
                    listView2.Items[index].Remove();
                    TotalAdvanceLabel.Text = TotalAdvance();
                }
                else
                    MessageBox.Show("You don't have right to remove this", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select any particular", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                    if (file_invoiceBox.Text != "" && !generateInvoice.Enabled)
                    {
                        //for file invoice
                        sqliteCommand = new SQLiteCommand(String.Format("update sqlite_sequence set seq = '{0}' where name='invoiceno'", file_invoiceBox.Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();
                        
                    }


                    //Updating NEW FILE
                    sqliteCommand = new SQLiteCommand("delete from files where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand(String.Format("insert into files  (fileno,name,date,invoiceno,customerrefno,invamount) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                        filenobox.Text,
                        namebox.Text,
                        dateTimePicker1.Text,
                        file_invoiceBox.Text,
                        customer_ref_no.Text,
                        TotalAmountLabel.Text), sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();


                    sqliteCommand = new SQLiteCommand("delete from importfiledetails where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand(String.Format("insert into importfiledetails  (fileno,name,ntnno,strno,date,ms,description,blawbno,countryio,shippingco,containerno,gdnodate,lcno,vesselname,invoiceno,igmnodate,indno,file_invoice_no,clientntn,clientstr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",
                            filenobox.Text,
                            namebox.Text,
                            ntnnobox.Text,
                            strno.Text,
                            dateTimePicker1.Text,
                            ms.Text,
                            descriptionbox.Text,
                            blawbnobox.Text,
                            countryio.Text,
                            shippingco.Text,
                            containerno.Text,
                            gdnodate.Text,
                            lcno.Text,
                            vesselname.Text,
                            customer_ref_no.Text,
                            igmnodate.Text,
                            indno.Text,
                            file_invoiceBox.Text,
                            ntnnobox.Text,
                            strno.Text), sqliteConnection);

                    sqliteCommand.ExecuteNonQuery();


                    sqliteCommand = new SQLiteCommand("delete from importfileparticulars where fileno = '" + filenobox.Text + "'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    //FILE PARTCULERS INSERTION
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {

                        sqliteCommand = new SQLiteCommand(String.Format("insert into importfileparticulars (fileno,particular,receiptno,quantity,rate,amount,remarks) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            filenobox.Text,
                            listView1.Items[i].SubItems[0].Text,
                            listView1.Items[i].SubItems[1].Text,
                            listView1.Items[i].SubItems[2].Text,
                            listView1.Items[i].SubItems[3].Text,
                            listView1.Items[i].SubItems[4].Text,
                            listView1.Items[i].SubItems[5].Text), sqliteConnection);
                        sqliteCommand.ExecuteNonQuery();
                    }

                    //FILE PARTICULARS AMOUNT INSERTION
                    FileTotalAmountcal();
                    //FILE ADVANCE PAYMENT INSERTION
                    //      sq = new SQLiteCommand("delete from pay where fileno = '" + filenobox.Text + "' and payer = '" + namebox.Text + "' and receivecheck='1' ", scn);
                    //     sq.ExecuteNonQuery();

                    sqliteCommand = new SQLiteCommand("delete from pay where fileno = '" + filenobox.Text + "' and receivecheck = '1'", sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();


                    for (int i = 0; i < listView2.Items.Count; i++)
                    {


                        sqliteCommand = new SQLiteCommand(String.Format("insert into pay (date,payer,fileno,slipno,remarks,amount,received,advancepaymentname,receivecheck) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            dateTimePicker1.Text, //date
                            namebox.Text, //payer
                            filenobox.Text, //fileno
                            listView2.Items[i].SubItems[1].Text, //slip no.
                            listView2.Items[i].SubItems[3].Text, //remarks
                            "0",
                            listView2.Items[i].SubItems[2].Text, //advance receive amount
                            listView2.Items[i].SubItems[0].Text, //advance payment name
                            "1"), sqliteConnection);
                        //dateTimePicker1.Text, //date
                        //namebox.Text, //payer
                        // filenobox.Text,//fileno
                        //listView2.Items[i].SubItems[1].Text, //slip no.
                        //listView2.Items[i].SubItems[3].Text, // remarks   
                        // "0", // amount
                        // listView2.Items[i].SubItems[2].Text, //received advance amount
                        // listView2.Items[i].SubItems[0].Text, scn)); //advance payment name
                        //  "",//amountcheck
                        //  "-"),scn); //receive check
                        sqliteCommand.ExecuteNonQuery();
                    }
                    
                    MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sqliteConnection.Close();
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Convert.ToString(exception), "Exception found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
