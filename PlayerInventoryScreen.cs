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
    class PlayerInventoryScreen : Screen
    {
        public PlayerInventoryScreen(ScreenManager sm, Item[] inventory, string inventoryName, Dictionary<int,string> slotDict) : base(sm)
        {
            inventoryOrg = inventory;
            this.slotDict = slotDict;
            this.inventoryName = inventoryName;
        }
        Dictionary<int, string> slotDict;
        string inventoryName;
        public Item[] inventoryOrg;
        public Item[] inventory;
        bool isLoaded = false;
        public override void show()
        {
            if (!isLoaded)
            {
                copyInventory();
                isLoaded = true;
            }
            refresh();
            while (true && !isClosed)
            {
                string line = readLine();
                if (line == "q")
                {
                    screenManager.closeScreen();
                    return;
                }
                if(line.Split(' ')[0] == "m")
                {
                    int slotId;
                    try
                    {
                        slotId = int.Parse(line.Split(' ')[1]);
                        if (slotId < 1 || slotId > 59)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Slot Id. Try again.");
                        continue;
                    }
                    screenManager.newScreen(new ModifyItemScreen(screenManager, inventory[slotId - 1]));
                    return;
                }
                if (line == "s")
                {
                    for (int i = 0; i < inventoryOrg.Length; i++)
                    {
                        inventoryOrg[i] = inventory[i];
                    }
                    screenManager.closeScreen();
                    return;
                }
                if (line == "d")
                {
                    copyInventory();
                    refresh();
                    continue;
                }
                Console.WriteLine("Invalid command. Try again.");
            }

        }

        private void refresh()
        {
            Console.Clear();
            Console.WriteLine("Reading {0}.", inventoryName);
            string[][] tc = new string[inventory.Length + 1 + slotDict.Count][];
            ConsoleTableBuilder ctb = new ConsoleTableBuilder("  ");
            ctb.feedLine("Slot ID", "Item Name", "Prefix", "Count" );
            for (int i = 0; i < inventory.Length; i++)
            {
                Item item = inventory[i];
                if (slotDict.ContainsKey(i))
                {
                    ctb.feedLine(slotDict[i] + ": ");
                }
                ctb.feedLine((i + 1) + "", (string)Language.GetText(item.Name), (string)Language.GetText(Lang.prefix[item.prefix].Value), item.stack + "");
            }
            ctb.print();
            Console.WriteLine("m <Slot ID> : modify a slot.");
            Console.WriteLine("d : discard change.");
            Console.WriteLine("q : quit without saving.");
            Console.WriteLine("s : save and quit");
        }

        private void copyInventory()
        {
            this.inventory = new Item[inventoryOrg.Length];
            for (int i = 0; i < inventoryOrg.Length; i++)
            {
                inventory[i] = inventoryOrg[i].Clone();
            }
        }
    }
}
