using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace terrariaPlayerEditor
{//ReLogic.Content.AssetRepository.FindSourceForAsset(string)
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Terraria Player Editor";
            //string filePath = "C:\\Users\\wkq\\Documents\\My Games\\Terraria\\Players\\tttt.plr";//Console.ReadLine();
            Main game = new Main();
            Utiles.loadResources();
            Console.WriteLine("Load complete. Press any key to continue.");


            Console.ReadKey();
            ScreenManager sm = new ScreenManager();
            SelectPlayerScreen selectPlayerScreen = new SelectPlayerScreen(sm);
            sm.newScreen(selectPlayerScreen);
        }

    }
}
