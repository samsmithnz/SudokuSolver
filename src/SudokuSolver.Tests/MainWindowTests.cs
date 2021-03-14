using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using System;

namespace SudokuSolver.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class MainWindowTests
    {

        [TestMethod]
        public void PopulatingMainWindowSquaresTest()
        {
            //Arrange
            string[] result = new string[27 + 27 + 27];

            //Act;
            for (int i = 0; i < result.Length; i++)
            {
                int xBase; //= IF(C56 >= 54, INT(INT(((C56 - 54) / 9)) * 3), IF(C56 >= 27, INT(INT(((C56 - 27) / 9)) * 3), INT(C56 / 9) * 3))
                if (i >= 54)
                {
                    xBase = (int)Math.Truncate(Math.Truncate((((decimal)i - 54m) / 9m)) * 3m);
                }
                else if (i >= 27)
                {
                    xBase = (int)Math.Truncate(Math.Truncate((((decimal)i - 27m) / 9m)) * 3m);
                }
                else
                {
                    xBase = (int)Math.Truncate((decimal)i / 9m) * 3;
                }
                int yBase = (int)Math.Truncate((decimal)i / 27m) * 3; //Math.Truncate(i/27)*3
                int x = xBase + (i % 3); // xBase + (i % 3)
                int y = yBase + (int)Math.Truncate((((decimal)i / 3m) % 3m)); // yBase + Math.Truncate(=((i/3) %3))
                result[i] = x.ToString() + "," + y.ToString();
            }

            //Assert
            TestPositions(result);
        }

        private static void TestPositions(string[] result)
        {
            Assert.AreEqual("0,0", result[0]);
            Assert.AreEqual("1,0", result[1]);
            Assert.AreEqual("2,0", result[2]);
            Assert.AreEqual("0,1", result[3]);
            Assert.AreEqual("1,1", result[4]);
            Assert.AreEqual("2,1", result[5]);
            Assert.AreEqual("0,2", result[6]);
            Assert.AreEqual("1,2", result[7]);
            Assert.AreEqual("2,2", result[8]);

            Assert.AreEqual("3,0", result[9]);
            Assert.AreEqual("4,0", result[10]);
            Assert.AreEqual("5,0", result[11]);
            Assert.AreEqual("3,1", result[12]);
            Assert.AreEqual("4,1", result[13]);
            Assert.AreEqual("5,1", result[14]);
            Assert.AreEqual("3,2", result[15]);
            Assert.AreEqual("4,2", result[16]);
            Assert.AreEqual("5,2", result[17]);

            Assert.AreEqual("6,0", result[18]);
            Assert.AreEqual("7,0", result[19]);
            Assert.AreEqual("8,0", result[20]);
            Assert.AreEqual("6,1", result[21]);
            Assert.AreEqual("7,1", result[22]);
            Assert.AreEqual("8,1", result[23]);
            Assert.AreEqual("6,2", result[24]);
            Assert.AreEqual("7,2", result[25]);
            Assert.AreEqual("8,2", result[26]);

            Assert.AreEqual("0,3", result[27]);
            Assert.AreEqual("1,3", result[28]);
            Assert.AreEqual("2,3", result[29]);
            Assert.AreEqual("0,4", result[30]);
            Assert.AreEqual("1,4", result[31]);
            Assert.AreEqual("2,4", result[32]);
            Assert.AreEqual("0,5", result[33]);
            Assert.AreEqual("1,5", result[34]);
            Assert.AreEqual("2,5", result[35]);
            Assert.AreEqual("3,3", result[36]);
            Assert.AreEqual("4,3", result[37]);
            Assert.AreEqual("5,3", result[38]);
            Assert.AreEqual("3,4", result[39]);
            Assert.AreEqual("4,4", result[40]);
            Assert.AreEqual("5,4", result[41]);
            Assert.AreEqual("3,5", result[42]);
            Assert.AreEqual("4,5", result[43]);
            Assert.AreEqual("5,5", result[44]);
            Assert.AreEqual("6,3", result[45]);
            Assert.AreEqual("7,3", result[46]);
            Assert.AreEqual("8,3", result[47]);
            Assert.AreEqual("6,4", result[48]);
            Assert.AreEqual("7,4", result[49]);
            Assert.AreEqual("8,4", result[50]);
            Assert.AreEqual("6,5", result[51]);
            Assert.AreEqual("7,5", result[52]);
            Assert.AreEqual("8,5", result[53]);

            Assert.AreEqual("0,6", result[54]);
            Assert.AreEqual("1,6", result[55]);
            Assert.AreEqual("2,6", result[56]);
            Assert.AreEqual("0,7", result[57]);
            Assert.AreEqual("1,7", result[58]);
            Assert.AreEqual("2,7", result[59]);
            Assert.AreEqual("0,8", result[60]);
            Assert.AreEqual("1,8", result[61]);
            Assert.AreEqual("2,8", result[62]);
            Assert.AreEqual("3,6", result[63]);
            Assert.AreEqual("4,6", result[64]);
            Assert.AreEqual("5,6", result[65]);
            Assert.AreEqual("3,7", result[66]);
            Assert.AreEqual("4,7", result[67]);
            Assert.AreEqual("5,7", result[68]);
            Assert.AreEqual("3,8", result[69]);
            Assert.AreEqual("4,8", result[70]);
            Assert.AreEqual("5,8", result[71]);
            Assert.AreEqual("6,6", result[72]);
            Assert.AreEqual("7,6", result[73]);
            Assert.AreEqual("8,6", result[74]);
            Assert.AreEqual("6,7", result[75]);
            Assert.AreEqual("7,7", result[76]);
            Assert.AreEqual("8,7", result[77]);
            Assert.AreEqual("6,8", result[78]);
            Assert.AreEqual("7,8", result[79]);
            Assert.AreEqual("8,8", result[80]);

        }

    }
}
