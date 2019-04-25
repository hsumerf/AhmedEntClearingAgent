using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public static class Sqlite
    {
        public static string ntnnumber;
        public static string strnumber;
        public static string address;
        public static string telno;
        public static string cellno;
        public static string emailaddress;
        public static string companyname;
        public static string companydescription;
        public static string chl;
        public static HeaderDataSet headerDataSet;

        public static string[] LoadFiles()
        {
            List<string> namelist = new List<string>();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select fileno from files", scn);
            SQLiteDataReader dr = sq.ExecuteReader();

            while (dr.Read())
            {

                namelist.Add(dr["fileno"].ToString());
            }

            scn.Close();
            return namelist.ToArray();
            
        }

        public static string[] LoadImportFiles()
        {
            List<string> namelist = new List<string>();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select fileno from importfiledetails", scn);
            SQLiteDataReader dr = sq.ExecuteReader();

            while (dr.Read())
            {

                namelist.Add(dr["fileno"].ToString());
            }

            scn.Close();
            return namelist.ToArray();

        }

        public static string[] LoadExportFiles()
        {
            List<string> namelist = new List<string>();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select fileno from exportfiledetails", scn);
            SQLiteDataReader dr = sq.ExecuteReader();

            while (dr.Read())
            {

                namelist.Add(dr["fileno"].ToString());
            }

            scn.Close();
            return namelist.ToArray();

        }

        public static void LoadCompany()
        {
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select * from CompanyTable", scn);
            SQLiteDataReader dr = sq.ExecuteReader();

            while (dr.Read())
            {

                ntnnumber = dr["ntnno"].ToString();
                strnumber = dr["strno"].ToString();
                address = dr["address"].ToString();
                telno = dr["telno"].ToString();
                cellno = dr["cellno"].ToString();
                emailaddress = dr["emailaddress"].ToString();
            }
            scn.Close();
        }

        public static void LoadHeaderDataSet()
        {
            headerDataSet = new HeaderDataSet();
            //DataSet1 expDataSet = new DataSet1();
            DataTable tempDataTable = headerDataSet.Tables.Add("Items");
            tempDataTable.Columns.Add("companyname");
            tempDataTable.Columns.Add("companydescription");
            tempDataTable.Columns.Add("ntnno");
            tempDataTable.Columns.Add("strno");
            tempDataTable.Columns.Add("chl");


            //adding values
            DataRow row = tempDataTable.NewRow();
            row["companyname"] = "Ahmed Trading Co.";
            row["companydescription"] = "Custom Clearing, Forwarding & Shipping Agents";
            row["ntnno"] = Sqlite.ntnnumber;
            row["strno"] = Sqlite.strnumber;
            row["chl"] = "CHL:2344";


            tempDataTable.Rows.Add(row);
        }

        public static void ColumnSorter(int index, System.Windows.Forms.ListView list)
        {
            sorter sorter = list.ListViewItemSorter as sorter;
            if (sorter == null)
            {
                sorter = new sorter(index);
                list.ListViewItemSorter = sorter;
            }
            else
            {
                sorter.Column = index;
            }
            list.Sort();
            list.ListViewItemSorter = null;
        }



        public static string[] LoadClients()
        {
            List<string> namelist = new List<string>();
            SQLiteConnection scn = new SQLiteConnection(@"data source = main.db");
            scn.Open();
            SQLiteCommand sq;

            sq = new SQLiteCommand("select name from clients", scn);
            SQLiteDataReader dr = sq.ExecuteReader();

            while (dr.Read())
            {

                namelist.Add(dr["name"].ToString());
            }

            scn.Close();
            return namelist.ToArray();
        }

        public static string GetFolderPath()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return folderDialog.SelectedPath;
            else
                return "";
        }
        public static void ExportToExcel(string path, ListView listsource)
        {
            StringBuilder CVS = new StringBuilder();
            for (int i = 0; i < listsource.Columns.Count; i++)
            {
                CVS.Append(listsource.Columns[i].Text + ",");
            }
            CVS.Append(Environment.NewLine);
            for (int i = 0; i < listsource.Items.Count; i++)
            {
                for (int j = 0; j < listsource.Columns.Count; j++)
                {
                    CVS.Append(listsource.Items[i].SubItems[j].Text + ",");
                }
                CVS.Append(Environment.NewLine);
            }
            System.IO.File.WriteAllText(path, CVS.ToString());
            Process.Start(path);
        }
    }
}
