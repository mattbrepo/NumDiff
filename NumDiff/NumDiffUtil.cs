using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace NumDiff
{
    delegate void HandleComparedFiles(int rows, int cols);
    delegate void HandleComparedValues(int row, int col, string field1, string field2, bool eql);

    class NumDiffUtil
    {
        private static NumberFormatInfo _nfiDot = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        public static bool TryParseTollerance(string toll, out double d)
        {
            return double.TryParse(toll, NumberStyles.Any, _nfiDot, out d);
        }

        public static bool Compare(string filePath1, string filePath2, string[] separators, double tollerance, HandleComparedFiles handleFiles, HandleComparedValues handleValues, out int numDiff, out int firstDiffRow, out int firstDiffCol, out string errMsg)
        {
            List<string[]> content1 = ReadSepFile(filePath1, separators);
            List<string[]> content2 = ReadSepFile(filePath2, separators);

            numDiff = -1;
            firstDiffRow = -1;
            firstDiffCol = -1;
            errMsg = "";
            if (content1 == null)
            {
                errMsg = "Cannot read " + filePath1;
                return false;
            }

            if (content2 == null)
            {
                errMsg = "Cannot read " + filePath2;
                return false;
            }

            if (content1.Count != content2.Count)
            {
                errMsg = "Line number differs: " + content1.Count + " != " + content2.Count;
                return false;
            }

            int maxColCount1 = content1.Max(x => x.Length);
            int maxColCount2 = content2.Max(x => x.Length);
            if (maxColCount1 != maxColCount2)
            {
                errMsg = "Max number of columns differs: " + maxColCount1 + " != " + maxColCount2;
                return false;
            }

            if (handleFiles != null)
                handleFiles(content1.Count, maxColCount1);

            // main loop
            numDiff = 0;
            for (int row = 0; row < content1.Count; row++)
            {
                if (content1[row].Length != content2[row].Length)
                {
                    errMsg = "At row " + row + " column number differs: " + content1[row].Length + " != " + content2[row].Length;
                    return false;
                }

                for (int col = 0; col < content1[row].Length; col++)
                {
                    string field1 = content1[row][col];
                    string field2 = content2[row][col];
                    double d1, d2;
                    bool eql = false;
                    if (TryParseTollerance(field1, out d1) && double.TryParse(field2, NumberStyles.Any, _nfiDot, out d2))
                    {
                        double diff = Math.Abs(d1 - d2);
                        eql = diff < tollerance;
                    }
                    else
                    {
                        eql = field1 == field2;
                    }

                    if (!eql)
                    {
                        numDiff++;
                        if (firstDiffRow == -1)
                        {
                            firstDiffRow = row;
                            firstDiffCol = col;
                        }
                    }

                    if (handleValues != null)
                        handleValues(row, col, field1, field2, eql);
                }
            }

            return true;
        }
            
        /// <summary>
        /// Read tab/comma separated file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="maxNumField"></param>
        /// <returns></returns>
        private static List<string[]> ReadSepFile(string filePath, string[] separators)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> res = new List<string[]>();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] fields = line.Split(separators, StringSplitOptions.None);
                    res.Add(fields);
                }

                return res;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
    }
}
