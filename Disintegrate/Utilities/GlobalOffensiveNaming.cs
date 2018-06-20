using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGSI.Nodes;

namespace Disintegrate.Utilities
{
    public static class GlobalOffensiveNaming
    {
        public static Dictionary<MapMode, string> ModeNames = new Dictionary<MapMode, string>
        {
            [MapMode.Casual] = "Casual",
            [MapMode.Competitive] = "Competitive",
            [MapMode.CoopMission] = "Co-op",
            [MapMode.Custom] = "Custom Game",
            [MapMode.DeathMatch] = "Deathmatch",
            [MapMode.GunGameProgressive] = "Arms Race",
            [MapMode.GunGameTRBomb] = "Demolition",
            [MapMode.ScrimComp2v2] = "Wingman",
            [MapMode.Undefined] = "Unknown"
        };
    }
}
