using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Net18Online.Models
{
    public class GuessTheNumber
    {
        /// <summary>
        /// Pool of players in current game
        /// </summary>
        private List<Player> players;

        public GuessTheNumber(int size)
        {
            var players = new List<Player>();
        }
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int GuessedNumber { get; set; }

        /// <summary>
        /// Upper limit of the potentially guessed number
        /// </summary>
        public int MaxNumber { get; set; } = 100;

        /// <summary>
        /// Lower limit of the potentially guessed number
        /// </summary>
        public int MinNumber { get; set; } = 1;

        /// <summary>
        /// Max attempt before lose. Use log2 because of advice option
        /// </summary>
        private int _maxAttempt { 
                get {
                    return _maxAttempt;
                }
                set {
                _maxAttempt = (int)Math.Log2(MaxNumber);
                } 
        }
        /// <summary>
        /// Checkbox that activates, if game options allows them
        /// </summary>
        private bool _advicesOn;

        private bool _isSecondGamerWin;

        internal class Advice
        {
            private GuessTheNumber? parent;

            internal int Id { get; set; }
            internal string AdviceName { get; set; }

            public Advice(GuessTheNumber parent)
            {
                this.parent = parent;
            }

            void DividedByTwo(int suppossedNumber)
            {
                if (parent.GuessedNumber % suppossedNumber == 0)
                    Console.WriteLine("Guessed number is divided by " + suppossedNumber);
                else
                    Console.WriteLine("Guessed number is NOT divided by " + suppossedNumber);
            }

            void MoreThanGuessed(int suppossedNumber)
            {
                if (parent.GuessedNumber < suppossedNumber)
                    Console.WriteLine($"Suppossed number {suppossedNumber} is MORE than guessed");
                else
                    Console.WriteLine($"Suppossed number {suppossedNumber} is LESS than guessed");
            }
        }

        #region codeForRewrite
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
                var guess = ReadNumber("Enter your guess");
                if (guess == Number)
                {
                    _isSecondGamerWin = true;
                    break;
                }
                attempt++;
            } while (attempt < MaxAttempt);
        }

        private void FirstGamerSetTheNumber()
        {
            Number = ReadNumber("Enter the number");
            MaxAttempt = ReadNumber("Enter max attempt count");
            Console.Clear();
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }
        #endregion
    }
}
