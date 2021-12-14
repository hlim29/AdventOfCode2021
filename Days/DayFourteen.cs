using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayFourteen
    {
        private string _seed;
        private readonly Dictionary<string, string> _instructions;

        public DayFourteen()
        {
            var input = Common.ReadFile("dayfourteen.txt").ToArray();
            _seed = input[0];
            _instructions = input[2..].Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1]);
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public long PartOne()
        {
            
            return Solve(10);
        }

        public long PartTwo()
        {
            return Solve(40);
        }

        private long Solve(int iterations)
        {
            var pairs = _seed.Select((x, i) => i < _seed.Length - 1 ? _seed[i..(i+2)] : string.Empty).Where(x => x != "").ToList();
            var pairCounts = pairs.Distinct().ToDictionary(x => x, x => (long)pairs.Count(y => y == x));

            var letterCounts = _seed.Distinct().ToDictionary(x => x.ToString(), x => (long)_seed.Count(y => y == x));

            Enumerable.Range(0, iterations).ToList().ForEach(_ =>
            {
                var newPairs = new Dictionary<string, long>();
                foreach(var currentPair in pairCounts.Keys.ToList())
                {
                    var currentPairCount = pairCounts[currentPair];
                    var insert = _instructions[currentPair];

                    var leftKey = currentPair[0] + insert;
                    var rightKey = insert + currentPair[1];

                    newPairs[leftKey] = newPairs.GetValueOrDefault(leftKey, 0) + currentPairCount;
                    newPairs[rightKey] = newPairs.GetValueOrDefault(rightKey, 0) +  currentPairCount;

                    letterCounts[insert] = letterCounts.GetValueOrDefault(insert, 0) + currentPairCount;
                }
                pairCounts = newPairs.ToDictionary(x => x.Key, x => x.Value);
            });

            return letterCounts.Max(x => x.Value) - letterCounts.Min(x => x.Value);
        }
    }
}
