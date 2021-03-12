using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveEasyGameTests
    {

        [TestMethod]
        public void SolveEasy1GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
2..1.5..3
.54...71.
.1.2.3.8.
6.28.73.4
.........
1.53.98.6
.2.7.1.6.
.81...24.
7..4.2..1
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true,true,true,
                false,false, true);

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

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(45, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
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
            int squaresSolved = gameState.SolveGame(true, true, true,
                false, false, true);

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

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(56, squaresSolved);
            //Assert.AreEqual(8, gameState.IterationsToSolve);
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
            int squaresSolved = gameState.SolveGame(true, true, true,
                false, false, true);

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

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(39, squaresSolved);
            //Assert.AreEqual(7, gameState.IterationsToSolve);
        }


        [TestMethod]
        public void SolveEasy4GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
..36.49..
....5....
9.......7
2.......6
.4.....5.
8.......1
1.......5
.........
.9273641.
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
753614982
628957134
914382567
275193846
341268759
869475321
136849275
487521693
592736418
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(59, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveEasy5GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
4..8.91..
..7....9.
95..2...7
1...9...3
3924.78..
6...3...9
72..8..6.
.1....2..
..31.2..4
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
436879152
287315496
951624387
175298643
392467815
648531729
724983561
519746238
863152974
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(49, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

    }
}
