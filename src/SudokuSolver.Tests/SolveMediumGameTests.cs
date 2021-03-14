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

        [TestMethod]
        public void SolveMedium2GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
.....8...
3......76
2.7..39..
.....264.
...957...
.528.....
..32..5.8
94......1
...6.....
        ";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
491768253
385429176
267513984
879132645
634957812
152846397
713294568
946385721
528671439
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(56, squaresSolved);
        }

    }
}
