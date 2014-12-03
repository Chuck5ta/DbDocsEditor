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
        private static string dbName = "";
        private static string serverName = "";
        private static string userName = "";
        private static string password = "";
        public static string sqldBconn = "";
        public static Form mainForm;

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

        /// <summary>
        /// Inserts a record into the dbdocstable table
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableNotes">The table notes.</param>
        public static void TableInsert(string tableName, string tableNotes)
        {
            var conn = new MySqlConnection(sqldBconn);
            var cmd = new MySqlCommand("", conn);
            cmd.Connection.Open();
            cmd.CommandText = "insert  into `dbdocstable`(`tableName`,`tableNotes`) "
                              + "VALUES (@tablename, @tablenotes)";

            cmd.Parameters.AddWithValue("@tablename", tableName);
            cmd.Parameters.AddWithValue("@tablenotes", tableNotes);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// Update a record in the dbdocstable table
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableNotes">The table notes.</param>
        public static void TableUpdate(string tableName, string tableNotes)
        {
            var conn = new MySqlConnection(sqldBconn);
            var cmd = new MySqlCommand("", conn);
            cmd.Connection.Open();
            cmd.CommandText = "update `dbdocstable` set `tablenotes`=@tableNotes where `tablename`=@tablename";

            cmd.Parameters.AddWithValue("@tablename", tableName);
            cmd.Parameters.AddWithValue("@tablenotes", tableNotes);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// Inserts a record into the dbdocsfields table
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldNotes">The field notes.</param>
        public static void FieldInsert(string tableName, string fieldName, string fieldNotes)
        {
            var conn = new MySqlConnection(sqldBconn);
            var cmd = new MySqlCommand("", conn);
            cmd.Connection.Open();
            cmd.CommandText = "insert  into `dbdocsfields`(`tableName`,`fieldName`,`fieldNotes`) "
                              + "VALUES (@tablename, @fieldname, @tablenotes)";

            cmd.Parameters.AddWithValue("@tablename", tableName);
            cmd.Parameters.AddWithValue("@fieldName", fieldName);
            cmd.Parameters.AddWithValue("@fieldNotes", fieldNotes);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// Updates a record in the dbdocsfields table
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldNotes">The field notes.</param>
        public static void FieldUpdate(string tableName, string fieldName, string fieldNotes)
        {
            var conn = new MySqlConnection(sqldBconn);
            var cmd = new MySqlCommand("", conn);
            cmd.Connection.Open();
            cmd.CommandText = "update `dbdocsfields` set `fieldnotes`=@fieldNotes where `tablename`=@tablename and `fieldname`=@fieldname";

            cmd.Parameters.AddWithValue("@tablename", tableName);
            cmd.Parameters.AddWithValue("@fieldName", fieldName);
            cmd.Parameters.AddWithValue("@fieldNotes", fieldNotes);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// Redisplays the form 'formName' passed
        /// </summary>
        /// <param name="formName"></param>

        public static void ShowThisForm(Form formName)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm == formName)
                {
                    frm.Show();
                    return;
                }
            }
        }

        /// <summary>
        /// Writes the connection setting into the registry.
        /// </summary>
        public static void WriteRegistry()
        { // The name of the key must include a valid root.
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\MaNGOS\\DBDocsEditor\\Connections";
            const string keyName = userRoot + "\\" + subkey;

            // Concat all the login requirements into a single field
            string[] connectionString = { serverName, userName, password, dbName };

            // Write in into the registry as a key call "<servername>-<dbname>"
            Registry.SetValue(keyName, serverName + "-" + DbName, connectionString);

            // Rebuild the internal list of connections
            PopulateConnections();
        }

        /// <summary>
        /// Deletes the connection setting from the registry. Has Two uses: a) The delete the selected connect entry, b) To remove the old entry when a server connnection is renamed
        /// </summary>
        /// <param name="keyname">The keyname.</param>
        public static void DeleteConnection(string keyname)
        {
            const string subkey = "Software\\MaNGOS\\DBDocsEditor\\Connections";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, true))
            {
                if (key == null)
                {
                    // Key doesn't exist. Do whatever you want to handle this case
                }
                else
                {
                    key.DeleteValue(keyname);
                }
            }
        }

        /// <summary>
        /// Populates the connection information from the registry and returns a list of the entries.
        /// </summary>
        /// <returns></returns>
        public static List<ConnectionInfo> PopulateConnections()
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\MaNGOS\\DBDocsEditor\\Connections";

            RegistryKey rk = Registry.CurrentUser;

            // Open a subKey as read-only
            RegistryKey dbDocsKey = rk.OpenSubKey(subkey);
            if (dbDocsKey != null)
            {
                var names = dbDocsKey.GetValueNames();

                if (names.GetLength(0) > 0)
                {
                    var allconnections = new List<ConnectionInfo>();
                    var connections = new ConnectionInfo();

                    // For every Connection entry in the registry
                    foreach (String connectionstring in names)
                    {
                        var theseValues = (string[])Registry.GetValue(userRoot + "\\" + subkey, connectionstring, "");

                        // Populate a connectionInfo structure with the values from the registry
                        connections.ServerNameorIp = theseValues[0];
                        connections.DatabaseUserName = theseValues[1];
                        connections.DatabasePassword = theseValues[2];
                        connections.DatabaseName = theseValues[3];

                        // Add the structure to a list
                        allconnections.Add(connections);
                    }
                    return allconnections;
                }
            }
            return null;
        }

        private static string GetSubtables(string subTableText)
        {
            var startPos = 1;
            var retString = "";

            while (startPos > 0)
            {        //¬subtable:xxx¬
                startPos = subTableText.IndexOf("¬subtable:", startPos + 1, System.StringComparison.Ordinal);
                if (startPos > 0)
                {
                    startPos = startPos + 10;
                }
                if (startPos <= 0) continue;

                var endPos = subTableText.IndexOf("¬", startPos + 1, System.StringComparison.Ordinal);
                retString = retString + subTableText.Substring(startPos, endPos - startPos) + ",";
            }
            return retString;
        }

        public static void ExtractSubTables(string SourceText, ListBox lstSubTables)
        {
            lstSubTables.Items.Clear();
            if (SourceText.Contains("¬subtable:"))
            {
                string subtables = GetSubtables(SourceText);
                string[] stringSeparators = new string[] { "," };
                string[] subarray = subtables.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tableid in subarray)
                {
                    if (!string.IsNullOrEmpty(tableid))
                    {
                        System.Data.DataSet dbViewSubtable = new System.Data.DataSet();
                        dbViewSubtable = ProgSettings.SelectRows("SELECT subtablename FROM dbdocssubtables where subtableid=" + tableid);
                        if (dbViewSubtable != null)
                        {
                            if (dbViewSubtable.Tables[0].Rows.Count > 0)
                            {
                                lstSubTables.Items.Add(subtables.Replace(",", "") + ":" + dbViewSubtable.Tables[0].Rows[0]["subtablename"].ToString());
                            }
                        }
                    }
                }
//                MessageBox.Show("Found subtables: " + subtables);
            }
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


        public struct ConnectionInfo
        {
            public string ServerNameorIp;
            public string DatabaseName;
            public string DatabaseUserName;
            public string DatabasePassword;
        }
    }
}