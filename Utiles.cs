using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.Localization;

namespace terrariaPlayerEditor
{
    public class Utiles
    {
        public static string GetItemName(Item item)
        {
            if (item.prefix < 0 || item.prefix >= Lang.prefix.Length)
            {
                return (string)Language.GetText(item.Name);
            }

            string text = (string)Language.GetText(Lang.prefix[item.prefix].Value);
            if (text == "")
            {
                return (string)Language.GetText(item.Name);
            }

            if (text.StartsWith("("))
            {
                return (string)Language.GetText(item.Name) + " " + text;
            }

            return text + " " + Language.GetText(item.Name);
        }

        public static List<PlayerFileData> LoadPlayer()
        {
            if (Terraria.Main.player[Terraria.Main.myPlayer] == null)
            {
                Terraria.Main.player[Terraria.Main.myPlayer] = new Player();
            }
            Terraria.Main.LoadPlayers();
            return Terraria.Main.PlayerList;

        }
        public static void loadResources()
        {
            for (int index1 = 0; index1 < TextureAssets.Item.Length; ++index1)
            {
                TextureAssets.Item[index1] = Asset<Texture2D>.Empty;
                var type = typeof(Asset<Texture2D>);
                var field = type.GetProperty("State");
                field.SetValue(TextureAssets.Item[index1], AssetState.Loading, null);
            }
            LanguageManager.Instance.SetLanguage(Main.Configuration.Get<string>("Language", "en-US"));
            Lang.InitializeLegacyLocalization();
            Console.WriteLine("Loaded language: " + Main.Configuration.Get<string>("Language", "en-US"));
        }
    }
}
