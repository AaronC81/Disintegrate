using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate.UI
{
    static class Program
    {
        // TODO: Single-click to open control panel

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            PresenceManager.Index<Providers.Dota2PresenceProvider>();
            PresenceManager.Index<Providers.GlobalOffensivePresenceProvider>();
            PresenceManager.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TrayIconContext(args.Contains("show")));
        }
    }
}
