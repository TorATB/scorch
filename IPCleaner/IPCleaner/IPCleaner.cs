﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    static class IPCleaner
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new IPCleanerForm());
            }
            else
            {
            }
        }

        

    }

  
}
