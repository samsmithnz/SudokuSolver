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

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(39, squaresSolved);
            //Assert.AreEqual(7, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveMedium1GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
9......7.
1.....2.4
.5..34..9
...5..4..
28..9.7..
..1..7...
7....1...
.24......
..8.....5
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
9421...7.
1...7.2.4
857234..9
.7951.42.
28549.7.1
4.1.27...
79..51.42
524.....7
.18742..5
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(32, gameState.UnsolvedSquareCount);
            Assert.AreEqual(26, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveMedium2GameTest()
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
        public void SolveHard1GameTest()
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
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
.....8..3
3......76
2.7..39.4
.....2645
.3.957..2
.528.6...
..32..5.8
94...5..1
.2.6..4..
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(47, gameState.UnsolvedSquareCount);
            Assert.AreEqual(9, squaresSolved);
            //Assert.AreEqual(3, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveHard2GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
.65.....8
7..86.4..
....2...9
.4...1..2
...2.7...
3..5...7.
4...5....
..1.79..3
9.....26.
        ";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
265194738
7.986.42.
..472.6.9
647931..2
..82473.6
3.2586.74
4.6352...
.21679.43
9.341826.
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(22, gameState.UnsolvedSquareCount);
            Assert.AreEqual(33, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveHard3GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
.84..25.1
31......9
.7....8..
...27..9.
4..9.8..7
.9..41...
..3....5.
7......42
8.95..61.
        ";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
.84..25.1
31......9
.7....8..
..827..9.
4..9.8..7
.97.41.8.
..3...758
7.....942
849527613
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(41, gameState.UnsolvedSquareCount);
            Assert.AreEqual(10, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveHard4GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
..73....9
38.46.7..
4..7...3.
.......73
.39...28.
87.......
.6...8..4
..3.74.28
1....26..
        ";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
..738...9
385469712
49.72.83.
...8.6.73
.39.4728.
87.2.3...
762..83.4
953674128
148.326.7
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(26, gameState.UnsolvedSquareCount);
            Assert.AreEqual(25, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveHard5GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
..4..8...
..3429...
.8.35.2..
.4....7..
367...891
..8....2.
..9.32.7.
...9743..
...5..9..
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
..4..8...
..3429...
.8.35.2..
.4....7..
367245891
..8...42.
4.9.32.7.
8..9743..
73.5..9..
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(43, gameState.UnsolvedSquareCount);
            Assert.AreEqual(8, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveHard6GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
4..27.6..
798156234
.2.84...7
237468951
849531726
561792843
.82.15479
.7..243..
..4.87..2
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame();

            //Assert     
            string expected = @"
4..27.6..
798156234
.2.84...7
237468951
849531726
561792843
.82.15479
.7..243..
..4.87.62
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(21, gameState.UnsolvedSquareCount);
            Assert.AreEqual(1, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveImpossible1GameTest()
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
            int squaresSolved = gameState.SolveGame();

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
        public void SolveImpossible2GameTest()
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
            int squaresSolved = gameState.SolveGame();

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
