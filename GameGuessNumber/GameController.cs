using System;

namespace GameGuessNumber
{
    class GameController
    {
        private Random _rand;
        private Player _player;
        private int _compScore;
        private int _compNumber;
        private int _numberOfAttemps;

        public GameController()
        {
            _rand = new Random();
            _numberOfAttemps = Settings.Attempts_Count;
            _player = new Player();
            _compScore = 0;
        }

        private bool IsEndOfRound()
        {
            if (_numberOfAttemps == 0 || _player.Number == _compNumber)
            {
                return true;
            }
            return false;
        }

        private bool IsEndOfGame()
        {
            if (_player.Score == Settings.ScoreCountToWin || _compScore == Settings.ScoreCountToWin)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerWonRound()
        {
            if (_player.Number == _compNumber)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerWonGame()
        {
            if (_player.Score == Settings.ScoreCountToWin)
            {
                return true;
            }
            return false;
        }

        private void AddScoreToRound()
        {
            if (IsPlayerWonRound())
            {
                _player.Score += Settings.DeltaScore;
                Console.Write("You win round.\nNew Round.");
            }
            else
            {
                _compScore += Settings.DeltaScore;
                Console.Write("Comp win round.\nNew Round.");
            }
        }

        private int GetValidNumberFromConsole()
        {
            while (true)
            {
                string number = Console.ReadLine();
                if (Int32.TryParse(number, out int validNumber))
                {
                    if (validNumber < Settings.MinValue || validNumber >= Settings.MaxValue)
                    {
                        Console.Write($"Enter valid number[{Settings.MinValue},{Settings.MaxValue - 1}]: ");
                        continue;
                    }
                    return validNumber;
                }
                else
                {
                    Console.Write($"Enter valid number[{Settings.MinValue},{Settings.MaxValue - 1}]: ");
                }
            }
        }

        private void DisplayUpdatedScore()
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            Console.SetCursorPosition(Console.CursorLeft + 30, Console.CursorTop);
            Console.Write($"Your score: {_player.Score}   Comp score: {_compScore}");
            Console.SetCursorPosition(0, cursorTop + 1);
        }

        public void Start()
        {
            while (!IsEndOfGame())
            {
                _compNumber = _rand.Next(Settings.MinValue, Settings.MaxValue);
                while (!IsEndOfRound())
                {
                    Console.Write($"Enter your number[{Settings.MinValue},{Settings.MaxValue - 1}]: ");
                    _player.Number = GetValidNumberFromConsole();

                    if (!IsPlayerWonRound())
                    {
                        _numberOfAttemps--;
                        Console.WriteLine($"Wrong!!! {_numberOfAttemps} chances left.");
                    }
                }
                AddScoreToRound();
                DisplayUpdatedScore();
                _numberOfAttemps = Settings.Attempts_Count;
                _player.Number = -1;
            }

            if (IsPlayerWonGame())
            {
                Console.WriteLine("You won!!! Congratulations on your victory.");
            }
            else
            {
                Console.WriteLine("You loser ;).");
            }
        }
    }
}
