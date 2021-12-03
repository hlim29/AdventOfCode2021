using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayThree
    {
        private readonly List<string> _entries;

        public DayThree()
        {
            _entries = Common.ReadFile("daythree.txt").ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetPowerConsumption()}");
            Console.WriteLine($"Part 2: {GetLifeSupportRating()}");
        }

        public int GetPowerConsumption()
        {
            var digitPositions = _entries.SelectMany(x => x.Select((y,i) => (Digit: y, Position: i))).ToList();

            var buffer = string.Empty;
            for (int i = 0; i < _entries[0].Length; i++)
            {
                buffer += digitPositions.Where(x => x.Position == i).GroupBy(x => x.Digit).OrderByDescending(x => x.Count()).First().Key;
            }

            var common = Convert.ToInt32(buffer, 2);
            var leastCommon = Convert.ToInt32(new string(buffer.Select(x => x == '0' ? '1' : '0').ToArray()), 2);
            return common * leastCommon;
        }

        public int GetLifeSupportRating()
        {
            var oxygen = _entries;
            var scrubber = _entries;

            for (int i = 0; i < _entries[0].Length && oxygen.Count() > 1; i++)
            {
                oxygen = Filter(oxygen, i, '1');
            }

            for (int i = 0; i < _entries[0].Length && scrubber.Count() > 1; i++)
            {
                scrubber = Filter(scrubber, i, '0');
            }

            var common = Convert.ToInt32(oxygen[0], 2);
            var leastCommon = Convert.ToInt32(scrubber[0], 2);
            return common*leastCommon;
        }

        private List<string> Filter(List<string> items, int position, char defaultDigit)
        {
            var digitPositions = items.SelectMany(x => x.Select((y, i) => (Digit: y, Position: i))).ToList();
            var digits = digitPositions.Where(x => x.Position == position).GroupBy(x => x.Digit);

            var sortedDigits = defaultDigit == '0' 
                ? digits.OrderBy(x => x.Count()).ToArray() 
                : digits.OrderByDescending(x => x.Count()).ToArray();

            var filterCriteria = digits.Count() == 1 ? sortedDigits[0].Key : sortedDigits[0].Count() == sortedDigits[1].Count() ? defaultDigit : sortedDigits[0].Key;
            return items.Where(x => x[position] == filterCriteria).ToList();
        }
    }
}
