using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            Traverse("start", new List<string>(), false);
            return _log.Distinct().ToList().Count;
        }

        public int PartTwo()
        {
            Traverse("start", new List<string>(), true);
            return _log.Distinct().ToList().Count;
        }

        private void Traverse(string current, List<string> visited, bool isPartTwo)
        {
            visited.Add(current);
            if (!_map.ContainsKey(current))
            {
                _log.Add(string.Join(",", visited));
                visited = new();
                return;
            }

            var multipleSmallVisits = visited.Any(x => x.ToLower() == x && visited.Count(y => y == x) == 2 );

            var next = multipleSmallVisits || !isPartTwo
                ? _map[current].Where(x => x.ToUpper() == x || x == "end" || !visited.Contains(x) && x != "start").ToList()
                : _map[current].Where(x => x != "start").ToList();

            foreach (var x in next)
            {
                Traverse(x, visited.ToList(), isPartTwo);
            }
        }
    }
}
