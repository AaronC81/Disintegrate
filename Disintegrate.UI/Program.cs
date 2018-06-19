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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            try
            {
                using (var mgrTask = UpdateManager.GitHubUpdateManager(@"https://github.com/OrangeFlash81/Disintegrate"))
                {
                    var mgr = mgrTask.Result;

                    SquirrelAwareApp.HandleEvents(
                        onInitialInstall: v =>
                        {
                            mgr.CreateShortcutsForExecutable("Disintegrate.UI.exe", ShortcutLocation.Startup, false);
                        },
                        onAppUpdate: v =>
                        {
                            mgr.CreateShortcutsForExecutable("Disintegrate.UI.exe", ShortcutLocation.Startup, true);
                        }
                    );

                    await mgr.UpdateApp();
                }

                PresenceManager.PreferenceLoader = Customization.Loader.LoadPreferences;

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
