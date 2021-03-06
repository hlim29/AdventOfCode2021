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
                    case 2:
                        var dive = new DayTwo();
                        dive.Process();
                        break;
                    case 3:
                        var binary = new DayThree();
                        binary.Process();
                        break;
                    case 4:
                        var bingo = new DayFour();
                        bingo.Process();
                        break;
                    case 5:
                        var vents = new DayFive();
                        vents.Process();
                        break;
                    case 6:
                        var fishies = new DaySix();
                        fishies.Process();
                        break;
                    case 7:
                        var fuel = new DaySeven();
                        fuel.Process();
                        break;
                    case 8:
                        var segment = new DayEight();
                        segment.Process();
                        break;
                    case 9:
                        var basin = new DayNine();
                        basin.Process();
                        break;
                    case 10:
                        var brackets = new DayTen();
                        brackets.Process();
                        break;
                    case 11:
                        var flash = new DayEleven();
                        flash.Process();
                        break;
                    case 12:
                        var twelve = new DayTwelve();
                        twelve.Process();
                        break;
                    case 13:
                        var fold = new DayThirteen();
                        fold.Process();
                        break;
                    case 14:
                        var polymers = new DayFourteen();
                        polymers.Process();
                        break;
                    case 15:
                        var risk = new DayFifteen();
                        risk.Process();
                        break;
                    case 17:
                        var fire = new DaySeventeen();
                        fire.Process();
                        break;
                    case 20:
                        var pixels = new DayTwenty();
                        pixels.Process();
                        break;
                    case 21:
                        var twentyone = new DayTwentyOne();
                        twentyone.Process();
                        break;
                    case 25:
                        Console.WriteLine($"Merry Christmas!");
                        var christmas = new DayTwentyFive();
                        christmas.Process();
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
