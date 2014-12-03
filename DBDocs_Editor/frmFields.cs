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

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmFields_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void lstSubtables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}