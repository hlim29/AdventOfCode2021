using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Days
{
    public class DayTwelve
    {
        private readonly List<string> _input;
        private List<string> _log;
        private readonly Dictionary<string, List<string>> _map = new();
        public DayTwelve()
        {
            _input = Common.ReadFile("daytwelve.txt").ToList();
            _log = new();
            _input.SelectMany(x => x.Split("-"))
                .Distinct().ToList().
                ForEach(letter =>
                {
                    _map.Add(letter, _input.Where(x => x.Split('-')[0] == letter || x.Split('-')[1] == letter).SelectMany(x => x.Split('-')).Where(x => x != letter).ToList());
                });
            _map.Remove("end");
            //var processedInput = _input.Select(x => (Start: x.Split('-')[0], End: x.Split('-')[1])).ToList();
            //var letters = processedInput.Select(x => x.Start).Concat(processedInput.Select(x => x.End)).Distinct().ToList();
            //letters.ForEach(x =>
            //{
            //    var a = processedInput.Where(y => x == y.End || x == y.Start).ToList();
            //    var list = new List<string>();
            //    a.ForEach(x =>
            //    {
            //        if (x.Start == )
            //        {
            //            list.Add(x.End);
            //        }
            //    });
            //});
            // _map = processedInput.ToDictionary(x => x.Start, x => processedInput.Where(y => y.Start == x.Start).Select(y=> y.End).ToList());    
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            Traverse("start", new List<string>());
            return _log.Count;
        }

        private void Traverse(string current, List<string> visited)
        {
            visited.Add(current);
            if (!_map.ContainsKey(current))
            {
                _log.Add(string.Join(",", visited));
                visited = new();
                return;
            }
            var next = _map[current].Where(x => x.ToUpper() == x || x == "end" || visited.Count(y => y == x) < 2).ToList();

            foreach(var x in next)
            {
                Traverse(x, visited.ToList());
            }
        }

        private List<string> GetNext(string current)
        {
            if (current == "end")
            {
                return new List<string>();
            }
            return _input.Where(x => x.Contains(current)).SelectMany(x => x.Split('-')).Where(x => x != current).ToList();
        }

        public int PartTwo()
        {
            return -1;
        }
    }
}
