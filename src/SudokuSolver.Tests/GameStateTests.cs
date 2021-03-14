using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GameStateTests
    {

        [TestMethod]
        public void GameNotLoadedTest()
        {
            //Arrange
            GameState gameState = new GameState();

            //Act
            try
            {
                //Calling process rules without loading a game first
                gameState.ProcessRules(false, false, false, false, false, false, false);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("Game not loaded", ex.Message);
            }

        }

    }
}
