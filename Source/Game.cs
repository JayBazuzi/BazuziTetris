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

        public Game()
        {
            this.CurrentPiece = new Piece.I();
            this.CurrentPieceLocation = new Location(5, 16);
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
            while (this.CurrentPieceLocation.Y > 0)
            {
                this.CurrentPieceDropOneStep();
            }

            TransferToWell(this.CurrentPiece);
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

        internal void CurrentPieceDropOneStep()
        {
            this.CurrentPieceLocation.Y--;
        }
    }
}
