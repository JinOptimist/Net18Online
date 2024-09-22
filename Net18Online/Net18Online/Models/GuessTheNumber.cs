


using System.Numerics;

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
        public int MinNumber { get; set; }
        public int MaxNumber {  get; set; }

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
            do
            {
                var guess = ReadNumber($"Введите ваше предположение (от {MinNumber} до {MaxNumber}):");
                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
                }
                else if (guess < Number) 
                {
                    Console.WriteLine("Your guess is too low.");
                    MinNumber = Math.Max(guess + 1, MinNumber);
                }
                else 
                {
                    Console.WriteLine("Your guess is too high.");
                    MaxNumber = Math.Min(guess - 1, MaxNumber);
                }
                Console.WriteLine($"New guessing range: from {MinNumber} to {MaxNumber}");
                attempt++;
            } while (attempt < MaxAttempt);
        }

        private void FirstGamerSetTheNumber()
        {
           
            MinNumber = ReadNumber("Enter lower limit of the range:");
            MaxNumber = ReadNumber("Enter upper limit of the range:");
            Number = ReadNumber($"Enter the number to guess (between {MinNumber} and {MaxNumber}):");
            MaxAttempt = ReadNumber("Enter max attempt count");
            Console.Clear();
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }
    }
}
