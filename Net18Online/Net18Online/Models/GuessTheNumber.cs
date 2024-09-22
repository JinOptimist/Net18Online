
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace Net18Online.Models
{
    public class GuessTheNumber // Game with 2 gamers
    {
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Range start number
        /// </summary>
        public int Num_Start_Range { get; set; }

        /// <summary>
        /// Range end number
        /// </summary>
        public int Num_End_Range { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }

        /// <summary>
        /// Is the second player the winner
        /// </summary>
        private bool _isSecondGamerWin;

        /// <summary>
        /// Start Game
        /// </summary>
        public void Start()
        {
            SelectRange();

            FirstGamerSetTheNumber();

            SecondGamerTryGuessTheNumber();

            ShowResult();
        }

        private void SelectRange() // Selecting the limits of the range and checking the condition for the existence of the range
        {
            Console.Clear();
            Console.WriteLine("First you need to select a range of numbers");
            Num_Start_Range = ReadNumber("Enter a number - the beginning of the range");
            Console.WriteLine("\nCool, now let's enter the end of the range");
            Num_End_Range = ReadNumber("Enter a number - the ending of the range");

            while (true) // Condition for the existence of a range
            {
                if (Num_End_Range < Num_Start_Range || Num_End_Range == Num_Start_Range)
                {
                    Num_End_Range = 0;
                    Num_End_Range = ReadNumber("\nOops, the end of the range is less than the beginning. " +
                        "Please enter a number greater than " + Num_Start_Range);
                }
                else { break; }
            }
        }

        static int CalculateMaxAttempts(int num_start_range, int num_end_range) // We calculate the maximum number of attempts using the formula
        {
            int range = num_end_range - num_start_range + 1;
            return (int)Math.Ceiling(Math.Log2(range));
        }

        private void FirstGamerSetTheNumber() // The first player chooses a number in the range and the maximum number of attempts
        {
            Console.Clear();
            Number = ReadNumber("Enter the number in range " + Num_Start_Range + " : " + Num_End_Range);
            if (Number < Num_Start_Range || Number > Num_End_Range) // Checking for a number in a range
            {
                Console.WriteLine("Your number is out of range. Press any button");
                Console.ReadLine();
                FirstGamerSetTheNumber();
            }
            MaxAttempt = CalculateMaxAttempts(Num_Start_Range, Num_End_Range); // Calculate the maximum number of attempts
            Console.WriteLine("\nMaximum number of attempts for this range is " + MaxAttempt);
            while (true) // We ask the player if he wants to change the number of attempts
            {
                Console.WriteLine("\nEnter \"Y\" to save the number of attempts or \"N\" to change");
                Console.WriteLine("Your answer...");
                var choose = Console.ReadLine();
                if (choose == "Y")
                {
                    break;
                }
                else if (choose == "N")
                {
                    MaxAttempt = ReadNumber("\nEnter the maximum number of attempts");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCould not recognize the answer, try again.");
                }
            }
            Console.Clear();
            
        }

        private void SecondGamerTryGuessTheNumber() // The second player tries to guess the number knowing the range and receiving hints
        {
            
            var attempt = 0;
            while(attempt < MaxAttempt) // While the number of attempts is less than the maximum number of attempts
            {
                attempt++;
                var guess = ReadNumber("You need to guess a number in a range "
                    + Num_Start_Range + " : " + Num_End_Range + "\nEnter your guess");
                if (guess == Number) // The second player guessed right
                {
                    _isSecondGamerWin = true;
                    break;
                }
                else if (guess < Number) // Number is less than the hidden number
                {
                    Console.WriteLine("\nOh no, your number is less than the hidden number. " +
                        "You have " + (MaxAttempt - attempt) + " more try");
                    Num_Start_Range = guess;
                }
                else if (guess > Number) // Number is greater than the hidden number
                {
                    Console.WriteLine("\nOh no, your number is greater than the hidden number. " +
                        "You have " + (MaxAttempt - attempt) + " more try");
                    Num_End_Range = guess;
                }
            }
        }      

        private int ReadNumber(string message) // We read the string, check if it can be a number and convert it to int
        {
            while (true) // We are trying to get numbers from the player
            {
                Console.WriteLine(message);
                var numberStr = Console.ReadLine();
                if (int.TryParse(numberStr, out int number)) // Checking if it is a number
                {
                    return int.Parse(numberStr); // Transform
                }
                else
                {
                    Console.WriteLine("Could not recognize the number, try again."); // Try again
                }
            }
        }

        private void ShowResult() // Let's find out the results
        {
            if (_isSecondGamerWin)
            {
                Console.WriteLine("\nCongratz");
            }
            else
            {
                Console.WriteLine("\nLooser");
            }
        }

    }

    public class GuessTheNumberWithComputer() // Game with computer
    {
        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Range start number
        /// </summary>
        public int Num_Start_Range { get; set; }

        /// <summary>
        /// Range end number
        /// </summary>
        public int Num_End_Range { get; set; }

        /// <summary>
        /// Max attempt before lose
        /// </summary>
        public int MaxAttempt { get; set; }

        /// <summary>
        /// Is the computer the winner
        /// </summary>
        private bool _isComputerWin;

        /// <summary>
        /// Start Game
        /// </summary>
        /// 
        public void Start()
        {
            SelectRange();

            GamerSetTheNumber();

            TypeOfSearch();


        }

        private void SelectRange() // Selecting the limits of the range and checking the condition for the existence of the range
        {
            Console.Clear();
            Console.WriteLine("First you need to select a range of numbers");
            Num_Start_Range = ReadNumber("Enter a number - the beginning of the range");
            Console.WriteLine("\nCool, now let's enter the end of the range");
            Num_End_Range = ReadNumber("Enter a number - the ending of the range");

            while (true) // Condition for the existence of a range
            {
                if (Num_End_Range < Num_Start_Range || Num_End_Range == Num_Start_Range)
                {
                    Num_End_Range = 0;
                    Num_End_Range = ReadNumber("\nOops, the end of the range is less than the beginning. " +
                        "Please enter a number greater than " + Num_Start_Range);
                }
                else { break; }
            }
        }

        private void GamerSetTheNumber() // The first player chooses a number in the range
        {
            Console.Clear();
            Number = ReadNumber("Enter the number in range " + Num_Start_Range + " : " + Num_End_Range);
            if (Number < Num_Start_Range || Number > Num_End_Range) // Checking for a number in a range
            {
                Console.WriteLine("Your number is out of range. Press any button");
                Console.ReadLine();
                GamerSetTheNumber();
            }
            Console.Clear();

        }

        private void TypeOfSearch()
        {
            Console.Clear();
            Console.WriteLine("Now you need to choose a method for searching for a number." +
                    " \nThere are 2 options to choose from: " +
                    "\n1 - You give the computer several attempts, and it tries to guess your number;" +
                    "\n2 - You give the computer an infinite number of tries and see how long it takes to guess the number");  
            while (true)
            {
                Console.WriteLine("\r\nEnter 1 to select the first option or enter 2 to select the second option");
                Console.WriteLine("Your answer...");
                var answer = Console.ReadLine();
                if (answer == "1")
                {
                    ComputerTryGuessTheNumberWithAttepmts();
                    break;
                }
                else if (answer == "2")
                {
                    ComputerTryGuessTheNumberWithoutAttempts();
                    break;
                }
                else
                {
                    Console.WriteLine("\nCould not recognize the answer, try again.");

                }
            }
        }
        

        private void ComputerTryGuessTheNumberWithAttepmts()
        {
            Console.Clear();
            MaxAttempt = CalculateMaxAttempts(Num_Start_Range, Num_End_Range); // Calculate the maximum number of attempts
            Console.WriteLine("\nMaximum number of attempts for this range is " + MaxAttempt);
            while (true) // We ask the player if he wants to change the number of attempts
            {
                Console.WriteLine("\nEnter \"Y\" to save the number of attempts or \"N\" to change");
                Console.WriteLine("Your answer...");
                var choose = Console.ReadLine();
                if (choose == "Y")
                {
                    break;
                }
                else if (choose == "N")
                {
                    MaxAttempt = ReadNumber("\nEnter the maximum number of attempts");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCould not recognize the answer, try again.");
                }
            }

            var attempt = 0;
            while (attempt < MaxAttempt) // While the number of attempts is less than the maximum number of attempts
            {
                attempt++;
                Console.Clear();
                Console.WriteLine("Number of attempts: " + attempt);
                Random rnd = new Random();
                int guess = rnd.Next(Num_Start_Range, Num_End_Range);
                Console.WriteLine("\nI think your number is " + guess);
                Console.WriteLine("\nYou must give a hint to the computer. " +
                    "\nEnter \"H\" if your number is higher;\n \"L\" if lower;\n \"Y\" if I guessed right");
                Console.WriteLine("Your answer...");
                var answer = Console.ReadLine();
                if (answer == "H") // Number is greater than the hidden number
                {
                    Num_Start_Range = guess + 1;
                }
                else if (answer == "L") // Number is less than the hidden number
                {
                    Num_End_Range = guess - 1;
                }
                else if (answer == "Y") // Computer won
                {
                    Console.WriteLine("\nI always win :)");
                    Console.WriteLine("Attempts used: " + attempt);
                    _isComputerWin = true;
                    break;
                }
                else
                {
                    Console.WriteLine("\nPlease, answer \"H\", \"L\" or \"Y\" ");
                    attempt--;
                    Console.WriteLine("Press any button");
                    Console.ReadLine();

                }
            }
            if (_isComputerWin == false)
            {
                Console.WriteLine("\nHow did I lose?");
                
            }
            
        }
        private void ComputerTryGuessTheNumberWithoutAttempts() // The computer tries to guess the number knowing the range and receiving hints
        {
            var attempts = 0;
            while (true) // Until the computer guesses the number
            {
                attempts++;
                Console.Clear();
                Console.WriteLine("Number of attempts: " + attempts);
                var guess = (Num_Start_Range + Num_End_Range) / 2; // binary search
                Console.WriteLine("\nI think your number is " + guess);
                Console.WriteLine("\nYou must give a hint to the computer. " +
                    "\nEnter \"H\" if your number is higher;\n \"L\" if lower;\n \"Y\" if I guessed right");
                Console.WriteLine("Your answer...");
                var answer = Console.ReadLine();
                if (answer == "H") // Number is greater than the hidden number
                {
                    Num_Start_Range = guess + 1;
                }
                else if (answer == "L") // Number is less than the hidden number
                {
                    Num_End_Range = guess - 1;
                }
                else if (answer == "Y") // Computer won
                {
                    Console.WriteLine("\nI always win :)");
                    Console.WriteLine("Attempts used: " + attempts);
                    break;
                }
                else
                {
                    Console.WriteLine("\nPlease, answer \"H\", \"L\" or \"Y\" ");
                    attempts--;
                    Console.WriteLine("Press any button");
                    Console.ReadLine();
                }
            }
        }

        static int CalculateMaxAttempts(int num_start_range, int num_end_range) // We calculate the maximum number of attempts using the formula
        {
            int range = num_end_range - num_start_range + 1;
            return (int)Math.Ceiling(Math.Log2(range));
        }


        private int ReadNumber(string message) // We read the string, check if it can be a number and convert it to int
        {
            while (true) // We are trying to get numbers from the player
            {
                Console.WriteLine(message);
                var numberStr = Console.ReadLine();
                if (int.TryParse(numberStr, out int number)) // Checking if it is a number
                {
                    return int.Parse(numberStr); // Transform
                }
                else
                {
                    Console.WriteLine("Could not recognize the number, try again."); // Try again
                }
            }
        }

    }
}
