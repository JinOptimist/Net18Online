


using System;

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

        public int Range { get; set; }

        private bool _isSecondGamerWin;

        private bool _isBotWin;

        public void Start()
        {
            int temp = 0;
            do
            {
                int selecter;
                selecter = ReadNumber("1 - play with human, 2 - play with bot");
                if (selecter == 1)
                {
                    PlayWithHuman();
                    temp++;
                }
                else if (selecter == 2)
                {
                    PlayWithBot();
                    temp++;
                }
                else
                {
                    Console.WriteLine("You need to write 1 or 2");
                }
            } while (temp < 1);
        }

        public void PlayWithBot()
        {

            FirstGamerSetTheNumber();

            BotTryGuessTheNumber();

            ShowResultWithBot();
        }

        public void PlayWithHuman()
        {

            FirstGamerSetTheNumber();

            SecondGamerTryGuessTheNumber();

            ShowResultWithHuman();
        }

        private void ShowResultWithBot()
        {
            if (_isBotWin)
            {
                Console.WriteLine("Bot win");
            }
            else
            {
                Console.WriteLine("Human win");
            }
        }

        private void ShowResultWithHuman()
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
                Random rnd = new Random();
                var guess = ReadNumber("Enter your guess");
                if (guess < 1 | guess > Range)
                {
                    Console.WriteLine($"Write number for 1 to {Range}");
                }
                else
                {
                    if (guess == Number)
                    {
                        _isSecondGamerWin = true;
                        break;
                    }
                    else if (guess > Number)
                    {
                        Console.WriteLine("You need a smaller number");
                        Range = rnd.Next(Range, Range+50);
                    }
                    else if (guess < Number)
                    {
                        Console.WriteLine("You need a higher number");
                        Range = rnd.Next(Range, Range+50);
                    }
                    attempt++;
                }
            } while (attempt < MaxAttempt);
        }

        private void BotTryGuessTheNumber()
        {
            int botMin = 0;
            int botMax = Range+50;
            var attempt = 0;
            do
            {
                System.Threading.Thread.Sleep(200);
                Random rndRange = new Random();
                Random rndNumner = new Random();
                int guess = rndNumner.Next(botMin, botMax);
                if (guess < 1 | guess > Range)
                {
                    Console.WriteLine($"Bot write illegal number {guess}");
                }
                else
                {
                    if (guess == Number)
                    {
                        Console.WriteLine($"Bot write number {guess}");
                        _isBotWin = true;
                        break;
                    }
                    else if (guess > Number)
                    {
                        Console.WriteLine($"Bot write number {guess}");
                        botMax = guess-1;
                        Range = rndRange.Next(Range, Range+50);
                    }
                    else if (guess < Number)
                    {
                        Console.WriteLine($"Bot write number {guess}");
                        botMin = guess+1;
                        Range = rndRange.Next(Range, Range+50);
                    }
                    attempt++;
                }
            } while (attempt < MaxAttempt);
        }
        private void FirstGamerSetTheNumber()
        {
            int temp = 0;
            int temp2 = 0;
            do
            {
                Range = ReadNumber("Enter the range"); ;
                if (Range > 0 & Range <= 500)
                {
                    temp2++;
                }
                else
                {
                    Console.WriteLine("You need to set range from 1 to 500");
                }
            } while (temp2 < 1);

            do
            {
                Number = ReadNumber("Enter the number");
                if (Number > 0 & Number <= Range)
                {
                    temp++;
                }
                else
                {
                    Console.WriteLine("You need to set number from 1 to 100");
                }
            } while (temp < 1);
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
