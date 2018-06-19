using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Customization
{
    /// <summary>
    /// Represents the methods in which a <see cref="PresenceProvider"/> may be customized.
    /// </summary>
    public class Customizer
    {
        public Customizer(List<string> icons, List<string> checkboxes, List<TextField> textFields)
        {
            Icons = icons;
            Checkboxes = checkboxes;
            TextFields = textFields;
        }

        /// <summary>
        /// The available icons preferences for the presence.
        /// </summary>
        public List<string> Icons { get; }

        /// <summary>
        /// The checkboxes available for the presence.
        /// </summary>
        public List<string> Checkboxes { get; }

        /// <summary>
        /// The text fields available for the presence.
        /// </summary>
        public List<TextField> TextFields { get; }

        /// <summary>
        /// The default preferences.
        /// </summary>
        public Preferences Default { get; set; }
    }
}
