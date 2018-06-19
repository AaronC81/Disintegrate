using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Customization.Customizers
{
    public class Dota2Customizer : Customizer
    {
        private static List<string> _icons = new List<string>
        {
            "Team",
            "None"
        };

        private static List<string> _checkboxes = new List<string> { };

        private static List<TextField> _textFields = new List<TextField>
        {
            new TextField("Kills", "12"),
            new TextField("Deaths", "5"),
            new TextField("Assists", "20"),
            new TextField("Denies", "32"),
            new TextField("LastHits", "173"),
            new TextField("Team", "Radiant"),
            new TextField("Hero", "Keeper of the Light"),
            new TextField("Level", "25"),
            new TextField("Gold", "1,435")
        };

        public Dota2Customizer() : base(_icons, _checkboxes, _textFields) { }
    }
}
