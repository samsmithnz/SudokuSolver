using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void EmptySquareTest()
        {
            //Arrange
            Square square = new Square();

            //Act

            //Assert
            Assert.AreEqual(0, square.CurrentSquare);
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(i + 1, square.PossibleSquares[i]);
            }
        }

        [TestMethod]
        public void PopulatedSquareTest()
        {
            //Arrange
            int currentSquare = 3;
            Square square = new Square(currentSquare);

            //Act

            //Assert
            Assert.AreEqual(currentSquare, square.CurrentSquare);
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(0, square.PossibleSquares[i]);
            }
        }

        [TestMethod]
        public void PossibleSquareTest()
        {
            //Arrange
            Square square = new Square();

            //Act
            for (int i = 0; i < 9; i++)
            {
                square.PossibleSquares[i] = 0;
            }
            square.PossibleSquares[2] = 3;
            square.PossibleSquares[8] = 9;

            //Assert
            Assert.AreEqual(0, square.CurrentSquare);
            string expected = @"
..3
...
..9
";
            Assert.AreEqual(Utility.TrimNewLines(expected), square.ToString());
        }


    }
}
