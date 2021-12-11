using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayEleven
    {
        private Dictionary<(int X, int Y), int> _grid;
        private Dictionary<(int X, int Y), int> _gridOriginal;
        private readonly List<(int X, int Y)> _offsets;

        public DayEleven()
        {
            _offsets = new List<(int X, int Y)> { (-1, 0), (-1,1), (0, 1), (1,1), (1, 0), (1,-1), (0, -1), (-1,-1)};
            _grid = Common.ReadFile("dayeleven.txt")
                .SelectMany((x,i) => x.Select((y,j) => (j,i,y))).ToDictionary(x => (x.j, x.i), x => x.y - '0');
            _gridOriginal = _grid.ToDictionary(x => x.Key, x => x.Value);
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {CountFlash(100)}");
            Console.WriteLine($"Part 2: {FirstFullFlashStep()}");
        }

        public int CountFlash(int iterations)
        {
            var sum = 0;
            for (int i = 0; i < iterations; i++)
            {
                sum += Iterate();
            }
            _grid = _gridOriginal.ToDictionary(x => x.Key, x => x.Value);
            return sum;
        }

        public int FirstFullFlashStep()
        {
            var iterations = 1;
            while (Iterate() < 100)
            {
                iterations++;   
            }
            return iterations;
        }

        private int Iterate()
        {
            var flashed = new HashSet<(int X, int Y)>();
            _grid.Keys.ToList().ForEach(x => _grid[x]++);
            var flashing = _grid.Keys.Where(x => _grid[x] >= 10).ToList();
            while (flashing.Count > 0)
            {
                flashed = flashed.Concat(flashing).ToHashSet();
                var flashNeighbours = flashing.SelectMany(x => _offsets.Select(o => (X: x.X + o.X, Y: x.Y + o.Y))).Where(x => _grid.ContainsKey(x)).ToList();
                flashNeighbours.ForEach(x => _grid[x]++);
                flashing = _grid.Keys.Where(x => _grid[x] >= 10 && !flashed.Contains(x)).ToList();
            }
            _grid.Keys.Where(x => _grid[x] > 9).ToList().ForEach(x => _grid[x] = 0);
            return flashed.Count;
        }
    }
}
