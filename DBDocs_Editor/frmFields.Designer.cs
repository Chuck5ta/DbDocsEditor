namespace DBDocs_Editor
{
    partial class frmFields
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
            this.lstFields = new System.Windows.Forms.ListBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.txtFieldName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFieldNotes = new System.Windows.Forms.TextBox();
            this.chkDBDocsEntry = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstSubtables = new System.Windows.Forms.ListBox();
            this.btnShowSubtables = new System.Windows.Forms.Button();
            this.lstLangs = new System.Windows.Forms.ListBox();
            this.chkUseEnglish = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fields";
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(12, 27);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(159, 394);
            this.lstFields.TabIndex = 2;
            this.lstFields.SelectedIndexChanged += new System.EventHandler(this.lstFields_SelectedIndexChanged);
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
            // txtFieldName
            // 
            this.txtFieldName.AutoSize = true;
            this.txtFieldName.Location = new System.Drawing.Point(248, 9);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(182, 13);
            this.txtFieldName.TabIndex = 5;
            this.txtFieldName.Text = "Select a Table from the list on the left";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Field Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Field Notes:";
            // 
            // txtFieldNotes
            // 
            this.txtFieldNotes.Location = new System.Drawing.Point(284, 27);
            this.txtFieldNotes.Multiline = true;
            this.txtFieldNotes.Name = "txtFieldNotes";
            this.txtFieldNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFieldNotes.Size = new System.Drawing.Size(441, 328);
            this.txtFieldNotes.TabIndex = 8;
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
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(180, 332);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save Entry";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstSubtables
            // 
            this.lstSubtables.ColumnWidth = 140;
            this.lstSubtables.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSubtables.FormattingEnabled = true;
            this.lstSubtables.HorizontalScrollbar = true;
            this.lstSubtables.ItemHeight = 14;
            this.lstSubtables.Location = new System.Drawing.Point(284, 361);
            this.lstSubtables.MultiColumn = true;
            this.lstSubtables.Name = "lstSubtables";
            this.lstSubtables.Size = new System.Drawing.Size(441, 60);
            this.lstSubtables.TabIndex = 11;
            this.lstSubtables.SelectedIndexChanged += new System.EventHandler(this.lstSubtables_SelectedIndexChanged);
            // 
            // btnShowSubtables
            // 
            this.btnShowSubtables.Location = new System.Drawing.Point(177, 395);
            this.btnShowSubtables.Name = "btnShowSubtables";
            this.btnShowSubtables.Size = new System.Drawing.Size(101, 23);
            this.btnShowSubtables.TabIndex = 12;
            this.btnShowSubtables.Text = "Show Subtables";
            this.btnShowSubtables.UseVisualStyleBackColor = true;
            this.btnShowSubtables.Click += new System.EventHandler(this.btnShowSubtables_Click);
            // 
            // lstLangs
            // 
            this.lstLangs.FormattingEnabled = true;
            this.lstLangs.Location = new System.Drawing.Point(177, 112);
            this.lstLangs.Name = "lstLangs";
            this.lstLangs.Size = new System.Drawing.Size(101, 147);
            this.lstLangs.TabIndex = 19;
            // 
            // chkUseEnglish
            // 
            this.chkUseEnglish.AutoSize = true;
            this.chkUseEnglish.Checked = true;
            this.chkUseEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseEnglish.Location = new System.Drawing.Point(180, 265);
            this.chkUseEnglish.Name = "chkUseEnglish";
            this.chkUseEnglish.Size = new System.Drawing.Size(105, 17);
            this.chkUseEnglish.TabIndex = 19;
            this.chkUseEnglish.Text = "English if missing";
            this.chkUseEnglish.UseVisualStyleBackColor = true;
            // 
            // frmFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 430);
            this.Controls.Add(this.lstLangs);
            this.Controls.Add(this.lstSubtables);
            this.Controls.Add(this.btnShowSubtables);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkDBDocsEntry);
            this.Controls.Add(this.txtFieldNotes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkUseEnglish);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmFields";
            this.Text = "Fields Form";
            this.Load += new System.EventHandler(this.frmFields_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label txtFieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFieldNotes;
        private System.Windows.Forms.CheckBox chkDBDocsEntry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstSubtables;
        private System.Windows.Forms.Button btnShowSubtables;
        private System.Windows.Forms.ListBox lstLangs;
        private System.Windows.Forms.CheckBox chkUseEnglish;
    }
}