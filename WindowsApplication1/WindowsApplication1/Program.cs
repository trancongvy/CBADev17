using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsApplication1
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
            System.Diagnostics.Process.Start("rasdial.exe", "115.84.182.129 ketoanquandat devUser@Infosoft");
            Application.Run(new Form1());
      
        }
    }
}