using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace WindowsFormsApp3
{
    public partial class allExport : Form
    {
        SQLiteConnection sqliteConnection;
        SQLiteCommand sqliteCommand;
        SQLiteDataReader sqliteDataReader;

        public allExport()
        {
            InitializeComponent();

            sqliteConnection = new SQLiteConnection(@"data source = main.db");
        }

        private void allExport_Load(object sender, EventArgs e)
        {
            sqliteConnection.Open();

            sqliteCommand = new SQLiteCommand("select * from exportfiledetails", sqliteConnection);
            sqliteDataReader = sqliteCommand.ExecuteReader();

            while (sqliteDataReader.Read())
            {
                listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["fileno"].ToString(),
                sqliteDataReader["file_invoice_no"].ToString(),
                sqliteDataReader["invoiceno"].ToString(),
                sqliteDataReader["name"].ToString()}));
            }

            sqliteConnection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "File No")
            {
                comboBox2.Items.Clear();

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select fileno from exportfiledetails", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                    comboBox2.Items.Add(sqliteDataReader["fileno"]);

                deleteEmptySpacesFromComboBox();

                sqliteConnection.Close();


            }

            else if (comboBox1.Text == "Invoice No")
            {
                comboBox2.Items.Clear();

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select file_invoice_no from exportfiledetails", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                    comboBox2.Items.Add(sqliteDataReader["file_invoice_no"]);

                deleteEmptySpacesFromComboBox();

                sqliteConnection.Close();
            }

            else if (comboBox1.Text == "Customer Ref No")
            {
                comboBox2.Items.Clear();

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select invoiceno from exportfiledetails", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                    comboBox2.Items.Add(sqliteDataReader["invoiceno"]);


                deleteEmptySpacesFromComboBox();


                sqliteConnection.Close();

            }

            else if (comboBox1.Text == "Client Name")
            {
                comboBox2.Items.Clear();

                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select name from exportfiledetails", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                    comboBox2.Items.Add(sqliteDataReader["name"]);

                deleteEmptySpacesFromComboBox();

                sqliteConnection.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "File No")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select * from exportfiledetails where fileno = '" + comboBox2.Text + "'", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    listView1.Items.Clear();

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["file_invoice_no"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["name"].ToString()}));

                }


                sqliteConnection.Close();
            }

            else if (comboBox1.Text == "Invoice No")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select * from exportfiledetails where file_invoice_no = '" + comboBox2.Text + "'", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    listView1.Items.Clear();

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["file_invoice_no"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["name"].ToString()}));

                }

                sqliteConnection.Close();
            }

            else if (comboBox1.Text == "Customer Ref No")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select * from exportfiledetails where invoiceno = '" + comboBox2.Text + "'", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    listView1.Items.Clear();

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["file_invoice_no"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["name"].ToString()}));

                }

                deleteEmptySpacesFromComboBox();


                sqliteConnection.Close();
            }

            else if (comboBox1.Text == "Client Name")
            {
                sqliteConnection.Open();

                sqliteCommand = new SQLiteCommand("select * from exportfiledetails where name = '" + comboBox2.Text + "'", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    listView1.Items.Clear();

                    listView1.Items.Add(new ListViewItem(new string[] { sqliteDataReader["fileno"].ToString(),
                        sqliteDataReader["file_invoice_no"].ToString(),
                        sqliteDataReader["invoiceno"].ToString(),
                        sqliteDataReader["name"].ToString()}));

                }

                sqliteConnection.Close();
            }


        }


        private void deleteEmptySpacesFromComboBox()
        {
            int i = 0;
            while (i < comboBox2.Items.Count)
            {
                if (comboBox2.Items[i].Equals(""))
                    comboBox2.Items.RemoveAt(i);
                i++;
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (!StartPage.Admin)
            {
                MessageBox.Show("You don't have rights to Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Export formExport = new Export();
                formExport.filenobox.Text = listView1.SelectedItems[0].Text;
                formExport.Show();
                this.Close();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected entry?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.OK)
            {
                sqliteConnection = new SQLiteConnection(@"data source = main.db");

                sqliteConnection.Open();
                sqliteCommand = new SQLiteCommand("delete from files where fileno = '" + listView1.SelectedItems[0].Text + "'", sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                

                sqliteCommand = new SQLiteCommand("delete from exportfiledetails where fileno = '" + listView1.SelectedItems[0].Text + "'", sqliteConnection);
                sqliteCommand.ExecuteNonQuery();

                sqliteCommand = new SQLiteCommand("delete from exportfileparticulars where fileno = '" + listView1.SelectedItems[0].Text + "'", sqliteConnection);
                sqliteCommand.ExecuteNonQuery();

                sqliteCommand = new SQLiteCommand("delete from pay where fileno = '" + listView1.SelectedItems[0].Text + "' and payer ='" + listView1.SelectedItems[0].SubItems[3].Text + "' and amountcheck = '1'", sqliteConnection);
                sqliteCommand.ExecuteNonQuery();

                sqliteConnection.Close();

                MessageBox.Show("Entry deleted successfully.", "Operation Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listView1.Refresh();

            }
        }
    }
}
