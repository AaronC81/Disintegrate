using Disintegrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleProvider
{
    public class ExamplePresenceProvider : PresenceProvider
    {
        public override string ProcessName => "qalculate";
        public override string AppId => "457208839205289984";
        public override StateFrequency StateFrequency => StateFrequency.FastAsPossible;

        private Thread _t;

        public override void Start()
        {
            _t = new Thread(() =>
            {
                while (true)
                {
                    PushState(new PresenceInfo("HELLO", "HELLO"));
                    Thread.Sleep(500);
                }
            });
            _t.Start();
        }

        public override void Stop()
        {
            _t.Abort();
        }
    }
}
