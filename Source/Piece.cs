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

        public readonly Bitmap Bitmap;

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
    }
}
