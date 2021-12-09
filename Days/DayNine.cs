using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayNine
    {
        private readonly Dictionary<(int X, int Y), int> _input;
        private readonly List<(int X, int Y)> _offsets;

        public DayNine()
        {
            _input = Common.ReadFile("daynine.txt").SelectMany((x,i) => x.Select((y,j) => (j,i,y))).ToDictionary(x => (x.j, x.i), x => x.y - '0');
            _offsets = new List<(int X, int Y)> { (-1, 0), (0, 1), (0, -1), (1, 0) };
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetLowPointSum()}");
            Console.WriteLine($"Part 2: {GetTopThreeBasinAggregate()}");
        }

        public int GetLowPointSum()
        {
            return GetLowPoints().Select(x => _input[x] + 1).Sum();
        }

        private List<(int X, int Y)> GetLowPoints()
        {
            var lowPoints = new List<(int X, int Y)>();
            foreach (var coord in _input.Keys)
            {
                var neighbours = _offsets.Select(x => (X: x.X + coord.X, Y: x.Y + coord.Y)).Where(x => _input.ContainsKey(x)).ToList();
                if (neighbours.All(x => _input[coord] < _input[x]))
                {
                    lowPoints.Add(coord);
                }
            }
            return lowPoints;
        }

        public int GetTopThreeBasinAggregate()
        {
            var lowPoints = GetLowPoints();
            return lowPoints.Select(x => GetBasinSize(x)).OrderByDescending(x => x).Take(3).Aggregate((x,y) => x*y);
        }

        private int GetBasinSize((int X, int Y) coord)
        {
            var queue = new Queue<(int X, int Y)>(new[] {coord});
            var visited = new List<(int X, int Y)>();

            while (queue.Count() > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current);
                var neighbours = _offsets.Select(x => (X: x.X + current.X, Y: x.Y + current.Y)).Where(x => _input.ContainsKey(x) && _input[x] != 9 && !visited.Contains(x) && !queue.Contains(x)).ToList();
                neighbours.ForEach(x => queue.Enqueue(x));
            }
            return visited.Count();
        }
    }
}
