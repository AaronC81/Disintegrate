using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameloop.Vdf;
using System.Text.RegularExpressions;

namespace Disintegrate.Configuration
{
    /// <summary>
    /// Provides methods for locating Steam games.
    /// </summary>
    public static class SteamLocator
    {
        /// <summary>
        /// Gets the path at which Steam is installed. Returns null if it can't be found.
        /// </summary>
        public static string GetInstallPath()
        {
            // The path is different for 64-bit and 32-bit machines
            var value = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam\", // 64-bit
                "InstallPath", null);
            if (value == null)
            {
                value = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam\", // 32-bit
                    "InstallPath", null);
            }

            return value;
        }

        /// <summary>
        /// Gets a list of all Steam library paths.
        /// </summary>
        public static List<string> GetLibraries()
        {
            var installPath = GetInstallPath();
            var libraries = new List<string>()
            {
                installPath
            };
            
            // Read and parse Steam library config info
            var libraryConfigPath = $"{installPath}\\steamapps\\libraryfolders.vdf";
            var libraryConfigText = File.ReadAllText(libraryConfigPath);
            dynamic libraryConfig = VdfConvert.Deserialize(libraryConfigText);

            // The VDF file has a structure like:
            // {
            //     ...
            //     "1"     "E:\..."
            //     "2"     "F:\..."
            // }
            int current = 1;
            while (true)
            {
                try
                {
                    var thisLibrary = (string)libraryConfig.Value[current.ToString()].Value;
                    libraries.Add(thisLibrary);
                    current++;
                }
                catch (KeyNotFoundException)
                {
                    // That's all the libraries
                    break;
                }
            }

            return libraries;
        }
             
        /// <summary>
        /// Attempts to find a Steam game by its app ID across all Steam libraries.
        /// </summary>
        /// <param name="appId">The app ID of the sought game.</param>
        /// <returns>The path to the game, or null if it can't be found.</returns>
        public static string FindGame(int appId)
        {
            var appManifestRegex = new Regex(@"^appmanifest_(\d+).acf$");

            foreach (var library in GetLibraries())
            {
                // Look for appmanifest_###.acf files in the library's steamapps folder
                var steamapps = $"{library}\\steamapps\\";
                foreach (var filePath in Directory.GetFiles(steamapps))
                {
                    var fileName = Path.GetFileName(filePath);
                    var match = appManifestRegex.Match(fileName);

                    if (match.Success)
                    {
                        // This is an appmanifest - find its ID
                        var thisId = int.Parse(match.Groups[1].Value);

                        if (appId == thisId)
                        {
                            // This is the app we were looking for - find the installation path
                            var configFileText = File.ReadAllText(filePath);
                            dynamic config = VdfConvert.Deserialize(configFileText);
                            var installFolder = config.Value.installdir.Value;

                            return $"{library}\\steamapps\\common\\{installFolder}";
                        }
                    }
                }
            }

            return null;
        }
    }
}
