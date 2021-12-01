using System;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayOne
    {
        private readonly int[] _entries;

        public DayOne()
        {
            _entries = Common.ReadFile("dayone.txt").Select(x => int.Parse(x)).ToArray();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetIncreases()}");
            Console.WriteLine($"Part 2: {GetGroupedIncreases()}");
        }

        private int GetIncreases()
        {
            return GetIncreases(_entries);
        }

        private int GetIncreases(int[] entries)
        {
            return entries.Select((x, i) => i < entries.Length - 1 && entries[i+1] > entries[i]).Count(x => x);
        }

        private int GetGroupedIncreases()
        {
            return GetIncreases(_entries.Select((x, i) => i < _entries.Length - 2 ? _entries[i..(i + 3)].Sum() : 0).ToArray());
        }
    }
}
