using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace DBDocs_Editor
{
    public class ProgSettings
    {   // Common Functions will be added in here
        private static string dbName = "newm0";
        private static string serverName = "localhost";
        private static string userName = "root";
        private static string password = "mangos";
        public static string sqldBconn = "";
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public static string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                if (dbName != "*")
                {
                    sqldBconn = "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + password + ";";
                }
                else
                {
                    sqldBconn = "SERVER=" + serverName + ";UID=" + userName + ";PWD=" + password + ";";
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public static string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                if (dbName != "*")
                {
                    sqldBconn = "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + password + ";";
                }
                else
                {
                    sqldBconn = "SERVER=" + serverName + ";UID=" + userName + ";PWD=" + password + ";";
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public static string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
                if (dbName != "*")
                {
                    sqldBconn = "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + password + ";";
                }
                else
                {
                    sqldBconn = "SERVER=" + serverName + ";UID=" + userName + ";PWD=" + password + ";";
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the db.
        /// </summary>
        /// <value>The name of the db.</value>
        public static string DbName
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
                if (dbName != "*")
                {
                    sqldBconn = "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + password + ";";
                }
                else
                {
                    sqldBconn = "SERVER=" + serverName + ";UID=" + userName + ";PWD=" + password + ";";
                }
            }
        }

        /// <summary>
        /// Returns a dataset contain the results of the query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet SelectRows(string query)
        {
            var conn = new MySqlConnection(sqldBconn);
            var adapter = new MySqlDataAdapter();
            var myDataset = new DataSet();
            adapter.SelectCommand = new MySqlCommand(query, conn);
            try
            {
                adapter.Fill(myDataset);
            }
            catch (Exception ex)
            {
                myDataset = null;
                MessageBox.Show("There was an error connecting to the database: \n" + ex.Message);
            }
            adapter.Dispose();
            conn.Close();
            return myDataset;
        }

        public static string SetLocalisationModifier(string langsetting)
        {
            if (langsetting.Length > 2)
            {
                langsetting = langsetting.Substring(langsetting.Length - 2);
            }
            if (langsetting == "EN")
            {
                langsetting = "";
            }

            if (!string.IsNullOrEmpty(langsetting))
            {
                langsetting = "_" + langsetting;
            }
            return langsetting;
        }

        public static void LoadLangs(ListBox lstLangs)
        {
            lstLangs.Items.Clear();
            lstLangs.Items.Add("ENGLISH  - EN");
            lstLangs.Items.Add("FRANCAIS - FR");
            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
        }
    }
}