using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Disintegrate
{
    public delegate void StateReadyEventArgs(PresenceProvider sender, PresenceInfo presenceInfo);

    /// <summary>
    /// A base for classes which may provide Discord rich presence information while a particular program is running.
    /// </summary>
    public abstract class PresenceProvider
    {
        /// <summary>
        /// The name of the process during which this provider is active. The ending '.exe' is not needed.
        /// </summary>
        public abstract string ProcessName { get; }

        /// <summary>
        /// The app ID to use when sending states to the Discord RPC.
        /// </summary>
        public abstract string AppId { get; }

        /// <summary>
        /// The configurator associated with this provider.
        /// </summary>
        public abstract Configuration.Configurator Configurator { get; }

        /// <summary>
        /// The customizer associated with this provider.
        /// </summary>
        public abstract Customization.Customizer Customizer { get; }

        /// <summary>
        /// The frequency at which states are emitted via <see cref="StateReady"/>.
        /// </summary>
        public abstract StateFrequency StateFrequency { get; }

        /// <summary>
        /// An event fired whenever this has a new state available.
        /// </summary>
        public event StateReadyEventArgs StateReady;

        /// <summary>
        /// Start listening for game events, and begin emitting states.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stop listening for game events, and stop emitting states.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Emits a new state.
        /// </summary>
        public void PushState(PresenceInfo presenceInfo) =>
            StateReady?.Invoke(this, presenceInfo);
    }
}
