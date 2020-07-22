using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumDiff
{
    public partial class GoToForm : Form
    {
        public GoToForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal bool GetRowCol(out int row, out int col)
        {
            row = 0;
            col = 0;
            bool res = int.TryParse(textBoxRow.Text, out row) && int.TryParse(textBoxCol.Text, out col);
            row = row - 1;
            col = col - 1;
            return res;
        }

        internal void SetRowCol(int row, int col)
        {
            textBoxRow.Text = (row + 1).ToString();
            textBoxCol.Text = (col + 1).ToString();
        }
    }
}
