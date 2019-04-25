using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Export export = new Export();
            export.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CustomerReceiving pay = new CustomerReceiving();
            pay.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Receive receive = new Receive();
            //receive.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ledger ledger = new Ledger();
            ledger.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormImport importform = new FormImport();
            importform.Show();
        }

        private void NewAccount_Click(object sender, EventArgs e)
        {
            NewClient newclient = new NewClient();
            if (StartPage.Admin)
                newclient.Removebutton.Enabled = true;
            newclient.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FilesReport filesreport = new FilesReport();
            filesreport.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OfficePayVoucher officepay = new OfficePayVoucher();
            officepay.Show();
        }

        private void newAdminUser_Click(object sender, EventArgs e)
        {
            NewAdminUser newAdmUser = new NewAdminUser();
            newAdmUser.Show();
        }
        

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Enabled = false;
            DateTime currentDate = DateTime.Now;
            string destFolder = Sqlite.GetFolderPath();
            if (destFolder == "")
            {
                MessageBox.Show("Please select any folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string destPath = string.Format("{0}/backup-{1}.db", destFolder, currentDate.ToString("dd-MM-yyyy"));
            if (File.Exists(destPath))
                destPath = string.Format("{0}/backup-{1}.db", destFolder, currentDate.ToString("dd-MM-yyyy-fff"));

            using (var source = new SQLiteConnection("Data Source=main.db"))
            using (var destination = new SQLiteConnection(string.Format(@"Data Source={0}", destPath)))
            {
                source.Open();
                destination.Open();
                source.BackupDatabase(destination, "main", "main", -1, null, 0);
            }
            this.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GC.Collect();   // yes, really release the db
            GC.WaitForPendingFinalizers();
            string filename = @"C:\Users\moheb\Desktop\AhmedEnt (2)\AhmedEnt\WindowsFormsApp3\bin\Debug\main.db";
            bool worked = false;
            int tries = 1;
            while ((tries < 8) && (!worked))
            {
                try
                {
                    Thread.Sleep(tries * 1000);
                    File.Delete(filename);
                    worked = true;
                }
                catch (IOException ex)   // delete only throws this on locking
                {
                    tries++;
                }
            }
            if (!worked)
                throw new IOException("Unable to close file " + filename);


        }

        private void button10_Click(object sender, EventArgs e)
        {
            AccountRegester accountregister = new AccountRegester();
            accountregister.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DebitNote debitnote = new DebitNote();
            debitnote.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ServicePayings servicepayings = new ServicePayings();
            servicepayings.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ReceivingServices receivingservices = new ReceivingServices();
            receivingservices.Show();
        }

        private void allImportBtn_Click(object sender, EventArgs e)
        {
            allImport allImport = new allImport();
            allImport.Show();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            allExport allExport = new allExport();
            allExport.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void recFilesBtn_Click(object sender, EventArgs e)
        {
            ReceivedFiles receivedFiles = new ReceivedFiles();
            receivedFiles.Show();
        }
    }
}
