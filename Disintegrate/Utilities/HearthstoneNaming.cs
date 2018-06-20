using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Utilities
{
    public static class HearthstoneNaming
    {
        public static Dictionary<string, string> GameTypeNames { get; } = new Dictionary<string, string>
        {
            ["GT_UNKNOWN"] = "Unknown",
            ["GT_VS_AI"] = "VS. Innkeeper",
            ["GT_VS_FRIEND"] = "VS. Friend",
            ["GT_TUTORIAL"] = "Tutorial",
            ["GT_ARENA"] = "Arena",
            ["GT_RANKED"] = "Ranked",
            ["GT_CASUAL"] = "Casual",
            ["GT_TAVERNBRAWL"] = "Tavern Brawl",
            ["GT_TB_1P_VS_AI"] = "Tavern Brawl",
            ["GT_TB_2P_COOP"] = "Tavern Brawl",
            ["GT_FSG_BRAWL_VS_FRIEND"] = "Fireside Gathering",
            ["GT_FSG_BRAWL"] = "Fireside Gathering",
            ["GT_FSG_BRAWL_1P_VS_AI"] = "Fireside Gathering",
            ["GT_FSG_BRAWL_2P_COOP"] = "Fireside Gathering",
        };

        public static Dictionary<string, string> FormatTypeNames { get; } = new Dictionary<string, string>
        {
            ["FT_WILD"] = "Wild",
            ["FT_STANDARD"] = "Standard",
            ["FT_UNKNOWN"] = "Unknown"
        };
    }
}
