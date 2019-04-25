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
    public partial class StartPage : Form
    {
        public static bool Admin = false;
        public StartPage()
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
            SQLiteCommand sq = new SQLiteCommand("select count(password) from "+temp+" where password = '"+password.Text+"' and username = '"+username.Text+"'", scn);
            int num = Convert.ToInt32(sq.ExecuteScalar());
            scn.Close();
            if (num == 0)
                MessageBox.Show("Invalid Password or Username", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (num == 1)
            {
                Main main = new Main();
                if (temp == "admin")
                {
                    Admin = true;
                    main.newAdminUser.Enabled = true;
                }
                else
                    Admin = false;

                if (Properties.Settings.Default.first == true)
                {
                    ProductKey ProductKey = new ProductKey();
                    ProductKey.Show();
                }
                else
                {
                    main.Show();
                    this.Hide();
                }


                
            }
            
        }

        private void admin_CheckedChanged(object sender, EventArgs e)
        {
            password.Text = "";
            username.Items.Clear();
            username.Items.Insert(0, "--Select Username--");
            username.SelectedIndex = 0;
            try
            {
                SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                scn.Open();
                SQLiteCommand sq;

                sq = new SQLiteCommand("select * from admin", scn);
                SQLiteDataReader dr = sq.ExecuteReader();
                //int id = 1;
                while (dr.Read())
                {

                    username.Items.Add(dr["username"]);
                }
                scn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void user_CheckedChanged(object sender, EventArgs e)
        {
            password.Text = "";
            username.Items.Clear();
            username.Items.Insert(0, "--Select Username--");
            username.SelectedIndex = 0;
            try
            {
                SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
                scn.Open();
                SQLiteCommand sq;

                sq = new SQLiteCommand("select * from user", scn);
                SQLiteDataReader dr = sq.ExecuteReader();
                //int id = 1;
                while (dr.Read())
                {

                    username.Items.Add(dr["username"]);
                }
                scn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void username_SelectedIndexChanged(object sender, EventArgs e)
        {
            password.Text = "";
        }

        private void StartPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
