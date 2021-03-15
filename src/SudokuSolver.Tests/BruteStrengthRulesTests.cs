using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class BruteStrengthRulesTests
    {
        [TestMethod]
        public void BruteStrengthHard1GameRuleTest()
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
.3.8.2..1
";

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true,
                true, true,
                true,
                true);

            //Assert
            string expected = @"
864123759
319457826
725698413
693284175
258719364
471365982
147536298
582941637
936872541
";

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(true, gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(51, squaresSolved);
        }

        [TestMethod]
        public void BruteStrengthHard2GameRuleTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
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

            //Act
            gameState.LoadGame(game);
            int squaresSolved = gameState.SolveGame(true, true, true,
                true, true,
                true,
                true);

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
            Assert.AreEqual(true, gameState.CrossCheckSuccessful);
            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(22, squaresSolved);
        }


    }
}
