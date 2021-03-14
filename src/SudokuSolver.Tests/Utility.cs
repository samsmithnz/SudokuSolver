namespace SudokuSolver.Tests
{
    public class Utility
    {
        public static string TrimNewLines(string input)
        {
            //Trim off any leading or trailing new lines 
            input = input.TrimStart('\r', '\n');
            input = input.TrimEnd('\r', '\n');
            input = input.Trim();

            return input;
        }
    }
}
