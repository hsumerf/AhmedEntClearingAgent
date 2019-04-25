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
    public partial class NewClient : Form
    {
        public NewClient()
        {
            InitializeComponent();
        }
        private void refreshlist()
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select name,details,ntnnumber,strnumber,address from Clients", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            int id = 1;
            while (dr.Read())
            {

                listView1.Items.Add(new ListViewItem(new[] {   id++.ToString(),
                                                              dr["name"].ToString(),
                                                              dr["ntnnumber"].ToString(),
                                                              dr["strnumber"].ToString(),
                                                              dr["address"].ToString(),
                                                              dr["details"].ToString()}));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand(String.Format("insert into Clients (name,details,ntnnumber,strnumber,address) values ('{0}','{1}', '{2}', '{3}','{4}')",
                 clientnamebox.Text,detailsbox.Text, ntnBox.Text, strBox.Text,Addressbox.Text), scn);

            sq.ExecuteNonQuery();
            refreshlist();
        }

        private void NewClient_Load(object sender, EventArgs e)
        {
            refreshlist();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listView1.Items.Remove(listView1.SelectedItems[0]);
            
            refreshlist();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                    scn.Open();
                    SQLiteCommand sq;

                    sq = new SQLiteCommand("delete from Clients where name = '"+ listView1.Items[index].SubItems[1].Text+"'", scn);

                    sq.ExecuteNonQuery();
                    listView1.Items[index].Remove();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select any client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
