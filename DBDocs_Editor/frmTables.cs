using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmTables : Form
    {
        public frmTables()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate the entry information when the item is selected in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = lstTables.Text;
            txtTableName.Text = selectedTable;

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
            string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Text);

            var dbViewList = ProgSettings.SelectRows("SELECT TableNotes FROM dbdocstable" + selectedLang + " where TableName='" + selectedTable + "'");
            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtTableNotes.Text = dbViewList.Tables[0].Rows[0]["TableNotes"].ToString();
                    chkDBDocsEntry.Checked = true;

                    //Check for Subtables
                    ProgSettings.ExtractSubTables(txtTableNotes.Text, lstSubtables);
                }
                else  // No dbdocs match
                {
                    txtTableNotes.Text = "";
                    chkDBDocsEntry.Checked = false;
                }
            }
            else  // No dbdocs match
            {
                txtTableNotes.Text = "";
                chkDBDocsEntry.Checked = false;
            }

            btnShowFields.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            ProgSettings.ShowThisForm(ProgSettings.mainForm);
            Close();
        }

        private void btnShowFields_Click(object sender, EventArgs e)
        {
            string selectedTable = lstTables.Text;

            var fieldScreen = new frmFields { TableName = selectedTable };
            fieldScreen.Show();
        }

        private void frmTables_Load(object sender, EventArgs e)
        {
            // The following command reads all the columns for the selected table
            DataSet dbViewList = ProgSettings.SelectRows("SELECT T.TABLE_NAME AS TableName, T.ENGINE AS TableEngine, T.TABLE_COMMENT AS TableComment FROM INFORMATION_SCHEMA.Tables T WHERE T.TABLE_NAME <> 'dtproperties' AND T.TABLE_SCHEMA <> 'INFORMATION_SCHEMA' AND t.Table_schema='" + ProgSettings.DbName + "' ORDER BY T.TABLE_NAME");

            // Did we return anything
            if (dbViewList != null)
            {
                // Do we have rows
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    lstTables.Items.Clear();

                    // for each Field returned, populate the listbox with the table name
                    for (var thisRow = 0; thisRow <= dbViewList.Tables[0].Rows.Count - 1; thisRow++)
                    {
                        var tableName = dbViewList.Tables[0].Rows[thisRow]["TableName"].ToString();
                        lstTables.Items.Add(tableName);
                    }
                }
            }

            ProgSettings.LoadLangs(lstLangs);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dbDocsTableOutput = new StringBuilder();
            string outputFolder = Application.ExecutablePath;

            // Strip the Executable name from the path
            outputFolder = outputFolder.Substring(0, outputFolder.LastIndexOf(@"\"));

            string selectedTable = lstTables.Text;
            txtTableName.Text = selectedTable;

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
            string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

            //delete from `dbdocstable` where `tableName`= 'creature';
            //insert  into `dbdocstable`(`tableName`,`tableNotes`) values ('script_texts','xxxx');
            dbDocsTableOutput.AppendLine("delete from `dbdocstable" + selectedLang + "` where `tableName`= '" + selectedTable + "';");
            dbDocsTableOutput.AppendLine("insert  into `dbdocstable" + selectedLang + "`(`tableName`,`tableNotes`) values ('" + selectedTable + "','" + txtTableNotes.Text + "');");

            // If the output folder doesnt exist, create it
            if (!Directory.Exists(outputFolder + @"\"))
            {
                Directory.CreateDirectory(outputFolder + @"\");
            }

            // Open the file for append and write the entries to it
            using (var outfile = new StreamWriter(outputFolder + @"\dbdocsTable" + selectedLang + ".SQL", true))
            {
                outfile.Write(dbDocsTableOutput.ToString());
            }

            //Now the next part, updating the db directly
            if (chkDBDocsEntry.Checked == false)
            {       //INSERT
                ProgSettings.TableInsert(selectedTable, txtTableNotes.Text);
            }
            else
            {       //UPDATE
                ProgSettings.TableUpdate(selectedTable, txtTableNotes.Text);
            }

            MessageBox.Show("Save Complete");
        }

        private void btnShowSubtables_Click(object sender, EventArgs e)
        {
            var subTableScreen = new frmSubtables { subTableId = "" };
            subTableScreen.Show();
        }

        private void lstSubtables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // The subtable entry in the listbox starts xx:, to need to trim everything after the :
            string subTableId = lstSubtables.Text;
            if (subTableId.Contains(":"))
            {
                subTableId = subTableId.Substring(0, subTableId.IndexOf(":"));
                var subTableScreen = new frmSubtables { subTableId = subTableId };
                subTableScreen.Show();
            }
        }
    }
}