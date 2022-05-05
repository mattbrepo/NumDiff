using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using NumDiffLib;

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
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            CheckAndClose();
        }

        private void CheckAndClose()
        {
            double d;
            if (!NumDiffUtil.TryParseTollerance(textBoxTollerance.Text, out d))
            {
                MessageBox.Show("Tollerance is not a valid number");
                return;
            }

            NumDiff.Properties.Settings.Default.Tollerance = d;
            NumDiff.Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxTollerance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                CheckAndClose();
        }
    }
}
