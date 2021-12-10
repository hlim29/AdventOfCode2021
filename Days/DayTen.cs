using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayTen
    {
        private readonly string[] _input;
        private readonly char[] _closingBrackets;
        private readonly List<string> _openingClosingPatterns;

        public DayTen()
        {
            _input = Common.ReadFile("dayten.txt");
            _closingBrackets = new char[] { '}', ')', ']', '>' };
            _openingClosingPatterns = new List<string> { "{}", "()", "[]", "<>" };
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetIllegalSum()}");
            Console.WriteLine($"Part 2: {GetMiddleCompletionScore()}");
        }

        public int GetIllegalSum()
        {
            return RemovePatterns().Select(x => x.FirstOrDefault(y => _closingBrackets.Contains(y))).Select(x => GetIllegalValue(x)).Sum();
        }

        public long GetMiddleCompletionScore()
        {
            var scores = new List<long>();
            var sanitised = RemovePatterns();

            foreach (var line in sanitised)
            {
                var hasIllegal = line.Any(x => _closingBrackets.Contains(x));
                if (!hasIllegal)
                {
                    long total = 0;
                    for (int j = line.Length - 1; j >= 0; j--)
                    {
                        total *= 5;
                        total += GetClosingValue(line[j]);
                    }
                    scores.Add(total);
                }
            }

            return scores.OrderBy(x => x).ToArray()[scores.Count / 2];
        }

        private List<string> RemovePatterns()
        {
            var output = new List<string>();
            for (int i = 0; i < _input.Length; i++)
            {
                var input = _input[i];
                while (_openingClosingPatterns.Any(x => input.Contains(x)))
                {
                    _openingClosingPatterns.ForEach(y =>
                    {
                        input = input.Replace(y, "");
                    });
                }
                output.Add(input);
            }
            return output;
        }

        private int GetIllegalValue(char input)
        {
            return input switch
            {
                '}' => 1197,
                ')' => 3,
                ']' => 57,
                '>' => 25137,
                _ => 0
            };
        }

        private int GetClosingValue(char input)
        {
            return input switch
            {
                '{' => 3,
                '(' => 1,
                '[' => 2,
                '<' => 4,
                _ => 1
            };
        }
    }
}
