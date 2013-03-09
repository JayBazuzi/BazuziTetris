using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BazuziTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.Clear();
            Console.WriteLine(game);

            var keyThread = new Thread(
                () =>
                {
                    do
                    {
                        var consoleKeyInfo = Console.ReadKey();

                        switch (consoleKeyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                game.CurrentPieceDropOneStep();
                                break;

                            case ConsoleKey.Spacebar:
                                game.DropAllTheWay();
                                break;

                            case ConsoleKey.UpArrow:
                                game.CurrentPieceRotate();
                                break;

                            case ConsoleKey.LeftArrow:
                                game.MoveLeft();
                                break;

                            case ConsoleKey.RightArrow:
                                game.MoveRight();
                                break;
                        }
                        Console.Clear();
                        Console.WriteLine(game);
                    } while (true);
                }
            );
            keyThread.Start();

            do
            {
                Thread.Sleep(1000);
                game.OnTick();
                Console.Clear();
                Console.WriteLine(game);

            } while (true);
        }
    }
}