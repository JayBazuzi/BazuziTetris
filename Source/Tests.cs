﻿// Copyright (c) 2013 Jay Bazuzi (JAY@BAZUZI.COM)

#region MIT LICENSE
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BazuziTetris
{
    public class Tests
    {
        [Fact]
        public void WellShouldStartEmpty()
        {
            Game game = new Game();
            foreach (var x in game.Well.HorizontalRange)
                foreach (var y in game.Well.VerticalRange)
                    Assert.False(game.Well[x, y]);
        }

        [Fact]
        public void DropIIntoWell()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            game.DropAllTheWay();

            foreach (int x in game.Well.HorizontalRange)
            {
                foreach (int y in game.Well.VerticalRange)
                {
                    if (x == 5 && y < 4)
                    {
                        Assert.True(game.Well[x, y], string.Format("x = {0}, y = {1}, \r\n\r\n{2}", x, y, game.Well.ToString()));
                    }

                    else
                    {
                        Assert.False(game.Well[x, y], game.Well.ToString());
                    }
                }
            }
        }

        [Fact]
        public void FullRowDisappears()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            game.DropAllTheWay();

            game.MoveLeft();
            game.DropAllTheWay();

            game.CurrentPieceRotate();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.DropAllTheWay();

            game.CurrentPieceRotate();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();

            Assert.True(game.Well[9, 0], game.ToString());

            game.DropAllTheWay();

            Assert.False(game.Well[9, 0], game.ToString());
        }

        [Fact]
        public void DropGivesNewPiece()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            Assert.Equal(16, game.CurrentPieceLocation.Y);
            Assert.Equal(5, game.CurrentPieceLocation.X);

            game.DropAllTheWay();

            Assert.Equal(16, game.CurrentPieceLocation.Y);
            Assert.Equal(5, game.CurrentPieceLocation.X);
        }

        [Fact]
        public void SlowDropGivesNewPiece()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            Assert.Equal(16, game.CurrentPieceLocation.Y);
            Assert.Equal(5, game.CurrentPieceLocation.X);

            foreach (var i in Enumerable.Range(0, 17)) game.CurrentPieceDropOneStep();

            Assert.Equal(16, game.CurrentPieceLocation.Y);
            Assert.Equal(5, game.CurrentPieceLocation.X);
        }

        [Fact]
        public void MoveLeft()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            Assert.Equal(5, game.CurrentPieceLocation.X);
            game.MoveLeft();
            Assert.Equal(4, game.CurrentPieceLocation.X);
        }

        [Fact]
        public void PieceShouldDropOneOnTick()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            Assert.Equal(16, game.CurrentPieceLocation.Y);
            game.OnTick();
            Assert.Equal(15, game.CurrentPieceLocation.Y);
        }

        [Fact]
        public void RotateIPieceTest()
        {
            var piece = new Piece.I();

            Assert.Equal(1, piece.Bitmap.Width);
            Assert.Equal(4, piece.Bitmap.Height);
            foreach (var x in piece.Bitmap.HorizontalRange)
                foreach (var y in piece.Bitmap.VerticalRange)
                    Assert.True(piece.Bitmap[x, y]);

            piece.Rotate();

            Assert.Equal(1, piece.Bitmap.Height);
            Assert.Equal(4, piece.Bitmap.Width);
            foreach (var x in piece.Bitmap.HorizontalRange)
                foreach (var y in piece.Bitmap.VerticalRange)
                    Assert.True(piece.Bitmap[x, y]);
        }

        [Fact]
        public void BitmapUnionTest()
        {
            var bitmap1 = new Bitmap(new bool[3, 3]
                {
                    {true,  false,  false},
                    {false, true,   false},
                    {false, false,  false},
                });

            var bitmap2 = new Bitmap(new bool[2, 2]
                {
                    {true,  true},
                    {false, false},
                });

            var resultBitmap = bitmap1.Union(bitmap2, new Location(1, 1));

            var expectedBitmap = new Bitmap(new bool[3, 3]
                {
                    {true,  false,  false},
                    {false, true,   true},
                    {false, false,  false},
                });

            Assert.Equal(expectedBitmap, resultBitmap, new BitmapComparer());
        }

        [Fact]
        public void LandOnAnotherPieceTest()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            game.DropAllTheWay();
            game.DropAllTheWay();

            foreach (int x in game.Well.HorizontalRange)
            {
                foreach (int y in game.Well.VerticalRange)
                {
                    if (x == 5 && y < 8)
                    {
                        Assert.True(game.Well[x, y], string.Format("x = {0}, y = {1}, \r\n\r\n{2}", x, y, game.Well.ToString()));
                    }

                    else
                    {
                        Assert.False(game.Well[x, y], game.Well.ToString());
                    }
                }
            }
        }

        [Fact]
        public void RotateCollisionTest()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            game.DropAllTheWay();
            Assert.True(game.CurrentPieceRotate());
            game.CurrentPieceRotate();
            game.MoveLeft();
            foreach (var i in Enumerable.Range(0, 14)) game.CurrentPieceDropOneStep();
            Assert.False(game.CurrentPieceRotate());
        }

        [Fact]
        public void RightToEdgeCollisionTest()
        {
            Game game = new Game();
            game.DropAllTheWay();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            Assert.False(game.MoveRight());
        }

        [Fact]
        public void RightPieceCollisionTest()
        {
            Game game = new Game(new Piece[] { new Piece.I(), new Piece.I(), new Piece.I(), new Piece.I() });
            game.DropAllTheWay();
            game.MoveLeft();
            foreach (var i in Enumerable.Range(0, 14)) game.CurrentPieceDropOneStep();
            Assert.False(game.MoveRight());
        }

        class BitmapComparer : IEqualityComparer<Bitmap>
        {
            bool IEqualityComparer<Bitmap>.Equals(Bitmap bitmap1, Bitmap bitmap2)
            {
                if (bitmap1.Width != bitmap2.Width) return false;
                if (bitmap1.Height != bitmap2.Height) return false;

                foreach (var x in bitmap1.HorizontalRange)
                    foreach (var y in bitmap1.VerticalRange)
                        if (bitmap1[x, y] != bitmap2[x, y])
                            return false;

                return true;
            }

            int IEqualityComparer<Bitmap>.GetHashCode(Bitmap obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
