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
    public partial class NewFile : Form
    {
        public NewFile()
        {
            InitializeComponent();
        }
        private void refreshlist()
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select fileno,name,date from files", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            int id = 1;
            while (dr.Read())
            {

                listView1.Items.Add(new ListViewItem(new[] {   id++.ToString(),
                                                              dr["fileno"].ToString(),
                                                              dr["name"].ToString(),
                                                              dr["date"].ToString()  }));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand(String.Format("insert into files (fileno,name,date) values ('{0}','{1}','{2}')",
                 filenobox.Text, namebox.Text, ntnnobox.Text, dateTimePicker1.Text), scn);

            sq.ExecuteNonQuery();
            sq = new SQLiteCommand(String.Format("insert into exportfiledetails (fileno,name,ntnno,date) values ('{0}','{1}','{2}','{3}')",
                filenobox.Text, namebox.Text, ntnnobox.Text, dateTimePicker1.Text), scn);

            sq.ExecuteNonQuery();
            refreshlist();
        }

        private void NewFile_Load(object sender, EventArgs e)
        {
            refreshlist();
            namebox.Items.AddRange(Sqlite.LoadClients());


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewClient newclient = new NewClient();
            newclient.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            namebox.Items.Clear();
            namebox.Items.AddRange(Sqlite.LoadClients());
        }
    }
}
