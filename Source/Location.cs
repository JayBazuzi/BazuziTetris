using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazuziTetris
{
    class Location
    {
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public readonly int X;
        public readonly int Y;

        public Location DownOne()
        {
            return new Location(this.X, this.Y - 1);
        }

        public Location LeftOne()
        {
            return new Location(this.X - 1, this.Y);
        }

        public Location RightOne()
        {
            return new Location(this.X + 1, this.Y);
        }
    }
}
