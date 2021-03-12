using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveHardGameTests
    {

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
        public void SolveHard7GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
3......8.
1..6.3..2
.6.......
.8.1..97.
...5.....
2.9..48..
..1...62.
.......43
.7..5.1..
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
3......86
1..6.3.52
.6....319
.8.1..97.
...5..2..
2.9..48..
..1...627
......543
.7..5.198
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(47, gameState.UnsolvedSquareCount);
            Assert.AreEqual(10, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }


        [TestMethod]
        public void SolveHard8GameTest()
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
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

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
        public void SolveHard9GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
...12....
.6839....
7...6....
.1.93....
.9..56...
6..8..93.
..9..2...
..6.....5
.2....3.1
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
9..12...3
26839....
731.6..29
.1.93....
.93256...
6..8.193.
3.9.12...
1.6..3295
.2...93.1
";

            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(39, gameState.UnsolvedSquareCount);
            Assert.AreEqual(17, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

    }
}
