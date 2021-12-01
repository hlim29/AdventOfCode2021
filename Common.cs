using System.IO;

namespace AdventOfCode2021
{
    public class Common
    {
        public static string[] ReadFile(string filename)
        {
            return File.ReadAllLines(@$"Inputs/{filename}");
        }

        public static string ReadRaw(string filename)
        {
            return File.ReadAllText(@$"Inputs/{filename}");
        }
    }
}
