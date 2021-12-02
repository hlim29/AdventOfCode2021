using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayTwo
    {
        private readonly List<(string Action, int Length)> _moves;

        public DayTwo()
        {
            _moves = Common.ReadFile("daytwo.txt").Select(x => x.Split(' ')).Select(x => ( Action: x[0], Length: int.Parse(x[1]))).ToList();
        }

        public void Process()
        {
            var timer = new Stopwatch();
            timer.Start();
            Console.WriteLine($"Part 1: {ProcessMove()}");
            Console.WriteLine($"Part 1: {timer.ElapsedMilliseconds} ms");
            timer.Restart();
            Console.WriteLine($"Part 2: {ProcessAim()}");
            Console.WriteLine($"Part 2: {timer.ElapsedMilliseconds} ms");
        }

        private int ProcessMove()
        {
            var horizontal = _moves.Where(x => x.Action == "forward").Sum(x => x.Length);
            var depth = _moves.Where(x => x.Action == "down").Sum(x => x.Length);
            depth -= _moves.Where(x => x.Action == "up").Sum(x => x.Length);

            return depth * horizontal;
        }

        private int ProcessAim()
        {
            var depth = 0;
            var horizontal = 0;
            var aim = 0;

            _moves.ForEach(x =>
            {
                switch (x.Action)
                {
                    case "forward":
                        horizontal += x.Length;
                        depth += x.Length * aim;
                        break;
                    case "down":
                        aim += x.Length;
                        break;
                    case "up":
                        aim -= x.Length;
                        break;
                }
            });

            return depth * horizontal;
        }
    }
}
