
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
        
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        private bool _isSecondGamerWin;

        public void Start()
        {
            FirstGamerSetTheRange();

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
            do
            {
                Console.WriteLine($"There are {MaxAttempt - attempt} attempts left!");
                Console.WriteLine($"Your range: [{MinValue};{MaxValue}]");
                var guess = ReadNumber("Enter your guess");

                while(guess < MinValue || guess > MaxValue)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Error: Your number is not in the range!");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Try again...");
                    Console.WriteLine($"Your range: [{MinValue};{MaxValue}]");
                    guess = ReadNumber("Enter your guess");
                }

                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
                }else if(guess < Number)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("The desired number more!");
                    Console.WriteLine("------------------------");
                    MinValue = guess;
                }
                else
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("The desired number less!");
                    Console.WriteLine("------------------------");
                    MaxValue = guess;
                }

                attempt++;

            } while (attempt < MaxAttempt);
        }

        private void FirstGamerSetTheRange()
        {
            MinValue = ReadNumber("Enter the min value range");

            do
            {
                MaxValue = ReadNumber("Enter the max value range");

                if (MaxValue < MinValue)
                {
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Error: Your min value range is greater than the max!");
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Try again...");
                    Console.WriteLine($"Your min value range is {MinValue}");
                }
            }
            while (MaxValue < MinValue);
        }

        private void FirstGamerSetTheNumber()
        {
            do
            {
                Number = ReadNumber("Enter the number");

                if(Number < MinValue || Number > MaxValue)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Error: Your number is not included in the range!");
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Try again...");
                    Console.WriteLine($"Your range: [{MinValue};{MaxValue}]");
                }

            } while (Number < MinValue || Number > MaxValue);

            MaxAttempt = AutoCountMaxAttempt();
            Console.Clear();
        }

        private int AutoCountMaxAttempt()
        {
            return (int)Math.Ceiling(Math.Log2(MaxValue - MinValue + 1)) + 1;
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();

            while (!int.TryParse(numberStr, out var num))
            {
                Console.WriteLine("Error: You didn't enter an integer!");
                Console.WriteLine("Try again...");
                Console.WriteLine(message);
                numberStr = Console.ReadLine();
            }
            
            return int.Parse(numberStr);
        }
    }
}
