using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayFifteen
    {
        private Dictionary<(int X, int Y), int> _grid;
        private readonly List<(int X, int Y)> _offsets;
        private readonly (int X, int Y) _destination;

        public DayFifteen()
        {
            _grid = Common.ReadFile("dayfifteen.txt").SelectMany((x, i) => x.Select((y, j) => (j, i, y))).ToDictionary(x => (x.j, x.i), x => x.y - '0');
            _offsets = new List<(int X, int Y)> { (-1, 0), (0, 1),(1, 0),  (0, -1) };
            _destination = (Common.ReadFile("dayfifteen.txt")[0].Length - 1, Common.ReadFile("dayfifteen.txt").Length - 1);
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            var distances = _grid.Keys.ToDictionary(x => x, x => int.MaxValue);
            var unvisited = _grid.Keys.ToList();
            distances[(0, 0)] = 0;
            while (unvisited.Count > 0)
            {
                var minDist = unvisited.Min(y => distances[y]);
                var current = unvisited.First(x => distances[x] == minDist);
                unvisited.Remove(current);
                _offsets.Select(x => (X: x.X + current.X, Y: x.Y + current.Y)).Where(x => unvisited.Contains(x)).ToList()
                    .ForEach(v =>
                {
                    var alt = distances[current] + _grid[v];
                    if (alt < distances[v])
                    {
                        distances[v] = alt;
                    }
                });
            }

            return distances[_destination];
        }

        public int PartTwo()
        {
            return -1;
        }
    }
}
