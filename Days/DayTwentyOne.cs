using System;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class DayTwentyOne
    {
        private (int Position, int Score) _playerOne;
        private (int Position, int Score) _playerTwo;

        public DayTwentyOne()
        {
            var input = Common.ReadFile("daytwentyone.txt");
            _playerOne = (input[0][^1] - '0', 0);
            _playerTwo = (input[1][^1] - '0', 0);
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");

            var input = Common.ReadFile("daytwentyone.txt");
            _playerOne = (input[0][^1] - '0', 0);
            _playerTwo = (input[1][^1] - '0', 0);

            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            bool isPlayerOneTurn = true;
            for (int position = 1; position < 1000; position += 3)
            {
                if (isPlayerOneTurn)
                {
                    Move(position, 1);
                }
                else
                {
                    Move(position, 2);
                }
                isPlayerOneTurn = !isPlayerOneTurn;

                if (_playerOne.Score >= 1000 || _playerTwo.Score >= 1000)
                {
                    return (position + 2) * Math.Min(_playerOne.Score, _playerTwo.Score);
                }
            }
            return -1;
        }

        public int PartTwo()
        {
            return -1;
        }

        private void Move(int start, int player)
        {
            var sum = Enumerable.Range(start, 3).Sum();
            var currentPosition = player == 1 ? _playerOne.Position : _playerTwo.Position;
            var add = (currentPosition + sum) % 10 == 0 ? 10 : (currentPosition + sum) % 10;
            if (player == 1)
            {
                _playerOne.Score += add;
                _playerOne.Position = add;
            }
            else
            {
                _playerTwo.Score += add;
                _playerTwo.Position = add;
            }
        }
    }
}
