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
            string selectedLang = ProgSettings.SetLocalisationModifier(lstLangs.Items[lstLangs.SelectedIndex].ToString());

            var dbViewList = ProgSettings.SelectRows("SELECT FieldNotes FROM dbdocsfields" + selectedLang + " where TableName='" + TableName + "'" + " AND FieldName='" + selectedField + "'");
            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtFieldNotes.Text = dbViewList.Tables[0].Rows[0]["FieldNotes"].ToString();
                    chkDBDocsEntry.Checked = true;

                    //Check for Subtables
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

        }

        private void lstSubtables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}