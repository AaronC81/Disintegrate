using Disintegrate.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProvider
{
    public class ExampleConfigurator : Configurator
    {
        public override string AppName => "Example";

        public override List<string> Configure()
        {
            return new List<string>();
        }

        public override bool IsConfigured() => true;
    }
}
