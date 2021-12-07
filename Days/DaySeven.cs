using System;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DaySeven
    {
        private readonly int[] _crabs;

        public DaySeven()
        {
            _crabs = Common.ReadRaw("dayseven.txt").Split(',').Select(x => int.Parse(x)).ToArray();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetMinFuel()}");
            Console.WriteLine($"Part 2: {GetMinFuelCumulative()}");
        }

        private int GetMinFuel()
        {
            return Enumerable.Range(_crabs.Min(), _crabs.Max()).Min(n => _crabs.Sum(x => Math.Abs(x - n)));
        }

        private int GetMinFuelCumulative()
        {
            return Enumerable.Range(_crabs.Min(), _crabs.Max()).Min(n => _crabs.Select(x => Math.Abs(x - n)).Sum(x => x * (x + 1) / 2));
        }
    }
}
