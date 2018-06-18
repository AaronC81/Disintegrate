using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Disintegrate.Configuration;

namespace Disintegrate.UI
{
    public partial class Menu : Form
    {
        private List<Configurator> _configurators = new List<Configurator>();

        public Menu()
        {
            InitializeComponent();
        }

        public Menu(List<Configurator> configurators) : this()
        {
            _configurators.AddRange(configurators);
        }

        private void ReloadGames()
        {
            gameTableLayout.Controls.Clear();
            Menu_Load(this, null);
        }

        private void Menu_Load(object sender, EventArgs _)
        {
            versionLabel.Text = $"Version {Info.Version}";

            foreach (var configurator in _configurators)
            {
                var gameEntry = new GameEntry(configurator);
                gameEntry.ConfigureButton.Click += (s, e) =>
                {
                    Configure(configurator);
                    ReloadGames();
                };
                gameTableLayout.Controls.Add(gameEntry);
            }

            foreach (RowStyle style in gameTableLayout.RowStyles)
            {
                style.SizeType = SizeType.AutoSize;
            }
        }

        private void Configure(Configurator configurator)
        {
            var name = configurator.AppName;

            if (MessageBox.Show($@"This will alter game files for {name}.
If something goes wrong you may have to reinstall the game.
Continue?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var changedFiles = configurator.Configure();
                    MessageBox.Show($@"Successfully configured {name}.
These files changed:
{string.Join("\n", changedFiles)}");
                }
                catch
                {
                    // TODO: More detail/crash log
                    MessageBox.Show("Something went wrong while configuring {name}.");
                }
            }
        }

        private void websiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://disint.cc/");
        }
    }
}
