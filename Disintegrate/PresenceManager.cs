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
        private static Thread _watcher;
        private static (int pid, PresenceRelay relay)? _active = null;

        /// <summary>
        /// Maps process names to <see cref="PresenceProvider"/> types.
        /// </summary>
        public static Dictionary<string, Type> Providers { get; } = new Dictionary<string, Type>();

        /// <summary>
        /// See <see cref="Index(Type)"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Index<T>() where T : PresenceProvider, new() =>
            Index(typeof(T));

        /// <summary>
        /// Indexes a <see cref="PresenceProvider"/>, allowing it to be launched automatically along
        /// with a process.
        /// </summary>
        /// <param name="t">The provider.</param>
        public static void Index(Type t)
        {
            var instance = (PresenceProvider)Activator.CreateInstance(t);
            var processName = instance.ProcessName;

            if (Providers.ContainsKey(processName))
            {
                throw new Exception($"Already indexed a provider for {processName}");
            }
            Providers[processName] = t;
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
            var soughtName = _active.Value.relay.Provider.ProcessName;

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

            foreach (var kv in Providers)
            {
                foreach (var process in processes)
                {
                    // If the provider has its sought process name, start the provider
                    if (kv.Key == process.ProcessName)
                    {
                        var newProvider = (PresenceProvider)Activator.CreateInstance(kv.Value);

                        var newRelay = new PresenceRelay(newProvider);
                        newRelay.Start();

                        _active = (pid: process.Id, relay: newRelay);
                    }
                }
            }
        }
    }
}
