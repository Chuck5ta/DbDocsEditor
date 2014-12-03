using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    public partial class frmServerSelect : Form
    {
        private bool blnEditmode = false;

        public frmServerSelect()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Set the screen to 'Edit' Mode
            ToggleControls(true);
            blnEditmode = true;
        }

        private void ToggleControls(bool editmode)
        {
            if (editmode)
            {
                // Set the 4 Textboxes to Editable
                txtDefaultDB.Enabled = true;
                txtPassword.Enabled = true;
                txtServerName.Enabled = true;
                txtUsername.Enabled = true;

                // Display Save/Cancel buttons
                btnCancel.Visible = true;
                btnSave.Visible = true;
                btnDelete.Enabled = true;

                //Disable other buttons.
                btnNew.Enabled = false;
                btnConnect.Enabled = false;
                lstServers.Enabled = false;
            }
            else
            {
                // Set the 4 Textboxes to Editable
                txtDefaultDB.Enabled = false;
                txtPassword.Enabled = false;
                txtServerName.Enabled = false;
                txtUsername.Enabled = false;

                // Display Save/Cancel buttons
                btnCancel.Visible = false;
                btnSave.Visible = false;
                btnDelete.Enabled = false;

                //Disable other buttons.
                btnNew.Enabled = true;
                btnConnect.Enabled = true;
                lstServers.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Set the screen to 'Normal' Mode
            ToggleControls(false);

            // Revert settings back to selected item settings
            lstServers_SelectedIndexChanged(sender, e);
            if (lstServers.SelectedIndex != 1)
            {
                btnConnect.Enabled = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Set the screen to 'Edit' Mode
            ToggleControls(true);
        }

        /// <summary>
        /// Populate the entry information when the item is selected in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedServer = lstServers.SelectedIndex;

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnConnect.Enabled = true;
        }

        private void frmServerSelect_Load(object sender, EventArgs e)
        {
            lstServers.Items.Clear();

            //// Add default entry
            lstServers.Items.Add("localhost-*");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ProgSettings.DbName = txtDefaultDB.Text;
            ProgSettings.Password = txtPassword.Text;
            ProgSettings.UserName = txtUsername.Text;
            ProgSettings.ServerName = txtServerName.Text;

            if (txtDefaultDB.Text != "*")
            {
                // Display Table List screen
                var tablesScreen = new frmTables { Text = "DBDocs: Using database " + txtDefaultDB.Text };
                tablesScreen.Show();
                Hide();
            }
            else
            {
                // Display Database List screen
                var dbScreen = new frmDatabaseSelect { Text = "DBDocs: Using Server " + txtServerName.Text };
                dbScreen.Show();
                Hide();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }
    }
}
