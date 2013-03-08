using System;
using System.Collections.Generic;
using System.Linq;

namespace BazuziTetris
{
    class Game
    {
        public Piece CurrentPiece;

        public Location CurrentPieceLocation;

        public readonly Bitmap Well = new Bitmap(10, 20);

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
            if (this.CurrentPieceLocation.Y == 0)
            {
                TransferToWell(this.CurrentPiece);
            }

            else
            {
                this.CurrentPieceDropOneStep();
            }
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
            this.Well.Overlay(piece.Bitmap, this.CurrentPieceLocation);
        }

        public override string ToString()
        {
            var gameBitmap = this.Well.Copy();
            gameBitmap.Overlay(this.CurrentPiece.Bitmap, this.CurrentPieceLocation);
            return gameBitmap.ToString();
        }

        /// <returns>returns true if dropped / false if landed</returns>
        internal bool CurrentPieceDropOneStep()
        {
            if (this.CurrentPieceLocation.Y == 0)
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

        internal void MoveLeft()
        {
            this.CurrentPieceLocation.X--;
        }

        internal void MoveRight()
        {
            this.CurrentPieceLocation.X++;
        }
    }
}
