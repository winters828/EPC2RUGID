using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPC2RUGID
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Change the run function if you want to start a different form
        /// or even two at the same time.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
