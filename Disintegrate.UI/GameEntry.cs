using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate.UI
{
    public partial class GameEntry : UserControl
    {
        public GameEntry()
        {
            InitializeComponent();
        }

        private PresenceApp _app;

        public GameEntry(PresenceApp app) : this()
        {
            _app = app;
            Configurator = app.Configurator;
        }

        private Configuration.Configurator _configurator;
        public Configuration.Configurator Configurator
        {
            get => _configurator;
            set
            {
                _configurator = value;

                gameNameLabel.Text = _app.AppName ?? "???";

                if (_configurator == null)
                {
                    configureButton.Enabled = false;
                    configureButton.Text = "???";
                }
                else if (_configurator.IsConfigured())
                {
                    configureButton.Enabled = false;
                    configureButton.Text = "Configured";
                }
                else
                {
                    configureButton.Enabled = true;
                    configureButton.Text = "Configure";
                }
            }
        }

        public Button ConfigureButton => configureButton;
        public Button CustomizeButton => customizeButton;

        private void GameEntry_Load(object sender, EventArgs e)
        {
            if (_app == null)
            {
                wipLabel.Text = "???";
                return;
            }

            wipLabel.Text = _app.WorkInProgress
                ? "Work in progress"
                : "";

            logoPictureBox.Image = _app.Logo;
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
