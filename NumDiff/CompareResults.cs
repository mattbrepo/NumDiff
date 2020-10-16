using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumDiff
{
    class DifferentCells
    {
        public long Col { get; set; }
        public long Row { get; set; }
    }

    class CompareResults
    {
        private readonly object _lockObj = new object();

        public string[] Separators { get; set; }
        public double Tollerance { get; set; }
        public string FilePath1 { get; set; }
        public string FilePath2 { get; set; }
        public long CountRows1 { get; private set; }
        public long CountRows2 { get; private set; }
        public long CountCols1 { get; private set; }
        public long CountCols2 { get; private set; }

        public ConcurrentBag<DifferentCells> Differences { get; set; }
        public ConcurrentBag<string> Errors { get; set; }

        public CompareResults()
        {
            Differences = new ConcurrentBag<DifferentCells>();
            Errors = new ConcurrentBag<string>();
        }

        public void AddRows(int fileNum, int rows)
        {
            lock (_lockObj)
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
            lock (_lockObj)
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
    }
}
