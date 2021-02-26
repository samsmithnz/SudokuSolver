using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void EasyGameTest()
        {
            //Arrange
            GameState gameState = new GameState();
            string game = @"
#ARuud
#D A random puzzle created by SudoCue
#CJust start plugging in the numbers
#B03-08-2006
#SSudoCue
#LEasy
#Uhttp://www.sudocue.net/
#S3
2..1.5..3
.54...71.
.1.2.3.8.
6.28.73.4
.........
1.53.98.6
.2.7.1.6.
.81...24.
7..4.2..1
";

            //Act
            gameState.LoadGame(game);

            //Assert
            string expected = @"
2..1.5..3
.54...71.
.1.2.3.8.
6.28.73.4
.........
1.53.98.6
.2.7.1.6.
.81...24.
7..4.2..1
";
            string expectedSquare0 = @"
2..
.54
.1.
";
            Assert.IsTrue(gameState.UnsolvedSquares == 45);
            Assert.IsTrue(Utility.TrimNewLines(gameState.OutputState()) == Utility.TrimNewLines(expected));
            Assert.AreEqual(Utility.TrimNewLines(expectedSquare0), gameState.SquareGroups[0].ToString());
        }



    }
}
