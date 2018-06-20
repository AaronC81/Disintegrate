using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;

namespace Disintegrate.Configuration.Configurators
{
    public class HearthstoneConfiguator : Configurator
    {
        private string _logConfigLocation = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%/Blizzard/Hearthstone/log.config");

        public override List<string> Configure()
        {
            // If the file already exists, read it, otherwise just create a blank one
            IniParser.Model.IniData data;
            if (File.Exists(_logConfigLocation))
            {
                data = new FileIniDataParser().ReadFile(_logConfigLocation);
            }
            else
            {
                data = new IniParser.Model.IniData();
            }

            // Create the [Power] section if it doesn't exist
            if (!data.Sections.ContainsSection("Power"))
            {
                data.Sections.AddSection("Power");
            }

            // Set things appropriately
            data.Sections["Power"]["LogLevel"] = "1";
            data.Sections["Power"]["Verbose"] = "true";
            data.Sections["Power"]["FilePrinting"] = "true";

            // Write back
            File.WriteAllText(_logConfigLocation, data.ToString());
            return new List<string> { _logConfigLocation };
        }

        public override bool IsConfigured()
        {
            // Check that the file actually exists
            if (!File.Exists(_logConfigLocation))
            {
                return false;
            }

            // Check that it contains a [Power] section
            var parsedIni = new FileIniDataParser().ReadFile(_logConfigLocation);

            if (!parsedIni.Sections.ContainsSection("Power"))
            {
                return false;
            }

            // Check that it has LogLevel=1, FilePrinting=true and Verbose=true
            var powerSection = parsedIni.Sections["Power"];

            bool EqualsIfExists(string key, string expected) =>
                powerSection.ContainsKey(key) && powerSection[key].ToLower() == expected.ToLower();

            return EqualsIfExists("Verbose", "true")
                && EqualsIfExists("LogLevel", "1")
                && EqualsIfExists("FilePrinting", "true");            
        }
    }
}
