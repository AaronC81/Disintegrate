using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate
{
    /// <summary>
    /// Relays states from a <see cref="PresenceProvider"/> to Discord through RPC.
    /// </summary>
    public class PresenceRelay
    {
        public PresenceRelay(PresenceProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// The provider to use.
        /// </summary>
        public PresenceProvider Provider { get; }

        private bool _stopped = false;
        
        /// <summary>
        /// Calls <see cref="PresenceProvider.Start"/> and broadcasts states to Discord.
        /// </summary>
        public void Start()
        {
            // We don't actually need to listen to any of these events
            var handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback += () => { };
            handlers.disconnectedCallback += (a, b) => { };
            handlers.errorCallback += (a, b) => { };

            DiscordRpc.Initialize(Provider.App.AppId, ref handlers, true, null);

            if (Provider.StateFrequency == StateFrequency.TimeControlled)
            {
                // Publish all states as soon as we have them
                Provider.StateReady += (o, p) =>
                {
                    if (_stopped) return;

                    FormatAndUpdate(p);
                };
            }
            else if (Provider.StateFrequency == StateFrequency.FastAsPossible)
            {
                // We'd only like to send an RPC state every 3 seconds

                long lastPublishTime = 0; // The Unix time (seconds) at which a state was last sent

                Provider.StateReady += (o, p) =>
                {
                    if (_stopped) return;

                    var timeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    // If it's been 3 seconds, send the new state
                    if (timeNow - lastPublishTime >= 3)
                    {
                        FormatAndUpdate(p);
                        lastPublishTime = timeNow;
                    }
                };
            }

            Provider.Start();
        }

        public void FormatAndUpdate(PresenceState state)
        {
            var formatter = Provider.App.GetFormatter();

            var info = formatter.StateToInfo(state);
            var rpcPresence = info.ToRpc();
            DiscordRpc.UpdatePresence(ref rpcPresence);
        }

        /// <summary>
        /// Calls <see cref="PresenceProvider.Stop"/> and stops broadcasting states to Discord.
        /// </summary>
        public void Stop()
        {
            Provider.Stop();

            _stopped = true;

            DiscordRpc.Shutdown();
        }
    }
}
