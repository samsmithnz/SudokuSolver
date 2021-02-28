using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SolveGameTests
    {

        [TestMethod]
        public void SolveEasyGameTest()
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
            int squaresSolved = gameState.SolveGame();

            //Assert
            string expected = @"
278145693
354698719
916273485
692817354
837564122
145329876
823751968
581936247
769482531
";

            Assert.AreEqual(0, gameState.UnsolvedSquareCount);
            Assert.AreEqual(43, squaresSolved);
            Assert.AreEqual(Utility.TrimNewLines(expected), gameState.OutputState());
            Assert.AreEqual(6, gameState.IterationsToSolve);
        }

    }
}
