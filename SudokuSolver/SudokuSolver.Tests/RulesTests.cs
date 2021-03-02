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
            int squaresSolved = gameState.ProcessRules(true, false, false);

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
            int squaresSolved = gameState.ProcessRules(false, true, false);

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
            int squaresSolved = gameState.ProcessRules(false, false, true);

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
            int squaresSolved = gameState.ProcessRules(true, true, true);

            //Assert
            string expected = @"
2781.5.93
354...712
9162.3.85
6928.73.4
83.......
1453.98.6
42.7.1.6.
581..624.
7..4.2..1
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(30, gameState.UnsolvedSquareCount);
            Assert.AreEqual(11, squaresSolved);
        }


    }
}
