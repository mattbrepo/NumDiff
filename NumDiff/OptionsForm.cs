using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace NumDiff
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            textBoxTollerance.Text = "" + NumDiff.Properties.Settings.Default.Tollerance;
            for (int i = 0; i < NumDiff.Properties.Settings.Default.Separators.Count; i++)
            {
                if (NumDiff.Properties.Settings.Default.Separators[i] == "TAB")
                    checkBoxTab.Checked = true;
                if (NumDiff.Properties.Settings.Default.Separators[i] == " ")
                    checkBoxSpace.Checked = true;
                if (NumDiff.Properties.Settings.Default.Separators[i] == ",")
                    checkBoxComma.Checked = true;
                if (NumDiff.Properties.Settings.Default.Separators[i] == ";")
                    checkBoxSemicolon.Checked = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            double d;
            if (!NumDiffUtil.TryParseTollerance(textBoxTollerance.Text, out d))
            {
                MessageBox.Show("Tollerance is not a valid number");
                return;
            }

            NumDiff.Properties.Settings.Default.Tollerance = d;
            NumDiff.Properties.Settings.Default.Separators.Clear();

            if (checkBoxTab.Checked)
                NumDiff.Properties.Settings.Default.Separators.Add("TAB");

            if (checkBoxSpace.Checked)
                NumDiff.Properties.Settings.Default.Separators.Add(" ");
            
            if (checkBoxComma.Checked)
                NumDiff.Properties.Settings.Default.Separators.Add(",");

            if (checkBoxSemicolon.Checked)
                NumDiff.Properties.Settings.Default.Separators.Add(";");

            NumDiff.Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
