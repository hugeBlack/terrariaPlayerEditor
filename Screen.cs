using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terrariaPlayerEditor
{
    public abstract class Screen
    {
        protected ScreenManager screenManager;
        public abstract void show();

        public bool isClosed = false;
        protected string readLine()
        {
            Console.Write(">> ");
            return Console.ReadLine();
        }
        public Screen(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
        }
    }
}
