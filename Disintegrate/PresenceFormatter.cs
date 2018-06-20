using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate
{
    /// <summary>
    /// Takes <see cref="PresenceState"/>s and produces <see cref="PresenceInfo"/>s which can be
    /// sent to Discord.
    /// </summary>
    public class PresenceFormatter
    {
        public PresenceFormatter(PresenceApp app)
        {
            App = app;
        }

        /// <summary>
        /// The app which this formatter serves.
        /// </summary>
        public PresenceApp App { get; set; }

        /// <summary>
        /// Converts <see cref="PresenceState"/> to a <see cref="PresenceInfo"/> based on the user's
        /// customization configuration.
        /// </summary>
        public PresenceInfo StateToInfo(PresenceState state)
        {
            var info = new PresenceInfo();

            var preferences = App.CachedPreferences;

            if (state.OverrideText != null)
            {
                (info.Detail, info.State) = state.OverrideText.Value;
            }
            else
            {
                (info.Detail, info.State) = preferences.FillFieldsByFunction(key => state.FieldValues[key]);
                (info.SmallImageKey, info.SmallImageText) = state.IconValues[preferences.Icon];
            }
            (info.LargeImageKey, info.LargeImageText) = state.ImageValue;

            return info;
        }
    }
}
