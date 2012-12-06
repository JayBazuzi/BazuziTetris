using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new Tests();
            tests.Test1();
        }
    }

    class Tests
    {
        public void Test1()
        {
            Game game = new Game();
            game.OnTick();
            Debug.Assert(game.CurrentPiece.Location.X == 15);
        }
    }

    enum Orientation
    {
        North,
        East,
        South,
        West
    }

    class Game
    {
        public readonly Well Well = new Well();

        public Piece CurrentPiece;

        public Game()
        {
            this.CurrentPiece = new Piece.I();
            this.CurrentPiece.Orientation = Orientation.North;
            this.CurrentPiece.Location.X = 16;
            this.CurrentPiece.Location.Y = 5;
        }

        public void OnTick()
        {
            this.CurrentPiece.Location.X--;
        }
    }

    class Well
    {
        public Block[,] Blocks = new Block[10, 20];
    }

    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    class Piece
    {
        protected Piece() { }

        public Orientation Orientation;
        public Location Location;

        public class I : Piece
        {
        }
    }

    class Block
    {
    }
}
