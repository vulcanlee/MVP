using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBusiness.Helpers
{
    public class ConsoleHelper
    {
        public void Output(string msg, ConsoleColor needForegroundColor, ConsoleColor needBackgroundColor)
        {
            Console.ForegroundColor = needForegroundColor;
            Console.BackgroundColor = needBackgroundColor;
            Console.Write(msg);
            Console.ResetColor();
        }

        public void GobackShow(string msg)
        {
            (_, var top) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, top);
            Console.Write(string.Concat(Enumerable.Repeat(" ", 100)));
            Console.SetCursorPosition(0, top);
            Console.Write(msg);
        }
    }
}
