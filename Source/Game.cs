﻿// Copyright (c) 2013 Jay Bazuzi (JAY@BAZUZI.COM)

#region MIT LICENSE
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace BazuziTetris
{
    class Game
    {
        public Piece CurrentPiece;

        public Location CurrentPieceLocation;

        public Bitmap Well = new Bitmap(new bool[10, 20]);

        class PieceStream : IEnumerable<Piece>
        {
            public IEnumerator<Piece> GetEnumerator()
            {
                var random = new Random(0);
                while (true)
                {
                    // shuffling is hard. I'm pretending it isn't.
                    var r = random.Next();
                    if (r < 1000)
                        yield return new Piece.I();
                    else if (r < 2000)
                        yield return new Piece.Box();
                    else if (r < 3000)
                        yield return new Piece.J();
                    else if (r < 4000)
                        yield return new Piece.L();
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<Piece> comingPieces;

        public Game() : this(new PieceStream()) { }

        public Game(IEnumerable<Piece> pieces)
        {
            this.comingPieces = pieces;
            NextPiece();
        }

        public void OnTick()
        {
            this.CurrentPieceDropOneStep();
        }

        internal void DropAllTheWay()
        {
            while (this.CurrentPieceDropOneStep())
            {
            }
        }

        private void NextPiece()
        {
            this.CurrentPiece = comingPieces.First(); comingPieces = comingPieces.Skip(1);
            this.CurrentPieceLocation = new Location(5, 16);
        }

        void TransferToWell(Piece piece)
        {
            this.Well = this.Well.Union(piece.Bitmap, this.CurrentPieceLocation);

            var newBitmap = new bool[this.Well.Width, this.Well.Height];

            int erasedLines = 0;
            for (int y = 0; y < this.Well.Height; y++)
            {
                bool all = true;
                foreach (var x in this.Well.HorizontalRange) all &= this.Well[x, y];

                if (all)
                {
                    erasedLines++;
                }
                else
                {
                    foreach (var x in this.Well.HorizontalRange)
                        newBitmap[x, y - erasedLines] = this.Well[x, y];
                }
            }

            this.Well = new Bitmap(newBitmap);
        }

        public override string ToString()
        {
            var gameBitmap = this.Well.Union(this.CurrentPiece.Bitmap, this.CurrentPieceLocation);
            return gameBitmap.ToString();
        }

        /// <returns>returns true if dropped / false if landed</returns>
        internal bool CurrentPieceDropOneStep()
        {
            var newLocation = this.CurrentPieceLocation.DownOne();
            if (Allowed(this.CurrentPiece.Bitmap, newLocation))
            {
                this.CurrentPieceLocation = newLocation;

                return true;
            }
            else
            {
                TransferToWell(this.CurrentPiece);
                NextPiece();
                return false;
            }
        }

        internal bool MoveLeft()
        {
            var newLocation = this.CurrentPieceLocation.LeftOne();
            if (Allowed(this.CurrentPiece.Bitmap, newLocation))
            {
                this.CurrentPieceLocation = newLocation;

                return true;
            }

            else return false;
        }

        internal bool MoveRight()
        {
            var newLocation = this.CurrentPieceLocation.RightOne();
            if (Allowed(this.CurrentPiece.Bitmap, newLocation))
            {
                this.CurrentPieceLocation = newLocation;

                return true;
            }

            else return false;
        }

        internal bool CurrentPieceRotate()
        {
            if (!CanRotate()) return false;

            this.CurrentPiece.Rotate();
            return true;
        }

        private bool CanRotate()
        {
            var rotated = this.CurrentPiece.Bitmap.Rotate();
            return Allowed(rotated, this.CurrentPieceLocation);
        }

        private bool Allowed(Bitmap bitmap, Location location)
        {
            return WithinWell(bitmap, location) && !Collision(bitmap, location);
        }

        bool WithinWell(Bitmap bitmap, Location location)
        {
            return location.X >= 0 &&
                location.Y >= 0 &&
                location.X + bitmap.Width <= this.Well.Width &&
                location.Y + bitmap.Height <= this.Well.Height;
        }

        bool Collision(Bitmap bitmap, Location location)
        {
            Bitmap collisionMap = this.Well.Intersection(bitmap, location);

            // TODO: Use IEnumerable.Any()
            foreach (var x in collisionMap.HorizontalRange)
                foreach (var y in collisionMap.VerticalRange)
                    if (collisionMap[x, y])
                        return true;

            return false;
        }
    }
}
