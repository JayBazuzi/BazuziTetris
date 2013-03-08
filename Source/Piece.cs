namespace BazuziTetris
{
    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    abstract class Piece
    {
        protected Piece(Location location, int width, int height)
        {
            this.Location = location;
            this.Bitmap = new Bitmap(width, height);
        }

        public Location Location;
        public readonly Bitmap Bitmap;

        public class I : Piece
        {
            public I(Location location)
                : base(location, 1, 4)
            {
                this.Bitmap[0, 0] = true;
                this.Bitmap[0, 1] = true;
                this.Bitmap[0, 2] = true;
                this.Bitmap[0, 3] = true;
            }
        }

        public void DropOneStep()
        {
            this.Location.Y--;
        }
    }
}
