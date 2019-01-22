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
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }
        //private void refreshlist()
        //{
        //    listView1.Items.Clear();
        //    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
        //    scn.Open();
        //    SQLiteCommand sq;

        //    sq = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", scn);
        //    SQLiteDataReader dr = sq.ExecuteReader();
        //    int id = 1;
        //    while (dr.Read())
        //    {

        //        listView1.Items.Add(new ListViewItem(new[] {   id++.ToString(),
        //                                                      dr["particular"].ToString(),
        //                                                      dr["receiptno"].ToString(),
        //                                                      dr["amount"].ToString(),
        //                                                      dr["remarks"].ToString()  }));
        //    }
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            filenobox.Items.AddRange(Sqlite.LoadFiles());

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void filenobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;
            sq = new SQLiteCommand("select * from exportfiledetails where fileno = '" + filenobox.Text + "'", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            while (dr.Read())
            {
                namebox.Text = dr["name"].ToString();
                ntnnobox.Text = dr["ntnno"].ToString();
                dateTimePicker1.Text = dr["date"].ToString();
                msbox.Text = dr["ms"].ToString();
                descriptionbox.Text = dr["description"].ToString();
                qtybox.Text = dr["qty"].ToString();
                portofloadingbox.Text = dr["portofloading"].ToString();
                qtyofcontainerbox.Text = dr["qtyofcontainer"].ToString();
                containernobox.Text = dr["sbillno"].ToString();
                consigneenamebox.Text = dr["consigneename"].ToString();
                shippingcompanybox.Text = dr["shippingcompany"].ToString();
                portofdischargebox.Text = dr["portofdischarge"].ToString();
                vesselnamebox.Text = dr["vesselname"].ToString();
                formebox.Text = dr["forme"].ToString();
                invoicenobox.Text = dr["invoiceno"].ToString();


            }
            //listview
            int id=1;
            sq = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", scn);
            dr = sq.ExecuteReader();
            while (dr.Read())
            {
                listView1.Items.Add(new ListViewItem(new[] { 
                                                             dr["particular"].ToString(),
                                                             dr["receiptno"].ToString(),
                                                             dr["amount"].ToString(),
                                                             dr["remarks"].ToString()}));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewFile newfile = new NewFile();
            newfile.Show();
        }
        
        private void itemaddbutton_Click(object sender, EventArgs e)
        {
            if (particularsbox.Text == "")
                return;
            //SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            //scn.Open();
            //SQLiteCommand sq;
            //sq = new SQLiteCommand("select * from exportfileparticulars where fileno = '" + filenobox.Text + "'", scn);
            //SQLiteDataReader dr = sq.ExecuteReader();
            //while (dr.Read())
            //{
            //    id++;
            //}
            ListViewItem itm = new ListViewItem();
            itm.Text = particularsbox.Text;
            //itm.SubItems.Add(particularsbox.Text);
            itm.SubItems.Add(receiptnobox.Text);
            itm.SubItems.Add(amountbox.Text);
            itm.SubItems.Add(remarksbox.Text);
            listView1.Items.Add(itm);


            particularsbox.Text = "";
            receiptnobox.Text = "";
            amountbox.Text = "";
            remarksbox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (namebox.Text == "")
            {
                MessageBox.Show("Client name can not be empty");
                return;

            }

            //for (int i = 0; i < listView1.Items.Count; i++)
            //{
            //    trucks += listView1.Items[i].Text;
            //}
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();

            SQLiteCommand sq;

            sq = new SQLiteCommand("delete from exportfiledetails where fileno = '" + filenobox.Text + "'", scn);
            sq.ExecuteNonQuery();
            sq = new SQLiteCommand("delete from exportfileparticulars where fileno = '" + filenobox.Text + "'", scn);
            sq.ExecuteNonQuery();

            sq = new SQLiteCommand(String.Format("insert into exportfiledetails  (fileno,name,ntnno,date,ms,description,qty,portofloading,qtyofcontainer,containerno,sbillno,consigneename,shippingcompany,portofdischarge,vesselname,forme,invoiceno) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                  filenobox.Text,
                  namebox.Text,
                  ntnnobox.Text,
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
                  invoicenobox.Text), scn);
            sq.ExecuteNonQuery();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sq = new SQLiteCommand(String.Format("insert into exportfileparticulars (fileno,particular,receiptno,amount,remarks) values ('{0}','{1}','{2}','{3}','{4}')",
                   filenobox.Text,
                   listView1.Items[i].SubItems[0].Text,
                   listView1.Items[i].SubItems[1].Text,
                   listView1.Items[i].SubItems[2].Text,
                   listView1.Items[i].SubItems[3].Text), scn);
                sq.ExecuteNonQuery();
            }

            MessageBox.Show("Data Saved Succsefully");
           // this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //filenobox.Items.AddRange(Sqlite.LoadFiles());
            filenobox.Items.Clear();
            filenobox.Items.AddRange(Sqlite.LoadFiles());


        }
    }
}
