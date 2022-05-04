using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumDiffLib
{
    public class DifferentCell
    {
        public string Name { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }

    public class CompareResults
    {
        private readonly object _lockObjRow = new object();
        private readonly object _lockObjCol = new object();

        public string[] Separators { get; set; }
        public double Tollerance { get; set; }
        public bool ReadHeaders { get; set; }
        public int NameColumnIndex { get; set; }

        public string FilePath1 { get; set; }
        public string FilePath2 { get; set; }
        public int CountRows1 { get; private set; }
        public int CountRows2 { get; private set; }
        public int CountCols1 { get; private set; }
        public int CountCols2 { get; private set; }
        public List<string> Headers { get; set; }

        public ConcurrentBag<DifferentCell> Differences { get; set; }
        public List<DifferentCell> OrderedDifferences { get; set; }
        public ConcurrentBag<string> Errors { get; set; }

        public CompareResults()
        {
            Differences = new ConcurrentBag<DifferentCell>();
            Errors = new ConcurrentBag<string>();
            Headers = new List<string>();
            NameColumnIndex = -1;
        }

        public void AddRows(int fileNum, int rows)
        {
            lock (_lockObjRow)
            {
                if (fileNum == 1)
                {
                    CountRows1 += rows;
                }
                else if (fileNum == 2)
                {
                    CountRows2 += rows;
                }
                else
                {
                    CountRows1 += rows;
                    CountRows2 += rows;
                }
            }
        }

        public void SetMaxCols(int fileNum, int cols)
        {
            lock (_lockObjCol)
            {
                if (fileNum == 1)
                {
                    if (cols > CountCols1)
                        CountCols1 = cols;
                }
                else if (fileNum == 2)
                {
                    if (cols > CountCols2)
                        CountCols2 = cols;
                }
                else
                {
                    if (cols > CountCols1)
                        CountCols1 = cols;
                    if (cols > CountCols2)
                        CountCols2 = cols;
                }
            }
        }

        public int GetMaxCountRows()
        {
            return Convert.ToInt32(Math.Max(CountRows1, CountRows2));
        }

        public int GetMaxCountCols()
        {
            return Convert.ToInt32(Math.Max(CountCols1, CountCols2));
        }

        public bool HasDifference(int rowIndex, int columnIndex)
        {
            return Differences.Any(x => x.Row == rowIndex && x.Col == columnIndex);
        }
    }
}
