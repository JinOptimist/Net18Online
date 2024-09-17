using Net18Online.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net18Online.Models
{
    internal class Player : IGuess
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
        bool IGuess.isBot {
            get
            {
                return _isBot;
            }
            set 
            {
                _isBot = value;
            }
        }

        private bool _isBot = false;
        private bool _isPlayerHaveAttempts;

        //public void Start()
        //{
        //    FirstGamerSetTheNumber();

        //    SecondGamerTryGuessTheNumber();

        //    ShowResult();
        //}

        //private void ShowResult()
        //{
        //    if (_isPlayerHaveAttempts)
        //    {
        //        Console.WriteLine("Congratz");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Looser");
        //    }
        //}

        //private void SecondGamerTryGuessTheNumber()
        //{
        //    var attempt = 0;
        //    do
        //    {
        //        var guess = ReadNumber("Enter your guess");
        //        if (guess == Number)
        //        {
        //            _isSecondGamerWin = true;
        //            break;
        //        }
        //        attempt++;
        //    } while (attempt < MaxAttempt);
        //}

        //private void FirstGamerSetTheNumber()
        //{
        //    Number = ReadNumber("Enter the number");
        //    MaxAttempt = ReadNumber("Enter max attempt count");
        //    Console.Clear();
        //}

        //private int ReadNumber(string message)
        //{
        //    Console.WriteLine(message);
        //    var numberStr = Console.ReadLine();
        //    return int.Parse(numberStr);
        //}
    }
}
