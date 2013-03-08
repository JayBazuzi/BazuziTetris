using System.Collections.Generic;
using System.Linq;

namespace BazuziTetris
{
    public class Well
    {
        public const int Width = 10;
        public const int Height = 20;

        readonly bool[,] _cells = new bool[Width, Height];
        public readonly IEnumerable<int> HorizontalRange = Enumerable.Range(0, Width);
        public readonly IEnumerable<int> VerticalRange = Enumerable.Range(0, Height);

        public bool this[int horizontalIndex, int verticalIndex]
        {
            get { return this._cells[horizontalIndex, verticalIndex]; }
            set { this._cells[horizontalIndex, verticalIndex] = value; }
        }
    }
}
