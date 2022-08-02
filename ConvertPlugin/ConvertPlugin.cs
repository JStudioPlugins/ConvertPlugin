using Rocket.API.Collections;
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

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "IncorrectGUID", "Someone didn't set the GUID for conversion to an ItemCurrencyAsset!" },
            { "CurrencyAmount", "You have {0} {1}" },
            { "CurrencyGrant", "Granted you {0} {1}" }
        };
    }
}
