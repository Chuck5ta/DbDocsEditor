using System;
using System.Windows.Forms;

namespace DBDocs_Editor
{
    internal static class DbDocsEditor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainform = new frmServerSelect();
            ProgSettings.mainForm = mainform;
            mainform.Text = "DBDocs Editor v0.0.2";
            Application.Run(mainform);
        }
    }
}