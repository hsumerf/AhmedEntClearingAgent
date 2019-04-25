using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class splashScreen : Form
    {
        public splashScreen()
        {
            InitializeComponent();
        }
        int sec = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (sec == 2)
            {
                if (Properties.Settings.Default.first == true)
                {
                    ProductKey ProductKey = new ProductKey();
                    ProductKey.Show();
                }
                else
                {
                    Sqlite.LoadCompany();
                    Sqlite.LoadHeaderDataSet();
                    StartPage startpage = new StartPage();
                    startpage.username.Items.Insert(0, "--Select Username--");
                    startpage.username.SelectedIndex = 0;
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

                            startpage.username.Items.Add(dr["username"]);
                        }
                        scn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    startpage.Show();

                }
                timer1.Enabled = false;
                this.Hide();

            }
            else
            {
                sec++;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://techfurq.com");
        }
    }
}
