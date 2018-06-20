using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Configuration.Configurators
{
    // TODO: This works, but is quite destructive.
    //       Could do with an INI reader/writer to implement this properly.
    public class HearthstoneConfiguator : Configurator
    {
        const string Config = @"[Power]
LogLevel=1
FilePrinting=true
ConsolePrinting=true
ScreenPrinting=false
Verbose=true";

        private string _logConfigLocation = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%/Blizzard/Hearthstone/log.config");

        public override List<string> Configure()
        {
            
            File.WriteAllText(_logConfigLocation, Config);
            return new List<string> { _logConfigLocation };
        }

        public override bool IsConfigured() =>
            File.Exists(_logConfigLocation) && File.ReadAllText(_logConfigLocation) == Config;
    }
}
