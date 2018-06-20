using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate
{
    /// <summary>
    /// An image name and description.
    /// </summary>
    public class ImageBundle
    {
        public ImageBundle(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public string Name { get; }
        public string Text { get; }

        public void Deconstruct(out string name, out string text) =>
            (name, text) = (Name, Text);
    }

    /// <summary>
    /// A collection of information from a <see cref="PresenceProvider"/> used to create a presence.
    /// </summary>
    public class PresenceState
    {
        public PresenceState(string lineOne, string lineTwo)
        {
            OverrideText = (lineOne, lineTwo);
        }

        public PresenceState(Dictionary<string, string> fieldValues, Dictionary<string, ImageBundle> iconValues, ImageBundle imageValue)
        {
            FieldValues = fieldValues;
            IconValues = iconValues;
            ImageValue = imageValue;
        }

        public PresenceState() { }

        /// <summary>
        /// The values of each valid field in this provider.
        /// </summary>
        public Dictionary<string, string> FieldValues { get; } = new Dictionary<string, string>();
        
        /// <summary>
        /// The icon names and description for each possible icon customization.
        /// </summary>
        public Dictionary<string, ImageBundle> IconValues { get; } = new Dictionary<string, ImageBundle>();

        /// <summary>
        /// The image name and description to use for this provider.
        /// </summary>
        public ImageBundle ImageValue { get; set; }

        /// <summary>
        /// Override text to display instead of traditional values, for example if the player is in menus.
        /// </summary>
        public (string, string)? OverrideText { get; set; } = null;
    }
}
