


using System.Security.Cryptography.X509Certificates;

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
        /// Min number
        /// </summary>
        public int MinNumber { get; set; }

        /// <summary>
        /// Max number
        /// </summary>
        public int MaxNumber { get; set; }

        private bool _isSecondGamerWin;

        public void Start()
        {
            FirstGamerSetTheNumber();

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

        private void ShowMoreOrLess(int guess)
        {
            if (guess < Number)
            {
                Console.WriteLine("You number is less than guessed number");
            }
            else if (guess > Number)
            {
                Console.WriteLine("You number is more than guessed number");
            }
        }

        private void SecondGamerTryGuessTheNumber()
        {
            var attempt = 0;
            Console.WriteLine("The guessed number lies in range between " + MinNumber + " and " + MaxNumber);
            do
            {
                var guess = ReadNumber("Enter your guess");
                if (guess < MinNumber || guess > MaxNumber) 
                    Console.WriteLine("The number is out of range");
                else ShowMoreOrLess(guess);
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
            MinNumber = ReadNumber("Enter the min number of range in which the guessed number lies");
            MaxNumber = ReadNumber("Enter the max number of range in which the guessed number lies");
            Number = ReadNumber("Enter the number");
            while (Number < MinNumber || Number > MaxNumber)
            {
                Number = ReadNumber("Enter the number in you range");
            }
            //MaxAttempt = ReadNumber("Enter max attempt count");
            MaxAttempt = AutoCreateRange("Max attempt count equl ");
            ChoseGameMode("Chose the game mode(1 or 2)\n1. Player with Player\n2. Player with computer");
            Console.Clear();
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }

        private int AutoCreateRange(string message)
        {
            var rest = MinNumber;
            var attempt = 0;
            int dif = MaxNumber - MinNumber;
            do
            {
                attempt++;
                rest = (int)Math.Ceiling((double)dif / 2);
                dif = rest;
            } while (rest >= 2);
            Console.WriteLine(message + attempt);;
            Console.ReadKey();
            return attempt;
        }

        private void ChoseGameMode(string message)
        {
            //Console.WriteLine(message + "\n1. Player with Player\n2. Player with computer");
            var gameMode = ReadNumber(message);
            do
            {
                if (gameMode == 1)
                {
                    SecondGamerTryGuessTheNumber();
                    break;
                }
                else if (gameMode == 2)
                {
                    GameWithComputer();
                    break;
                }
                else
                {
                    gameMode = ReadNumber("Select existing game mode");
                }
            }while(gameMode != 1 ||  gameMode != 2);
        }

        private void GameWithComputer()
        {
            var attempt = 0;
            var dif = MaxNumber - MinNumber;
            var rest = (int)Math.Ceiling((double)dif / 2);
            var compNumber = rest;
            var biggerNumber = MaxNumber;
            var lessNumber = MinNumber;
            var result = 0; 
            _isSecondGamerWin = false;
            result = ReadNumber($"{compNumber} - Is it your number?\n1. Yes\n2. No");

            while (result !=1)
            {
                attempt++;
                if (attempt >= MaxAttempt) { break; }
                Console.WriteLine();
                var moreOrLess = ReadNumber("Your number more or less?\n1. Less\n2. More");

                if (moreOrLess == 1)
                {
                    biggerNumber = compNumber;
                }
                else
                {
                    lessNumber = compNumber;
                }
                dif = biggerNumber - lessNumber;
                rest = (int)Math.Ceiling((double)dif / 2);
                compNumber = lessNumber + rest;
                result = ReadNumber($"{compNumber} - Is it your number?\n1. Yes\n2. No");
            }
            if (compNumber == Number)
            {
                _isSecondGamerWin = true;
            }
            ShowResult();
        }
    }
}
