using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disintegrate.Configuration;
using Disintegrate.Customization;

namespace Disintegrate.Apps
{
    public class HearthstoneApp : PresenceApp
    {
        public override string AppName => "Hearthstone";
        public override string AppId => "459027202449866782";
        public override string ProcessName => "Hearthstone";
        public override Image Logo => Properties.Resources.HearthstoneLogo;
        public override bool WorkInProgress => true;
        public override Customizer Customizer => new Customization.Customizers.HearthstoneCustomizer();

        public override Configurator Configurator => new Configuration.Configurators.HearthstoneConfiguator();

        public override PresenceProvider MakeProvider() => new Providers.HearthstonePresenceProvider(this);
    }
}
