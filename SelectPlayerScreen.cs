using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;

namespace terrariaPlayerEditor
{
    class SelectPlayerScreen : Screen
    {
        List<PlayerFileData> ps;
        public SelectPlayerScreen(ScreenManager screenManager) : base(screenManager)
        {
            ps = Utiles.LoadPlayer();
        }
        public override void show()
        {
            refresh();
            while (!isClosed)
            {
                string line = readLine();
                if (line == "q")
                {
                    screenManager.closeScreen();
                    return;
                }
                if(line == "r")
                {
                    ps = Utiles.LoadPlayer();
                    continue;
                }
                try
                {
                    if(int.Parse(line)<1 || int.Parse(line) > ps.Count)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid index. Try again.");
                    continue;
                }
                PlayerFileData playerFileData = ps[int.Parse(line) - 1];
                screenManager.newScreen(new SelectModeScreen(screenManager, playerFileData));
                break;
            }
        }

        private void refresh()
        {
            Console.Clear();
            Console.WriteLine("Please choose a player. Enter index to continue.");
            ConsoleTableBuilder ctb = new ConsoleTableBuilder("  ");
            ctb.feedLine("Index", "Player Name", "File Name");
            for (int i = 0; i < ps.Count; i++)
            {
                ctb.feedLine("" + (i + 1), ps[i].Player.name, ps[i].GetFileName());
            }
            ctb.print();
            Console.WriteLine("q - quit editor");
            Console.WriteLine("r - reload players");
        }
    }
}
