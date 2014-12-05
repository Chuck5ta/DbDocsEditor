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

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;

            DataSet dbViewList = null;
            if (lstLangs.SelectedIndex == 0)
            {   // If English, connect to main table
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocsfields`.`FieldNotes` FROM `dbdocsfields` WHERE `FieldName` = '" + selectedField + "'");
            }
            else
            {   // If Non-English, join to localised table and grab field
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocsfields_localised`.`FieldNotes`, `dbdocsfields`.`FieldNotes` as FieldNotesEnglish FROM `dbdocsfields` INNER JOIN `dbdocsfields_localised` ON `dbdocsfields`.`fieldId` = `dbdocsfields_localised`.`fieldId` where TableName='" + TableName + "'" + " AND FieldName='" + selectedField + "' (AND `dbdocsfields_localised`.`languageId`=" + lstLangs.SelectedIndex + "  OR `dbdocsfields`.`languageId`=0);");
            }

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtFieldNotes.Text = dbViewList.Tables[0].Rows[0]["FieldNotes"].ToString();
                    if (chkUseEnglish.Checked == true)
                    {
                        if (string.IsNullOrEmpty(txtFieldNotes.Text))
                        {
                            txtFieldNotes.Text = dbViewList.Tables[0].Rows[0]["FieldNotesEnglish"].ToString();
                        }
                    }

                    chkDBDocsEntry.Checked = true;

                    //Check for Subtables
                    ProgSettings.ExtractSubTables(txtFieldNotes.Text, lstSubtables);
                }
                else  // No dbdocs match
                {
                    txtFieldNotes.Text = "";
                    chkDBDocsEntry.Checked = false;
                }
            }
            else  // No dbdocs match
            {
                txtFieldNotes.Text = "";
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
        //    var dbDocsTableOutput = new StringBuilder();
        //    string outputFolder = Application.ExecutablePath;

        //    // Strip the Executable name from the path
        //    outputFolder = outputFolder.Substring(0, outputFolder.LastIndexOf(@"\"));

        //    string selectedField = lstFields.Text;
        //    txtFieldName.Text = selectedField;

        //    if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
        //    string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

        //    //delete from `dbdocsfields` where `tableName`= 'creature' and `fieldName`= 'entry';
        //    //insert  into `dbdocsfields`(`tableName`,`fieldName`,`tableNotes`) values ('creature','entry','xxxx');
        //    dbDocsTableOutput.AppendLine("delete from `dbdocsfields" + selectedLang + "` where `tableName`= '" + TableName + " and `fieldName`= '" + selectedField + "';");
        //    dbDocsTableOutput.AppendLine("insert  into `dbdocsfields" + selectedLang + "`(`tableName`,`fieldName`,`tableNotes`) values ('" + TableName + "','" + selectedField + "','" + txtFieldNotes.Text + "');");

        //    // If the output folder doesnt exist, create it
        //    if (!Directory.Exists(outputFolder + @"\"))
        //    {
        //        Directory.CreateDirectory(outputFolder + @"\");
        //    }

        //    // Open the file for append and write the entries to it
        //    using (var outfile = new StreamWriter(outputFolder + @"\dbdocsFields" + selectedLang + ".SQL", true))
        //    {
        //        outfile.Write(dbDocsTableOutput.ToString());
        //    }

        //    //Now the next part, updating the db directly
        //    if (chkDBDocsEntry.Checked == false)
        //    {       //INSERT
        //        ProgSettings.FieldInsert(TableName, selectedField, txtFieldNotes.Text);
        //    }
        //    else
        //    {       //UPDATE
        //        ProgSettings.FieldUpdate(TableName, selectedField, txtFieldNotes.Text);
        //    }

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