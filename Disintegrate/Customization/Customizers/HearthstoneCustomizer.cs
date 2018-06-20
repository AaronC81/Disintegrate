using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Customization.Customizers
{
    public class HearthstoneCustomizer : Customizer
    {
        private static List<string> _icons = new List<string>
        {
            "None"
        };

        private static List<string> _checkboxes = new List<string> { };

        private static List<TextField> _textFields = new List<TextField>
        {
            new TextField("GameType", "Ranked"),
            new TextField("Format", "Standard")
        };

        public HearthstoneCustomizer() : base(_icons, _checkboxes, _textFields)
        {
            Default = new Preferences
            {
                CheckedCheckboxes = new List<string>(),
                Customizer = this,
                Icon = "None",
                LineOne = "{GameType}",
                LineTwo = "{Format}"
            };
        }
    }
}
