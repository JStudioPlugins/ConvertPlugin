using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertPlugin
{
    public class ConvertPluginConfiguration : IRocketPluginConfiguration
    {
        public float ConvertRate { get; set; }
        public string CurrencyGUID { get; set; }

        public void LoadDefaults()
        {
            ConvertRate = 0.75f;
            CurrencyGUID = "1d525d3009bd44e9a70c46781cd458d3";
        }
    }
}
