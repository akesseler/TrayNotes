/*
 * Copyright (C)  2014  Axel Kesseler
 * 
 * This software is free and you can use it for any purpose. Furthermore, 
 * you are free to copy, to modify and/or to redistribute this software.
 * 
 * In addition, this software is distributed in the hope that it will be 
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace plexdata.TrayNotes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Program.IsRunning)
            {
                Application.Run(new MainForm());
            }
            else
            {
                string message = String.Format("Program \"{0}\" is already running. Cannot start the program twice.", AboutBox.Title);
                MessageBox.Show(message, AboutBox.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private static bool IsRunning
        {
            get
            {
                Process current = Process.GetCurrentProcess();

                foreach (Process process in Process.GetProcesses())
                {
                    // Skip the own process...
                    if (current.Id == process.Id) { continue; }

                    // Hopefully, each process name is unique...
                    if (0 == String.Compare(current.ProcessName, process.ProcessName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
