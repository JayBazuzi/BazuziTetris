using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazuziTetris
{
    class Bitmap
    {
        public readonly int Width;
        public readonly int Height;

        readonly IReadOnlyList<IReadOnlyList<bool>> _cells;

        public Bitmap(bool[,] values)
        {
            this.Width = values.GetLength(0);
            this.Height = values.GetLength(1);
            var cells = new List<List<bool>>();
            foreach (var x in HorizontalRange)
            {
                cells.Add(new List<bool>());

                foreach (var y in VerticalRange)
                {
                    cells[x].Add(values[x, y]);
                }
            }
            this._cells = cells;
        }

        public IEnumerable<int> HorizontalRange { get { return Enumerable.Range(0, Width); } }
        public IEnumerable<int> VerticalRange { get { return Enumerable.Range(0, Height); } }

        public bool this[int horizontalIndex, int verticalIndex]
        {
            get { return this._cells[horizontalIndex][verticalIndex]; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var y in VerticalRange.Reverse())
            {
                stringBuilder.Append(string.Format("{0,2} ", y));
                foreach (var x in HorizontalRange)
                {
                    stringBuilder.Append(this._cells[x][y] ? 'X' : '.');
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("   " + string.Join("", this.HorizontalRange));

            return stringBuilder.ToString();
        }

        public Bitmap Union(Bitmap bitmap, Location location)
        {
            var newValues = new bool[this.Width, this.Height];

            // first, copy in to a mutable array
            foreach (var x in HorizontalRange)
                foreach (var y in VerticalRange)
                        newValues[x, y] = this[x, y];

            // Second, insert the other bitmal
            foreach (var x in bitmap.HorizontalRange)
                foreach (var y in bitmap.VerticalRange)
                    newValues[x + location.X, y + location.Y] |= bitmap[x, y];

            return new Bitmap(newValues);
        }

        internal Bitmap Rotate()
        {
            var newValues = new bool[this.Height, this.Width];

            foreach (var x in HorizontalRange)
                foreach (var y in VerticalRange)
                    newValues[this.Height - y - 1, x] = this[x, y];

            return new Bitmap(newValues);
        }
    }
}
