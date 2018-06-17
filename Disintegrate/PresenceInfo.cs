using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate
{
    /// <summary>
    /// A collection of Discord presence information.
    /// </summary>
    public class PresenceInfo
    {
        public PresenceInfo(string state, string detail)
        {
            State = state;
            Detail = detail;
        }

        /// <summary>
        /// This presence's 'State' field.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// This presence's 'Detail' field.
        /// </summary>
        public string Detail { get; set; }
        
        /// <summary>
        /// The key for the small image to display in this presence.
        /// </summary>
        public string SmallImageKey { get; set; }

        /// <summary>
        /// The hover text for the small image to display in this presence.
        /// </summary>
        public string SmallImageText { get; set; }

        /// <summary>
        /// The key for the large image to display in this presence.
        /// </summary>
        public string LargeImageKey { get; set; }

        /// <summary>
        /// The hover text for the small image to display in this presence.
        /// </summary>
        public string LargeImageText { get; set; }

        /// <summary>
        /// Converts this to a <see cref="DiscordRpc.RichPresence"/>.
        /// </summary>
        public DiscordRpc.RichPresence ToRpc() =>
            new DiscordRpc.RichPresence
            {
                state = State,
                details = Detail,
                smallImageKey = SmallImageKey,
                smallImageText = SmallImageText,
                largeImageKey = LargeImageKey,
                largeImageText = LargeImageText
            };
    }
}
