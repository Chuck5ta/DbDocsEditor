using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmSubtablesLookup : Form
    {
        public int subTableId = 0;

        public frmSubtablesLookup()
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
            subTableId = 0;  // Force to new entry before the lookup updates it should it exist

            DataSet dbViewList = null;
            dbViewList = ProgSettings.SelectRows("SELECT subtableid,languageid, subtablecontent,subtabletemplate FROM `dbdocssubtables` WHERE `subtablename` = '" + selectedSubtable + "'");

            if (dbViewList != null)
            {
                if (dbViewList.Tables[0].Rows.Count > 0)
                {
                    txtSubtableContent.Text = dbViewList.Tables[0].Rows[0]["subtablecontent"].ToString();
                    txtSubtableTemplate.Text = dbViewList.Tables[0].Rows[0]["subtabletemplate"].ToString();

                    subTableId = Convert.ToInt32(dbViewList.Tables[0].Rows[0]["subtableid"]);
                        
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
                    txtSubtableName.Text = "";
                }
            }
            else  // No dbdocs match
            {
                txtSubtableName.Text = "";
            }

            btnSave.Enabled = true;
        }

        private void frmsubtables_Load(object sender, EventArgs e)
        {
            //ProgSettings.LoadLangs(lstLangs);

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
                Text = "SubTable: " + lstsubtables.Text;
            }
            else
            { 
                Text = "SubTables"; 
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            subTableId = Convert.ToInt32(ProgSettings.LookupSubTableId(lstsubtables.Text));
            Close();
//            MessageBox.Show("Save Complete");
        }

        private void frmsubtables_FormClosing(object sender, FormClosingEventArgs e)
        {
            subTableId = 0;
            Close();
        }
    }
}