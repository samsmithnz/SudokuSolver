using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void SimpleRowExampleGameTest()
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

            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(1, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), Utility.TrimNewLines(gameState.OutputState()));
            //Assert.AreEqual(Utility.TrimNewLines(expectedSquare0), gameState.SquareGroups[0].ToString());
        }


        [TestMethod]
        public void SimpleColumnExampleGameTest()
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

            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            //Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(gameState.GameBoardPossibilities[2, 0]));
            //Assert.AreEqual(1, gameState.GameBoardPossibilities[2, 0].Count);
            Assert.AreEqual(1, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), Utility.TrimNewLines(gameState.OutputState()));
            //Assert.AreEqual(Utility.TrimNewLines(expectedSquare0), gameState.SquareGroups[0].ToString());
        }

        [TestMethod]
        public void EasyGameTest()
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
            int squaresSolved = gameState.ProcessRules(true, true);

            //Assert
            string expected = @"
27.1.5.93
354...71.
9162.3.8.
6928.73.4
.........
1453.98.6
.2.7.1.6.
581..624.
7..4.2..1
";

            Assert.AreEqual(36, gameState.UnsolvedSquareCount);
            Assert.AreEqual(5, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(gameState.OutputState()), Utility.TrimNewLines(expected));
        }

        [TestMethod]
        public void GameNotLoadedTest()
        {
            //Arrange
            GameState gameState = new GameState();

            //Act
            try
            {
                gameState.ProcessRules();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex.Message == "Game not loaded");
            }

        }



    }
}
