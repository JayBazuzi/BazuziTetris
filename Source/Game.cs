namespace BazuziTetris
{
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
}