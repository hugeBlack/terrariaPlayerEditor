using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terrariaPlayerEditor
{
    public class ScreenManager
    {
        LinkedList<Screen> screens = new LinkedList<Screen>();
        public void newScreen(Screen screen)
        {
            Console.Clear();
            screens.AddLast(screen);
            screen.show();
            Console.WriteLine();
        }
        public void closeScreen()
        {
            
            Console.Clear();
            screens.Last.Value.isClosed = true;
            screens.RemoveLast();
            if(screens.Count > 0)
            {
                screens.Last.Value.show();
            }
        }
    }
}
