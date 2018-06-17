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

        public GameEntry(Configuration.Configurator configurator) : this()
        {
            Configurator = configurator;
        }

        private Configuration.Configurator _configurator;
        public Configuration.Configurator Configurator
        {
            get => _configurator;
            set
            {
                _configurator = value;

                gameNameLabel.Text = _configurator.AppName;

                if (_configurator.IsConfigured())
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
    }
}
