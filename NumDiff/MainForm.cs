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

namespace NumDiff
{
    public partial class MainForm : Form
    {
        private const int MAX_FILE_PATH_VISIBLE = 50;
        private const string APP_NAME = "NumDiff v0.3";
        
        private string _filePath1, _filePath2;
        
        public MainForm()
        {
            InitializeComponent();
            
            this.Text = APP_NAME;
            toolStripStatusLabel1.Text = "";
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
        }

        /// <summary>
        /// Start the compare job
        /// </summary>
        private void DoCompare()
        {
            if (_filePath1 == null || _filePath2 == null)
                return;

            string errMsg;
            int numDiff;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            if (!NumDiffUtil.Compare(_filePath1, _filePath2, GetSeparators(), NumDiff.Properties.Settings.Default.Tollerance, SetData, ShowData, out numDiff, out errMsg))
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(errMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            //%toolstrip%
            if (numDiff == 0)
            {
                toolStripStatusLabel1.Text += "No difference found";
            }
            else
            {
                toolStripStatusLabel1.Text += "Differences found: " + numDiff;
            }
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
                    textBox2.SelectionStart = textBox1.TextLength;
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
            for (int col = 0; col < cols; col++)
            {
                dataGridView1.Columns.Add("col " + col, "col " + col);
                dataGridView2.Columns.Add("col " + col, "col " + col);
            }
            for (int row = 0; row < rows; row++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[row].HeaderCell.Value = "" + (row + 1);
                dataGridView2.Rows.Add();
                dataGridView2.Rows[row].HeaderCell.Value = "" + (row + 1);
            }


            toolStripStatusLabel1.Text = "Rows: " + rows + ", Cols: " + cols + ", "; //%toolstrip%
            vScrollBarMain.Maximum = rows;
        }

        /// <summary>
        /// Show cell data
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <param name="eql"></param>
        private void ShowData(int row, int col, string field1, string field2, bool eql)
        {
            dataGridView1[col, row].Value = field1;
            dataGridView2[col, row].Value = field2;

            if (!eql)
            {
                dataGridView1[col, row].Style.BackColor = Color.Yellow;
                dataGridView2[col, row].Style.BackColor = Color.Yellow;
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

        #endregion

        #region scroll

        private void vScrollBarMain_Scroll(object sender, ScrollEventArgs e)
        {
            //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex + (e.NewValue - e.OldValue);
            int newValue = Math.Max(0, Math.Min(e.NewValue, dataGridView1.Rows.Count - 1));
            dataGridView1.FirstDisplayedScrollingRowIndex = newValue;
            dataGridView2.FirstDisplayedScrollingRowIndex = newValue;
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
