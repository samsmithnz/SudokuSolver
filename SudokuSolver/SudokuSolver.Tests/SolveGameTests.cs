using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveGameTests
    {

        [TestMethod]
        public void SolveEasy1GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
27.1.5..3
354...71.
9162.3.8.
6.28.73.4
.........
1.53.98.6
.2.7.1.6.
.81...24.
7..4.2..1
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert
            string expected = @"
278145693
354698719
916273485
692817354
837564122
145329876
823751968
581936247
769482531
";

            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(43, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.OutputState());
            Assert.AreEqual(6, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveEasy2GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
..9..3..7
..2..5..1
..4..6..8
.5..4....
.3..7..6.
....2..9.
6..8..5..
8..9..3..
7..1..4..
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert
            string expected = @"
..9..3..7
..2..5..1
..4..6..8
.5..4....
.3..7..6.
....2..9.
6..8..5..
8..9..3..
7..1..4..
";

            Assert.AreEqual(56, gameState.UnsolvedSquareCount);
            Assert.AreEqual(0, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.OutputState());
            Assert.AreEqual(1, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveEasy3GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
932654187
.7.1..9..
18.9.35..
2...6...5
3...92..8
4...1...2
..35.1.49
..1..9.63
..92.6751
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert
            string expected = @"
932654187
.7.1..9..
18.9.35..
2...6...5
3...92..8
4...1...2
..35.1.49
..1..9.63
849236751
";

            Assert.AreEqual(36, gameState.UnsolvedSquareCount);
            Assert.AreEqual(3, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.OutputState());
            Assert.AreEqual(3, gameState.IterationsToSolve);
        }

    }
}
