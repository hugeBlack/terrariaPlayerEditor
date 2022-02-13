using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;
using Terraria.Localization;

namespace terrariaPlayerEditor
{
    public class SelectModeScreen : Screen
    {
        public override void show()
        {
            Console.WriteLine("Modifying Player'{0}'", playerFileData.Name);
            Console.WriteLine("1 - modify basic information and apperance");
            Console.WriteLine("2 - modify inventory, ammo slot and coin slot");
            Console.WriteLine("3 - modify equipment");
            Console.WriteLine("4 - modify pets, minecart, mount and hook slot");
            Console.WriteLine("5 - modify {0}", (string)Language.GetText(Lang.GetItemNameValue(87)));
            Console.WriteLine("6 - modify {0}", (string)Language.GetText(Lang.GetItemNameValue(346)));
            Console.WriteLine("7 - modify {0}", (string)Language.GetText(Lang.GetItemNameValue(3813)));
            Console.WriteLine("8 - modify {0}", (string)Language.GetText(Lang.GetItemNameValue(4076)));
            Console.WriteLine("9 - modify dye of equipment");
            Console.WriteLine("10 - modify dye of pets, minecart, mount and hook");
            Console.WriteLine("s - save player to the disk");
            Console.WriteLine("q - quit without saving");
            while (true && !isClosed)
            {
                string line = readLine();
                if(line == "q")
                {
                    screenManager.closeScreen();
                    return;
                }
                if(line == "s") { 
                    var type = typeof(Player);
                    var method = type.GetMethod("InternalSavePlayerFile" , BindingFlags.NonPublic| BindingFlags.Static);
                    object[] parammeter = { playerFileData };
                    method.Invoke(playerFileData.Player, parammeter);
                    screenManager.closeScreen();
                    return;
                }
                int id;
                try
                {
                    id = Convert.ToInt32(line);
                }
                catch
                {
                    Console.WriteLine("Invalid number. Try again.");
                    continue;
                }
                if (id == 1)
                {
                    screenManager.newScreen(new BasicInformationScreen(screenManager, playerFileData));
                    continue;
                }
                if (id == 2)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    dict.Add(0, "Inventory");
                    dict.Add(50, "Coin Slot");
                    dict.Add(54, "Ammo Slot");
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.inventory, "inventory", dict));
                    continue;
                }
                if (id == 3)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    dict.Add(0, "Armor");
                    dict.Add(3, "Accessories");
                    dict.Add(10, "Social Armor");
                    dict.Add(13, "Social Accessories");
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.armor, "equipments", dict));
                    continue;
                }
                if (id == 4)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    dict.Add(0, "Pet");
                    dict.Add(1, "Light pet");
                    dict.Add(2, "Minecart");
                    dict.Add(3, "Mount");
                    dict.Add(4, "Hook");
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.miscEquips, "pets, minecart, mount and hook", dict));
                    continue;
                }

                if (id == 5)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.bank.item, (string)Language.GetText(Lang.GetItemNameValue(87)), dict));
                    continue;
                }
                if (id == 6)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.bank2.item, (string)Language.GetText(Lang.GetItemNameValue(346)), dict));
                    continue;
                }
                if (id == 7)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.bank3.item, (string)Language.GetText(Lang.GetItemNameValue(3813)), dict));
                    continue;
                }
                if (id == 8)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.bank4.item, (string)Language.GetText(Lang.GetItemNameValue(4076)), dict));
                    continue;
                }
                if (id == 9)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    dict.Add(0, "Armor");
                    dict.Add(3, "Accessories");
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.dye, "dyes of equipment", dict));
                    continue;
                }
                if (id == 10)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    dict.Add(0, "Pet");
                    dict.Add(1, "Light pet");
                    dict.Add(2, "Minecart");
                    dict.Add(3, "Mount");
                    dict.Add(4, "Hook");
                    screenManager.newScreen(new PlayerInventoryScreen(screenManager, playerFileData.Player.miscDyes, "dyes of pets, minecart, mount and hook", dict));
                    continue;
                }
                Console.WriteLine("Invalid number. Try again.");
            }
        }
        PlayerFileData playerFileData;
        public SelectModeScreen(ScreenManager sm,PlayerFileData playerFileData) : base(sm)
        {
            this.playerFileData = playerFileData;

        }
    }
}
