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

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmsubtables_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}