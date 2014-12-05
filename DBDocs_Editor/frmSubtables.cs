using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmSubtables : Form
    {
        public string subTableId = "";

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
            txtSubtableName.Text = selectedSubtable;

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;

            DataSet dbViewList = null;
            if (lstLangs.SelectedIndex == 0)
            {   // If English, connect to main table
                dbViewList = ProgSettings.SelectRows("SELECT subtableid,subtablecontent,subtabletemplate FROM `dbdocssubtables` WHERE `subtablename` = '" + selectedSubtable + "'");
            }
            else
            {   // If Non-English, join to localised table and grab field
                dbViewList = ProgSettings.SelectRows("SELECT `dbdocssubtables`.`subtableid`, `dbdocssubtables_localised`.`subtabletemplate`, `dbdocssubtables_localised`.`subtablecontent`, `dbdocssubtables`.`subtabletemplate` as subtableTemplateEnglish, `dbdocssubtables`.`subtablecontent` as subTableContentEnglish FROM `dbdocssubtables` INNER JOIN `dbdocssubtables_localised` ON `dbdocssubtables`.`subtableid` = `dbdocssubtables_localised`.`subtableid` WHERE `subtablename` = '" + selectedSubtable + "' AND (`dbdocssubtables_localised`.`languageId`=" + lstLangs.SelectedIndex + "  OR `dbdocssubtables`.`languageId`=0);");
            }

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    subTableId = dbViewList.Tables[0].Rows[0]["subtableid"].ToString();
                    txtSubtableContent.Text = dbViewList.Tables[0].Rows[0]["subtablecontent"].ToString();
                    txtSubtableTemplate.Text = dbViewList.Tables[0].Rows[0]["subtabletemplate"].ToString();
                        
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

                    chkDBDocsEntry.Checked = true;

                    if (string.IsNullOrEmpty(txtSubtableTemplate.Text))
                    {   //If the template is missing, attempt to build it from the content, only for historic entries !!
                        txtSubtableTemplate.Text = ProgSettings.ConvertHtmlToTemplate(txtSubtableContent.Text);

                        //Save the updated Template
                        btnSave_Click(sender, e);
                    }
                }
                else  // No dbdocs match
                {
                    txtSubtableName.Text = "";
                    chkDBDocsEntry.Checked = false;
                }
            }
            else  // No dbdocs match
            {
                txtSubtableName.Text = "";
                chkDBDocsEntry.Checked = false;
            }

            btnSave.Enabled = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmsubtables_Load(object sender, EventArgs e)
        {
            System.Data.DataSet dbViewList;
            if (string.IsNullOrEmpty(subTableId))
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

            lstsubtables.Items.Clear();

            // for each Field returned, populate the listbox with the table name
            for (var thisRow = 0; thisRow <= dbViewList.Tables[0].Rows.Count - 1; thisRow++)
            {
                var fieldName = dbViewList.Tables[0].Rows[thisRow]["subtablename"].ToString();
                lstsubtables.Items.Add(fieldName);
            }

            ProgSettings.LoadLangs(lstLangs);

            if (!string.IsNullOrEmpty(subTableId))
            {   //Select the first entry if we passed in an Id
                if (lstsubtables.SelectedIndex < 0) lstsubtables.SelectedIndex = 0;
                Text = "SubTable: " + lstsubtables.Text;
            }
            else
            { 
                Text = "SubTables"; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //StringBuilder dbDocsTableOutput = new StringBuilder();
            //string outputFolder = Application.ExecutablePath;

            //// Strip the Executable name from the path
            //outputFolder = outputFolder.Substring(0, outputFolder.LastIndexOf(@"\"));

            //string selectedField = lstsubtables.Text;
            //txtSubtableName.Text = selectedField;

            //if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
            //string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

            //// If the output folder doesnt exist, create it
            //if (!Directory.Exists(outputFolder + @"\"))
            //{
            //    Directory.CreateDirectory(outputFolder + @"\");
            //}

            //// This table save works a little different to the others, since this is a table with an id we can use that to perform the insert/update logic

            //if (!string.IsNullOrEmpty(subTableId))
            //{ // This is an update to an existing subtable

            //    //delete from `dbdocssubtables` where `subtableid`= xx;
            //    //insert  into `dbdocstable`(`tableName`,`tableNotes`) values ('script_texts','xxxx');
            //    dbDocsTableOutput.AppendLine("delete from `dbdocssubtables" + selectedLang + "` where `subtableId`= " + subTableId + ";");
            //    dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables" + selectedLang + "`(`subtableId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + subTableId + ",'" + selectedField + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");

            //    ProgSettings.SubTableUpdate(subTableId, selectedField, txtSubtableContent.Text, txtSubtableTemplate.Text);

            //}
            //else
            //{ // This is to insert a new subtable
            //    string newSubtableId = ProgSettings.GetNewSubTableId();
            //    dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables" + selectedLang + "`(`subtableId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + newSubtableId.ToString() + ",'" + selectedField + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");
            //    ProgSettings.SubTableInsert(newSubtableId, selectedField, txtSubtableContent.Text, txtSubtableTemplate.Text);
            //    subTableId = newSubtableId.ToString();
            //}

            //// Open the file for append and write the entries to it
            //using (StreamWriter outfile = new StreamWriter(outputFolder + @"\dbdocssubtables" + selectedLang + ".SQL", true))
            //{
            //    outfile.Write(dbDocsTableOutput.ToString());
            //}
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
        }

        private void btnRenderContent_Click(object sender, EventArgs e)
        {
            webBrowse.DocumentText = txtSubtableContent.Text;
        }

    }
}