
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
                Console.WriteLine($"Your range: [{MinValue};{MaxValue}]");
                var guess = ReadNumber("Enter your guess");
                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
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
                    Console.WriteLine("Error: Your min value range is greater than the max!");
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
                    Console.WriteLine("Error: Your number is not included in the range!");
                    Console.WriteLine("Try again...");
                    Console.WriteLine($"Your range: [{MinValue};{MaxValue}]");
                }

            } while (Number < MinValue || Number > MaxValue);

            MaxAttempt = ReadNumber("Enter max attempt count");
            Console.Clear();
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
