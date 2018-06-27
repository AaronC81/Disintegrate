using Disintegrate.Configuration;
using Disintegrate.Customization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disintegrate.Apps
{
    public class GlobalOffensiveApp : PresenceApp
    {
        public override string AppName => "CS:GO";
        public override string AppId => "457492341318746113";
        public override string ProcessName => "csgo";
        public override Image Logo => Properties.Resources.GlobalOffensiveLogo;
        public override Customizer Customizer => new Customization.Customizers.GlobalOffensiveCustomizer();

        public override Configurator Configurator => new Configuration.Configurators.GlobalOffensiveConfigurator();

        public override PresenceProvider MakeProvider() => new Providers.GlobalOffensivePresenceProvider(this);
    }
}
