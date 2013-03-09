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
            static readonly Bitmap s_bitmap = new Bitmap(new bool[,] {{
                    true,
                    true,
                    true,
                    true,
                    }});

            public I()
                : base(s_bitmap)
            {
            }
        }

        public class Box : Piece
        {
            static readonly Bitmap s_bitmap = new Bitmap(new bool[,] {
                    { true,                     true},
                    { true,                     true},
            });

            public Box()
                : base(s_bitmap)
            {
            }

        }

        public class J : Piece
        {
            static readonly Bitmap s_bitmap = new Bitmap(new bool[,] {
                    { false, true },
                    { false, true },
                    { true, true },
                    });

            public J()
                : base(s_bitmap)
            {
            }
        }

        public class L : Piece
        {
            static readonly Bitmap s_bitmap = new Bitmap(new bool[,] {
                    { true, false },
                    { true, false },
                    { true, true },
                    });

            public L()
                : base(s_bitmap)
            {
            }
        }

        public void Rotate()
        {
            this.Bitmap = this.Bitmap.Rotate();
        }
    }
}
