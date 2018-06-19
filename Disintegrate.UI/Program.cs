using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace Disintegrate.UI
{
    static class Program
    {
        // TODO: Single-click to open control panel

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager(@"https://github.com/OrangeFlash81/Disintegrate"))
                {
                    await mgr.Result.UpdateApp();
                }

                PresenceManager.Index<Providers.Dota2PresenceProvider>();
                PresenceManager.Index<Providers.GlobalOffensivePresenceProvider>();
                PresenceManager.Start();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new TrayIconContext(args.Contains("show")));
            }
            catch (Exception e)
            {
#if DEBUG
                throw;
#else
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/disintegrate-crash.log";
                File.WriteAllText(path, e.Message + "\n" + e.StackTrace);
#endif
            }
        }
    }
}
