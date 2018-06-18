using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Configuration.Configurators
{
    public class GlobalOffensiveConfigurator : Configurator
    {
        const int SteamId = 730;
        const string FileName = "gamestate_integration_disintegrate.cfg";
        const string Config = @"""Disintegrate Integration Configuration""
{
    ""uri""           ""http://localhost:4000/""
    ""timeout""       ""5.0""
    ""buffer""        ""0.1""
    ""throttle""      ""0.1""
    ""heartbeat""     ""3.0""
    ""data""
    {
        ""provider""            ""1""
        ""map""                 ""1""
        ""round""               ""1""
        ""player_id""           ""1""
        ""player_state""        ""1""
        ""player_weapons""      ""1""
        ""player_match_stats""  ""1""
    }
}
";
        public override string AppName => "CS:GO";

        public override List<string> Configure()
        {
            var gsiDir = $"{SteamLocator.FindGame(SteamId)}\\csgo\\cfg\\";
            Directory.CreateDirectory(gsiDir);

            File.WriteAllText($"{gsiDir}\\{FileName}", Config);

            return new List<string> { $"{gsiDir}\\{FileName}" };
        }

        public override bool IsConfigured() =>
            File.Exists($"{SteamLocator.FindGame(SteamId)}\\csgo\\cfg\\{FileName}");
    }
}
