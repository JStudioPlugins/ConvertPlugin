using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertPlugin
{
    public class ConvertPlugin : RocketPlugin <ConvertPluginConfiguration>
    {
        public static ConvertPlugin Instance { get; set; }
        protected override void Load()
        {
            Instance = this;
        }

        protected override void Unload()
        {
            
        }
    }
}
