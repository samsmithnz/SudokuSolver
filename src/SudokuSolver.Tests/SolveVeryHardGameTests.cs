using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveVeryHardGameTests
    {

        [TestMethod]
        public void SolveVeryHard6GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
...6..2..
8.4.3....
.....9...
4.5.....7
71.......
..3.5...8
3...7...4
.....19..
...2...6.
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
...6..2..
8.4.3....
.....9...
4.5.....7
71.......
..3.5...8
3...7...4
.....19..
...2...6.
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(60, gameState.UnsolvedSquareCount);
            Assert.AreEqual(0, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveVeryHard7GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
8........
..36.....
.7..9.2..
.5..7....
....457..
...1...3.
..1....68
..85...1.
.9....4..
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
8........
..36.....
.7..9.2..
.5..7....
....457..
...1...3.
..1....68
..85...1.
.9....4..
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(60, gameState.UnsolvedSquareCount);
            Assert.AreEqual(0, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

    }
}
