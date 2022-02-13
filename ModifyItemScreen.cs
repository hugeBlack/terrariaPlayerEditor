using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace terrariaPlayerEditor
{
    class ModifyItemScreen : Screen
    {
        Item item;
        Item itemOrg;
        public ModifyItemScreen(ScreenManager sm,Item item) : base(sm)
        {
            itemOrg = item;
            this.item = itemOrg.Clone();
        }
        public override void show()
        {
            refresh();
            while (true && !isClosed)
            {
                string line = readLine();
                string[] args = line.Split(' ');
                if (line == "q")
                {
                    screenManager.closeScreen();
                    return;
                }
                if (line == "s")
                {
                    itemOrg.stack = item.stack;
                    itemOrg.prefix = item.prefix;
                    itemOrg.netID = item.netID;
                    itemOrg.type = item.type;
                    itemOrg.prefix = item.prefix;
                    itemOrg.Refresh();
                    screenManager.closeScreen();
                    return;
                }
                if (args[0] == "id")
                {
                    int id;
                    try
                    {
                        id = int.Parse(args[1]);
                        if (id < 0 || id > ItemID.Count - 1)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid ID. Try again.");
                        continue;
                    }
                    if (item.netID == 0)
                    {
                        item.stack = 1;
                    }
                    item.type = id;
                    item.netID = id;
                    item.Refresh();
                    refresh();
                    continue;
                }
                if (args[0] == "si")
                {
                    int id;
                    try
                    {
                        id = int.Parse(args[1]);
                        if (id < 0 || id > ItemID.Count - 1)
                        {
                            throw new Exception();
                        }
                        Console.WriteLine(Language.GetText(Lang.GetItemNameValue(id)));
                    }
                    catch
                    {
                        Console.WriteLine("Invalid ID. Try again.");
                        continue;
                    }
                    continue;
                }
                if (args[0] == "pre")
                {
                    int id;
                    try
                    {
                        id = int.Parse(args[1]);
                        if (id < 0 || id > Lang.prefix.Length -1 )
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid ID. Try again.");
                        continue;
                    }
                    item.prefix = (byte)id;
                    item.Refresh();

                    refresh();
                    continue;
                }
                if (args[0] == "stack")
                {
                    int count;
                    try
                    {
                        count = int.Parse(args[1]);
                        if (count < 0)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid count. Try again.");
                        continue;
                    }
                    item.stack = count;
                    refresh();
                    continue;
                }
                if(args[0] == "pi")
                {
                    int pre;
                    try
                    {
                        pre = int.Parse(args[1]);
                        if (pre < 0 || pre > Lang.prefix.Length - 1)
                        {
                            throw new Exception();
                        }
                        Console.WriteLine(Language.GetText(Lang.prefix[pre].Value));
                    }
                    catch
                    {
                        for(int i =0;i< Lang.prefix.Length; i++)
                        {
                            Console.WriteLine(i+"\t"+Language.GetText(Lang.prefix[i].Value));
                        }
                        
                    }
                    continue;
                }
                if (args[0] == "d")
                {
                    this.item = itemOrg.Clone();
                    refresh();
                }
                if (args[0] == "sn")
                {
                    searchItem(args[1].ToLower());
                    continue;
                }
                Console.WriteLine("Invalid command. Try again.");
            }
        }

        private void refresh()
        {
            Console.Clear();
            Console.WriteLine("Modifying Item : {0} ×{1}", Utiles.GetItemName(item), item.stack);
            
            ConsoleTableBuilder ctb = new ConsoleTableBuilder("  ");
            ctb.feedLine("Item Id", item.netID + "");
            ctb.feedLine("Prefix Id" , item.prefix + "");
            if (item.damage > 0)
            {
                ctb.feedLine("Damage" , item.damage+"");
            }
            if(item.knockBack > 0)
            {
                ctb.feedLine("Knockback" , item.knockBack+"");
            }
            if(item.mana > 0)
            {
                ctb.feedLine("Mana" , item.mana+"");
            }
            if(item.defense >0)
            {
                ctb.feedLine("Defense" , item.defense + "");
            }
            if(item.crit > 0)
            {
                ctb.feedLine("Critic" , item.crit + "");
            }
            if(item.shootSpeed > 0)
            {
                ctb.feedLine("Shoot Speed" , item.shootSpeed+"");
            }
            if(item.scale != 1)
            {
                ctb.feedLine("Scale" , item.scale+"");
            }
            if(item.ToolTip != null && item.ToolTip.Lines > 0)
            {
                ctb.feedLine("Tooltip", item.ToolTip.GetLine(0));
            }
            item.RebuildTooltip();
            for (int i = 1;i< item.ToolTip.Lines; i++)
            {
                ctb.feedLine("", item.ToolTip.GetLine(i));
            }
            ctb.print();
            Console.WriteLine("id <ID> - change id");
            Console.WriteLine("si <Name> - get name from id");
            Console.WriteLine("sn - search id from name");
            Console.WriteLine("pre <ID> - change prefix");
            Console.WriteLine("pi [ID] - display prefix name or all prefixes");
            Console.WriteLine("stack - change stack count");
            Console.WriteLine("d - discard change");
            Console.WriteLine("s - save and quit");
            Console.WriteLine("q - quit without saving");
        }

        private void searchItem(string str)
        {
            for(int i = 0;i< ItemID.Count - 1; i++)
            {
                if (((string)Language.GetText(Lang.GetItemNameValue(i))).ToLower().IndexOf(str) > -1)
                {
                    Console.WriteLine(i+"\t"+ Language.GetText(Lang.GetItemNameValue(i)));
                }
                
            }
        }
    }
}
