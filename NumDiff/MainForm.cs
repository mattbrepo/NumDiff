using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using NumDiffLib;

//%%% 
// * there is still a lot to do on the datagrid!!!
// * show complete list of row/col different (maybe in a separate sortable table)
// * add option to show only column with at least one differnent value
// * add option to ignore na<->number
// * add option to use first row as table header
// * add button to jump to previous/next difference cell

namespace NumDiff
{
    public partial class MainForm : Form
    {
        private const string APP_NAME = "NumDiff v0.5";
        private const int MAX_FILE_PATH_VISIBLE = 50;
        private const int READ_BLOCK_LINES = 50;
        
        private string _filePath1, _filePath2, _lastSearch;
        private int _lastGoToRow, _lastGoToCol;

        private int[] _readBlockRowIndex = new int[2];
        private List<string[]>[] _readBlockRows = new List<string[]>[2];
        private CompareResults _cmp;

        public MainForm()
        {
            InitializeComponent();
            
            this.Text = APP_NAME;
            dataGridView1.VirtualMode = true;
            dataGridView2.VirtualMode = true;
            ResetAll(true);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //%prodebug%
            //SetFilePath(1, @"C:\Users\barbarie\Desktop\util\alva\projects\workspace\utility\test_alvaModel\single_test\mtxYPred.txt");
            //SetFilePath(2, @"C:\Users\barbarie\Desktop\util\alva\projects\workspace\utility\test_alvaModel\single_test\zzz_BMF_OLS_YPred.txt");
            //DoCompare();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NumDiff.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Reset the UI
        /// </summary>
        /// <param name="alsoFilePath"></param>
        private void ResetAll(bool alsoFilePath)
        {
            if (alsoFilePath)
            {
                SetFilePath(1, null);
                SetFilePath(2, null);
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            vScrollBarMain.Value = 0;
            toolStripStatusRowsCols.Text = "";
            toolStripStatusDiffResult.Text = "";
            toolStripStatusCurrCellLeft.Text = "";
            toolStripStatusCurrCellRight.Text = "";

            _lastGoToRow = 0;
            _lastGoToCol = 0;
            _lastSearch = "";
        }

        /// <summary>
        /// Start the compare job
        /// </summary>
        private void DoCompare()
        {
            if (_filePath1 == null || _filePath2 == null)
                return;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            _cmp = new CompareResults() { Tollerance = NumDiff.Properties.Settings.Default.Tollerance, Separators = GetSeparators(), FilePath1 = _filePath1, FilePath2 = _filePath2 };
            if (!NumDiffUtil.ReadCompare(_cmp))
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

                string errMsg = string.Join("\n", _cmp.Errors);
                MessageBox.Show(errMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // read first block (it could be optimized)
            UpdateReadBlock(0);

            // set UI data
            SetData(_cmp.GetMaxCountRows(), _cmp.GetMaxCountCols());

            //%toolstrip%
            if (_cmp.Differences.Count == 0)
            {
                toolStripStatusDiffResult.Text = "No difference found";
            }
            else
            {
                toolStripStatusDiffResult.Text = "Differences found: " + _cmp.Differences.Count;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Get the separators string array
        /// </summary>
        /// <returns></returns>
        private string[] GetSeparators()
        {
            string[] res = new string[NumDiff.Properties.Settings.Default.Separators.Count];
            for (int i = 0; i < res.Length; i++)
            {
                if (NumDiff.Properties.Settings.Default.Separators[i] == "TAB")
                    res[i] = "\t";
                else
                    res[i] = NumDiff.Properties.Settings.Default.Separators[i];
            }

            return res;
        }

        /// <summary>
        /// Get only the last part of a file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetOnlyLastPart(string filePath)
        {
            if (filePath.Length > MAX_FILE_PATH_VISIBLE)
            {
                return "..." + filePath.Substring(filePath.Length - MAX_FILE_PATH_VISIBLE);
            }
            else
            {
                return filePath;
            }
        }

        /// <summary>
        /// Set an active file path
        /// </summary>
        /// <param name="num"></param>
        /// <param name="filePath"></param>
        private void SetFilePath(int num, string filePath)
        {
            if (num == 1)
            {
                _filePath1 = filePath;
                if (filePath == null)
                {
                    textBox1.Text = "";
                }
                else
                {
                    textBox1.Text = filePath;
                    textBox1.SelectionStart = textBox1.TextLength;
                }
            }
            else
            {
                _filePath2 = filePath;
                if (filePath == null)
                {
                    textBox2.Text = "";
                }
                else
                {
                    textBox2.Text = filePath;
                    textBox2.SelectionStart = textBox2.TextLength;
                }
            }
        }

        /// <summary>
        /// Set UI data grid rows/cols
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        private void SetData(int rows, int cols)
        {
            // reset data grid
            this.Text = APP_NAME + " - " + GetOnlyLastPart(_filePath1) + " - " + GetOnlyLastPart(_filePath2);

            ResetAll(false);
            dataGridView1.ColumnCount = cols;
            dataGridView1.RowCount = rows;
            dataGridView2.ColumnCount = cols;
            dataGridView2.RowCount = rows;

            toolStripStatusRowsCols.Text = "Rows: " + rows + ", Cols: " + cols; //%toolstrip%
            vScrollBarMain.Maximum = rows;
            hScrollBarMain.Maximum = cols;
        }

        private string GetCellValue(int fileNum, int row, int col)
        {
            if (_readBlockRows[fileNum - 1] == null || row < _readBlockRowIndex[fileNum - 1] || row > (_readBlockRowIndex[fileNum - 1] + _readBlockRows[fileNum - 1].Count))
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                UpdateReadBlock(row);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                if (_readBlockRows[fileNum - 1] == null)
                    return "";
            }

            int intRow = row - _readBlockRowIndex[fileNum - 1];

            if (intRow < 0 || intRow >= _readBlockRows[fileNum - 1].Count)
                return "";
            if (col < 0 || col >= _readBlockRows[fileNum - 1][intRow].Length)
                return "";
            return _readBlockRows[fileNum - 1][intRow][col];
        }

        private void UpdateReadBlock(int row)
        {
            int visibleRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Displayed);
            if (visibleRowCount == 0)
                visibleRowCount = READ_BLOCK_LINES;

            for (int fileNum = 1; fileNum <= 2; fileNum++)
            {
                _readBlockRows[fileNum - 1] = NumDiffUtil.ReadBlock(fileNum == 1 ? _filePath1 : _filePath2, GetSeparators(), row, visibleRowCount);
                _readBlockRowIndex[fileNum - 1] = row;
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1; // https://social.msdn.microsoft.com/Forums/windows/en-US/327ff3e0-098c-4657-9af4-20d31fe5d6f0/sum-of-the-columns-fillweight-values-cannot-exceed-65535?forum=winformsdatacontrols
        }

        private void dataGridView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1; // https://social.msdn.microsoft.com/Forums/windows/en-US/327ff3e0-098c-4657-9af4-20d31fe5d6f0/sum-of-the-columns-fillweight-values-cannot-exceed-65535?forum=winformsdatacontrols
        }

        #region drag drop
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length < 1)
                return;

            if (files.Length >= 2)
            {
                SetFilePath(1, files[0]);
                SetFilePath(2, files[1]);
            }
            else
            {
                SetFilePath(1, files[0]);
            }
            DoCompare();
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length < 1)
                return;

            if (files.Length >= 2)
            {
                SetFilePath(1, files[0]);
                SetFilePath(2, files[1]);
            }
            else
            {
                SetFilePath(2, files[0]);
            }
            DoCompare();
        } 
        #endregion

        #region menu

        private void openFile1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            SetFilePath(1, ofd.FileName);
            DoCompare();
        }

        private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            SetFilePath(2, ofd.FileName);
            DoCompare();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm of = new OptionsForm();
            DialogResult dr = of.ShowDialog(this);
            if (dr == DialogResult.OK)
                DoCompare();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetAll(true);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetAll(false);
            DoCompare();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm gf = new GoToForm();
            gf.SetRowCol(_lastGoToRow, _lastGoToCol);
            DialogResult dr = gf.ShowDialog(this);
            if (dr != DialogResult.OK)
                return;

            int row, col;
            if (!gf.GetRowCol(out row, out col))
                return;
            try
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
                dataGridView2.CurrentCell = dataGridView2.Rows[row].Cells[col];
                _lastGoToRow = row;
                _lastGoToCol = col;
            }
            catch
            {
            }
        }
        
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm sf = new SearchForm();
            sf.SetSearch(_lastSearch);
            DialogResult dr = sf.ShowDialog(this);
            if (dr != DialogResult.OK)
                return;
            _lastSearch = sf.GetSearch();

            // %improve% very basic search (still usefull when searching a column name)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString().Contains(_lastSearch))
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        MessageBox.Show(_lastSearch + " found at " + (cell.RowIndex + 1) + "," + (cell.ColumnIndex + 1), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            MessageBox.Show("String not found", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region grid event

        private void vScrollBarMain_Scroll(object sender, ScrollEventArgs e)
        {
            //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex + (e.NewValue - e.OldValue);
            int newValue = Math.Max(0, Math.Min(e.NewValue, dataGridView1.Rows.Count - 1));
            dataGridView1.FirstDisplayedScrollingRowIndex = newValue;
            dataGridView2.FirstDisplayedScrollingRowIndex = newValue;
        }

        private void dataGridView2_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = GetCellValue(2, e.RowIndex, e.ColumnIndex);
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = GetCellValue(1, e.RowIndex, e.ColumnIndex);
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_cmp.HasDifference(e.RowIndex, e.ColumnIndex))
                e.CellStyle.BackColor = Color.Yellow;
            else
                e.CellStyle.BackColor = SystemColors.Window;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_cmp.HasDifference(e.RowIndex, e.ColumnIndex))
                e.CellStyle.BackColor = Color.Yellow;
            else
                e.CellStyle.BackColor = SystemColors.Window;
        }

        private void showDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_cmp != null && _cmp.Differences.Count > 0)
            {
                string msg = "";
                for (int i = 0; i < _cmp.Differences.Count; i++) //%%% ordinare per riga/colonna
                {
                    msg += (_cmp.Differences.ElementAt(i).Row + 1) + ", " + (_cmp.Differences.ElementAt(i).Col + 1) + "\n";
                }
                MessageBox.Show(msg);
            }
        }

        private void dataGridView2_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                toolStripStatusCurrCellRight.Text = "R: " + (e.Cell.RowIndex + 1) + "," + (e.Cell.ColumnIndex + 1);
            }
        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                toolStripStatusCurrCellLeft.Text = "L: " + (e.Cell.RowIndex + 1) + "," + (e.Cell.ColumnIndex + 1);
            }
        }

        private void hScrollBarMain_Scroll(object sender, ScrollEventArgs e)
        {
            int newValue = Math.Max(0, Math.Min(e.NewValue, dataGridView1.Columns.Count - 1));
            dataGridView1.FirstDisplayedScrollingColumnIndex = newValue;
            dataGridView2.FirstDisplayedScrollingColumnIndex = newValue;
        }

        #endregion

    }
}
