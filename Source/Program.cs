using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

    public class Tests
    {
        [Fact]
        public void Test1()
        {
            Game game = new Game();
            game.OnTick();
            Assert.Equal(15, game.CurrentPiece.Location.X);
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
        public Piece CurrentPiece;

        public Game()
        {
            this.CurrentPiece = new Piece.I();
            this.CurrentPiece.Location.X = 16;
            this.CurrentPiece.Location.Y = 5;
        }

        public void OnTick()
        {
            this.CurrentPiece.Location.X--;
        }
    }

    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    class Piece
    {
        protected Piece() { }

        public Orientation Orientation = Orientation.North;
        public Location Location;

        public class I : Piece
        {
        }
    }
}
