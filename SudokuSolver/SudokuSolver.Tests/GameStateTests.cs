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
        public void GameNotLoadedTest()
        {
            //Arrange
            GameState gameState = new GameState();

            //Act
            try
            {
                //Calling process rules without loading a game first
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
