using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazuziTetris
{
    class Bitmap
    {
        public readonly int Width;
        public readonly int Height;

        readonly bool[,] _cells;

        public Bitmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this._cells = new bool[Width, Height];
        }

        public IEnumerable<int> HorizontalRange { get { return Enumerable.Range(0, Width); } }
        public IEnumerable<int> VerticalRange { get { return Enumerable.Range(0, Height); } }

        public bool this[int horizontalIndex, int verticalIndex]
        {
            get { return this._cells[horizontalIndex, verticalIndex]; }
            set { this._cells[horizontalIndex, verticalIndex] = value; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var y in VerticalRange.Reverse())
            {
                stringBuilder.Append(string.Format("{0,2} ", y));
                foreach (var x in HorizontalRange)
                {
                    stringBuilder.Append(this._cells[x, y] ? 'X' : '.');
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("   " + string.Join("", this.HorizontalRange));

            return stringBuilder.ToString();
        }

        public void Overlay(Bitmap bitmap, Location location)
        {
            foreach (var x in bitmap.HorizontalRange)
                foreach (var y in bitmap.VerticalRange)
                    if (bitmap[x, y])
                        this[x + location.X, y + location.Y] = true;
        }

        public Bitmap Copy()
        {
            var newBitmap = new Bitmap(this.Width, this.Height);
            newBitmap.Overlay(this, new Location(0, 0));
            return newBitmap;
        }
    }
}
