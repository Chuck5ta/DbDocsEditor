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
            Close();
        }

        private void btnShowFields_Click(object sender, EventArgs e)
        {
        }

        private void frmTables_Load(object sender, EventArgs e)
        {
            //Temporarily set one of the db params to trigger an update of the connection values
            ProgSettings.DbName = "NewM0";
            
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
        }

        private void btnShowSubtables_Click(object sender, EventArgs e)
        {
        }

        private void lstSubtables_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}