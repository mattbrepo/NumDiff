namespace NumDiff
{
    partial class GoToForm
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
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.labelRow = new System.Windows.Forms.Label();
            this.textBoxCol = new System.Windows.Forms.TextBox();
            this.labelCol = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(62, 20);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(137, 20);
            this.textBoxRow.TabIndex = 0;
            this.textBoxRow.Text = "1";
            this.textBoxRow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRow_KeyPress);
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(12, 23);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(29, 13);
            this.labelRow.TabIndex = 8;
            this.labelRow.Text = "Row";
            // 
            // textBoxCol
            // 
            this.textBoxCol.Location = new System.Drawing.Point(62, 46);
            this.textBoxCol.Name = "textBoxCol";
            this.textBoxCol.Size = new System.Drawing.Size(137, 20);
            this.textBoxCol.TabIndex = 1;
            this.textBoxCol.Text = "1";
            this.textBoxCol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCol_KeyPress);
            // 
            // labelCol
            // 
            this.labelCol.AutoSize = true;
            this.labelCol.Location = new System.Drawing.Point(12, 49);
            this.labelCol.Name = "labelCol";
            this.labelCol.Size = new System.Drawing.Size(22, 13);
            this.labelCol.TabIndex = 10;
            this.labelCol.Text = "Col";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(43, 83);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(124, 83);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // GoToForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 116);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxCol);
            this.Controls.Add(this.labelCol);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.labelRow);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go to..";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.TextBox textBoxCol;
        private System.Windows.Forms.Label labelCol;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}