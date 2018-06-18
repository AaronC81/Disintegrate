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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Contains("installer"))
            {
                // This has just been installed
                // Running it normally would 'block' the installer
                // Instead, relaunch this as a background process
                Process.Start(Application.ExecutablePath, "show");

                return;
            }

            var (providers, errors) = Loader.LoadAllProviders();

            foreach (var provider in providers)
            {
                PresenceManager.Index(provider.Provider);
            }

            PresenceManager.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TrayIconContext(providers, errors, args.Contains("show")));
        }
    }
}
