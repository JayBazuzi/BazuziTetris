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
                while (true)
                {
                    yield return new Piece.I();
                    yield return new Piece.Box();
                    yield return new Piece.J();
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
