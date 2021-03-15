using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveHardGameTests
    {

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
739865421
814723659
647931582
158247396
392586174
486352917
521679843
973418265
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(55, squaresSolved);
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
6273814.9
385469712
49172.835
2148.6.73
539147286
87.2.3.61
762.183.4
953674128
148.326.7
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(11, gameState.UnsolvedSquareCount);
            Assert.AreEqual(40, squaresSolved);
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
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
415273698
798156234
623849517
237468951
849531726
561792843
382615479
176924385
954387162
";

             Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(22, squaresSolved);
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
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
942168573
163975284
857234169
679513428
285496731
431827956
796351842
524689317
318742695
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(58, squaresSolved);
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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(39, gameState.UnsolvedSquareCount);
            Assert.AreEqual(17, squaresSolved);
            //Assert.AreEqual(5, gameState.IterationsToSolve);
        }

    }
}
