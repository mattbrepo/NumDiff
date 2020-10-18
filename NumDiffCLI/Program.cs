using System;
using System.Collections.Generic;
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
                Console.WriteLine("usage: NumDiff <filepath1> <filepath2> [options]");
                Console.WriteLine();
                Console.WriteLine("options:");
                Console.WriteLine("-toll <value>: tollerance value (default: " + DEFAULT_TOLLERANCE + ")");
                Console.WriteLine("-sep <TAB|SPACE|any char>: field separator (default: TAB)");
                Console.WriteLine("-jnd: Just print Number of Differences");
                Console.WriteLine("-name <column num>: print the row name which is in specified column");
                return 0;
            }

            string filePath1 = args[0];
            string filePath2 = args[1];

            double tollerance = DEFAULT_TOLLERANCE;
            //%%%if (!NumDiffUtil.TryParseTollerance(args[0], out tollerance))
            //%%%{
            //%%%    Console.WriteLine("Tollerance is not a valid number");
            //%%%    MyExit(0);
            //%%%    return;
            //%%%}

            string[] separators = new string[1];
            separators[0] = DEFAULT_SEPARATOR;
            //%%%if (args[1] == "TAB")
            //%%%    separators[0] = "\t";
            //%%%else if (args[1] == "SPACE")
            //%%%    separators[0] = " ";
            //%%%else
            //%%%    separators[0] = args[1];

            int nameColumnIndex = 1; //%%%

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

            Console.WriteLine("file 1: " + filePath1);
            Console.WriteLine("file 2: " + filePath2);
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
    }
}
