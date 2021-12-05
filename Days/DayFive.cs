using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Vent
    {
        public (int X, int Y) Origin { get; set; }
        public (int X, int Y) Destination { get; set; }
    }

    public class DayFive
    {
        private readonly List<Vent> _vents;
        private Dictionary<(int X, int Y), int> _grid = new Dictionary<(int X, int Y), int>();

        public DayFive()
        {
            _vents = Common.ReadFile("dayfive.txt").Select(x =>
            {
                var arrowDelimited = x.Split(" -> ");
                return new Vent
                {
                    Origin = (int.Parse(arrowDelimited[0].Split(',')[0]), int.Parse(arrowDelimited[0].Split(',')[1])),
                    Destination = (int.Parse(arrowDelimited[1].Split(',')[0]), int.Parse(arrowDelimited[1].Split(',')[1]))
                };
            }).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetVentOverlaps(false)}");
            Console.WriteLine($"Part 2: {GetVentOverlaps(true)}");
        }

        public int GetVentOverlaps(bool isPartTwo)
        {
            _grid = new Dictionary<(int X, int Y), int>();
            foreach (var entry in _vents)
            {
                if (entry.Origin.X == entry.Destination.X || entry.Origin.Y == entry.Destination.Y)
                {
                    var xRange = GenerateRange(entry.Origin.X, entry.Destination.X);
                    var yRange = GenerateRange(entry.Origin.Y, entry.Destination.Y);

                    foreach (var x in xRange)
                    {
                        foreach (var y in yRange)
                        {
                            _grid.TryGetValue((x, y), out var currentCount);
                            currentCount++;
                            _grid[(x, y)] = currentCount;
                        }
                    }
                }

                if (isPartTwo)
                {
                    if (Math.Abs(entry.Origin.X - entry.Destination.X) == Math.Abs(entry.Origin.Y - entry.Destination.Y))
                    {
                        var xRange = GenerateRange(entry.Origin.X, entry.Destination.X);
                        var yRange = GenerateRange(entry.Origin.Y, entry.Destination.Y);

                        for (var i = 0; i < xRange.Count(); i++)
                        {
                            var currentCoordinate = (xRange[i], yRange[i]);
                            _grid.TryGetValue(currentCoordinate, out var currentCount);
                            currentCount++;
                            _grid[currentCoordinate] = currentCount;
                        }
                    }
                }
            }

            return _grid.Values.Where(x => x > 1).Count();
        }

        private List<int> GenerateRange(int x, int y)
        {
            var min = Math.Min(x, y);
            var max = Math.Max(x, y);

            var range = Enumerable.Range(min, max - min + 1).ToList();
            if (min == y)
            {
                range.Reverse();
            }
            return range;
        }
    }
}
