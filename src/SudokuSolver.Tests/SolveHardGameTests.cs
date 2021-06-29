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
217385469
385469712
496721835
524816973
639547281
871293546
762158394
953674128
148932657
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.IsTrue(gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(51, squaresSolved);
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
