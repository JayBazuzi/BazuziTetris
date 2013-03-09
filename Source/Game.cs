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
                    yield return new Piece.I();
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
        }

        public override string ToString()
        {
            var gameBitmap = this.Well.Union(this.CurrentPiece.Bitmap, this.CurrentPieceLocation);
            return gameBitmap.ToString();
        }

        /// <returns>returns true if dropped / false if landed</returns>
        internal bool CurrentPieceDropOneStep()
        {
            var newLocation = new Location(this.CurrentPieceLocation.X, this.CurrentPieceLocation.Y - 1);
            if (Allowed(this.CurrentPiece.Bitmap, newLocation))
            {
                this.CurrentPieceLocation.Y--;

                return true;
            }
            else
            {
                TransferToWell(this.CurrentPiece);
                NextPiece();
                return false;
            }
        }

        internal void MoveLeft()
        {
            if (this.CurrentPieceLocation.X > 0)
                this.CurrentPieceLocation.X--;
        }

        internal bool MoveRight()
        {
            var newLocation = new Location(this.CurrentPieceLocation.X + 1, this.CurrentPieceLocation.Y);
            if (Allowed(this.CurrentPiece.Bitmap, newLocation))
            {
                this.CurrentPieceLocation.X++;
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
