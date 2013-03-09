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
            if (this.CurrentPieceLocation.Y == 0 || CanDrop())
            {
                TransferToWell(this.CurrentPiece);
                NextPiece();
                return false;
            }
            else
            {
                this.CurrentPieceLocation.Y--;

                return true;
            }
        }

        private bool CanDrop()
        {
            Bitmap collision = this.Well.Intersection(this.CurrentPiece.Bitmap, new Location(this.CurrentPieceLocation.X, this.CurrentPieceLocation.Y - 1));

            // TODO: Use IEnumerable.Any()
            foreach (var x in collision.HorizontalRange)
                foreach (var y in collision.VerticalRange)
                    if (collision[x, y])
                        return true;

            return false;
        }

        internal void MoveLeft()
        {
            if (this.CurrentPieceLocation.X > 0)
                this.CurrentPieceLocation.X--;
        }

        internal void MoveRight()
        {
            if (this.CurrentPieceLocation.X + this.CurrentPiece.Bitmap.Width < this.Well.Width)
                this.CurrentPieceLocation.X++;
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
            if (this.Well.Width < this.CurrentPieceLocation.X + rotated.Width) return false;

            Bitmap collision = this.Well.Intersection(rotated, this.CurrentPieceLocation);

            // TODO: Use IEnumerable.Any()
            foreach (var x in collision.HorizontalRange)
                foreach (var y in collision.VerticalRange)
                    if (collision[x, y])
                        return false;

            return true;
        }
    }
}
