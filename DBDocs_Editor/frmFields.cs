using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmFields : Form
    {
        // Since we rely on a TableName for the Fields references, set a public one here which can be set by the caller
        public string TableName = "";
		int fieldId = 0;

        public frmFields()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate the entry information when the item is selected in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedField = lstFields.Text;
            txtFieldName.Text = selectedField;
            fieldId = ProgSettings.LookupFieldId(TableName, selectedField);    // Force to new entry before the lookup updates it should it exist

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;

            DataSet dbViewList = null;
            if (lstLangs.SelectedIndex == 0)
            {   // If English, connect to main table
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocsfields`.`FieldId`, `dbdocsfields`.`FieldNotes`,`dbdocsfields`.`FieldComment` FROM `dbdocsfields` WHERE `tablename` = '" + TableName + "' and `FieldName` = '" + selectedField + "'");
            }
            else
            {   // If Non-English, join to localised table and grab field
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocsfields`.`FieldId`, `dbdocsfields_localised`.`FieldNotes`, `dbdocsfields_localised`.`FieldComment`,`dbdocsfields`.`FieldNotes` as FieldNotesEnglish, `dbdocsfields`.`FieldComment` as FieldCommentEnglish FROM `dbdocsfields` INNER JOIN `dbdocsfields_localised` ON `dbdocsfields`.`fieldId` = `dbdocsfields_localised`.`fieldId` where TableName='" + TableName + "'" + " AND FieldName='" + selectedField + "' AND (`dbdocsfields_localised`.`languageId`=" + lstLangs.SelectedIndex + "  OR `dbdocsfields`.`languageId`=0);");
            }

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtFieldNotes.Text = dbViewList.Tables[0].Rows[0]["FieldNotes"].ToString();
                    txtFieldComment.Text = dbViewList.Tables[0].Rows[0]["FieldComment"].ToString();
                    if (string.IsNullOrEmpty(txtFieldComment.Text)) 
                    {
                        txtFieldComment.Text=txtFieldNotes.Text.Substring(0,80);
                    }

                    fieldId = Convert.ToInt32(dbViewList.Tables[0].Rows[0]["fieldId"]);
                        
                    // If the 'Use English' if blank checkbox is ticked
                    if (chkUseEnglish.Checked == true)
                    {   // If Localised SubTable Template is blank, go grab the English
                        if (string.IsNullOrEmpty(txtFieldNotes.Text))
                        {
                            txtFieldNotes.Text = dbViewList.Tables[0].Rows[0]["FieldNotesEnglish"].ToString();
                        }
                        if (string.IsNullOrEmpty(txtFieldComment.Text))
                        {
                            txtFieldComment.Text = dbViewList.Tables[0].Rows[0]["FieldCommentEnglish"].ToString();
                        }
                        if (string.IsNullOrEmpty(txtFieldComment.Text)) 
                        { 
                            txtFieldComment.Text = txtFieldNotes.Text.Substring(0, 80); 
                        }
                    }
                    else
                    {
                        txtFieldNotes.Text = "";
                        txtFieldComment.Text="";
                    }
                    chkDBDocsEntry.Checked = true;

                    //Check for Subtables
                    ProgSettings.ExtractSubTables(txtFieldNotes.Text, lstSubtables);
                }
                else  // No dbdocs match
                {
                    txtFieldNotes.Text = "";
                    txtFieldComment.Text="";
                    chkDBDocsEntry.Checked = false;
                }
            }
            else  // No dbdocs match
            {
                txtFieldNotes.Text = "";
                txtFieldComment.Text="";
                chkDBDocsEntry.Checked = false;
            }

            btnSave.Enabled = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            Text = "DBDocs for Table: " + TableName;

            // The following command reads all the columns for the selected table
            DataSet dbViewList = ProgSettings.SelectRows("SHOW COLUMNS FROM " + TableName);

            // Did we return anything
            if (dbViewList != null)
            {
                // Do we have rows
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    lstFields.Items.Clear();

                    // for each Field returned, populate the listbox with the column name
                    for (var thisRow = 0; thisRow <= dbViewList.Tables[0].Rows.Count - 1; thisRow++)
                    {
                        var fieldName = dbViewList.Tables[0].Rows[thisRow]["Field"].ToString();
                        lstFields.Items.Add(fieldName);
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

            string selectedField = lstFields.Text;
            txtFieldName.Text = selectedField;

            // If the output folder doesnt exist, create it
            if (!Directory.Exists(outputFolder + @"\"))
            {
                Directory.CreateDirectory(outputFolder + @"\");
            }

            if (fieldId == 0)   // New Record
            {
                if (lstLangs.SelectedIndex != 0)
                {   // If English, connect to main table
                    dbDocsTableOutput.AppendLine("-- WARNING: The default entry should really be in english --");
                }

                //insert  into `dbdocsfields`(`languageId`, `tableName`,`fieldName`,`tableNotes`) values (0,'creature','entry','xxxx');
                dbDocsTableOutput.AppendLine("insert  into `dbdocsfields` (`languageId`,`tableName`,`fieldName`,`tableComments`,`tableNotes`) values (" + lstLangs.SelectedIndex + ",'" + TableName + "','" + selectedField + "','" + txtFieldComment.Text + "','" + txtFieldNotes.Text + "');");

                // Open the file for append and write the entries to it
                using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable.SQL", true))
                {
                    outfile.Write(dbDocsTableOutput.ToString());
                }

                // Write the entry out to the Database directly

                // For an insert, the record is always saved to the primary table, regardless of the language
                // Since the system is English based, it should really have an English base record.

                //                          Language Id        Selected Table   Field              Notes
                ProgSettings.FieldInsert(lstLangs.SelectedIndex, TableName, selectedField, txtFieldComment.Text, txtFieldNotes.Text);
        
            }
            else                // Updated Record
            {
                if (lstLangs.SelectedIndex == 0)
                {   // If English, connect to main table
                    //update `dbdocsfields` set `fieldnotes`= xxx where `fieldId`= xxx and languageId=yyy;
                    dbDocsTableOutput.AppendLine("update `dbdocsfields` set `FieldComment` = " + txtFieldComment.Text + ", `fieldNotes` = " + txtFieldNotes.Text + " where `fieldId`= '" + fieldId + " and `languageId`= " + lstLangs.SelectedIndex + ";");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }
                else
                {
                    dbDocsTableOutput.AppendLine("delete from `dbdocsfields_localised` where `fieldId`= '" + fieldId + " and `languageId`= " + lstLangs.SelectedIndex + ";");
                    dbDocsTableOutput.AppendLine("insert into `dbdocsfields_localised` (`fieldId`,`languageId`,`FieldComment`,`fieldNotes`) values (" + fieldId + ", " + lstLangs.SelectedIndex + ", " + txtFieldComment.Text + ", " + txtFieldNotes.Text + ");");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocsTable_localised.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }

                // Write the entry out to the Database directly
                // For an update the logic to decide which table to update is in the Update function itself
                
                //                       Field ID         Language ID          Notes
                ProgSettings.FieldUpdate(fieldId, lstLangs.SelectedIndex, txtFieldComment.Text, txtFieldNotes.Text);

            }

            //MessageBox.Show("Save Complete");
            lblStatus.Text = DateTime.Now.ToString() + " Save Complete for " + selectedField;
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
                if (ProgSettings.LookupFieldEntry(lstLangs.SelectedIndex, fieldId) == true)
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
                if (ProgSettings.LookupFieldEntryLocalised(lstLangs.SelectedIndex, fieldId) == true)
                {
                    chkDBDocsEntry.Checked = true;
                }
                else
                {
                    chkDBDocsEntry.Checked = false;
                    
                    // If 'Use English' is not selected, clear the text
                    if (chkUseEnglish.Checked == false)
                    {
                        txtFieldNotes.Text = "";
                    }
                }
            }
        }

        private void btnInsertSubtable_Click(object sender, EventArgs e)
        {
            frmSubtablesLookup lookup = new frmSubtablesLookup();
            lookup.languageId = lstLangs.SelectedIndex;
            lookup.ShowDialog();

            // Once the Dialog has closed, has the user selected an entry
            if (lookup.subTableId != 0)
            {
                var insertText = "¬subtable:" + lookup.subTableId.ToString().Trim() + "¬";
                var selectionIndex = txtFieldNotes.SelectionStart;
                txtFieldNotes.Text = txtFieldNotes.Text.Insert(selectionIndex, insertText);
                txtFieldNotes.SelectionStart = selectionIndex + insertText.Length;
            }
        }
    }
}