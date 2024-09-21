


namespace Net18Online.Models
{
    public class GuessTheNumber
    {
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }
        /// <summary>
        /// Min number value
        /// </summary>
        public int MinNumber { get; set; }
        /// <summary>
        /// Max number value
        /// </summary>
        public int MaxNumber { get; set; }
        /// <summary>
        /// Minimum number to prompt for user input
        /// </summary>
        private int _minGuessNumber = 0;
        /// <summary>
        /// Maximum number to prompt for user input
        /// </summary>
        private int _maxGuessNumber = 0;

        private bool _isSecondGamerWin;

        public void Start()
        {
            Console.WriteLine("Select game mode:");
            Console.WriteLine("1 - Two players. The first one makes a guess. The second one guesses.");
            Console.WriteLine("2 - Bot guesses the player's number");
            Console.WriteLine("0 - Exit");

            int gameMode;
            do
            {
                gameMode = ReadNumber($"Enter game mode:");
            } while (!Enumerable.Range(0, 3).Contains(gameMode));

            Console.Clear();

            switch (gameMode)
            {
                case 1:
                    {
                        FirstGamerSetTheNumber();
                        SecondGamerTryGuessTheNumber();
                        ShowResult();
                    }
                    break;
                case 2:
                    {
                        SetTheNumber();
                        BotGuessesTheNumber();
                    }
                    break;
                default:
                    break;
            }
        }

        private void ShowResult()
        {
            if (_isSecondGamerWin)
            {
                Console.WriteLine("Congratz");
            }
            else
            {
                Console.WriteLine("Looser");
            }
        }

        private void SecondGamerTryGuessTheNumber()
        {
            var attempt = 0;
            var guess = 0;
            do
            {
                guess = ReadNumber($"{GetHelper(attempt, guess)} {GetGuessCalcCount()}\nEnter your guess in the range from {_minGuessNumber} to {_maxGuessNumber} (attemt {attempt+1} of {MaxAttempt}):");
                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
                }
                else
                {
                    UpdateGuessRange(guess);
                }
                attempt++;
            } while (attempt < MaxAttempt);
        }

        private void FirstGamerSetTheNumber()
        {
            SetTheNumber();

            MaxAttempt = ReadNumber("Enter max attempt count");
            Console.Clear();
        }

        private void SetTheNumber()
        {
            MinNumber = ReadNumber("Enter min value number");
            _minGuessNumber = MinNumber;

            do
            {
                MaxNumber = ReadNumber($"Enter max value number");
                if (MaxNumber >= MinNumber)
                {
                    _maxGuessNumber = MaxNumber;
                    break;
                }
            } while (true);

            do
            {
                Number = ReadNumber($"Enter the number in range from {MinNumber} to {MaxNumber}");
                if (MinNumber <= Number && Number <= MaxNumber)
                {
                    break;
                }
            } while (true);
        }

        private int ReadNumber(string message)
        {
            do
            { 
                Console.WriteLine(message);
                var numberStr = Console.ReadLine();

                if (int.TryParse(numberStr, out int value))
                {
                    return value;
                }
            } while (true);
        }

        private void UpdateGuessRange(int guessNumber)
        {
            if (Enumerable.Range(_minGuessNumber, Number - _minGuessNumber + 1).Contains(guessNumber))
            {
                _minGuessNumber = guessNumber + 1;
            }
            else if(Enumerable.Range(Number, _maxGuessNumber - Number + 1).Contains(guessNumber))
            {
                _maxGuessNumber = guessNumber - 1;
            }
        }

        private string GetHelper(int attemptNum, int prevGuess)
        {
            return attemptNum == 0 ? string.Empty :
                prevGuess < Number ? "The guessed number is higher." : "The guessed number is less.";
        }

        private string GetGuessCalcCount()
        {
            return $"It is possible to guess in {Math.Ceiling(Math.Log2(_maxGuessNumber - _minGuessNumber + 1))} attempts.";
        }

        private void BotGuessesTheNumber()
        {
            var attempt = 0;
            var guess = 0;
            do
            {
                attempt++;

                guess = GetBotNumber();
                Console.WriteLine($"Attempt #{attempt}: {guess}");
                
                UpdateGuessRange(guess);
            } while (guess != Number);

            Console.WriteLine($"Number guessed. Number of attempts - {attempt}.");
        }

        private int GetBotNumber()
        {
            return _minGuessNumber + (int)Math.Ceiling((double)(_maxGuessNumber - _minGuessNumber) / 2);
        }
    }
}
