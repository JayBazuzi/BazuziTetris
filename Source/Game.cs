using System;
using System.Collections.Generic;
using System.Linq;

namespace BazuziTetris
{
    class Game
    {
        public Piece CurrentPiece;

        public readonly Well Well = new Well();

        public Game()
        {
            this.CurrentPiece = new Piece.I(new Location(16, 5));
        }

        public void OnTick()
        {
            if (this.CurrentPiece.Location.X == 0)
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
            while (this.CurrentPiece.Location.X > 0)
            {
                this.CurrentPiece.DropOneStep();
            }

            TransferToWell(this.CurrentPiece);
        }

        void TransferToWell(Piece piece)
        {
            foreach (var x in Enumerable.Range(0, piece.Bitmap.GetLength(0)))
                foreach (var y in Enumerable.Range(0, piece.Bitmap.GetLength(1)))
                    if (piece.Bitmap[x, y])
                        this.Well[x + piece.Location.X, y + piece.Location.Y] = true;
        }
    }
}
