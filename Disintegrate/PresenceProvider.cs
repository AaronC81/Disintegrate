using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate
{
    public delegate void StateReadyEventArgs(PresenceProvider sender, PresenceState presenceInfo);

    /// <summary>
    /// Gathers data for a presence to be passed to a <see cref="PresenceFormatter"/>.
    /// </summary>
    public abstract class PresenceProvider
    {
        public PresenceProvider(PresenceApp app)
        {
            App = app;
        }

        public PresenceApp App { get; }

        /// <summary>
        /// The frequency at which this provider will emit new states.
        /// </summary>
        public abstract StateFrequency StateFrequency { get; }

        /// <summary>
        /// Start listening for game events, and begin emitting states.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stop listening for game events, and stop emitting states.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// An event fired whenever this has a new state available.
        /// </summary>
        public event StateReadyEventArgs StateReady;

        /// <summary>
        /// Emits a new state.
        /// </summary>
        public void PushState(PresenceState presenceState) =>
            StateReady?.Invoke(this, presenceState);
    }
}
