using System;
using System.Collections.Generic;

namespace Net18Online.Models
{
    public class GuessTheNumber
    {
        private List<Player> players;

        public GuessTheNumber()
        {
            players = new List<Player>();
        }

        public int GuessedNumber { get; private set; }
        public int MaxNumber { get; set; } = 100;
        public int MinNumber { get; set; } = 1;

        private int _maxAttempt;
        public int MaxAttempt
        {
            get => _maxAttempt;
            private set => _maxAttempt = value;
        }

        private int _maxAdvices;
        public int MaxAdvices
        {
            get => _maxAdvices;
            private set => _maxAdvices = value;
        }

        private bool _advicesOn;

        private void UpdateMaxAttempt()
        {
            MaxAttempt = (int)Math.Log2(MaxNumber - MinNumber) + 1;
        }

        public void AddPlayer()
        {
            Console.WriteLine("Enter player name:");
            var name = Console.ReadLine();
            var player = new Player(name)
            {
                AdviceCount = MaxAdvices,
                AttemptCount = MaxAttempt
            };
            players.Add(player);
            Console.WriteLine($"Player {name} added.");
        }

        public void AddBot()
        {
            Console.WriteLine("Enter bot name:");
            var botName = Console.ReadLine();
            var botPlayer = new Player(botName, true)
            {
                AdviceCount = 0,
                AttemptCount = MaxAttempt
            };
            players.Add(botPlayer);
            Console.WriteLine($"Bot {botPlayer.Name} added.");
        }

        public void Options()
        {
            Console.WriteLine("Enter the minimum number of the range:");
            MinNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the maximum number of the range:");
            MaxNumber = int.Parse(Console.ReadLine());

            UpdateMaxAttempt();

            string isRandom;
            do
            {
                Console.WriteLine("Will the number be random? (yes/no):");
                isRandom = Console.ReadLine().ToLower();
                if (isRandom != "yes" && isRandom != "no")
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                }
            } while (isRandom != "yes" && isRandom != "no");

            if (isRandom == "yes")
            {
                Random rnd = new Random();
                GuessedNumber = rnd.Next(MinNumber, MaxNumber + 1);
            }
            else
            {
                GuessedNumber = ReadNumber($"Please enter a number to guess in the range from {MinNumber} to {MaxNumber}:");
            }

            if (_advicesOn = AskYesNo("Will advices be used? (yes/no):"))
            {
                Console.WriteLine("How many hints can be used?");
                MaxAdvices = int.Parse(Console.ReadLine());
            }

            bool addingPlayers = true;
            while (addingPlayers)
            {
                Console.WriteLine("Do you want to add a player? Enter 1; \n Add a bot? Enter 2. \n Finish? Enter 0.:");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        AddBot();
                        break;
                    case "0":
                        addingPlayers = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter '1', '2', or '0'.");
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("Game settings are complete. Let's start the game!");
        }

        /// <summary>
        /// Start of the game
        /// </summary>

        public void Start()
        {
            bool hasWinner = false;

            while (!hasWinner)
            {
                foreach (var player in players)
                {
                    if (player.AttemptCount > 0)
                    {
                        Console.WriteLine($"Player {player.Name} is guessing!");
                        int guess = player.GuessNumber(MinNumber, MaxNumber);
                        if (CheckGuess(guess, player))
                        {
                            Console.WriteLine($"{player.Name} guessed correctly! [Конгратулатионз]!");
                            hasWinner = true;
                            break; 
                        }
                        player.AttemptCount--; 
                        Console.WriteLine($"{player.Name} did not guess correctly. Attempts remaining: {player.AttemptCount}");
                    }
                    else
                    {
                        Console.WriteLine($"{player.Name} has no attempts left.");
                    }
                }
                if (players.All(p => p.AttemptCount <= 0))
                {
                    break; 
                }
            }

            if (!hasWinner)
            {
                Console.WriteLine("No one guessed the number.[Плаки-плаки]");
            }
        }

        /// <summary>
        /// Check of player/bot guess to clarify correct it or not
        /// </summary>
        private bool CheckGuess(int guess, Player player)
        {
            if (guess == GuessedNumber)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Bruh, you wrong");
                if (_advicesOn)
                {
                    GiveAdvice(guess, player);
                }
                return false;
            }
        }

        private int ReadNumber(string message)
        {
            Console.WriteLine(message);
            return int.Parse(Console.ReadLine());
        }
        /// <summary>
        /// Im tired of this choices, so new func being born
        /// </summary>

        private bool AskYesNo(string message)
        {
            string input;
            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine().ToLower();
            } while (input != "yes" && input != "no");

            return input == "yes";
        }
        /// <summary>
        /// Advice nested class implementation 
        /// </summary>

        private void GiveAdvice(int guess, Player player)
        {
            Console.WriteLine($"{player.Name}, do you want a hint? (yes/no)");
            if (Console.ReadLine().ToLower() == "yes")
            {
                if (player.AdviceCount > 0)
                {
                    Advice advice = new Advice(this);
                    Console.WriteLine("Select the type of hint: [1] - Divisible by 2, [2] - More or less than the number");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            advice.DividedByTwo(guess);
                            break;
                        case "2":
                            advice.MoreThanGuessed(guess);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Hint not provided.");
                            return;
                    }

                    player.AttemptCount--;
                    player.AdviceCount--;
                    Console.WriteLine($"{player.Name}, you have {player.AttemptCount} attempts and {player.AdviceCount} hints remaining.");
                }
                else
                {
                    Console.WriteLine($"{player.Name}, you have no more hints available.");
                }
            }
        }
        /// <summary>
        /// Class containing methods implementing all advices. Can be useful for scaling
        /// </summary>
        internal class Advice
        {
            private GuessTheNumber parent;

            public Advice(GuessTheNumber parent)
            {
                this.parent = parent;
            }

            public void DividedByTwo(int supposedNumber)
            {
                Console.WriteLine(parent.GuessedNumber % 2 == 0 ? "The guessed number is divisible by 2." : "The guessed number is not divisible by 2.");
            }

            public void MoreThanGuessed(int supposedNumber)
            {
                Console.WriteLine(parent.GuessedNumber < supposedNumber ? $"The entered number {supposedNumber} is greater than the guessed number." : $"The entered number {supposedNumber} is less than the guessed number.");
            }
        }
    }
}
