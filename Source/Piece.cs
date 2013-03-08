namespace BazuziTetris
{
    /// <summary>
    /// A 2d shape composed of 4 blocks
    /// </summary>
    abstract class Piece
    {
        protected Piece(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }

        public Bitmap Bitmap { get; private set; }

        public class I : Piece
        {
            static Bitmap s_bitmap;

            static I()
            {
                s_bitmap = new Bitmap(1, 4);
                s_bitmap[0, 0] = true;
                s_bitmap[0, 1] = true;
                s_bitmap[0, 2] = true;
                s_bitmap[0, 3] = true;
            }

            public I()
                : base(s_bitmap.Copy())
            {
            }
        }

        public void Rotate()
        {
            Bitmap newBitmap = new Bitmap(this.Bitmap.Height, this.Bitmap.Width);

            foreach (var x in Bitmap.HorizontalRange)
                foreach (var y in Bitmap.VerticalRange)
                    newBitmap[this.Bitmap.Height - y - 1, x] = Bitmap[x, y];

            this.Bitmap = newBitmap;
        }
    }
}
