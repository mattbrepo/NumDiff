namespace NumDiff
{
    partial class OptionsForm
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
            this.checkBoxTab = new System.Windows.Forms.CheckBox();
            this.checkBoxComma = new System.Windows.Forms.CheckBox();
            this.checkBoxSemicolon = new System.Windows.Forms.CheckBox();
            this.checkBoxSpace = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelTollerance = new System.Windows.Forms.Label();
            this.textBoxTollerance = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkBoxTab
            // 
            this.checkBoxTab.AutoSize = true;
            this.checkBoxTab.Location = new System.Drawing.Point(12, 12);
            this.checkBoxTab.Name = "checkBoxTab";
            this.checkBoxTab.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTab.TabIndex = 0;
            this.checkBoxTab.Text = "Tab";
            this.checkBoxTab.UseVisualStyleBackColor = true;
            // 
            // checkBoxComma
            // 
            this.checkBoxComma.AutoSize = true;
            this.checkBoxComma.Location = new System.Drawing.Point(12, 35);
            this.checkBoxComma.Name = "checkBoxComma";
            this.checkBoxComma.Size = new System.Drawing.Size(61, 17);
            this.checkBoxComma.TabIndex = 1;
            this.checkBoxComma.Text = "Comma";
            this.checkBoxComma.UseVisualStyleBackColor = true;
            // 
            // checkBoxSemicolon
            // 
            this.checkBoxSemicolon.AutoSize = true;
            this.checkBoxSemicolon.Location = new System.Drawing.Point(106, 12);
            this.checkBoxSemicolon.Name = "checkBoxSemicolon";
            this.checkBoxSemicolon.Size = new System.Drawing.Size(75, 17);
            this.checkBoxSemicolon.TabIndex = 2;
            this.checkBoxSemicolon.Text = "Semicolon";
            this.checkBoxSemicolon.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpace
            // 
            this.checkBoxSpace.AutoSize = true;
            this.checkBoxSpace.Location = new System.Drawing.Point(106, 35);
            this.checkBoxSpace.Name = "checkBoxSpace";
            this.checkBoxSpace.Size = new System.Drawing.Size(57, 17);
            this.checkBoxSpace.TabIndex = 3;
            this.checkBoxSpace.Text = "Space";
            this.checkBoxSpace.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(145, 108);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(64, 108);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelTollerance
            // 
            this.labelTollerance.AutoSize = true;
            this.labelTollerance.Location = new System.Drawing.Point(12, 74);
            this.labelTollerance.Name = "labelTollerance";
            this.labelTollerance.Size = new System.Drawing.Size(57, 13);
            this.labelTollerance.TabIndex = 6;
            this.labelTollerance.Text = "Tollerance";
            // 
            // textBoxTollerance
            // 
            this.textBoxTollerance.Location = new System.Drawing.Point(106, 71);
            this.textBoxTollerance.Name = "textBoxTollerance";
            this.textBoxTollerance.Size = new System.Drawing.Size(100, 20);
            this.textBoxTollerance.TabIndex = 7;
            this.textBoxTollerance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxTollerance_KeyUp);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 143);
            this.Controls.Add(this.textBoxTollerance);
            this.Controls.Add(this.labelTollerance);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxSpace);
            this.Controls.Add(this.checkBoxSemicolon);
            this.Controls.Add(this.checkBoxComma);
            this.Controls.Add(this.checkBoxTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Shown += new System.EventHandler(this.OptionsForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxTab;
        private System.Windows.Forms.CheckBox checkBoxComma;
        private System.Windows.Forms.CheckBox checkBoxSemicolon;
        private System.Windows.Forms.CheckBox checkBoxSpace;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTollerance;
        private System.Windows.Forms.TextBox textBoxTollerance;
    }
}