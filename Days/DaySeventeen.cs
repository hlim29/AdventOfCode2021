using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Days
{
    public class DaySeventeen
    {
        private readonly (int Lower, int Upper) _xBounds;
        private readonly (int Lower, int Upper) _yBounds;

        public DaySeventeen()
        {
            var regex = new Regex(@"target area: x=(?<xLower>[\-\d]+)..(?<xUpper>[\-\d]+), y=(?<yLower>[\-\d]+)..(?<yUpper>[\-\d]+)");
            var input = Common.ReadRaw("dayseventeen.txt");
            var regexMatch = regex.Matches(input).First();
            _xBounds = (int.Parse(regexMatch.Groups["xLower"].Value), int.Parse(regexMatch.Groups["xUpper"].Value));
            _yBounds = (int.Parse(regexMatch.Groups["yLower"].Value), int.Parse(regexMatch.Groups["yUpper"].Value));
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            return GetLandingTrajectories().Max(x => x.Result.MaxY);
        }

        public int PartTwo()
        {
            return GetLandingTrajectories().Count;
        }

        private List<(int X, int Y, (int MaxY, bool DidLand) Result)> GetLandingTrajectories()
        {
            List<(int X, int Y, (int MaxY, bool DidLand) Result)> fires = new();
            Enumerable.Range(0, 1000).ToList().ForEach(x =>
            {
                Enumerable.Range(-1000, 2000).ToList().ForEach(y =>
                {
                    fires.Add((x, y, Fire(x, y)));
                });
            });
            return fires.Where(x => x.Result.DidLand).ToList();
        }

        public (int MaxY, bool DidLand) Fire(int xVelocity, int yVelocity)
        {
            var current = (X: 0, Y: 0);
            var maxY = 0;
            while (true)
            {
                current.X += xVelocity;
                current.Y += yVelocity;

                xVelocity = Math.Max(0, xVelocity-1);
                yVelocity--;
                maxY = Math.Max(maxY, current.Y);   
                if (current.X >= _xBounds.Lower && current.X <= _xBounds.Upper && current.Y >= _yBounds.Lower && current.Y <= _yBounds.Upper)
                {
                    return (maxY, true);
                }
                if (current.X > _xBounds.Upper || current.Y < _yBounds.Lower)
                {
                    return (maxY, false);
                }
            }
        }
    }
}
