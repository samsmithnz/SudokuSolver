using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;

namespace SudokuSolver.Tests
{
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
            Assert.IsNull(square.CurrentSquare);
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


    }
}
