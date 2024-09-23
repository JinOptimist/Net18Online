
namespace Net18Online.Models
{
    class GuessTheNumberBot
    {
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Range for guess
        /// </summary>
        public int MinRangeNumber { get; set; }
        private int _MaxRangeNumber;
        public int MaxRangeNumber
        {
            get
            {
                return _MaxRangeNumber;
            }
            set
            {
                if (value < MinRangeNumber) { _MaxRangeNumber = MinRangeNumber * 5; }
                else { _MaxRangeNumber = value; }
            }
        }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }

        private bool _isSecondGamerWin;

        public void Start()
        {
            FirstGamerSetTheNumber();

            SecondGamerTryGuessTheNumber();

            ShowResult();
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
            var random = new Random();
            do
            {
                var guess = random.Next(MinRangeNumber, MaxRangeNumber+1);
                Console.WriteLine(guess);
                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
                }
                ShowAttemptResult(guess);
                ChangeRange(guess);
                attempt++;
            } while (attempt < MaxAttempt);
        }

        private void FirstGamerSetTheNumber()
        {
            MinRangeNumber = ReadNumber("Enter range min");
            MaxRangeNumber = ReadNumber("Enter range max");
            Number = ReadNumber("Enter the number", MinRangeNumber, MaxRangeNumber);
            MaxAttempt = CalculateAttempts((MaxRangeNumber - MinRangeNumber));
            Console.Clear();
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }
        /// <summary>
        /// ReadNumber function with range
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int ReadNumber(string message, int min, int max)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();

            if (int.Parse(numberStr) < min || int.Parse(numberStr) > max)
            {
                Console.WriteLine("\nPlease enter number in the range\n");
                return ReadNumber(message, min, max);
            }

            return int.Parse(numberStr);
        }

        private void ShowAttemptResult(int guess)
        {
            if (guess < Number)
            {
                Console.WriteLine("The number is larger than guess\n");
            }
            else
            {
                Console.WriteLine("The number is less than guess\n");
            }
        }
        private void ChangeRange(int guess)
        {
            if (guess < Number && guess > MinRangeNumber)
            {
                MinRangeNumber = guess;
            }
            if (guess > Number && guess < MaxRangeNumber)
            {
                MaxRangeNumber = guess;
            }
        }
        private int CalculateAttempts(int range) => Convert.ToInt32((range * 0.1));
    }
}
