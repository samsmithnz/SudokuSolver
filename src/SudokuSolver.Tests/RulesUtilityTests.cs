using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class RulesUtilityTests
    {

        [TestMethod]
        public void CrossCheckRowRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
888888888
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
            bool result = RulesUtility.CrossCheckResult(gameState.GameBoard);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CrossCheckColumnRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
8........
8.36.....
87..9.2..
85..7....
8...457..
8..1...3.
8.1....62
8.25...1.
89....4..
";

            //Act
            gameState.LoadGame(game);
            bool result = RulesUtility.CrossCheckResult(gameState.GameBoard);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CrossCheckGroupRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
888......
8886.....
888.9.2..
.5..7....
....457..
...1...3.
..1....68
..25...1.
.9....4..
";

            //Act
            gameState.LoadGame(game);
            bool result = RulesUtility.CrossCheckResult(gameState.GameBoard);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void SquareGroupExtractionRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
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

            //Act
            gameState.LoadGame(game);
            int[,] topLeft = RulesUtility.ExtractSquareGroupFromGameBoard(gameState.GameBoard, 0, 0);
            int[,] topMiddle = RulesUtility.ExtractSquareGroupFromGameBoard(gameState.GameBoard, 1, 0);
            int[,] topRight = RulesUtility.ExtractSquareGroupFromGameBoard(gameState.GameBoard, 2, 0);
            int[,] bottomRight = RulesUtility.ExtractSquareGroupFromGameBoard(gameState.GameBoard, 2, 2);

            //Assert      
            string expectedTopLeft = @"
123
456
789
";
            string expectedTopMiddle = @"
456
789
123
";
            string expectedTopRight = @"
789
123
456
";
            string expectedBottomRight = @"
891
567
234
";
            Assert.AreEqual(Utility.TrimNewLines(expectedTopLeft),
                Utility.TrimNewLines(RulesUtility.ConvertGameBoardToString(topLeft).Replace(".", "0")));
            Assert.AreEqual(Utility.TrimNewLines(expectedTopMiddle),
                Utility.TrimNewLines(RulesUtility.ConvertGameBoardToString(topMiddle).Replace(".", "0")));
            Assert.AreEqual(Utility.TrimNewLines(expectedTopRight),
                Utility.TrimNewLines(RulesUtility.ConvertGameBoardToString(topRight).Replace(".", "0")));
            Assert.AreEqual(Utility.TrimNewLines(expectedBottomRight),
                Utility.TrimNewLines(RulesUtility.ConvertGameBoardToString(bottomRight).Replace(".", "0")));
        }



        [TestMethod]
        public void SquareGroupInsertationRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
.........
.........
.........
912345678
678912345
345678912
234567...
891234...
567891...
";
            int[,] topLeft = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };
            int[,] topMiddle = new int[3, 3] { { 4, 7, 1 }, { 5, 8, 2 }, { 6, 9, 3 } };
            int[,] topRight = new int[3, 3] { { 7, 1, 4 }, { 8, 2, 5 }, { 9, 3, 6 } };
            int[,] bottomRight = new int[3, 3] { { 8, 5, 2 }, { 9, 6, 3 }, { 1, 7, 4 } };

            //Act
            gameState.LoadGame(game);
            gameState.GameBoard = RulesUtility.InsertSquareGroupIntoGameBoard(gameState.GameBoard, topLeft, 0, 0);
            gameState.ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(gameState.GameBoard).Replace("0", ".");
            gameState.GameBoard = RulesUtility.InsertSquareGroupIntoGameBoard(gameState.GameBoard, topMiddle, 1, 0);
            gameState.ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(gameState.GameBoard).Replace("0", ".");
            gameState.GameBoard = RulesUtility.InsertSquareGroupIntoGameBoard(gameState.GameBoard, topRight, 2, 0);
            gameState.ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(gameState.GameBoard).Replace("0", ".");
            gameState.GameBoard = RulesUtility.InsertSquareGroupIntoGameBoard(gameState.GameBoard, bottomRight, 2, 2);
            gameState.ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(gameState.GameBoard).Replace("0", ".");

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
            Assert.AreEqual(Utility.TrimNewLines(expected), Utility.TrimNewLines(gameState.ProcessedGameBoardString));

        }

        [TestMethod]
        public void SquareGroupPossibilitiesExtractionRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
12345678.
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";
            HashSet<int>[,] expectedTopRight = new HashSet<int>[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    expectedTopRight[x, y] = new HashSet<int>();
                }
            }
            expectedTopRight[2, 0].Add(9);

            //Act
            gameState.LoadGame(game);
            gameState.ProcessRules(true, false, false, false, false, false, false);
            HashSet<int>[,] topRight = RulesUtility.ExtractSquareGroupFromGamePossibilities(gameState.GameBoardPossibilities, 2, 0);

            //Assert      
            Assert.IsTrue(expectedTopRight[2, 0].SetEquals(topRight[2, 0]));
        }


        [TestMethod]
        public void SquareGroupPossibilitiesInsertationRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
12345678.
456789123
789123456
912345678
678912345
345678912
234567891
891234567
567891234
";
            HashSet<int>[,] expectedTopRight = new HashSet<int>[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    expectedTopRight[x, y] = new HashSet<int>();
                }
            }
            expectedTopRight[2, 0].Add(9);
            HashSet<int>[,] topRight = new HashSet<int>[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    topRight[x, y] = new HashSet<int>();
                }
            }
            topRight[2, 0].Add(9);

            //Act
            gameState.LoadGame(game);
            gameState.ProcessRules(true, false, false, false, false, false, false);
            gameState.GameBoardPossibilities = RulesUtility.InsertSquareGroupIntoGamePossibilities(gameState.GameBoardPossibilities, topRight, 0, 0);

            //Assert
            Assert.IsTrue(expectedTopRight[2, 0].SetEquals(topRight[2, 0]));
            Assert.IsTrue(expectedTopRight[2, 0].SetEquals(gameState.GameBoardPossibilities[2, 0]));
        }


        [TestMethod]
        public void GetNthElementTests()
        {
            //Arrange
            HashSet<int> emptyItemSet = new HashSet<int>();
            HashSet<int> threeItemSet = new HashSet<int>
            {
                1,
                2,
                3
            };

            //Act
            int emptyItem1 = RulesUtility.GetNthElement(emptyItemSet, 1);
            int threeItem1 = RulesUtility.GetNthElement(threeItemSet, 1);
            int threeItem2 = RulesUtility.GetNthElement(threeItemSet, 2);
            int threeItem3 = RulesUtility.GetNthElement(threeItemSet, 3);

            //Assert
            Assert.AreEqual(0, emptyItem1);
            Assert.AreEqual(1, threeItem1);
            Assert.AreEqual(2, threeItem2);
            Assert.AreEqual(3, threeItem3);
        }

    }
}
