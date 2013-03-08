using Xunit;

namespace BazuziTetris
{
    public class RotationTests
    {
        [Fact]
        public void OriginalOrientationShouldBeNorth()
        {
            Game game = new Game();
            Assert.Equal(Orientation.North, game.CurrentPiece.Orientation);
        }

        [Fact]
        public void OneRotateShouldBeEast()
        {
            Game game = new Game();
            game.CurrentPiece.Rotate();
            Assert.Equal(Orientation.East, game.CurrentPiece.Orientation);
        }

        [Fact]
        public void FourRotateShouldBeNorthAgain()
        {
            Game game = new Game();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            game.CurrentPiece.Rotate();
            Assert.Equal(Orientation.North, game.CurrentPiece.Orientation);
        }
    }
}