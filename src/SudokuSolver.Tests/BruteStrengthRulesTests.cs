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
        public void BruteStrengthRuleTest()
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
            int squaresSolved = gameState.ProcessRules(false, false,
                false, false, false,
                true,
                true);

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

            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.ProcessedGameBoardString);
            Assert.AreEqual(22, gameState.UnsolvedSquareCount);
            Assert.AreEqual(0, squaresSolved);
        }


    }
}
