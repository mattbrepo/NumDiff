using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NumDiff
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        static void MyExit(int res)
        {
            System.Windows.Forms.SendKeys.SendWait("{ENTER}"); // needed to actually exit the application
            Environment.Exit(res);
            Application.Exit();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // redirect console output to parent process;
                // must be before any calls to Console.WriteLine()
                AttachConsole(ATTACH_PARENT_PROCESS);

                if (args.Length < 4)
                {
                    Console.WriteLine("usage: NumDiff <tollerance> <TAB|,|SPACE|;> <filepath1> <filepath2>");
                    MyExit(0);
                    return;
                }

                double tollerance;
                if (!NumDiffUtil.TryParseTollerance(args[0], out tollerance))
                {
                    Console.WriteLine("Tollerance is not a valid number");
                    MyExit(0);
                    return;
                }

                string[] separators = new string[1];
                if (args[1] == "TAB")
                    separators[0] = "\t";
                else if (args[1] == "SPACE")
                    separators[0] = " ";
                else
                    separators[0] = args[1];

                string errMsg;
                int numDiff, firstDiffRow, firstDiffCol;
                if (!NumDiffUtil.Compare(args[2], args[3], separators, tollerance, null, null, out numDiff, out firstDiffRow, out firstDiffCol, out errMsg))
                {
                    Console.WriteLine(errMsg);
                    MyExit(0);
                    return;
                }

                if (numDiff == 0)
                {
                    Console.WriteLine("No difference found");
                    MyExit(1);
                }
                else
                {
                    Console.WriteLine("Differences found: " + numDiff);
                    MyExit(-1);
                }

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            return;
        }
    }
}
