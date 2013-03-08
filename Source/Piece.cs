namespace BazuziTetris
{
    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    abstract class Piece
    {
        protected Piece(Location location)
        {
            this.Location = location;
        }

        public Orientation Orientation = Orientation.North;
        public Location Location;

        public class I : Piece
        {
            public I(int x, int y)
                : base(new Location(x, y))
            {

            }
            public override void TransferToWell(Well well)
            {
                well.Cells[this.Location.X + 0, this.Location.Y] = true;
                well.Cells[this.Location.X + 1, this.Location.Y] = true;
                well.Cells[this.Location.X + 2, this.Location.Y] = true;
                well.Cells[this.Location.X + 3, this.Location.Y] = true;
            }
        }

        public void DropOneStep()
        {
            this.Location.X--;
        }

        public void Rotate()
        {
            this.Orientation++;
            if (this.Orientation == Orientation.Count) this.Orientation = 0;
        }

        public abstract void TransferToWell(Well well);
    }
}