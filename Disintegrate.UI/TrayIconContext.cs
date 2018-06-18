using Disintegrate.UI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate.UI
{
    public class TrayIconContext : ApplicationContext
    {
        // TODO: Actually show errors
        private NotifyIcon _notifyIcon;
        private List<LoadedProvider> _loadedProviders;
        private List<LoadError> _errors;

        public TrayIconContext(List<LoadedProvider> loadedProviders, List<LoadError> errors, bool launchNow = false) {
            _loadedProviders = loadedProviders;

            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.Icon,
                Visible = true,
                Text = "Disintegrate",
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Open", (s, e) => {
                        ShowMenu();
                    }),
                    new MenuItem("Exit", (s, e) => {
                        _notifyIcon.Visible = false;
                        Application.Exit();
                        Environment.Exit(0);
                    })
                })
            };

            if (launchNow)
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            var configurators = _loadedProviders.Select(p => p.Configurator).ToList();
            var instantiatedConfigurators = configurators
                .Select(c => (Configuration.Configurator)Activator.CreateInstance(c))
                .ToList();

            (new Menu(instantiatedConfigurators)).Show();
        }
    }
}
