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
        }
    }

    public class Tests
    {
        [Fact]
        public void WellShouldStartEmpty()
        {
            Game game = new Game();
            foreach (var item in game.Well.Cells)
            {
                Assert.False(item);
            }
        }

        [Fact]
        public void PieceShouldDropOneOnTick()
        {
            Game game = new Game();
            game.OnTick();
            Assert.Equal(15, game.CurrentPiece.Location.X);
        }
    }

    public class RotationTests
    {
        [Fact]
        public void OriginalOrientationShouldBeNorth()
        {
            Game game = new Game();
            Assert.Equal(Orientation.North, game.CurrentPiece.Orientation);
        }

        [Fact]
        public void OneRotateShouldBeEast()
        {
            Game game = new Game();
            game.CurrentPiece.Rotate();
            Assert.Equal(Orientation.East, game.CurrentPiece.Orientation);
        }

        [Fact]
        public void FourRotateShouldBeNorthAgain()
        {
            Game game = new Game();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            Assert.Equal(Orientation.North, game.CurrentPiece.Orientation);
        }
    }

    enum Orientation
    {
        North,
        East,
        South,
        West,

        Count
    }

    class Well
    {
        public bool[,] Cells = new bool[10, 20];
    }

    class Game
    {
        public Piece CurrentPiece;

        public readonly Well Well = new Well();

        public Game()
        {
            this.CurrentPiece = new Piece.I();
            this.CurrentPiece.Location.X = 16;
            this.CurrentPiece.Location.Y = 5;
        }

        public void OnTick()
        {
            this.CurrentPiece.DropOneStep();
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

        internal void DropOneStep()
        {
            this.Location.X--;
        }

        internal void Rotate()
        {
            this.Orientation++;
            if (this.Orientation == Orientation.Count) this.Orientation = 0;
        }
    }
}
