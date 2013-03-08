using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazuziTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            do
            {
                Console.Clear();

                Console.WriteLine(game);

                var consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        game.CurrentPiece.DropOneStep();
                        break;

                    case ConsoleKey.Spacebar:
                        game.DropAllTheWay();
                        break;
                }

            } while (true);
        }
    }
}
