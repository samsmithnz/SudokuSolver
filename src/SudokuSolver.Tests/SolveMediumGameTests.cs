using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveMediumGameTests
    {

        [TestMethod]
        public void SolveMedium1GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
...9...3.
.725.6.9.
.94.1.5..
.5.....4.
.....73..
12..43...
.......2.
5.....6..
...67.1..
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true,
                true, false, true);

            //Assert     
            string expected = @"
615924837
872536491
394718562
753169248
948257316
126843759
467381925
581492673
239675184
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(56, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

    }
}
