using Force.DeepCloner;
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

        public static Preferences LoadPreferences(PresenceApp app)
        { 
            var path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\preferences\\{MakeValidFileName(app.AppName)}";
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                return Preferences.Deserialize(data, app.Customizer);
            }
            else
            {
                return app.Customizer.Default.DeepClone();
            }
        }

        public static void SavePreferences(PresenceApp app, Preferences newPrefs)
        {
            var prefPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\preferences";
            Directory.CreateDirectory(prefPath);
            var path = $"{prefPath}\\{MakeValidFileName(app.AppName)}";
            File.WriteAllText(path, newPrefs.Serialize());
        }
    }
}
