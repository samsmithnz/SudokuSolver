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

        [TestMethod]
        public void SolveVeryHard3GameTest()
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
984..25.1
31......9
.7....8..
..827..9.
4..9.8..7
.97.41.8.
..3...758
7.....942
849527613
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(40, gameState.UnsolvedSquareCount);
            Assert.AreEqual(11, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveVeryHard5GameTest()
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(43, gameState.UnsolvedSquareCount);
            Assert.AreEqual(8, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveVeryHard1GameTest()
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(47, gameState.UnsolvedSquareCount);
            Assert.AreEqual(10, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }


        [TestMethod]
        public void SolveVeryHard9GameTest()
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(39, gameState.UnsolvedSquareCount);
            Assert.AreEqual(17, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveVeryHard8GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
8..1.3.5.
31.4..8..
..5......
.9.2....5
.587.936.
4....5.8.
......2..
..2..1.37
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
8..1.3.5.
31.45.8..
..5.....3
.9.2....5
2587.936.
4....5982
......2..
..2..1.37
.........
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(50, gameState.UnsolvedSquareCount);
            Assert.AreEqual(5, squaresSolved);
            //Assert.AreEqual(9, gameState.IterationsToSolve);
        }

        [TestMethod]
        public void SolveVeryHard10GameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
1..2.8.3.
73.1..2..
..2......
.8.5....4
.639.785.
5....2.9.
......5..
..8..4.13
.5.3.1..8
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true, true, true, true);

            //Assert     
            string expected = @"
1..2.8.3.
73.14.28.
8.2.35..1
289563174
463917852
5..482396
3..8..5..
928.54.13
65.3.1..8
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(26, gameState.UnsolvedSquareCount);
            Assert.AreEqual(25, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

    }
}
