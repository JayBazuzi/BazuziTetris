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
                    if (x < 4 && y == 5)
                    {
                        Assert.True(game.Well[x, y]);
                    }

                    else
                    {
                        Assert.False(game.Well[x, y]);
                    }
                }

            }
        }

        [Fact]
        public void PieceShouldDropOneOnTick()
        {
            Game game = new Game();
            Assert.Equal(16, game.CurrentPiece.Location.X);
            game.OnTick();
            Assert.Equal(15, game.CurrentPiece.Location.X);
        }
    }
}
