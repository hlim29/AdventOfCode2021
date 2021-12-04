using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class BingoNumber
    {
        public int Number { get; set; }
        public (int X, int Y) Coord { get; set; }   
        public int Card { get; set; }
    }

    public class DayFour
    {
        private readonly List<int> _drawNumbers;
        private readonly List<BingoNumber> _bingoNumbers;
        private readonly int _cardCount;

        public DayFour()
        {
            var input = Common.ReadRaw("dayfour.txt");
            _drawNumbers = input.Split("\n")[0].Split(',').Select(x => int.Parse(x)).ToList();
            var cards = input.Replace("  ", " ").Split("\n\n").ToArray()[1..];
            _cardCount = cards.Count();
            _bingoNumbers = cards.SelectMany((x, i) => x.Split("\n").SelectMany((y, j) => y.Trim().Split(" ").Select((z, k) => new BingoNumber { Number = int.Parse(z), Coord = (X: k, Y: j), Card = i }))).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetWinnerScore(false)}");
            Console.WriteLine($"Part 2: {GetWinnerScore(true)}");
        }
        
        public int GetWinnerScore(bool isPartTwo)
        {
            var winners = new HashSet<int>();
            for (int takeAmount = 5; takeAmount < _drawNumbers.Count(); takeAmount++)
            {
                for (int cardIndex = 0; cardIndex < _cardCount; cardIndex++)
                {
                    var currentCard = _bingoNumbers.Where(x => x.Card == cardIndex).ToList();
                    var currentDraw = _drawNumbers.Take(takeAmount).ToList();
                    var isWinner = IsWinner(currentDraw, currentCard);

                    if (isPartTwo)
                    {
                        if (isWinner)
                        {
                            winners.Add(cardIndex);
                        }
                        if (winners.Count() == _cardCount)
                        {
                            var unmarkedSum = CalculateUnmarkedSum(currentDraw, currentCard);
                            return _drawNumbers[takeAmount - 1] * unmarkedSum;
                        }
                    } 
                    else
                    {
                        if (isWinner)
                        {
                            var unmarkedSum = CalculateUnmarkedSum(currentDraw, currentCard);
                            return _drawNumbers[takeAmount - 1] * unmarkedSum;
                        }
                    }
                }
            }
            
            return -1;
        }

        private bool IsWinner(List<int> winningNumbers, List<BingoNumber> cards)
        {
            for (int i = 0; i < 5; i++)
            {
                var xWinners = cards.Where(x => x.Coord.X == i && winningNumbers.Contains(x.Number)).ToList();
                var yWinners = cards.Where(x => x.Coord.Y == i && winningNumbers.Contains(x.Number)).ToList();
                if (xWinners.Count() == 5 || yWinners.Count() == 5)
                {
                    return true;
                }
            }
            return false;
        }

        private int CalculateUnmarkedSum(List<int> winningNumbers, List<BingoNumber> cards)
        {
            return cards.Where(x => !winningNumbers.Contains(x.Number)).Select(x => x.Number).Sum();
        }
    }
}
