using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Configuration
{
    /// <summary>
    /// A base for classes which configure a game to work with its <see cref="PresenceProvider"/>.
    /// </summary>
    public abstract class Configurator
    {
        /// <summary>
        /// Determine whether the game is already configured.
        /// </summary>
        public abstract bool IsConfigured();

        /// <summary>
        /// Configure the game to work with its <see cref="PresenceProvider"/>, returning a list of
        /// altered files.
        /// </summary>
        public abstract List<string> Configure();

        /// <summary>
        /// The recognizable name of the app which the user is configuring.
        /// </summary>
        public abstract string AppName { get; }
    }
}
