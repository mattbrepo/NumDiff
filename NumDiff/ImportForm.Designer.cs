namespace NumDiff
{
    partial class ImportForm
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.checkBoxHasHeader = new System.Windows.Forms.CheckBox();
            this.textBoxTollerance = new System.Windows.Forms.TextBox();
            this.labelTollerance = new System.Windows.Forms.Label();
            this.checkBoxSpace = new System.Windows.Forms.CheckBox();
            this.checkBoxSemicolon = new System.Windows.Forms.CheckBox();
            this.checkBoxComma = new System.Windows.Forms.CheckBox();
            this.checkBoxTab = new System.Windows.Forms.CheckBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.checkBoxHasHeader);
            this.pnlTop.Controls.Add(this.textBoxTollerance);
            this.pnlTop.Controls.Add(this.labelTollerance);
            this.pnlTop.Controls.Add(this.checkBoxSpace);
            this.pnlTop.Controls.Add(this.checkBoxSemicolon);
            this.pnlTop.Controls.Add(this.checkBoxComma);
            this.pnlTop.Controls.Add(this.checkBoxTab);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(514, 100);
            this.pnlTop.TabIndex = 16;
            // 
            // checkBoxHasHeader
            // 
            this.checkBoxHasHeader.AutoSize = true;
            this.checkBoxHasHeader.Location = new System.Drawing.Point(12, 79);
            this.checkBoxHasHeader.Name = "checkBoxHasHeader";
            this.checkBoxHasHeader.Size = new System.Drawing.Size(61, 17);
            this.checkBoxHasHeader.TabIndex = 22;
            this.checkBoxHasHeader.Text = "Header";
            this.checkBoxHasHeader.UseVisualStyleBackColor = true;
            this.checkBoxHasHeader.CheckedChanged += new System.EventHandler(this.checkBoxHasHeader_CheckedChanged);
            // 
            // textBoxTollerance
            // 
            this.textBoxTollerance.Location = new System.Drawing.Point(103, 45);
            this.textBoxTollerance.Name = "textBoxTollerance";
            this.textBoxTollerance.Size = new System.Drawing.Size(100, 20);
            this.textBoxTollerance.TabIndex = 21;
            // 
            // labelTollerance
            // 
            this.labelTollerance.AutoSize = true;
            this.labelTollerance.Location = new System.Drawing.Point(9, 48);
            this.labelTollerance.Name = "labelTollerance";
            this.labelTollerance.Size = new System.Drawing.Size(57, 13);
            this.labelTollerance.TabIndex = 20;
            this.labelTollerance.Text = "Tollerance";
            // 
            // checkBoxSpace
            // 
            this.checkBoxSpace.AutoSize = true;
            this.checkBoxSpace.Location = new System.Drawing.Point(304, 15);
            this.checkBoxSpace.Name = "checkBoxSpace";
            this.checkBoxSpace.Size = new System.Drawing.Size(57, 17);
            this.checkBoxSpace.TabIndex = 19;
            this.checkBoxSpace.Text = "Space";
            this.checkBoxSpace.UseVisualStyleBackColor = true;
            this.checkBoxSpace.CheckedChanged += new System.EventHandler(this.checkBoxSpace_CheckedChanged);
            // 
            // checkBoxSemicolon
            // 
            this.checkBoxSemicolon.AutoSize = true;
            this.checkBoxSemicolon.Location = new System.Drawing.Point(106, 15);
            this.checkBoxSemicolon.Name = "checkBoxSemicolon";
            this.checkBoxSemicolon.Size = new System.Drawing.Size(75, 17);
            this.checkBoxSemicolon.TabIndex = 18;
            this.checkBoxSemicolon.Text = "Semicolon";
            this.checkBoxSemicolon.UseVisualStyleBackColor = true;
            this.checkBoxSemicolon.CheckedChanged += new System.EventHandler(this.checkBoxSemicolon_CheckedChanged);
            // 
            // checkBoxComma
            // 
            this.checkBoxComma.AutoSize = true;
            this.checkBoxComma.Location = new System.Drawing.Point(210, 15);
            this.checkBoxComma.Name = "checkBoxComma";
            this.checkBoxComma.Size = new System.Drawing.Size(61, 17);
            this.checkBoxComma.TabIndex = 17;
            this.checkBoxComma.Text = "Comma";
            this.checkBoxComma.UseVisualStyleBackColor = true;
            this.checkBoxComma.CheckedChanged += new System.EventHandler(this.checkBoxComma_CheckedChanged);
            // 
            // checkBoxTab
            // 
            this.checkBoxTab.AutoSize = true;
            this.checkBoxTab.Location = new System.Drawing.Point(12, 15);
            this.checkBoxTab.Name = "checkBoxTab";
            this.checkBoxTab.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTab.TabIndex = 16;
            this.checkBoxTab.Text = "Tab";
            this.checkBoxTab.UseVisualStyleBackColor = true;
            this.checkBoxTab.CheckedChanged += new System.EventHandler(this.checkBoxTab_CheckedChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.buttonCancel);
            this.pnlBottom.Controls.Add(this.buttonOk);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 380);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(514, 43);
            this.pnlBottom.TabIndex = 18;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(346, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(427, 8);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dataGridView1);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 100);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(514, 280);
            this.pnlCenter.TabIndex = 19;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(514, 280);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 423);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MinimizeBox = false;
            this.Name = "ImportForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.Shown += new System.EventHandler(this.ImportForm_Shown);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox textBoxTollerance;
        private System.Windows.Forms.Label labelTollerance;
        private System.Windows.Forms.CheckBox checkBoxSpace;
        private System.Windows.Forms.CheckBox checkBoxSemicolon;
        private System.Windows.Forms.CheckBox checkBoxComma;
        private System.Windows.Forms.CheckBox checkBoxTab;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckBox checkBoxHasHeader;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}