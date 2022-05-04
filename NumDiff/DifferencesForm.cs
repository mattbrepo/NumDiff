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
    public partial class DifferencesForm : Form
    {
        private CompareResults _cmp;

        public int OrdDiffIdx { get; set; }

        public DifferencesForm()
        {
            InitializeComponent();
        }

        public void SetCompareResults(CompareResults cmp)
        {
            _cmp = cmp;
        }

        private void DifferencesForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add(new DataGridViewColumn(new DataGridViewTextBoxCell()) { HeaderText = "Row", Name = "Row" });
            dataGridView1.Columns.Add(new DataGridViewColumn(new DataGridViewTextBoxCell()) { HeaderText = "Col", Name = "Col" });
            //dataGridView1.Columns.Add(new DataGridViewColumn(new DataGridViewTextBoxCell()) { HeaderText = "Col Header", Name = "ColHeader" });

            dataGridView1.RowCount = _cmp.OrderedDifferences.Count;
            for (int i = 0; i < _cmp.OrderedDifferences.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = "" + (i + 1);

                dataGridView1.Rows[i].Cells[0].Value = "" + (_cmp.OrderedDifferences[i].Row);
                dataGridView1.Rows[i].Cells[1].Value = "" + (_cmp.OrderedDifferences[i].Col + 1);
                //dataGridView1.Rows[row].Cells[2].Value = ""; //%future% add header
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.OrdDiffIdx = e.RowIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
