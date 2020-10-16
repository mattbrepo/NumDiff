using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;

namespace NumDiff
{
    delegate void HandleComparedFiles(int rows, int cols);
    delegate void HandleComparedValues(int row, int col, string field1, string field2, bool eql);

    class NumDiffUtil
    {
        private const int MAX_BLOCK_LINES = 200;
        private static NumberFormatInfo _nfiDot = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Read the file and pass the content to an async procedure
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool ReadCompareFiles(CompareResults cmp, out List<Thread> threads)
        {
            int countLines = 0;
            threads = new List<Thread>();
            try
            {
                using (FileStream fileStream1 = File.OpenRead(cmp.FilePath1))
                using (StreamReader streamReader1 = new StreamReader(fileStream1))
                using (FileStream fileStream2 = File.OpenRead(cmp.FilePath2))
                using (StreamReader streamReader2 = new StreamReader(fileStream2))
                {
                    List<string> lineBlock1 = new List<string>();
                    List<string> lineBlock2 = new List<string>();
                    long firstLineBlock = 0;
                    string line1, line2;
                    while ((line1 = streamReader1.ReadLine()) != null && (line2 = streamReader2.ReadLine()) != null)
                    {
                        lineBlock1.Add(line1);
                        lineBlock2.Add(line2);
                        if (lineBlock1.Count >= MAX_BLOCK_LINES)
                        {
                            // pass lineBlocks to a thread
                            NumDiffUtil ndu = new NumDiffUtil();
                            string[] block1 = lineBlock1.ToArray();
                            string[] block2 = lineBlock2.ToArray();
                            Thread thr = new Thread(() => ndu.ProcessLineBlock(firstLineBlock, block1, block2, cmp));
                            thr.Start();
                            threads.Add(thr);

                            lineBlock1 = new List<string>();
                            lineBlock2 = new List<string>();
                            firstLineBlock = countLines + 1;
                        }

                        countLines++;
                    }

                    // pass last lineBlocks to a thread
                    NumDiffUtil nduLast = new NumDiffUtil();
                    string[] blockLast1 = lineBlock1.ToArray();
                    string[] blockLast2 = lineBlock2.ToArray();
                    Thread thrLast = new Thread(() => nduLast.ProcessLineBlock(firstLineBlock, blockLast1, blockLast2, cmp));
                    thrLast.Start();
                    threads.Add(thrLast);

                    // set row count
                    cmp.AddRows(0, countLines);
                    if (!streamReader1.EndOfStream)
                    {
                        int countExtraLines = 0;
                        while ((line1 = streamReader1.ReadLine()) != null)
                            countExtraLines++;
                        cmp.AddRows(1, countExtraLines);
                    }
                    else if (!streamReader1.EndOfStream)
                    {
                        int countExtraLines = 0;
                        while ((line2 = streamReader2.ReadLine()) != null)
                            countExtraLines++;
                        cmp.AddRows(2, countExtraLines);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private void ProcessLineBlock(long firstLineBlock, string[] lineBlock1, string[] lineBlock2, CompareResults cmp)
        {
            try
            {
                int minLines = Math.Min(lineBlock1.Length, lineBlock2.Length);
                for (int row = 0; row < minLines; row++)
                {
                    long realRow = firstLineBlock + row;

                    string line1 = lineBlock1[row];
                    string[] fields1 = line1.Split(cmp.Separators, StringSplitOptions.None);

                    string line2 = lineBlock2[row];
                    string[] fields2 = line2.Split(cmp.Separators, StringSplitOptions.None);

                    if (fields1.Length != fields2.Length)
                    {
                        cmp.SetMaxCols(1, fields1.Length);
                        cmp.SetMaxCols(2, fields2.Length);
                        string errMsg = "At row " + realRow + " column number differs: " + fields1.Length + " != " + fields2.Length;
                        cmp.Errors.Add(errMsg);
                        return;
                    }

                    cmp.SetMaxCols(0, fields1.Length);

                    for (int col = 0; col < fields1.Length; col++)
                    {
                        string field1 = fields1[col];
                        string field2 = fields2[col];
                        double d1, d2;
                        bool eql = false;
                        if (TryParseTollerance(field1, out d1) && double.TryParse(field2, NumberStyles.Any, _nfiDot, out d2))
                        {
                            double diff = Math.Abs(d1 - d2);
                            eql = diff < cmp.Tollerance;
                        }
                        else
                        {
                            eql = field1 == field2;
                        }

                        if (!eql)
                        {
                            cmp.Differences.Add(new DifferentCells { Row = realRow, Col = col });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cmp.Errors.Add("ProcessLineBlock exception at block starting from " + firstLineBlock + ", ex: " + ex.ToString());
            }
        }

        public static bool TryParseTollerance(string toll, out double d)
        {
            return double.TryParse(toll, NumberStyles.Any, _nfiDot, out d);
        }

        public static bool Compare(string filePath1, string filePath2, string[] separators, double tollerance, out CompareResults cmp)
        {
            cmp = new CompareResults() { Tollerance = tollerance, Separators = separators, FilePath1 = filePath1, FilePath2 = filePath2 };
            List<Thread> threads;
            if (!ReadCompareFiles(cmp, out threads))
            {
                cmp.Errors.Add("Cannot read files");
                return false;
            }

            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }

            if (cmp.CountRows1 != cmp.CountRows2)
            {
                cmp.Errors.Add("Line number differs: " + cmp.CountRows1 + " != " + cmp.CountRows2);
                return false;
            }
            
            if (cmp.CountCols1 != cmp.CountCols2)
            {
                cmp.Errors.Add("Max number of columns differs: " + cmp.CountCols1 + " != " + cmp.CountCols2);
                return false;
            }

            return true;
        }
    }
}
