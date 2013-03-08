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
