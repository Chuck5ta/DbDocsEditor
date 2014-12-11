using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmSubtables : Form
    {
        public int subTableId = 0;
        bool blnTextChanged = false;

        public frmSubtables()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate the entry information when the item is selected in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstsubtables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSubtable = lstsubtables.Text;
            subTableId = 0;  // Force to new entry before the lookup updates it should it exist

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;

            DataSet dbViewList = null;
            if (lstLangs.SelectedIndex == 0)
            {   // If English, connect to main table
                dbViewList = ProgSettings.SelectRows("SELECT subtableid,languageid, subtablecontent,subtabletemplate FROM `dbdocssubtables` WHERE `subtablename` = '" + selectedSubtable + "'");
            }
            else
            {   // If Non-English, join to localised table and grab field
                dbViewList = ProgSettings.SelectRows("SELECT COALESCE(`dbdocstable_localised`.`languageid`,-1) AS languageId,`dbdocssubtables`.`subtableid`, `dbdocssubtables_localised`.`subtabletemplate`, `dbdocssubtables_localised`.`subtablecontent`, `dbdocssubtables`.`subtabletemplate` as subtableTemplateEnglish, `dbdocssubtables`.`subtablecontent` as subTableContentEnglish FROM `dbdocssubtables` LEFT JOIN `dbdocssubtables_localised` ON `dbdocssubtables`.`subtableid` = `dbdocssubtables_localised`.`subtableid` WHERE `subtablename` = '" + selectedSubtable + "' AND (`dbdocssubtables_localised`.`languageId`=" + lstLangs.SelectedIndex + "  OR `dbdocssubtables`.`languageId`=0);");
            }

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dbViewList.Tables[0].Rows[0]["languageId"]) == lstLangs.SelectedIndex)
                    { 
                        txtSubtableContent.Text = dbViewList.Tables[0].Rows[0]["subtablecontent"].ToString();
                        txtSubtableTemplate.Text = dbViewList.Tables[0].Rows[0]["subtabletemplate"].ToString();
                    }
                    else 
                    {
                        txtSubtableContent.Text = "";
                        txtSubtableTemplate.Text = ""; 
                    }

                    subTableId = Convert.ToInt32(dbViewList.Tables[0].Rows[0]["subtableid"]);
                        
                    // If the 'Use English' if blank checkbox is ticked
                    if (chkUseEnglish.Checked == true)
                    {   // If Localised SubTable Template is blank, go grab the English
                        if (string.IsNullOrEmpty(txtSubtableTemplate.Text))
                        {
                            txtSubtableContent.Text = dbViewList.Tables[0].Rows[0]["subtablecontentEnglish"].ToString();
                            txtSubtableTemplate.Text = dbViewList.Tables[0].Rows[0]["subtabletemplateEnglish"].ToString();
                        }
                    }

                    // Render the HTML
                    webBrowse.DocumentText = txtSubtableContent.Text;

                    if (string.IsNullOrEmpty(txtSubtableTemplate.Text))
                    {   //If the template is missing, attempt to build it from the content, only for historic entries !!
                        txtSubtableTemplate.Text = ProgSettings.ConvertHtmlToTemplate(txtSubtableContent.Text);

                        //Save the updated Template
                        btnSave_Click(sender, e);
                    }
                }
                else  // No dbdocs match
                {
                    txtSubtableContent.Text = "";
					txtSubtableTemplate.Text = "";
                }
            }
            else  // No dbdocs match
            {
                txtSubtableContent.Text = "";
				txtSubtableTemplate.Text = "";
            }
            blnTextChanged = false;
            btnSave.Enabled = false;
            mnuSave.Enabled = btnSave.Enabled;
        }

        private void frmsubtables_Load(object sender, EventArgs e)
        {
            // Populate the Language Pulldown
			ProgSettings.LoadLangs(lstLangs);

            DataSet dbViewList;
            if (subTableId==0)
            {
                // The following command reads all the columns for all the subtables
                dbViewList = ProgSettings.SelectRows("SELECT subtablename from dbdocssubtables");
            }
            else
            {
                // The following command reads all the columns for just the selected subtable
                dbViewList = ProgSettings.SelectRows("SELECT subtablename from dbdocssubtables where subtableid=" + subTableId);
            }

            // Did we return anything
            if (dbViewList == null) return;

            // Do we have rows
            if (dbViewList.Tables[0].Rows.Count <= 0) return;

            // for each Field returned, populate the listbox with the table name
            for (var thisRow = 0; thisRow <= dbViewList.Tables[0].Rows.Count - 1; thisRow++)
            {
                var fieldName = dbViewList.Tables[0].Rows[thisRow]["subtablename"].ToString();
                lstsubtables.Items.Add(fieldName);
            }

            if (subTableId !=0)
            {   //Select the first entry if we passed in an Id
                if (lstsubtables.SelectedIndex < 0) lstsubtables.SelectedIndex = 0;
                Text = "SubTable: " + lstsubtables.Text;
            }
            else
            { 
                Text = "SubTables"; 
            }

            blnTextChanged = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // First thing we need to do it sync the other tabs data based on the Template
            if (txtSubtableTemplate.Text.Contains("\r\n"))
            {
                // Uses the first line of the template as the headings
                string header = txtSubtableTemplate.Text;
                header = header.Substring(0, header.IndexOf("\r\n"));

                // Renders everything beyond the first line as the table body
                string body = txtSubtableTemplate.Text;
                body = body.Substring(body.IndexOf("\r\n") + 2, body.Length - (body.IndexOf("\r\n") + 2));

                // Sync the Content tab with the current template
                txtSubtableContent.Text = ProgSettings.ConvertTemplateToHtml(header, body);

                // Not technically needed, but render the HTML panel
                webBrowse.DocumentText = txtSubtableContent.Text;
            }            
            var dbDocsTableOutput = new StringBuilder();
            string outputFolder = Application.ExecutablePath;

            // Strip the Executable name from the path
            outputFolder = outputFolder.Substring(0, outputFolder.LastIndexOf(@"\"));

            string selectedTable = lstsubtables.Text;

            // If the output folder doesnt exist, create it
            if (!Directory.Exists(outputFolder + @"\"))
            {
                Directory.CreateDirectory(outputFolder + @"\");
            }

            if (subTableId == 0)   // New Record
            {
                if (lstLangs.SelectedIndex != 0)
                {   // If English, connect to main table
                    dbDocsTableOutput.AppendLine("-- WARNING: The default entry should really be in english --");
                }

                int newSubtableId = Convert.ToInt32(ProgSettings.GetNewSubTableId());
                dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables`(`subtableId`,`languageId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + newSubtableId.ToString() + "," + lstLangs.SelectedIndex + ",'" + selectedTable + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");

                // Open the file for append and write the entries to it
                using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocssubtables.SQL", true))
                {
                    outfile.Write(dbDocsTableOutput.ToString());
                }

                // Write the entry out to the Database directly

                // For an insert, the record is always saved to the primary table, regardless of the language
                // Since the system is English based, it should really have an English base record.

                ProgSettings.SubTableInsert(newSubtableId, lstLangs.SelectedIndex, selectedTable, txtSubtableContent.Text, txtSubtableTemplate.Text);
                subTableId = newSubtableId;
                blnTextChanged = false;
                btnSave.Enabled = false;
                mnuSave.Enabled = btnSave.Enabled;
            }
            else                // Updated Record
            {
                if (lstLangs.SelectedIndex == 0)
                {   // If English, connect to main table
                    dbDocsTableOutput.AppendLine("delete from `dbdocssubtables` where `subtableId`= " + subTableId + " and languageId=" + lstLangs.SelectedIndex + ";");
                    dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables`(`subtableId`,`languageId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + subTableId.ToString() + "," + lstLangs.SelectedIndex + ",'" + selectedTable + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocssubtables.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }
                else
                {
                    dbDocsTableOutput.AppendLine("delete from `dbdocssubtables_localised` where `subtableId`= " + subTableId + " and languageId=" + lstLangs.SelectedIndex + ";");
                    dbDocsTableOutput.AppendLine("insert  into `dbdocssubtable_localised`(`subtableId`,`languageId`,`subtablecontent`,`subtableTemplate`) values (" + subTableId.ToString() + "," + lstLangs.SelectedIndex + ",'" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");

                    // Open the file for append and write the entries to it
                    using (var outfile = new StreamWriter(outputFolder + @"\" + ProgSettings.DbName + "_dbdocssubtables_localised.SQL", true))
                    {
                        outfile.Write(dbDocsTableOutput.ToString());
                    }
                }

                // Write the entry out to the Database directly
                // For an update the logic to decide which table to update is in the Update function itself
                
                ProgSettings.SubTableUpdate(subTableId, lstLangs.SelectedIndex, selectedTable, txtSubtableContent.Text, txtSubtableTemplate.Text);

                blnTextChanged = false;
                btnSave.Enabled = false;
                mnuSave.Enabled = btnSave.Enabled;

            }
            lblStatus.Text = DateTime.Now.ToString() + " Save Complete for " + selectedTable;
        }


        private void btnRebuildContent_Click(object sender, EventArgs e)
        {
            if (txtSubtableTemplate.Text.Contains("\r\n"))
            {
                string header = txtSubtableTemplate.Text;
                header = header.Substring(0,header.IndexOf("\r\n"));

                string body = txtSubtableTemplate.Text;
                body=body.Substring(body.IndexOf("\r\n")+2,body.Length-(body.IndexOf("\r\n")+2));

                txtSubtableContent.Text = ProgSettings.ConvertTemplateToHtml(header,body);
            }

            webBrowse.DocumentText = txtSubtableContent.Text;
        }

        private void btnRenderContent_Click(object sender, EventArgs e)
        {
        }

        private void lstLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Are we looking for a localised version ?
            
            
            if (lstLangs.SelectedIndex > 0)
            {
                // Check whether a localised version exists
                if (ProgSettings.LookupTableEntryLocalised(lstLangs.SelectedIndex, subTableId) == false)
                {
                    // If 'Use English' is not selected, clear the text
                    if (chkUseEnglish.Checked == false)
                    {
                        txtSubtableContent.Text = "";
                        txtSubtableTemplate.Text = "";
                    }
                }
            }
            blnTextChanged = false;
            btnSave.Enabled = false;
            mnuSave.Enabled = btnSave.Enabled;
        }

        private void txtSubTableName_TextChanged(object sender, EventArgs e)
        {
            blnTextChanged = true;
            btnSave.Enabled = true;
            mnuSave.Enabled = btnSave.Enabled;
        }

        private void frmsubtables_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Has any text changed on the form
            if (blnTextChanged == true)
            {
                // Ask the user if they which to close without saving
                var response = MessageBox.Show(this, "You have unsaved changes, continue ?", "Exit Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == System.Windows.Forms.DialogResult.No)
                {
                    // Bail out of the form closing
                    e.Cancel = true;
                }
                else
                {
                    // User said yes to closing anyway
                    ProgSettings.ShowThisForm(ProgSettings.mainForm);
                }
            }
            else
            {   // Nothing changed, close normally
                ProgSettings.ShowThisForm(ProgSettings.mainForm);
            }
        }

        private void chkUseEnglish_Click(object sender, EventArgs e)
        {
            if (chkUseEnglish.Checked == false)
            {
                chkUseEnglish.Checked = true;
            }
            else
            {
                chkUseEnglish.Checked = false;
            }
        }

        private void btnCloseWindow_Click(object sender, EventArgs e)
        {
            if (blnTextChanged == true)
            {

                var response = MessageBox.Show(this, "You have unsaved changes, continue ?", "Exit Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            ProgSettings.ShowThisForm(ProgSettings.mainForm);
            Close();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (blnTextChanged == true)
            {

                var response = MessageBox.Show(this, "You have unsaved changes, continue ?", "Exit Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            Application.Exit();
        }

        private void btnNewEntry_Click(object sender, EventArgs e)
        {
            // Get the Next Subtable EntryID
            int thissubTableId = Convert.ToInt32(ProgSettings.GetNewSubTableId());

            string thisSubtableName = "";
            DialogResult returnVal = 0;
            returnVal = ProgSettings.ShowInputDialog(ref thisSubtableName, "subTable Name");

            // If the user clicked ok, add the new table
            if (returnVal == DialogResult.OK) 
            {
                // Add to the table
                ProgSettings.SubTableInsert(thissubTableId, lstLangs.SelectedIndex, thisSubtableName, "To be populated", "To be populated");
                subTableId = thissubTableId;
                
                // Add it to the listbox and select it
                lstsubtables.Items.Add(thisSubtableName);

                int intSubTableListIndex = lstsubtables.Items.IndexOf(thisSubtableName);

                lstsubtables.SelectedIndex = intSubTableListIndex; // lstsubtables.Items.Count - 1;

				blnTextChanged = true;
	            btnSave.Enabled = true;
	            mnuSave.Enabled = btnSave.Enabled;
            }
        }

        private void btnStripTemplate_Click(object sender, EventArgs e)
        {
            txtSubtableTemplate.Text = ProgSettings.ConvertHtmlToTemplate(txtSubtableTemplate.Text);
        }

        private void txtSubtableTemplate_TextChanged(object sender, EventArgs e)
        {
            blnTextChanged = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutScreen = new About();
            aboutScreen.ShowDialog();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            btnNewEntry_Click(sender, e);
        }

    }
}