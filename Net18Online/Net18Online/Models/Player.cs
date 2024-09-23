using Net18Online.Interfaces;
using System;

namespace Net18Online.Models
{
    public class Player : IGuess
    {
        /// <summary>
        /// Number which we suggested to guessing
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Counter of available advices
        /// </summary>
        public int AdviceCount { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current attempt count of player
        /// </summary>
        public int AttemptCount { get; set; }
        /// <summary>
        /// Flag, that means player is bot
        /// </summary>
        public bool isBot { get 
            {
                return _isBot;
        }
            set {
                _isBot = value;
            }
        } 

        private bool _isBot { get; set; }

        public Player(string name, bool isBot = false)
        {
            Name = name;
            this.isBot = isBot;

            if (isBot)
            {
                AdviceCount = 0;
            }
            else
            {
                AdviceCount = 3; 
            }
        }

        public Player()
        {
        }

        /// <summary>
        /// Method for number guessing by bot
        /// </summary>
        public int GuessNumber(int minRange, int maxRange)
        {
            if (isBot)
            {
                Random random = new Random();
                int botGuess = random.Next(minRange, maxRange + 1); 
                Console.WriteLine($"{Name} (Bot) guessed: {botGuess}");
                return botGuess;
            }
            else
            {
                Console.WriteLine($"{Name}, enter your guess between {minRange} and {maxRange}:");
                int guess;
                while (!int.TryParse(Console.ReadLine(), out guess) || guess < minRange || guess > maxRange)
                {
                    Console.WriteLine($"Please enter a valid number between {minRange} and {maxRange}.");
                }
                return guess;
            }
        }

    }
}
