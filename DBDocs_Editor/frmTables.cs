using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmTables : Form
    {
        int tableId = 0;

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

            DataSet dbViewList = null;
            if (lstLangs.SelectedIndex == 0)
            {   // If English, connect to main table
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocstable`.`tableId`,`dbdocstable`.`tableNotes` FROM `dbdocstable` WHERE `tablename` = '" + selectedTable + "'"); 
            }
            else
            {   // If Non-English, join to localised table and grab field
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocstable_localised`.`tableNotes`, `dbdocstable`.`tableId`, `dbdocstable`.`tableNotes` as TableNotesEnglish FROM `dbdocstable` LEFT JOIN `dbdocstable_localised` ON `dbdocstable`.`tableId` = `dbdocstable_localised`.`tableId` WHERE `tablename` = '" + selectedTable + "' AND (`dbdocstable_localised`.`languageId`=" + lstLangs.SelectedIndex + " OR `dbdocstable`.`languageId`=0)"); 
            }

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtTableNotes.Text = dbViewList.Tables[0].Rows[0]["TableNotes"].ToString();
                    tableId = Convert.ToInt32(dbViewList.Tables[0].Rows[0]["TableId"]);
                        
                    // If the 'Use English' if blank checkbox is ticked
                    if (chkUseEnglish.Checked == true)
                    {   // If Localised SubTable Template is blank, go grab the English
                        if (string.IsNullOrEmpty(txtTableNotes.Text))
                        {
                            txtTableNotes.Text = dbViewList.Tables[0].Rows[0]["TableNotesEnglish"].ToString();
                        }
                    }
                    else
                    {
                        txtTableNotes.Text = "";
                    }
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

            // If the output folder doesnt exist, create it
            if (!Directory.Exists(outputFolder + @"\"))
            {
                Directory.CreateDirectory(outputFolder + @"\");
            }

            if (tableId == 0)   // New Record
            {
                if (lstLangs.SelectedIndex != 0)
                {   // If English, connect to main table
                    dbDocsTableOutput.AppendLine("-- WARNING: The default entry should really be in english --");
                }

                //insert  into `dbdocstable`(`languageId`,`tableName`,`tableNotes`) values (2,'script_texts','xxxx');
                dbDocsTableOutput.AppendLine("insert  into `dbdocstable`(`languageId`,`tableName`,`tableNotes`) values (" + lstLangs.SelectedIndex + ", '" + selectedTable + "','" + txtTableNotes.Text + "');");

                // Open the file for append and write the entries to it
                using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable.SQL", true))
                {
                    outfile.Write(dbDocsTableOutput.ToString());
                }

                // Write the entry out to the Database directly

                // For an insert, the record is always saved to the primary table, regardless of the language
                // Since the system is English based, it should really have an English base record.

                //                       Selected Table        Language ID              Notes
                ProgSettings.TableInsert(selectedTable, lstLangs.SelectedIndex, txtTableNotes.Text);
        
            }
            else                // Updated Record
            {
                if (lstLangs.SelectedIndex == 0)
                {   // If English, connect to main table
                    //update `dbdocstable` set `languageId`=xx,`tableName`=yy,`tableNotes`=zz where tableId=aa;
                    dbDocsTableOutput.AppendLine("update `dbdocstable` set `languageId`=" + lstLangs.SelectedIndex + ", `tableName`='" + selectedTable + "', `tableNotes`='" + txtTableNotes.Text + "' where tableId=" + tableId + ";");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }
                else
                {
                    dbDocsTableOutput.AppendLine("delete from `dbdocstable_localisation` where `languageId`=" + lstLangs.SelectedIndex + " and `tableId`= " + tableId + ";");
                    dbDocsTableOutput.AppendLine("insert  into `dbdocstable_localisation`(`tableId`,`languageId`,`tableNotes`) values ("+ tableId + ", " + lstLangs.SelectedIndex + ", '" + txtTableNotes.Text + "');");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable_localised.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }

                // Write the entry out to the Database directly
                // For an update the logic to decide which table to update is in the Update function itself
                
                //                       Table ID         Language ID          Notes
                ProgSettings.TableUpdate(tableId, lstLangs.SelectedIndex, txtTableNotes.Text);

            }

            MessageBox.Show("Save Complete");
        }


        private void btnShowSubtables_Click(object sender, EventArgs e)
        {
            var subTableScreen = new frmSubtables { subTableId = 0 };
            subTableScreen.Show();
        }

        private void lstSubtables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // The subtable entry in the listbox starts xx:, to need to trim everything after the :
            int thissubTableId = 0;
            if (lstSubtables.Text.Contains(":"))
            {
                thissubTableId = Convert.ToInt32(lstSubtables.Text.Substring(0, lstSubtables.Text.IndexOf(":")));
                var subTableScreen = new frmSubtables { subTableId = thissubTableId };
                subTableScreen.Show();
            }
        }

        private void lstLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Are we looking for a localised version ?
            
            
            if (lstLangs.SelectedIndex == 0)
            {
                if (ProgSettings.LookupTableEntry(lstLangs.SelectedIndex, tableId) == true)
                {
                    chkDBDocsEntry.Checked = true;
                }
                else
                {
                    chkDBDocsEntry.Checked = false;
                }
            }
            else
            {
                // Check whether a localised version exists
                if (ProgSettings.LookupTableEntryLocalised(lstLangs.SelectedIndex, tableId) == true)
                {
                    chkDBDocsEntry.Checked = true;
                }
                else
                {
                    chkDBDocsEntry.Checked = false;
                    
                    // If 'Use English' is not selected, clear the text
                    if (chkUseEnglish.Checked == false)
                    {
                        txtTableNotes.Text = "";
                    }
                }
            }
        }
    }
}