using System;
using System.Collections.Generic;
using System.Linq;

namespace BazuziTetris
{
    class Game
    {
        public Piece CurrentPiece;

        public readonly Bitmap Well = new Bitmap(10, 20);

        public Game()
        {
            this.CurrentPiece = new Piece.I(new Location(5, 16));
        }

        public void OnTick()
        {
            if (this.CurrentPiece.Location.Y == 0)
            {
                TransferToWell(this.CurrentPiece);
            }

            else
            {
                this.CurrentPiece.DropOneStep();
            }
        }

        internal void DropAllTheWay()
        {
            while (this.CurrentPiece.Location.Y > 0)
            {
                this.CurrentPiece.DropOneStep();
            }

            TransferToWell(this.CurrentPiece);
        }

        void TransferToWell(Piece piece)
        {
            this.Well.Overlay(piece.Bitmap, piece.Location);
        }

        public override string ToString()
        {
            var gameBitmap = this.Well.Copy();
            gameBitmap.Overlay(this.CurrentPiece.Bitmap, this.CurrentPiece.Location);
            return gameBitmap.ToString();
        }
    }
}
