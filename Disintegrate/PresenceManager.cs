using Disintegrate.Customization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Disintegrate
{
    /// <summary>
    /// Handles starting and stopping of <see cref="PresenceProvider"/> instances when processes
    /// start and stop.
    /// </summary>
    public static class PresenceManager
    {
        /// <summary>
        /// A function called to laod <see cref="Preferences"/> for providers.
        /// </summary>
        public static Func<PresenceApp, Preferences> PreferenceLoader { get; set; }

        private static Thread _watcher;
        private static (int pid, PresenceRelay relay)? _active = null;

        /// <summary>
        /// Maps process names to <see cref="PresenceProvider"/> types.
        /// </summary>
        public static Dictionary<string, PresenceApp> Apps { get; } = new Dictionary<string, PresenceApp>();

        /// <summary>
        /// Indexes a <see cref="PresenceApp"/>, allowing it to be launched automatically along
        /// with a process.
        /// </summary>
        public static void Index(PresenceApp app)
        {
            var processName = app.ProcessName;

            if (Apps.ContainsKey(processName))
            {
                throw new Exception($"Already indexed a provider for {processName}");
            }
            Apps[processName] = app;
        }

        /// <summary>
        /// Start listening for process changes.
        /// </summary>
        public static void Start()
        {
            _watcher = new Thread(() =>
            {
                while (true)
                {
                    var processes = Process.GetProcesses();

                    StopProviderIfProcessDied(processes);
                    StartProviderIfProcessAvailable(processes);

                    Thread.Sleep(5000);
                }
            });
            _watcher.Start();
        }

        /// <summary>
        /// Checks if the process which the provider is attached to has died. If so, the provider
        /// is stopped and <see cref="_active"/> is freed.
        /// </summary>
        public static void StopProviderIfProcessDied(Process[] processes)
        {
            // If there's no running provider, return
            if (_active == null) return;

            var processNames = processes.Select(p => p.ProcessName);
            var soughtName = _active.Value.relay.Provider.App.ProcessName;

            // If the name isn't found, stop the provider
            if (!processNames.Contains(soughtName))
            {
                _active.Value.relay.Stop();
                _active = null;
            }
        }

        public static void StartProviderIfProcessAvailable(Process[] processes)
        {
            // If there's already a running provider, return
            if (_active != null) return;

            foreach (var kv in Apps)
            {
                foreach (var process in processes)
                {
                    // If the provider has its sought process name, start the provider
                    if (kv.Key == process.ProcessName)
                    {
                        var newProvider = kv.Value.MakeProvider();

                        var newRelay = new PresenceRelay(newProvider);
                        newRelay.Start();

                        _active = (pid: process.Id, relay: newRelay);
                    }
                }
            }
        }
    }
}
