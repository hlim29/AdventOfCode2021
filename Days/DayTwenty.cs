using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayTwenty
    {
        private readonly string _algorithm;
        private Dictionary<(int X, int Y), bool> _grid;
        private Dictionary<(int X, int Y), bool> _original;
        private List<(int X, int Y)> _neighbourOffsets;

        public DayTwenty()
        {
            var input = Common.ReadFile("daytwenty.txt").ToArray();
            _algorithm = input[0];
            _grid = input[2..].SelectMany((x, i) => x.Select((y, j) => (j, i, y))).ToDictionary(x => (x.j, x.i), x => x.y == '#');
            _original = _grid;
            _neighbourOffsets = new List<(int X, int Y)> { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1), };
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {IterateTwice()}");
            Console.WriteLine($"Part 2: {IterateFifty()}");
        }

        public int IterateTwice()
        {
            Enumerable.Range(0, 2).ToList().ForEach(x => Expand());
            return _grid.Count(x => x.Value);
        }

        public int IterateFifty()
        {
            _grid = _original;
            Enumerable.Range(0, 50).ToList().ForEach(x => Expand());
            return _grid.Count(x => x.Value);

        }

        private void Expand()
        {
            var minX = _grid.Keys.Min(x => x.X);
            var minY = _grid.Keys.Min(x => x.Y);
            var maxX = _grid.Keys.Max(x => x.X);
            var maxY = _grid.Keys.Max(x => x.Y);

            Dictionary<(int X, int Y), bool> newGrid = new();

            for (int y = minY - 1; y <= maxY + 1; y++)
            {
                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    var miniGrid = _neighbourOffsets.Select(z => (z.X + x, z.Y + y)).ToList();
                    var binary = new string(miniGrid.Select(z => IsLit(z, minX+1) ? '1' : '0').ToArray());
                    newGrid[(x, y)] = _algorithm[Convert.ToInt32(binary, 2)] == '#';
                }
            }

            _grid = newGrid;
        }

        private bool IsLit((int X, int Y) coord, int minX)
        {
            if (!_grid.ContainsKey(coord) && minX % 2 == 0 && _algorithm[0] == '#')
            {
                return true;
            }
            if (!_grid.ContainsKey(coord))
            {
                return false;
            }
            return _grid[coord];
        }
    }
}
