using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disintegrate.Configuration;
using Disintegrate.Customization;

namespace Disintegrate.Apps
{
    public class Dota2App : PresenceApp
    {
        public override string AppName => "DOTA 2";
        public override string AppId => "457208839205289984";
        public override string ProcessName => "dota2";
        public override Customizer Customizer => new Customization.Customizers.Dota2Customizer();

        public override Configurator Configurator => new Configuration.Configurators.Dota2Configurator();

        public override PresenceProvider MakeProvider() => new Providers.Dota2PresenceProvider(this);
    }
}
