using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Customization.Customizers
{
    public class GlobalOffensiveCustomizer : Customizer
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
            new TextField("Team", "Counter-Terrorists"),
            new TextField("MVPs", "3"),
            new TextField("Score", "CT 11 - 4 T"),
            new TextField("Map", "de_mirage"),
            new TextField("Mode", "Competitive")
        };

        public GlobalOffensiveCustomizer() : base(_icons, _checkboxes, _textFields)
        {
            Default = new Preferences
            {
                CheckedCheckboxes = new List<string>(),
                Customizer = this,
                Icon = "Team",
                LineOne = "{Score}",
                LineTwo = "{Kills}/{Assists}/{Deaths} - {MVPs} MVPs"
            };
        }
    }
}
