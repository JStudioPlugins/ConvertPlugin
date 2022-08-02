using Rocket.API;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SDG.Unturned.ItemCurrencyAsset;

namespace ConvertPlugin
{
    class CommandCalculate : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "calculate";

        public string Help => "Caluates the amount of currency you have on you.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "j.calculate" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (Assets.find(Guid.Parse(ConvertPlugin.Instance.Configuration.Instance.CurrencyGUID)) is ItemCurrencyAsset asset)
            {
                uint amount = 0;
                List<Guid> CurrencyGUIDs = new List<Guid>();
                List<uint> CurrencyValues = new List<uint>();
                foreach (Entry entry in asset.entries)
                {
                    CurrencyGUIDs.Add(entry.item.GUID);
                    CurrencyValues.Add(entry.value);
                }
                for (byte page = 0; page < PlayerInventory.PAGES - 2; page++)
                {
                    for (byte index = 0; index < player.Player.inventory.getItemCount(page); index++)
                    {
                        var jar = player.Player.inventory.getItem(page, index);
                        if (jar == null)
                            continue;
                        ItemAsset item = (ItemAsset)Assets.find(EAssetType.ITEM, jar.item.id);
                        if (CurrencyGUIDs.Contains(item.GUID))
                        {
                            int ListIndex = CurrencyGUIDs.FindIndex(x => x == item.GUID);
                            uint NewAmount = amount + CurrencyValues[ListIndex];
                            amount = NewAmount;
                        }
                    }
                }
                //UnturnedChat.Say(player, $"You have {amount} {asset.valueFormat.Replace("{0:N0} ", "")}");
                UnturnedChat.Say(player, ConvertPlugin.Instance.Translate("CurrencyAmount", amount, asset.valueFormat.Replace("{0:N0} ", "")));
            }
            else
            {
                UnturnedChat.Say(player, ConvertPlugin.Instance.Translate("IncorrectGUID"), Color.red);
            }
        }
    }
}
