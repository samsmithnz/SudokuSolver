using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System;
using System.IO;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GameFolderTests
    {

        [TestMethod]
        public void ProcessFolderOfYAMLTest()
        {
            //Arrange
            string sourceFolder = Path.Combine(Directory.GetCurrentDirectory(), "games");
            string[] files = Directory.GetFiles(sourceFolder);
            GameState gameState = new GameState();

            //Act            
            foreach (string path in files) //process every game file in the folder
            {
                //Open the file and read out the game
                string game = File.ReadAllText(path);
                //Load the game
                gameState.LoadGame(game);
                //Solve the game
                int solvedSquares = gameState.SolveGame(true, true, true, true, true, true);
                //Check that it was solved.
                switch (gameState.GameLevel)
                {
                    case "Easy":
                    case "Medium":
                        Assert.AreEqual(0, gameState.UnsolvedSquareCount);
                        break;
                    //case "Hard":
                    //    if (solvedSquares >= 35)
                    //    {
                    //        Assert.AreEqual(solvedSquares, path);
                    //    }
                    //    Assert.IsTrue(solvedSquares >= 0);
                    //    Assert.IsTrue(solvedSquares <= 35);
                    //    Assert.IsTrue(gameState.UnsolvedSquareCount > 0);
                    //    break;
                    //case "Very Hard":
                    //    if (solvedSquares >= 5)
                    //    {
                    //        Assert.AreEqual(solvedSquares, path);
                    //    }
                    //    Assert.IsTrue(solvedSquares >= 0);
                    //    Assert.IsTrue(solvedSquares <= 5);
                    //    Assert.IsTrue(gameState.UnsolvedSquareCount > 0);
                    //    break;
                    //default:
                    //    throw new Exception("Unknown game: " + path);
                    //    //Assert.IsTrue(gameState.GameLevel == "");
                    //    //break;
                }
            }

            ////Assert
            ////TODO: Solve roadblocks with the "FilesToIgnore"
            ////Check if any errors were detected
            //Assert.AreEqual(null, comments.FirstOrDefault(s => s.Contains("#Error:")));
            ////Check that the remaining comments equals what we expect
            //Assert.AreEqual(28, comments.Count);
        }

    }
}
