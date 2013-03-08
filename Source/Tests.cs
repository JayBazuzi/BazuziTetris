using Xunit;

namespace BazuziTetris
{
    public class Tests
    {
        [Fact]
        public void WellShouldStartEmpty()
        {
            Game game = new Game();
            foreach (var x in game.Well.HorizontalRange)
                foreach (var y in game.Well.VerticalRange)
                    Assert.False(game.Well[x, y]);
        }

        [Fact]
        public void DropIIntoWell()
        {
            Game game = new Game();
            game.DropAllTheWay();

            foreach (int x in game.Well.HorizontalRange)
            {
                foreach (int y in game.Well.VerticalRange)
                {
                    if (x == 5 && y < 4)
                    {
                        Assert.True(game.Well[x, y], string.Format("x = {0}, y = {1}, \r\n\r\n{2}", x, y, game.Well.ToString()));
                    }

                    else
                    {
                        Assert.False(game.Well[x, y], game.Well.ToString());
                    }
                }
            }
        }

        [Fact]
        public void MoveLeft()
        {
            Game game = new Game();
            Assert.Equal(5, game.CurrentPieceLocation.X);
            game.MoveLeft();
            Assert.Equal(4, game.CurrentPieceLocation.X);
        }

        [Fact]
        public void PieceShouldDropOneOnTick()
        {
            Game game = new Game();
            Assert.Equal(16, game.CurrentPieceLocation.Y);
            game.OnTick();
            Assert.Equal(15, game.CurrentPieceLocation.Y);
        }

        [Fact]
        public void RotateIPieceTest()
        {
            var piece = new Piece.I();

            Assert.Equal(1, piece.Bitmap.Width);
            Assert.Equal(4, piece.Bitmap.Height);
            foreach (var x in piece.Bitmap.HorizontalRange)
                foreach (var y in piece.Bitmap.VerticalRange)
                    Assert.True(piece.Bitmap[x, y]);

            piece.Rotate();

            Assert.Equal(1, piece.Bitmap.Height);
            Assert.Equal(4, piece.Bitmap.Width);
            foreach (var x in piece.Bitmap.HorizontalRange)
                foreach (var y in piece.Bitmap.VerticalRange)
                    Assert.True(piece.Bitmap[x, y]);
        }
    }
}
