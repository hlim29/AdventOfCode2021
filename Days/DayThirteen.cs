using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayThirteen
    {
        private HashSet<(int X, int Y)> _dots;
        private readonly List<(string Axis, int Line)> _foldInstructions;
        public DayThirteen()
        {
            _dots = Common.ReadRaw("daythirteen.txt").Split("\r\n\r\n")[0].Split("\r\n").Select(x =>
            (X: int.Parse(x.Split(',')[0]), Y: int.Parse(x.Split(',')[1]))).ToHashSet();

            var instructions = Common.ReadRaw("daythirteen.txt").Replace("fold along ", "").Split("\r\n\r\n")[1].Split("\r\n").ToList();
            _foldInstructions = instructions.Select(x => (x.Split("=")[0], int.Parse(x.Split("=")[1]))).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {FoldFirst()}");
            Console.WriteLine($"Part 2: ");
            FoldAllThenPrint();
        }

        public int FoldFirst()
        {
            return Fold(_foldInstructions[0].Axis, _foldInstructions[0].Line, _dots).Count;
        }

        public void FoldAllThenPrint()
        {
            var paper = _dots;
            _foldInstructions.ForEach(x => paper = Fold(x.Axis, x.Line, paper));

            var maxX = paper.Max(x => x.X);
            var maxY = paper.Max(x => x.Y);

            for (var y = 0; y <= maxY; y++)
            {
                var buffer = string.Empty;
                for (var x = 0; x <= maxX; x++)
                {
                    buffer += paper.Contains((x, y)) ? "#" : ".";
                }
                Console.WriteLine(buffer);
            }
        }

        private static HashSet<(int X, int Y)> Fold(string axis, int line, HashSet<(int X, int Y)> paper)
        {
            if (axis == "x")
            {
                paper.Where(x => x.X > line).ToList().ForEach(x =>
                {
                    var newX = x.X - (x.X - line) * 2;
                    paper.Add((newX, x.Y));
                    paper.Remove(x);
                });
            }
            else
            {
                paper.Where(x => x.Y > line).ToList().ForEach(x =>
                {
                    var newY = x.Y - (x.Y - line) * 2;
                    paper.Add((x.X, newY));
                    paper.Remove(x);
                });
            }
            return paper;
        }
    }
}
