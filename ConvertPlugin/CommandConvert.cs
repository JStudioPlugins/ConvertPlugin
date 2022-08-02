using Rocket.API;
using Rocket.Core.Assets;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConvertPlugin
{
    public class CommandConvert : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "convert";

        public string Help => "Converts your experience to currency.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "j.convert" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            uint amount = 0;
            if (ConvertPlugin.Instance.Configuration.Instance.ConvertRate < 1)
            {
                float f = (float)player.Experience * ConvertPlugin.Instance.Configuration.Instance.ConvertRate;
                amount = (uint)f;
            }
            else
            {
                float f = (float)player.Experience / ConvertPlugin.Instance.Configuration.Instance.ConvertRate;
                amount = (uint)f;
            }
            Asset asset = Assets.find(Guid.Parse(ConvertPlugin.Instance.Configuration.Instance.CurrencyGUID));
            switch (asset)
            {
                case ItemAsset _:
                    UnturnedChat.Say(player, ConvertPlugin.Instance.Translate("IncorrectGUID"), Color.red);
                    break;
                case ItemCurrencyAsset itemCurrencyAsset:
                    itemCurrencyAsset.grantValue(player.Player, amount);
                    //UnturnedChat.Say(player, $"Granted you {amount} {itemCurrencyAsset.valueFormat.Replace("{0:N0} ", "")}");
                    UnturnedChat.Say(player, ConvertPlugin.Instance.Translate("CurrencyGrant", amount, itemCurrencyAsset.valueFormat.Replace("{0:N0} ", "")));
                    player.Experience = 0;
                    break;
            }
        }
    }
}
