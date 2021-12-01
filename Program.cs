using AdventOfCode2021.Days;
using System;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2021");
            var input = -1;
            while (input == -1)
            {
                input = AskForDay();
                switch (input)
                {
                    case 1:
                        var report = new DayOne();
                        report.Process();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid day. Please try again.");
                        break;
                }
                input = -1;
            }
        }

        static int AskForDay()
        {
            Console.WriteLine("Please enter in a day (1-25), 0 to exit:");
            return ParseInput(Console.ReadLine());
        }

        static int ParseInput(string input)
        {
            var isValid = int.TryParse(input, out var number);
            if (!isValid)
                return -1;
            return number;
        }
    }
}
