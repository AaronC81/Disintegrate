using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disintegrate.Customization;

namespace Disintegrate
{
    /// <summary>
    /// Represents a rich presence application.
    /// </summary>
    public abstract class PresenceApp
    {
        public abstract PresenceProvider MakeProvider();

        protected PresenceFormatter _formatter;
        public PresenceFormatter GetFormatter()
        {
            if (_formatter == null)
            {
                _formatter = new PresenceFormatter(this);
            }
            return _formatter;
        }

        public abstract string AppName { get; }
        public abstract string AppId { get; }
        public abstract string ProcessName { get; }
        public abstract Image Logo { get; }
        public virtual bool WorkInProgress => false;

        public abstract Customizer Customizer { get; }

        public abstract Configuration.Configurator Configurator { get; }

        protected Preferences _cachedPreferences;
        public Preferences CachedPreferences => _cachedPreferences ?? LoadPreferences();

        public void ClearCachedPreferences()
        {
            _cachedPreferences = null;
        }

        public Preferences LoadPreferences()
        {
            _cachedPreferences = Loader.LoadPreferences(this);
            return _cachedPreferences;
        }
    }
}
