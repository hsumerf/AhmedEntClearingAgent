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

            sq = new SQLiteCommand("select name,details from Clients", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            int id = 1;
            while (dr.Read())
            {

                listView1.Items.Add(new ListViewItem(new[] {   id++.ToString(),
                                                              dr["name"].ToString(),
                                                              dr["details"].ToString()}));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand(String.Format("insert into Clients (name,details) values ('{0}','{1}')",
                 clientnamebox.Text,detailsbox.Text), scn);

            sq.ExecuteNonQuery();
            refreshlist();
        }

        private void NewClient_Load(object sender, EventArgs e)
        {
            refreshlist();
        }
    }
}
