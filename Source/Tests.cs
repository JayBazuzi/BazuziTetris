using Xunit;

namespace BazuziTetris
{
    public class Tests
    {
        [Fact]
        public void WellShouldStartEmpty()
        {
            Game game = new Game();
            foreach (var item in game.Well.Cells)
            {
                Assert.False(item);
            }
        }

        [Fact]
        public void DropIIntoWell()
        {
            Game game = new Game();
            game.DropAllTheWay();

            for (int x = 0; x < game.Well.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < game.Well.Cells.GetLength(1); y++)
                {
                    if (x < 4 && y == 5)
                    {
                        Assert.True(game.Well.Cells[x, y]);
                    }

                    else
                    {
                        Assert.False(game.Well.Cells[x, y]);
                    }
                }

            }
        }

        [Fact]
        public void PieceShouldDropOneOnTick()
        {
            Game game = new Game();
            game.OnTick();
            Assert.Equal(15, game.CurrentPiece.Location.X);
        }
    }
}