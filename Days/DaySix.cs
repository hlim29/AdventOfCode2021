using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DaySix
    {
        private Dictionary<int, long> _input;

        public DaySix()
        {
            _input = Common.ReadFile("daysix.txt")[0].Split(',').Select(x => int.Parse(x)).GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());  
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetFishAtDays(80)}");
            Console.WriteLine($"Part 2: {GetFishAtDays(256)}");
        }

        public long GetFishAtDays(int days)
        {
            var fish = new Dictionary<int, long>(_input);

            Enumerable.Range(0, 9).Where(x => !_input.Keys.Contains(x)).ToList().ForEach(x =>
            {
                fish[x] = 0;
            });

            for (var d = 0; d < days; d++)
            {
                for (var i = 0; i <= 8; i++)
                {
                    fish[i - 1] = fish[i];
                }

                fish[6] += fish[-1];
                fish[8] = fish[-1];
                fish.Remove(-1);
            }

            return fish.Values.Sum();
        }
    }
}
