using System.Diagnostics;
using System.IO.Compression;
using Microsoft.VisualBasic;

namespace Net18Online.Models
{
    public class GuessTheNumber
    {
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int RangeOfTheNumber { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }

        /// <summary>
        /// Range of the number where gamer try to guess
        /// </summary>
        public int StartNumberOfTheRange { get; private set; }
        public int EndNumberOfTheRange { get; private set; }

        private bool _isSecondGamerWin;
        private bool _isBotWin;

        public void Start()
        {   
            int Whoplay;
            do
            {
                Console.WriteLine("Click 1 if you want to play with bot");
                Console.WriteLine("Or Click 2 if you want to play with player (2 gamer)");
                Whoplay = int.Parse(Console.ReadLine());

                switch(Whoplay)
                {
                    case 1: FirstGamerSetTheNumber();
                            BotSetTheNumber(); 
                            ShowResult();break;

                    case 2: FirstGamerSetTheNumber(); 
                            SecondGamerTryGuessTheNumber();
                            ShowResult(); break;
                
                    default: Console.WriteLine("We didn't offer this option, please try again"); break;
                }
            } while(Whoplay > 2 || Whoplay == 0);
        }

        private void BotSetTheNumber()
        {
            //Random random = new Random();
            bool guessed = false;

            while (!guessed)
            {
                //int guess = random.Next(StartNumberOfTheRange, EndNumberOfTheRange + 1);
                var guess = (StartNumberOfTheRange + EndNumberOfTheRange) / 2;
                Console.WriteLine($"Bot suggestion: {guess}");

                if (guess == Number)
                {
                    Console.WriteLine("Bot guess the number!");
                    guessed = true;
                    _isBotWin = true;
                }
                else if (guess < Number)
                {
                    Console.WriteLine("Set number bigger");
                    StartNumberOfTheRange = guess + 1;
                }
                else
                {
                    Console.WriteLine("Set number smaller");
                    EndNumberOfTheRange = guess - 1;
                }

            }
        }

        private void ShowResult()
        {
            if (_isSecondGamerWin || _isBotWin)
            {
                Console.WriteLine("Congratz");
            }
            else
            {
                Console.WriteLine($"The guess number {Number}");
                Console.WriteLine("Looser");
            }
        }

        private void SecondGamerTryGuessTheNumber()
        {  
            var sw = new Stopwatch();
            Console.WriteLine($"You have {MaxAttempt} attempts and {MaxAttempt*10} seconds");
            var attempt = 0;
            sw.Start();

            do
            {
                Console.WriteLine($"Range where you need to guess, number from {StartNumberOfTheRange} to {EndNumberOfTheRange}");

                var guess = ReadNumber("Enter your guess");
                
                if(guess >= StartNumberOfTheRange && guess <= EndNumberOfTheRange)
                {
                    if (sw.ElapsedMilliseconds >= MaxAttempt*10000)
                    {
                        Console.WriteLine("Time is up");
                        break;
                    }
                    if (guess == Number)
                    {
                        _isSecondGamerWin = true;
                        break;
                    } 
                    else if (guess > Number)
                    {
                        Console.WriteLine("Set number smaller"); 
                        EndNumberOfTheRange = guess;
                    }   
                    else
                    { 
                        Console.WriteLine("Set number bigger");
                        StartNumberOfTheRange = guess;
                    }
                    attempt++;
                }
                else 
                {
                    Console.WriteLine("Number out of the range");
                    attempt++;
                }
            } while (attempt < MaxAttempt || sw.ElapsedMilliseconds < MaxAttempt*10000);
        }

        private void FirstGamerSetTheNumber()
        {
            Number = ReadNumber("Enter the number");
            
            StartNumberOfTheRange = ReadNumber("Enter the start number of the range");
            EndNumberOfTheRange = ReadNumber("Enter the end number of the range");

            //MaxAttempt = ReadNumber("Enter max attempt count");
            MaxAttempt = (int)Math.Ceiling(Math.Log2(EndNumberOfTheRange - StartNumberOfTheRange + 1)+1);
            Console.WriteLine($"Max attempts = {MaxAttempt}");

            Console.WriteLine("Clic Enter for the transmission to gamer");
            Console.ReadLine();
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
