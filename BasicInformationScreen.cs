using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;

namespace terrariaPlayerEditor
{
    internal class BasicInformationScreen : Screen
    {
        public BasicInformationScreen(ScreenManager screenManager, PlayerFileData playerFileData) : base(screenManager)
        {
            this.playerFileData = playerFileData;
            this.player = playerFileData.Player;
            reset();
        }
        PlayerFileData playerFileData;
        Player player;
        bool hasExtraAccessory;
        Dictionary<string,long> infoDict;

        private void reset()
        {
            infoDict = new Dictionary<string, long>();
            infoDict.Add("mh", player.statLifeMax);
            infoDict.Add("health", player.statLife);
            infoDict.Add("mana", player.statMana);
            infoDict.Add("mm", player.statManaMax);
            infoDict.Add("mode", player.difficulty);
            infoDict.Add("hs", player.hair);
            infoDict.Add("cloth", player.skinVariant);
            infoDict.Add("fqc", player.anglerQuestsFinished);
            hasExtraAccessory = player.extraAccessory;
            infoDict.Add("bq", player.bartenderQuestLog);
            infoDict.Add("gs", player.golferScoreAccumulated);
            infoDict.Add("pt", playerFileData.GetPlayTime().Ticks);
        }

        public override void show()
        {
            refresh();
            while (!isClosed)
            {
                string line = readLine();
                if(line == "q")
                {
                    screenManager.closeScreen();
                    return;
                }
                if(line == "d")
                {
                    reset();
                    refresh();
                    continue;
                }
                if(line == "s")
                {
                    player.statLifeMax = (int)infoDict["mh"];
                    player.statLife = (int)infoDict["health"];
                    player.statMana = (int)infoDict["mana"];
                    player.statManaMax = (int)infoDict["mm"];
                    player.difficulty = (byte) infoDict["mode"];
                    player.hair = (int)infoDict["hs"];
                    player.skinVariant = (int)infoDict["cloth"];
                    player.anglerQuestsFinished = (int)infoDict["fqc"];
                    player.extraAccessory = hasExtraAccessory;
                    player.bartenderQuestLog = (int)infoDict["bq"];
                    player.golferScoreAccumulated = (int)infoDict["gs"];
                    playerFileData.SetPlayTime(new TimeSpan(infoDict["pt"]));
                    screenManager.closeScreen();
                    return;
                }
                string[] args = line.Split(' ');
                if(line == "hea")
                {
                    if(args[1] == "true")
                    {
                        hasExtraAccessory = true;
                        refresh();
                    }
                    else if (args[1] == "false")
                    {
                        hasExtraAccessory = false;
                        refresh();
                    }
                    else
                    {
                        Console.WriteLine("Invalid boolean. Use true or false.");
                    }
                    continue;
                }
                if (infoDict.ContainsKey(args[0]))
                {
                    long num1;
                    try
                    {
                        num1 = long.Parse(args[1]);
                        if(num1 < 0)
                        {
                            throw new Exception();
                        }
                        infoDict[args[0]] = num1;
                        refresh();
                    }
                    catch
                    {
                        Console.WriteLine("Invalid number.");
                    }
                    continue;
                }
                Console.WriteLine("Invalid command. Try again.");
            }
        }
        public void refresh()
        {
            Console.Clear();
            Console.WriteLine("Modifying Player'{0}'", playerFileData.Name);
            string[] str = { "Softcore", "Mediumcore", "Hardcore", "Jounery" };
            Console.WriteLine("mode <Int>       - modify difficulty. now: {0}:{1}", infoDict["mode"], str[infoDict["mode"]]);
            Console.WriteLine("                   Difficulty: 0:Softcore, 1:Mediumcore, 2:Hardcore, 3:Jounery, ");
            Console.WriteLine("health <Int>     - modify current health. now: {0}", infoDict["health"]);
            Console.WriteLine("mh <Int>         - modify difficulty. now: {0}", infoDict["mh"]);
            Console.WriteLine("nama <Int>       - modify current mana. now: {0}", infoDict["mana"]);
            Console.WriteLine("mm <Int>         - modify max mana. now: {0}", infoDict["mm"]);
            Console.WriteLine("hs <Int>         - modify hair style. now: {0}", infoDict["hs"]);
            Console.WriteLine("cloth <Int>      - modify cloth style. now: {0}", infoDict["cloth"]);
            Console.WriteLine("fqc <Int>        - modify fishing cuest complete count. now: {0}", infoDict["fqc"]);
            Console.WriteLine("hea <true|false> - modify wether the player has extra accessory slots. now: {0}", hasExtraAccessory);
            Console.WriteLine("bq <Int>         - modify bartender quest complete count. now: {0}", infoDict["bq"]);
            Console.WriteLine("gs <Int>         - modify accumulated golfer score. now: {0}", infoDict["gs"]);
            Console.WriteLine("pt <Int>         - modify played time (seconds*10^7). now: {0}", infoDict["pt"]);
            Console.WriteLine("s - save and quit");
            Console.WriteLine("q - quit without saving");
            Console.WriteLine("d - discard changes");
        }
    }
}
