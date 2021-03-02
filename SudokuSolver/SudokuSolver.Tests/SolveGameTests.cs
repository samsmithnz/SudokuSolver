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
354698712
916273485
692817354
837564129
145329876
423751968
581936247
769482531
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(41, squaresSolved);
            Assert.AreEqual(5, gameState.IterationsToSolve);
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
169483257
382795641
574216938
957641823
238579164
416328795
621834579
845967312
793152486
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(56, squaresSolved);
            Assert.AreEqual(8, gameState.IterationsToSolve);
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
675128934
184973526
218367495
357492618
496815372
763581249
521749863
849236751
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(39, squaresSolved);
            Assert.AreEqual(7, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveEasy4GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
.7...5...
1...3.5.8
...2.9.6.
91.5..42.
68.3...1.
254.9...3
7.68.1.4.
345..6.71
..1.7.2.6
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
479685132
162734598
538219764
913568427
687342915
254197683
726851349
345926871
891473256
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(43, squaresSolved);
            Assert.AreEqual(4, gameState.IterationsToSolve);
        }

    }
}
