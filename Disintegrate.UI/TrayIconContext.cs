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
        private NotifyIcon _notifyIcon;

        public TrayIconContext(bool launchNow = false) {
            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.Icon,
                Visible = true,
                Text = "Disintegrate",
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Open", (s, e) => {
                        (new Menu()).Show();
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
                (new Menu()).Show();
            }
        }
    }
}
