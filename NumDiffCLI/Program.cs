using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumDiffLib;

namespace NumDiffCLI
{
    class Program
    {
        private const double DEFAULT_TOLLERANCE = 0.0001;
        private const string DEFAULT_SEPARATOR = "\t";

        static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: NumDiff <path1> <path2> [options]");
                Console.WriteLine();
                Console.WriteLine("options:");
                Console.WriteLine("-dir: paths refer to two directories)");
                Console.WriteLine("-toll <value>: tollerance value (default: " + DEFAULT_TOLLERANCE + ")");
                Console.WriteLine("-sep <TAB|SPACE|any char>: field separator (default: TAB)");
                Console.WriteLine("-name <column num>: print the row name which is in specified the 0-based column number (default: 1)");
                //Console.WriteLine("-jnd: Just print Number of Differences");
                return 0;
            }

            string path1 = args[0];
            string path2 = args[1];

            double tollerance = DEFAULT_TOLLERANCE;
            if (HasArg(args, "-toll"))
            {
                string val = GetArgPar(args, "-toll", 1);
                if (!NumDiffUtil.TryParseTollerance(val, out tollerance))
                {
                    Console.WriteLine("Tollerance is not a valid number");
                    return 1;
                }
            }

            string[] separators = new string[1];
            separators[0] = DEFAULT_SEPARATOR;
            if (HasArg(args, "-sep"))
            {
                string val = GetArgPar(args, "-sep", 1);
                if (val == "TAB")
                    separators[0] = "\t";
                else if (val == "SPACE")
                    separators[0] = " ";
                else
                    separators[0] = val;
            }

            int nameColumnIndex = 1;
            if (HasArg(args, "-name"))
            {
                string val = GetArgPar(args, "-name", 1);
                if (!int.TryParse(val, out nameColumnIndex))
                {
                    Console.WriteLine("Column number is not a valid number");
                    return 1;
                }
            }

            if (HasArg(args, "-dir"))
            {
                string dir1 = Path.GetDirectoryName(path1);
                string pattern1 = Path.GetFileName(path1);
                if (pattern1 == "") pattern1 = "*.*";

                string dir2 = Path.GetDirectoryName(path2);
                string pattern2 = Path.GetFileName(path2);
                if (pattern2 == "") pattern2 = "*.*";

                string[] files1 = Directory.GetFiles(dir1, pattern1);
                for (int i = 0; i < files1.Count(); i++)
                {
                    string filePath1 = files1[i];
                    string filePath2 = Path.Combine(dir2, Path.GetFileName(filePath1));

                    Console.WriteLine();
                    Console.WriteLine("========================================= " + (i + 1));
                    if (File.Exists(filePath2))
                    {
                        ManageCompareFiles(filePath1, filePath2, nameColumnIndex, tollerance, separators);
                    }
                    else
                    {
                        Console.WriteLine("File does not exists: " + filePath2);
                    }
                }

                // check for files existing only path2
                string[] files2 = Directory.GetFiles(dir2, pattern2);
                for (int i = 0; i < files2.Count(); i++)
                {
                    string filePath2 = files2[i];
                    string filePath1 = Path.Combine(dir1, Path.GetFileName(filePath2));

                    if (!File.Exists(filePath1))
                    {
                        Console.WriteLine();
                        Console.WriteLine("========================================= " + (files1.Count() + i + 1));

                        Console.WriteLine("File does not exists: " + filePath1);
                    }
                }

                return 0;
            }

            ManageCompareFiles(path1, path2, nameColumnIndex, tollerance, separators);

            return 0;
        }

        private static int ManageCompareFiles(string filePath1, string filePath2, int nameColumnIndex, double tollerance, string[] separators)
        {
            Console.WriteLine("file 1: " + filePath1);
            Console.WriteLine("file 2: " + filePath2);

            CompareResults cmp = new CompareResults() { Tollerance = tollerance, Separators = separators, ReadHeaders = true, NameColumnIndex = nameColumnIndex, FilePath1 = filePath1, FilePath2 = filePath2 };
            if (!NumDiffUtil.ReadCompare(cmp))
            {
                string errMsg = string.Join("\n", cmp.Errors);
                Console.WriteLine(errMsg);
                return 2;
            }

            if (cmp.Differences.Count == 0)
            {
                Console.WriteLine("No difference found");
                return 1;
            }

            Console.WriteLine("Differences found: " + cmp.Differences.Count);
            Console.WriteLine();
            Console.WriteLine("============================ differences");

            // sort and group the differences
            var diffs = cmp.Differences.OrderBy(x => x.Row).ThenBy(x => x.Col).GroupBy(x => x.Row).ToList();
            string oldHeader = "";
            for (int i = 0; i < diffs.Count; i++)
            {
                string header = "ROW";
                string values1 = "" + diffs[i].Key;
                string values2 = "" + diffs[i].Key;
                if (nameColumnIndex >= 0)
                {
                    DifferentCell cell = diffs[i].ElementAt(0);
                    header += "\t" + cmp.Headers[nameColumnIndex];
                    values1 += "\t" + cell.Name;
                    values2 += "\t" + cell.Name;
                }

                for (int j = 0; j < diffs[i].Count(); j++)
                {
                    DifferentCell cell = diffs[i].ElementAt(j);
                    header += "\t" + cmp.Headers[cell.Col];
                    values1 += "\t" + cell.Value1;
                    values2 += "\t" + cell.Value2;
                }

                if (oldHeader != header)
                {
                    oldHeader = header;
                    Console.WriteLine();
                    Console.WriteLine(header);
                }
                Console.WriteLine(values1);
                Console.WriteLine(values2);
            }

            if (cmp.Errors.Count > 0)
            {
                Console.WriteLine("============================ errors");
                for (int i = 0; i < cmp.Errors.Count; i++)
                {
                    Console.WriteLine(cmp.Errors.ElementAt(i));
                }
            }

            return 0;
        }

        private static bool HasArg(string[] args, string arg)
        {
            string val = GetArgPar(args, arg, 0);

            if (val != "")
                return true;
            return false;
        }

        private static string GetArgPar(string[] args, string arg, int parCount)
        {
            for (int i = 0; i < args.Count(); i++)
            {
                if (args[i] == arg)
                {
                    return args[i + parCount];
                }
            }
            return "";
        }


    }
}
