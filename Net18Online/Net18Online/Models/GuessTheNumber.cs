


namespace Net18Online.Models
{
    internal class Game
    {
        public int Number { get; set; }
        public int Attempt { get; set; }
        public int MaxNumber { get; set; }
        public int MinNumber { get; set; }

        private bool _isSecondWin = false;



        public void Start()
        {
            Console.WriteLine("Do you want to play with: 1) the computer or 2) another player. (1/2)");
            var f = Console.ReadLine();
            if (int.Parse(f) == 1)
            {
                Console.Clear();
                GamerSetTheNuumberForBot();

                BinarySearch();
            }
            else
            {
                Console.Clear();

                FirstGamerSetTheNuumber();

                NumberOfAttempts();

                SecondGamerTryGuesTheNumber();

                ShowResult();
            }
        }

        private void BinarySearch()
        {
            int l = MinNumber;
            int r = MaxNumber;

            while (l < r && Attempt > 0)
            {
                Console.WriteLine("You have made a number " + (int)(l + (r - l) / 2) + "? (Yes/No)");
                var f = Console.ReadLine();
                if (f == "Yes")
                {
                    Console.WriteLine("I WIN");
                    break;
                }
                else
                {
                    Attempt--;
                    if (Attempt == 0)
                    {
                        Console.WriteLine("I LOOSE(((");
                        break;
                    }
                    Console.WriteLine("Is the hidden number greater than " + (int)(l + (r - l) / 2) + "? (Yes/No)");
                    var q = Console.ReadLine();
                    if (q == "Yes")
                    {
                        l = (int)(l + (r - l) / 2);
                    }
                    else
                    {
                        r = (int)(l + (r - l) / 2);
                    }
                }
            }
        }

        private void GamerSetTheNuumberForBot()
        {
            Attempt = ReadNumber("Enter attmept number");
            MaxNumber = ReadNumber("Enter upper bound");
            MinNumber = ReadNumber("Enter lower bound");
            Console.Clear();
        }

        private void FirstGamerSetTheNuumber()
        {
            Number = ReadNumber("Enter the number");
            Attempt = ReadNumber("Enter attmept number");
            MaxNumber = ReadNumber("Enter upper bound");
            MinNumber = ReadNumber("Enter lower bound");
            Console.Clear();
        }

        private bool CheckingForCorrectInput(int guess)
        {
            if (guess > MaxNumber)
            {
                return false;
            }
            else if (guess < MinNumber)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OutputOfInputError(int guess)
        {
            if (guess > MaxNumber)
            {
                Console.WriteLine("You have entered the upper boundary, enter number again");
            }
            else if (guess < MinNumber)
            {
                Console.WriteLine("You have entered the lower boundary, enter number again");
            }
        }

        private void NumberOfAttempts()
        {
            int Numberattempt = (int)Math.Log(MaxNumber - MinNumber + 1, 2) + 1;
            Console.WriteLine("Max number of attempts " + Numberattempt);
        }

        private void ChangingBorders(int guess)
        {
            if (guess < Number)
            {
                MinNumber = guess;
                Console.WriteLine("The hidden number is bigger");
            }
            else
            {
                MaxNumber = guess;
                Console.WriteLine("The hidden number is less");
            }
        }

        private void SecondGamerTryGuesTheNumber()
        {
            var att = 0;
            do
            {
                var guess = ReadNumber("Enter your guess");
                if (guess == Number)
                {
                    _isSecondWin = true;
                    break;
                }
                else
                {
                    while (CheckingForCorrectInput(guess) != true)
                    {
                        OutputOfInputError(guess);
                        var guessstr = Console.ReadLine();
                        guess = int.Parse(guessstr);
                    }
                    ChangingBorders(guess);
                }
                att++;
            } while (att < Attempt);

        }

        private void ShowResult()
        {
            if (_isSecondWin == true)
            {
                Console.WriteLine("YOU WIN");
            }
            else
            {
                Console.WriteLine("YOU LOOSE");
            }
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            var numberStr = Console.ReadLine();
            return int.Parse(numberStr);
        }
    }
}
