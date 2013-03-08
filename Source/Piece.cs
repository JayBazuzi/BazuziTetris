namespace BazuziTetris
{
    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    abstract class Piece
    {
        protected Piece(Location location, bool[,] bitmap)
        {
            this.Location = location;
            this.Bitmap = bitmap;
        }

        public Location Location;
        public readonly bool[,] Bitmap;

        public class I : Piece
        {
            public I(Location location)
                : base(location, new bool[4, 1]
                    {
                        {true},
                        {true},
                        {true},
                        {true},
                    })
            {
            }
        }

        public void DropOneStep()
        {
            this.Location.X--;
        }
    }
}
