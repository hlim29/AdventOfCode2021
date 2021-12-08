using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayEight
    {
        private readonly List<(string[] Cipher, string[] Output)> _input;

        public DayEight()
        {
            _input = Common.ReadFile("dayeight.txt").Select(x => (Left: x.Split('|')[0].Split(' '), Right: x.Split('|')[1].Split(' '))).ToList();

        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {CountDistinctDigits()}");
            Console.WriteLine($"Part 2: {GetOutputSum()}");
        }

        public int CountDistinctDigits()
        {
            var uniques = new int[] { 2, 3, 4, 7 };
            return _input.Sum(x => x.Output.Count(r => uniques.Contains(r.Length)));
        }

        private int GetOutputSum()
        {
            return _input.Sum(x => GetOutput(x));
        }

        private int GetOutput((string[] Cipher, string[] Output) input)
        {
            var patterns = input.Cipher;
            var one = patterns.Single(x => x.Length == 2);
            var four = patterns.Single(x => x.Length == 4);
            var seven = patterns.Single(x => x.Length == 3);
            var eight = patterns.Single(x => x.Length == 7);

            var three = patterns.Single(x => x.Length == 5 && one.All(x.Contains));
            var five = patterns.Single(x => x.Length == 5 && four.Except(one).All(x.Contains));
            var two = patterns.Single(x => x.Length == 5 && !one.All(x.Contains) && !four.Except(one).All(x.Contains));

            var nine = patterns.Single(x => x.Length == 6 && four.All(x.Contains) && five.All(x.Contains));
            var zero = patterns.Single(x => x.Length == 6 && !five.All(x.Contains) && !four.All(x.Contains));
            var six = patterns.Single(x => x.Length == 6 && five.All(x.Contains) && !four.All(x.Contains));

            var cipher = new List<string> { zero, one, two, three, four, five, six, seven, eight, nine };

            var digits = input.Output.Where(x => x.Length > 0).Select(x => cipher.IndexOf(cipher.First(y => y.All(x.Contains) && x.Length == y.Length))).ToList();
            return int.Parse(string.Join("", digits));
        }
    }
}
