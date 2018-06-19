using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate.Customization
{
    /// <summary>
    /// Handles loading and saving <see cref="Preferences"/>.
    /// </summary>
    public static class Loader
    {
        public static string MakeValidFileName(string original)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                original = original.Replace(invalidChar, ' ');
            }

            return original;
        }

        public static Preferences LoadPreferences(PresenceProvider provider)
        {
            var appName = provider.Configurator.AppName;

            var path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\preferences\\{MakeValidFileName(appName)}";
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                return Preferences.Deserialize(data);
            }
            else
            {
                return null;
            }
        }

        public static void SavePreferences(PresenceProvider provider, Preferences prefs)
        {
            var appName = provider.Configurator.AppName;

            var path = $"{Application.ExecutablePath}\\preferences\\{MakeValidFileName(appName)}";
            File.WriteAllText(path, prefs.Serialize());
        }
    }
}
