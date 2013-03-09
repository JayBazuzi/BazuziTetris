﻿// Copyright (c) 2013 Jay Bazuzi (JAY@BAZUZI.COM)

#region MIT LICENSE
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

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
