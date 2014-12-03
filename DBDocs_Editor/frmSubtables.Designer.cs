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
            this.txtSubtableNotes = new System.Windows.Forms.TextBox();
            this.chkDBDocsEntry = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkHasContent = new System.Windows.Forms.CheckBox();
            this.chkHasTemplate = new System.Windows.Forms.CheckBox();
            this.btnRenderContent = new System.Windows.Forms.Button();
            this.btnRebuildContent = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lstLangs = new System.Windows.Forms.ListBox();
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
            // txtSubtableNotes
            // 
            this.txtSubtableNotes.Location = new System.Drawing.Point(284, 27);
            this.txtSubtableNotes.Multiline = true;
            this.txtSubtableNotes.Name = "txtSubtableNotes";
            this.txtSubtableNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubtableNotes.Size = new System.Drawing.Size(441, 328);
            this.txtSubtableNotes.TabIndex = 8;
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
            // 
            // btnRebuildContent
            // 
            this.btnRebuildContent.Location = new System.Drawing.Point(177, 255);
            this.btnRebuildContent.Name = "btnRebuildContent";
            this.btnRebuildContent.Size = new System.Drawing.Size(101, 23);
            this.btnRebuildContent.TabIndex = 14;
            this.btnRebuildContent.Text = "Rebuild Content";
            this.btnRebuildContent.UseVisualStyleBackColor = true;
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
            // frmSubtables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 430);
            this.Controls.Add(this.lstLangs);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnRenderContent);
            this.Controls.Add(this.btnRebuildContent);
            this.Controls.Add(this.chkHasTemplate);
            this.Controls.Add(this.chkHasContent);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkDBDocsEntry);
            this.Controls.Add(this.txtSubtableNotes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubtableName);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lstsubtables);
            this.Controls.Add(this.label1);
            this.Name = "frmSubtables";
            this.Text = "subtables Form";
            this.Load += new System.EventHandler(this.frmsubtables_Load);
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
        private System.Windows.Forms.TextBox txtSubtableNotes;
        private System.Windows.Forms.CheckBox chkDBDocsEntry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkHasContent;
        private System.Windows.Forms.CheckBox chkHasTemplate;
        private System.Windows.Forms.Button btnRenderContent;
        private System.Windows.Forms.Button btnRebuildContent;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox lstLangs;
    }
}

