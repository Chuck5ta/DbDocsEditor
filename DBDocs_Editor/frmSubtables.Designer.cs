namespace DBDocs_Editor
{
    partial class frmSubtables
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lstsubtables = new System.Windows.Forms.ListBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.txtSubtableName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDBDocsEntry = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkHasContent = new System.Windows.Forms.CheckBox();
            this.chkHasTemplate = new System.Windows.Forms.CheckBox();
            this.btnRenderContent = new System.Windows.Forms.Button();
            this.btnRebuildContent = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lstLangs = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSubtableTemplate = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSubtableContent = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.webBrowse = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "subtables";
            // 
            // lstsubtables
            // 
            this.lstsubtables.FormattingEnabled = true;
            this.lstsubtables.Location = new System.Drawing.Point(12, 27);
            this.lstsubtables.Name = "lstsubtables";
            this.lstsubtables.Size = new System.Drawing.Size(162, 394);
            this.lstsubtables.TabIndex = 2;
            this.lstsubtables.SelectedIndexChanged += new System.EventHandler(this.lstsubtables_SelectedIndexChanged);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(650, 4);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 4;
            this.btnQuit.Text = "Close";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // txtSubtableName
            // 
            this.txtSubtableName.AutoSize = true;
            this.txtSubtableName.Location = new System.Drawing.Point(281, 9);
            this.txtSubtableName.Name = "txtSubtableName";
            this.txtSubtableName.Size = new System.Drawing.Size(182, 13);
            this.txtSubtableName.TabIndex = 5;
            this.txtSubtableName.Text = "Select a Table from the list on the left";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "subTable Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Template:";
            // 
            // chkDBDocsEntry
            // 
            this.chkDBDocsEntry.AutoSize = true;
            this.chkDBDocsEntry.Enabled = false;
            this.chkDBDocsEntry.Location = new System.Drawing.Point(180, 43);
            this.chkDBDocsEntry.Name = "chkDBDocsEntry";
            this.chkDBDocsEntry.Size = new System.Drawing.Size(96, 17);
            this.chkDBDocsEntry.TabIndex = 9;
            this.chkDBDocsEntry.Text = "DB Docs Entry";
            this.chkDBDocsEntry.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 332);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save Entry";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkHasContent
            // 
            this.chkHasContent.AutoSize = true;
            this.chkHasContent.Enabled = false;
            this.chkHasContent.Location = new System.Drawing.Point(180, 66);
            this.chkHasContent.Name = "chkHasContent";
            this.chkHasContent.Size = new System.Drawing.Size(85, 17);
            this.chkHasContent.TabIndex = 11;
            this.chkHasContent.Text = "Has Content";
            this.chkHasContent.UseVisualStyleBackColor = true;
            // 
            // chkHasTemplate
            // 
            this.chkHasTemplate.AutoSize = true;
            this.chkHasTemplate.Enabled = false;
            this.chkHasTemplate.Location = new System.Drawing.Point(180, 89);
            this.chkHasTemplate.Name = "chkHasTemplate";
            this.chkHasTemplate.Size = new System.Drawing.Size(92, 17);
            this.chkHasTemplate.TabIndex = 12;
            this.chkHasTemplate.Text = "Has Template";
            this.chkHasTemplate.UseVisualStyleBackColor = true;
            // 
            // btnRenderContent
            // 
            this.btnRenderContent.Location = new System.Drawing.Point(177, 226);
            this.btnRenderContent.Name = "btnRenderContent";
            this.btnRenderContent.Size = new System.Drawing.Size(101, 23);
            this.btnRenderContent.TabIndex = 13;
            this.btnRenderContent.Text = "Render Content";
            this.btnRenderContent.UseVisualStyleBackColor = true;
            this.btnRenderContent.Click += new System.EventHandler(this.btnRenderContent_Click);
            // 
            // btnRebuildContent
            // 
            this.btnRebuildContent.Location = new System.Drawing.Point(177, 255);
            this.btnRebuildContent.Name = "btnRebuildContent";
            this.btnRebuildContent.Size = new System.Drawing.Size(101, 23);
            this.btnRebuildContent.TabIndex = 14;
            this.btnRebuildContent.Text = "Rebuild Content";
            this.btnRebuildContent.UseVisualStyleBackColor = true;
            this.btnRebuildContent.Click += new System.EventHandler(this.btnRebuildContent_Click);
            // 
            // listBox1
            // 
            this.listBox1.ColumnWidth = 140;
            this.listBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(284, 361);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(441, 60);
            this.listBox1.TabIndex = 15;
            // 
            // lstLangs
            // 
            this.lstLangs.FormattingEnabled = true;
            this.lstLangs.Location = new System.Drawing.Point(180, 112);
            this.lstLangs.Name = "lstLangs";
            this.lstLangs.Size = new System.Drawing.Size(98, 108);
            this.lstLangs.TabIndex = 16;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(284, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 322);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSubtableTemplate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 296);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Template Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtSubtableTemplate
            // 
            this.txtSubtableTemplate.Location = new System.Drawing.Point(0, 0);
            this.txtSubtableTemplate.Multiline = true;
            this.txtSubtableTemplate.Name = "txtSubtableTemplate";
            this.txtSubtableTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubtableTemplate.Size = new System.Drawing.Size(441, 309);
            this.txtSubtableTemplate.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSubtableContent);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(441, 296);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML Markup";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSubtableContent
            // 
            this.txtSubtableContent.Location = new System.Drawing.Point(-4, 0);
            this.txtSubtableContent.Multiline = true;
            this.txtSubtableContent.Name = "txtSubtableContent";
            this.txtSubtableContent.ReadOnly = true;
            this.txtSubtableContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubtableContent.Size = new System.Drawing.Size(441, 321);
            this.txtSubtableContent.TabIndex = 9;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.webBrowse);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(441, 296);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rendered HTML";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // webBrowse
            // 
            this.webBrowse.Location = new System.Drawing.Point(0, 0);
            this.webBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.webBrowse.MinimumSize = new System.Drawing.Size(23, 22);
            this.webBrowse.Name = "webBrowse";
            this.webBrowse.Size = new System.Drawing.Size(441, 296);
            this.webBrowse.TabIndex = 6;
            // 
            // frmSubtables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 430);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lstLangs);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnRenderContent);
            this.Controls.Add(this.btnRebuildContent);
            this.Controls.Add(this.chkHasTemplate);
            this.Controls.Add(this.chkHasContent);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkDBDocsEntry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubtableName);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lstsubtables);
            this.Controls.Add(this.label1);
            this.Name = "frmSubtables";
            this.Text = "subtables Form";
            this.Load += new System.EventHandler(this.frmsubtables_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstsubtables;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label txtSubtableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDBDocsEntry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkHasContent;
        private System.Windows.Forms.CheckBox chkHasTemplate;
        private System.Windows.Forms.Button btnRenderContent;
        private System.Windows.Forms.Button btnRebuildContent;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox lstLangs;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSubtableContent;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtSubtableTemplate;
        internal System.Windows.Forms.WebBrowser webBrowse;
    }
}

