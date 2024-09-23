
using System;
using System.Collections.Generic;



namespace Net18Online.Net18Online.Models
{
    public class GuessTheNumber
    {
        public GuessTheNumber( GameService gameService, Bounds bounds )
        {
            _gameService = gameService;
            Bounds = bounds;
        }
        public GameService gameService;
        public Bounds bounds;
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }

        private bool _isSecondGamerWin;

        public void Start()
        {
            FirstGamerSetTheNumber();

            SecondGamerTryGuessTheNumber(gameService.gamers[1]);

            ShowResult();
        }

        private void ShowResult()
        {
            if( _isSecondGamerWin )
            {
                Console.WriteLine("Congratz");
            } else
            {
                Console.WriteLine("Looser");
            }
        }

        private void SecondGamerTryGuessTheNumber( Gamer gamer )
        {
            do
            {
                var guess = ReadNumber("Enter your guess");
                if( guess == Number )
                {
                    _isSecondGamerWin = true;
                    break;
                }
                gamer.attempt--;
            } while( attempt > 0 );
        }

        private void FirstGamerSetTheNumber()
        {
            Number = ReadNumber("Enter the number");
            Console.WriteLine(Number + "Это намбер");
            MaxAttempt = ReadNumber("Enter max attempt count");
            Console.Clear();
            gameService.gamers.foreach( {
                Gamer gamer => gamer.SetAttempt(MaxAttempt) ) ;
                Console.WriteLine($"Game started for {gameService.gamers.Count} players");
                Console.WriteLine($"The Max attempt is {MaxAttempt}");
            }

            private int ReadNumber( string message )
            {
                Console.WriteLine(message);
                var numberStr = Console.ReadLine();
                return int.Parse(numberStr);
            }
        }
    }
