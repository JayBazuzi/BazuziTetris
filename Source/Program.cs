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
        public void DropIIntoWell()
        {
            Game game = new Game();
            game.DropAllTheWay();

            for (int x = 0; x < game.Well.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < game.Well.Cells.GetLength(1); y++)
                {
                    if (x < 4 && y == 5)
                    {
                        Assert.True(game.Well.Cells[x, y]);
                    }

                    else
                    {
                        Assert.False(game.Well.Cells[x, y]);
                    }
                }

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
            this.CurrentPiece = new Piece.I(this.Well.Cells.GetLength(0), this.Well.Cells.GetLength(1) / 2);
            this.CurrentPiece.Location.X = 16;
            this.CurrentPiece.Location.Y = 5;
        }

        public void OnTick()
        {
            if (this.CurrentPiece.Location.X == 0)
            {
                this.CurrentPiece.TransferToWell(this.Well);
            }

            else
            {
                this.CurrentPiece.DropOneStep();
            }
        }

        internal void DropAllTheWay()
        {
            while (this.CurrentPiece.Location.X > 0)
            {
                this.CurrentPiece.DropOneStep();
            }

            this.CurrentPiece.TransferToWell(this.Well);
        }
    }

    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    abstract class Piece
    {
        protected Piece(Location location)
        {
            this.Location = location;
        }

        public Orientation Orientation = Orientation.North;
        public Location Location;

        public class I : Piece
        {
            public I(int x, int y)
                : base(new Location(x, y))
            {

            }
            public override void TransferToWell(Well well)
            {
                well.Cells[this.Location.X + 0, this.Location.Y] = true;
                well.Cells[this.Location.X + 1, this.Location.Y] = true;
                well.Cells[this.Location.X + 2, this.Location.Y] = true;
                well.Cells[this.Location.X + 3, this.Location.Y] = true;
            }
        }

        public void DropOneStep()
        {
            this.Location.X--;
        }

        public void Rotate()
        {
            this.Orientation++;
            if (this.Orientation == Orientation.Count) this.Orientation = 0;
        }

        public abstract void TransferToWell(Well well);
    }
}
