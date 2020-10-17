﻿using System;
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
                Console.WriteLine(); // better like this

                MyExit(-1);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            return;
        }
    }
}
