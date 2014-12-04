using System;
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
            string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

            var dbViewList = ProgSettings.SelectRows("SELECT * FROM dbdocssubtables" + selectedLang + " where subtablename='" + selectedSubtable + "'");
            if (dbViewList.Tables[0].Rows.Count > 0)
            {
                for (int thisRow = 0; thisRow <= dbViewList.Tables[0].Rows.Count - 1; thisRow++)
                {
                    subTableId = dbViewList.Tables[0].Rows[thisRow]["subtableid"].ToString();
                    txtSubtableContent.Text = dbViewList.Tables[0].Rows[thisRow]["subtablecontent"].ToString();
                    txtSubtableTemplate.Text = dbViewList.Tables[0].Rows[thisRow]["subtabletemplate"].ToString();
                    chkDBDocsEntry.Checked = true;

                    if (string.IsNullOrEmpty(txtSubtableTemplate.Text))
                    {   //If the template is missing, attempt to build it from the content, only for historic entries !!
                        txtSubtableTemplate.Text = ProgSettings.ConvertHtmlToTemplate(txtSubtableContent.Text);
                        
                        //Save the updated Template
                        btnSave_Click(sender, e);
                    }
                }
            }
            else  // No dbdocs match
            {
                txtSubtableName.Text = "";
                chkDBDocsEntry.Checked = false;
            }
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
            StringBuilder dbDocsTableOutput = new StringBuilder();
            string outputFolder = Application.ExecutablePath;

            // Strip the Executable name from the path
            outputFolder = outputFolder.Substring(0, outputFolder.LastIndexOf(@"\"));

            string selectedField = lstsubtables.Text;
            txtSubtableName.Text = selectedField;

            if (lstLangs.SelectedIndex < 0) lstLangs.SelectedIndex = 0;
            string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

            // If the output folder doesnt exist, create it
            if (!Directory.Exists(outputFolder + @"\"))
            {
                Directory.CreateDirectory(outputFolder + @"\");
            }

            // This table save works a little different to the others, since this is a table with an id we can use that to perform the insert/update logic

            if (!string.IsNullOrEmpty(subTableId))
            { // This is an update to an existing subtable

                //delete from `dbdocssubtables` where `subtableid`= xx;
                //insert  into `dbdocstable`(`tableName`,`tableNotes`) values ('script_texts','xxxx');
                dbDocsTableOutput.AppendLine("delete from `dbdocssubtables" + selectedLang + "` where `subtableId`= " + subTableId + ";");
                dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables" + selectedLang + "`(`subtableId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + subTableId + ",'" + selectedField + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");

                ProgSettings.SubTableUpdate(subTableId, selectedField, txtSubtableContent.Text, txtSubtableTemplate.Text);

            }
            else
            { // This is to insert a new subtable
                string newSubtableId = ProgSettings.GetNewSubTableId();
                dbDocsTableOutput.AppendLine("insert  into `dbdocssubtables" + selectedLang + "`(`subtableId`,`subtableName`,`subtablecontent`,`subtableTemplate`) values (" + newSubtableId.ToString() + ",'" + selectedField + "','" + ProgSettings.PrepareSqlString(txtSubtableContent.Text) + "','" + ProgSettings.PrepareSqlString(txtSubtableTemplate.Text) + "');");
                ProgSettings.SubTableInsert(newSubtableId, selectedField, txtSubtableContent.Text, txtSubtableTemplate.Text);
                subTableId = newSubtableId.ToString();
            }

            // Open the file for append and write the entries to it
            using (StreamWriter outfile = new StreamWriter(outputFolder + @"\dbdocssubtables" + selectedLang + ".SQL", true))
            {
                outfile.Write(dbDocsTableOutput.ToString());
            }
        }


        private void btnRebuildContent_Click(object sender, EventArgs e)
        {
        }

    }
}