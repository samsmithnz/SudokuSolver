using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class RulesTests
    {
        [TestMethod]
        public void RowRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
12.456789
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.ProcessRules(true, false, false, true);

            //Assert
            string expected = @"
123456789
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(1, squaresSolved);
        }


        [TestMethod]
        public void ColumnRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
123456789
456789123
.89123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.ProcessRules(false, true, false, true);

            //Assert
            string expected = @"
123456789
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(1, squaresSolved);
        }

        [TestMethod]
        public void SquareGroupRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
123456789
45.789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.ProcessRules(false, false, true, true);

            //Assert
            string expected = @"
123456789
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(1, squaresSolved);
        }

        [TestMethod]
        public void NakedPairsRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
..738...9
38.4697.2
49.72.83.
...8.6.73
.39.4728.
87.2.3...
762..83.4
953674128
148.326.7
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.ProcessRules(true, true, true, true);

            //Assert
            string expected = @"
..738...9
38.4697.2
49.72.83.
...8.6.73
.39.4728.
87.2.3...
762..83.4
953674128
148.326.7
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(28, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(0, squaresSolved);
        }

        [TestMethod]
        public void AllRulesTest()
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
            int squaresSolved = gameState.ProcessRules(true, true, true, true);
            bool crossCheckSuccessful = Rules.CrossCheckResultRule(gameState.GameBoard);

            //Assert       
            string expected = @"
2781.5693
3546.8712
916273.85
692817354
8.75.4129
1.53.9876
42.751.68
581936247
76.482.31
";

            Assert.IsTrue(crossCheckSuccessful);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(11, gameState.UnsolvedSquareCount);
            Assert.AreEqual(30, squaresSolved);
        }


    }
}
