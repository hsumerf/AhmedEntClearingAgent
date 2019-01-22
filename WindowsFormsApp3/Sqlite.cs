using System;
using System.Collections.Generic;
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

            return namelist.ToArray();
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

            return namelist.ToArray();
        }

    }
}
