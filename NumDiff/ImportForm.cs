using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NumDiffLib;

namespace NumDiff
{
    public partial class ImportForm : Form
    {
        public string FilePath1 { get; set; }

        public ImportForm()
        {
            InitializeComponent();
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportForm_Shown(object sender, EventArgs e)
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
            UpdateGrid();
        }

        private string[] GetSeparators()
        {
            List<string> res = new List<string>();
            if (checkBoxTab.Checked)
                res.Add("TAB");

            if (checkBoxSpace.Checked)
                res.Add(" ");

            if (checkBoxComma.Checked)
                res.Add(",");

            if (checkBoxSemicolon.Checked)
                res.Add(";");

            return res.ToArray();
        }

        private void UpdateGrid()
        {
            const int MAX_COLS = 100;

            CompareResults cmp = new CompareResults()
            {
                Tollerance = 0,
                Separators = GetSeparators()
            };

            try
            {
                List<string[]> content = NumDiffUtil.ReadBlock(FilePath1, cmp, 0, 10);

                try
                {
                    dataGridView1.RowCount = 1;
                    dataGridView1.ColumnCount = 1;
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0]; // required by BeginEdit
                    dataGridView1.BeginEdit(false);

                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.ColumnCount = Math.Min(MAX_COLS, content[0].Length);
                    dataGridView1.RowCount = content.Count;

                    if (checkBoxHasHeader.Checked)
                    {
                        for (int col = 0; col < dataGridView1.ColumnCount; col++)
                        {
                            dataGridView1.Columns[col].HeaderText = content[0][col];
                        }
                        content.RemoveAt(0);
                    }

                    for (int row = 0; row < content.Count; row++)
                    {
                        for (int col = 0; col < dataGridView1.ColumnCount; col++)
                        {
                            dataGridView1.Rows[row].Cells[col].Value = content[row][col];
                        }
                    }

                }
                finally
                {
                    dataGridView1.EndEdit();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void checkBoxTab_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void checkBoxHasHeader_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1; // https://social.msdn.microsoft.com/Forums/windows/en-US/327ff3e0-098c-4657-9af4-20d31fe5d6f0/sum-of-the-columns-fillweight-values-cannot-exceed-65535?forum=winformsdatacontrols
        }

        private void checkBoxSemicolon_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void checkBoxComma_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void checkBoxSpace_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }
    }
}
