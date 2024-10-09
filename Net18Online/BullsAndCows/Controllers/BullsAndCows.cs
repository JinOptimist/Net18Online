using System.Text;

namespace BullsAndCows.Controllers
{
    public class BullAndCow
    {
        /// <summary>
        /// Number which second player try to guess
        /// </summary>
        public int NumberOfTheFirstGamer { get; set; }

        /// <summary>
        /// Number which first player try to guess
        /// </summary>
        public int NumberOfTheSecondGamer { get; set; }

        public int LengthOfNumber { get; set; }

        private bool _isFirstGamerWin;

        private bool _isSecondGamerWin;

        public void Start()
        {
            FirstGamerSetTheNumber();

            SecondGamerSetTheNumber();

            StartOfTheGame();

            ShowResult();
        }

        /// <summary>
        /// Main functions of the game
        /// </summary>
        private void StartOfTheGame()
        {
            Console.ResetColor();
            Console.Clear();
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (HandlePlayerAttempt("(FP)Try to guess: ", NumberOfTheSecondGamer))
                {
                    _isFirstGamerWin = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (HandlePlayerAttempt("(SP)Your last chance: ", NumberOfTheFirstGamer))
                    {
                        _isSecondGamerWin = true;
                        return;
                    }
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                if (HandlePlayerAttempt("(SP)Try to guess: ", NumberOfTheFirstGamer))
                {
                    _isSecondGamerWin = true;
                    return;
                }
            } while (true);
        }

        public bool HandlePlayerAttempt(string prompt, int targetNumber)
        {
            var attempt = 0;
            do
            {
                attempt = ReadNumber(prompt);
                Console.WriteLine(CountOfBullsAndCows(attempt, targetNumber));
                LengthOfNumber = attempt.ToString().Length;
                if (!IsValidYourNumberWeShoudGuess(attempt))
                {
                    continue;
                }
                break;
            } while (true);

            return attempt == targetNumber;
        }

        private void ShowResult()
        {
            if (_isFirstGamerWin && _isSecondGamerWin)
            {
                Console.WriteLine("So, it's tie");
            }
            else
            {
                if (_isFirstGamerWin)
                {
                    Console.WriteLine("Congrats to first player");
                }
                else
                {
                    Console.WriteLine("Congrats to second player");
                }
            }
        }

        private void SecondGamerSetTheNumber()
        {
            do
            {
                Console.Clear();
                NumberOfTheSecondGamer = ReadNumber("Input your number(Second player)");
                LengthOfNumber = NumberOfTheSecondGamer.ToString().Length;

                if (!IsValidYourNumberWeShoudGuess(NumberOfTheSecondGamer))
                {
                    continue;
                }

                break;

            } while (true);
        }

        private void FirstGamerSetTheNumber()
        {
            do
            {
                Console.Clear();
                NumberOfTheFirstGamer = ReadNumber("Input your number(First player)");
                LengthOfNumber = NumberOfTheFirstGamer.ToString().Length;

                if (!IsValidYourNumberWeShoudGuess(NumberOfTheFirstGamer))
                {
                    continue;
                }

                break;

            } while (true);
        }

        public bool IsValidYourNumberWeShoudGuess(int number)
        {
            return LengthOfNumber == 4 && number >= 0;
        }

        private string CountOfBullsAndCows(int attempt, int targetNumber)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var attemptStr = attempt.ToString();
            var targetStr = targetNumber.ToString();

            var bulls = attemptStr
                .Where((digit, index) => digit == targetStr[index])
                .Count();

            var cows = attemptStr
                .Where((digit, index) => digit != targetStr[index] && targetStr.Contains(digit))
                .Count();
            
            return $"{bulls}A{cows}B";
        }

        public int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }
    }
}
