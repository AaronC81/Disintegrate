using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Disintegrate.Configuration.Configurators;
using Disintegrate.Configuration;
using System.Reflection;
using Force.DeepCloner;
using System.IO;

namespace Disintegrate.UI
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ReloadGames()
        {
            gameTableLayout.Controls.Clear();
            Menu_Load(this, null);
        }

        private void Menu_Load(object sender, EventArgs _)
        {
            gameTableLayout.Controls.Clear();

            var version = Assembly.GetEntryAssembly().GetName().Version;
            versionLabel.Text = $"{version}";

            int currentColumn = 1;
            gameTableLayout.ColumnCount = PresenceManager.Apps.Count;

            gameTableLayout.ColumnStyles.Clear();

            foreach (var kv in PresenceManager.Apps)
            {
                var app = kv.Value;

                var gameEntry = new GameEntry(app);
                gameEntry.ConfigureButton.Click += (s, e) =>
                {
                    Configure(app.Configurator);
                    ReloadGames();
                };
                gameEntry.CustomizeButton.Click += (s, e) =>
                {
                    var editor = new PreferencesEditor(app, app.CachedPreferences);
                    editor.ShowDialog();
                    Menu_Load(sender, _);
                };

                gameTableLayout.Controls.Add(gameEntry);
                gameTableLayout.SetColumn(gameEntry, currentColumn);

                gameTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100 / PresenceManager.Apps.Count));

                currentColumn++;
            }
        }

        private void Configure(Configurator configurator)
        {
            if (MessageBox.Show($@"This will alter game files.
If something goes wrong you may have to reinstall the game.
Continue?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var changedFiles = configurator.Configure();
                    MessageBox.Show($@"Successfully configured.
These files changed:
{string.Join("\n", changedFiles)}");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while configuring. A log has been written to your desktop.");
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/config-crash.log";
                    File.WriteAllText(path, e.Message + "\n" + e.StackTrace);
                }
            }
        }

        private void websiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://disint.cc/");
        }
    }
}
