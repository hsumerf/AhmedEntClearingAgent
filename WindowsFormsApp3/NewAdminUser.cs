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
    public partial class NewAdminUser : Form
    {
        public NewAdminUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp;
            if (user.Checked)
                temp = "user";
            else
                temp = "admin";
            SQLiteConnection scn = new SQLiteConnection(@"data source =  main.db");
            scn.Open();
            SQLiteCommand sq = new SQLiteCommand("insert into " + temp + " (username,password) values ('" + username.Text + "','" + password.Text + "')", scn);
            try
            {
                sq.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
                password.Text = "";
                username.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if(temp == "admin")
                refreshAdminslist();
            else
                refreshUserslist();
        }

        private void refreshUserslist()
        {
            listView1.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select username from user", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            int id = 1;
            while (dr.Read())
            {

                listView1.Items.Add(new ListViewItem(new[] {   id++.ToString(),
                                                              dr["username"].ToString()}));
            }
        }

        private void refreshAdminslist()
        {
            listView2.Items.Clear();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select username from admin", scn);
            SQLiteDataReader dr = sq.ExecuteReader();
            int id = 1;
            while (dr.Read())
            {

                listView2.Items.Add(new ListViewItem(new[] {   id++.ToString(),
                                                              dr["username"].ToString()}));
            }
        }

        private void load(object sender, EventArgs e)
        {
            refreshAdminslist();
            refreshUserslist();
            if (!StartPage.Admin)
            {
                removeAdmin.Enabled = false;
                removeUser.Enabled = false;
            }
        }

        private void remove_User(object sender, EventArgs e)
        {
            try
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                DialogResult res = MessageBox.Show("Are you sure you want to delete "+ listView1.Items[index].SubItems[1].Text, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                    scn.Open();
                    SQLiteCommand sq;

                    sq = new SQLiteCommand("delete from user where username = '" + listView1.Items[index].SubItems[1].Text + "'", scn);

                    sq.ExecuteNonQuery();
                    listView1.Items[index].Remove();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select any user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void remove_Admin(object sender, EventArgs e)
        {
            try
            {
                int index = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                DialogResult res = MessageBox.Show("Are you sure you want to delete "+ listView2.Items[index].SubItems[1].Text, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                    scn.Open();
                    SQLiteCommand sq;

                    sq = new SQLiteCommand("delete from admin where username = '" + listView2.Items[index].SubItems[1].Text + "'", scn);

                    sq.ExecuteNonQuery();
                    listView2.Items[index].Remove();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select any admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
