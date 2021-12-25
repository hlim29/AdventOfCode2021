using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayTwentyFive
    {
        private Dictionary<(int X, int Y), char> _grid;
        private readonly int _gridYMax;
        private readonly int _gridXMax;

        public DayTwentyFive()
        {
            var input = Common.ReadFile("daytwentyfive.txt").ToList();
            _grid = input.SelectMany((x, i) => x.Select((y, j) => (j, i, y))).ToDictionary(x => (x.j, x.i), x => x.y);
            _gridYMax = input.Count;
            _gridXMax = input[0].Length;
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
        }

        public int PartOne()
        {
            for (var i = 1; i < int.MaxValue; i++)
            {
                var moveX = Move(true);
                var moveY = Move(false);

                if (moveX && moveY)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool Move(bool isX)
        {
            var direction = isX ? '>' : 'v';
            var toMove = _grid.Keys.Where(x => _grid[x] == direction).ToList();
            var moved = new List<(int X, int Y)>();
            toMove.ForEach(x =>
            {
                var newCoord = isX ? ((x.X + 1) % _gridXMax, x.Y) : (x.X, (x.Y + 1) % _gridYMax);
                if (_grid[newCoord] == '.')
                {
                    _grid[newCoord] = direction;
                    moved.Add(x);
                }
            });
            moved.ForEach(x => _grid[x] = '.');
            return moved.Count == 0;
        }
    }
}
