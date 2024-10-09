namespace GuessTheNumberGame.GameModels
{
    public class GuessTheNumber
    {
        /// <summary>
        /// Default text color
        /// </summary>
        private ConsoleColor currentTextColor = ConsoleColor.White;

        /// <summary>
        /// Default background color
        /// </summary>
        private ConsoleColor currentBackgroundColor = ConsoleColor.Black;

        /// <summary>
        /// Our estimated number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Number which we try to guess
        /// </summary>
        public int RandomNumber { get; set; }

        /// <summary>
        /// The number from which the range begins
        /// </summary>
        public int StartOfRange { get; set; }

        /// <summary>
        /// The number that ends the range
        /// </summary>
        public int EndOfRange { get; set; }

        /// <summary>
        /// Number of attempts used
        /// </summary>
        public int CountOfAttempts { get; set; }

        /// <summary>
        /// Variants of difficulty level
        /// </summary>
        public enum DifficultyLevel
        {
            Easy = 1,
            Medium = 2,
            Hard = 3,
            Custom = 4
        }

        public DifficultyLevel SelectedDifficulty { get; set; }

        /// <summary>
        /// Function to get a random number
        /// </summary>
        public Random Random { get; private set; } = new Random();

        /// <summary>
        /// List with achievements
        /// </summary>
        public List<Achievement> achievements = new List<Achievement>
        {
            new Achievement("Easy mod", "Guess the number in Easy mode in 3 attempts"),
            new Achievement("Medium mod", "Guess the number in Medium mode in 7 attempts"),
            new Achievement("Hard mod", "Guess the number in Hard mode in 15 attempts")
        };

        /// <summary>
        /// Menu control
        /// </summary>
        public enum ConsoleContollerInMenu
        {
            Start = 1,
            Achievement = 2,
            Designs = 3,
            Quit = 0

        }
        public ConsoleContollerInMenu SelectedAction { get; set; }

        public void Menu()
        {
            Console.Clear();
            UseTextColor();
            UseBackGroundColor();
            Console.WriteLine("Menu GUESS THE NUMBER");
            Console.WriteLine();
            Console.WriteLine("1. Start the Game");
            Console.WriteLine("2. View achievements");
            Console.WriteLine("3. Choose themes and design");
            Console.WriteLine("0. Quit the Game");
            Console.WriteLine();

            while (true)
            {
                var numberOfController = ReadNumber("Select action: ");
                if (numberOfController >= 0 && numberOfController <= 3)
                {
                    SelectedAction = (ConsoleContollerInMenu)(numberOfController);
                    break;
                }
                Console.WriteLine("Invalid input. Please enter action.");
            }

            if (SelectedAction == ConsoleContollerInMenu.Start)
            {
                Start();
            }
            else if (SelectedAction == ConsoleContollerInMenu.Achievement)
            {
                ViewAchievements();
            }
            else if (SelectedAction == ConsoleContollerInMenu.Designs)
            {
                ThemesAndDesigns();
            }
            else if (SelectedAction == ConsoleContollerInMenu.Quit)
            {
                Console.WriteLine();
                Console.WriteLine("Bye-bye");
                Environment.Exit(0);
            }
        }

        public void Start()
        {
            Console.Clear();

            ChooseDifficultyLevel();

            RandomNumberInRange();

            GamerTryGuessTheNumber();
        }

        public void ViewAchievements()
        {
            Console.Clear();
            Console.WriteLine("List of achievements:");
            Console.WriteLine();
            foreach (var achievement in achievements)
            {
                if (achievement.IsUnlocked)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"{achievement.Name}: {achievement.Description} - {(achievement.IsUnlocked ? "Unlocked" : "Not Unlocked")}");
                Console.ResetColor();
            }
            WaitPressEnter();
            Menu();
        }

        public void ThemesAndDesigns()
        {
            var numberOnController = 0;
            Console.Clear();
            Console.WriteLine("Don't like the design? Let's change");
            Console.WriteLine();
            Console.WriteLine("What are we going to change?");
            Console.WriteLine("1. Text color");
            Console.WriteLine("2. Background color");
            Console.WriteLine("0. Back to menu");
            Console.WriteLine();
            while (true)
            {
                numberOnController = ReadNumber("Select action: ");
                if (numberOnController >= 0 && numberOnController <= 2)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter action.");
            }
            if (numberOnController == 1)
            {
                ChangingTheTextColor();
            }
            else if (numberOnController == 2)
            {
                ChangingTheBackgroundColor();
            }
            else if (numberOnController == 0)
            {
                Menu();
            }
        }

        private void ChooseDifficultyLevel()
        {
            Console.Clear();
            var numberOfDiffucultyLevel = 0;
            Console.WriteLine("Welcome to the game Guess The Number");
            Console.WriteLine();
            Console.WriteLine("First, let's select the difficulty level:");
            Console.WriteLine("1. Easy - Range from 0 to 10 ");
            Console.WriteLine("2. Medium - Range from 0 to 100");
            Console.WriteLine("3. Hard - Range from 0 to 1000");
            Console.WriteLine("4. Custom - You can choose the range yourself");
            Console.WriteLine();

            while (true)
            {
                numberOfDiffucultyLevel = ReadNumber("Choose difficulty level: ");
                if (numberOfDiffucultyLevel >= 1 && numberOfDiffucultyLevel <= 4)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter difficulty level.");
            }

            Console.WriteLine();
            SelectedDifficulty = (DifficultyLevel)(numberOfDiffucultyLevel);
            Console.WriteLine($"You have chosen the difficulty level: {SelectedDifficulty}");

            switch (SelectedDifficulty)
            {
                case DifficultyLevel.Easy:
                    StartOfRange = 0;
                    EndOfRange = 10;
                    break;
                case DifficultyLevel.Medium:
                    StartOfRange = 0;
                    EndOfRange = 100;
                    break;
                case DifficultyLevel.Hard:
                    StartOfRange = 0;
                    EndOfRange = 1000;
                    break;
                case DifficultyLevel.Custom:
                    while (true)
                    {
                        StartOfRange = ReadNumber("Select start of range: ");
                        EndOfRange = ReadNumber("Select end of range: ");
                        if (StartOfRange >= EndOfRange)
                        {
                            Console.WriteLine("Invalid range. Please try again.");
                        }
                        else { break; }
                    }
                    break;
            }
        }

        private void RandomNumberInRange()
        {
            RandomNumber = Random.Next(StartOfRange, EndOfRange);
        }

        private void GamerTryGuessTheNumber()
        {
            CountOfAttempts = 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Now let's guess the number in the range from {StartOfRange} to {EndOfRange}");
                Console.WriteLine($"Number of attempts: {CountOfAttempts}");
                Console.WriteLine();
                Number = ReadNumber("Enter the number: ");
                if (Number < RandomNumber)
                {
                    Console.WriteLine();
                    Console.WriteLine("Hah, my number is higher than yours");
                    StartOfRange = Number;
                    WaitPressEnter();
                }
                else if (Number > RandomNumber)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nope, my number is less than yours");
                    EndOfRange = Number;
                    WaitPressEnter();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Congratulations, you did it!");
                    break;
                }
                CountOfAttempts++;
            }
            IsAchivmentUnlock();
            WaitPressEnter();
            Menu();
        }

        public void IsAchivmentUnlock()
        {
            if (SelectedDifficulty == DifficultyLevel.Easy && CountOfAttempts <= 3)
            {
                achievements[0].Unlock();
            }
            else if (SelectedDifficulty == DifficultyLevel.Medium && CountOfAttempts <= 7)
            {
                achievements[1].Unlock();
            }
            else if (SelectedDifficulty == DifficultyLevel.Hard && CountOfAttempts <= 15)
            {
                achievements[2].Unlock();
            }
        }

        public int ReadNumber(string message)
        {
            int number;
            while (true)
            {
                Console.WriteLine(message);
                var numberStr = Console.ReadLine();

                if (int.TryParse(numberStr, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        public void ChangingTheTextColor()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select text color:");
                Console.WriteLine("1. Red");
                Console.WriteLine("2. Green");
                Console.WriteLine("3. Blue");
                Console.WriteLine("4. Yellow");
                Console.WriteLine("5. Reset color");
                Console.WriteLine("0. Save and exit");
                Console.WriteLine();

                var numberOnController = 0;
                while (true)
                {
                    numberOnController = ReadNumber("Choose color: ");
                    if (numberOnController >= 0 && numberOnController <= 5)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid input. Please enter color.");
                }

                if (numberOnController == 0)
                {
                    break;
                }

                switch (numberOnController)
                {
                    case 1:
                        currentTextColor = ConsoleColor.Red;
                        break;
                    case 2:
                        currentTextColor = ConsoleColor.Green;
                        break;
                    case 3:
                        currentTextColor = ConsoleColor.Blue;
                        break;
                    case 4:
                        currentTextColor = ConsoleColor.Yellow;
                        break;
                    case 5:
                        currentTextColor = ConsoleColor.White;
                        break;
                }
                UseTextColor();
            }
            ThemesAndDesigns();

        }

        public void UseTextColor()
        {
            Console.ForegroundColor = currentTextColor;
        }

        public void ChangingTheBackgroundColor()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select background color:");
                Console.WriteLine("1. Red");
                Console.WriteLine("2. Green");
                Console.WriteLine("3. Blue");
                Console.WriteLine("4. Yellow");
                Console.WriteLine("5. Reset color");
                Console.WriteLine("0. Save and exit");
                Console.WriteLine();

                var numberOnController = 0;
                while (true)
                {
                    numberOnController = ReadNumber("Choose color: ");
                    if (numberOnController >= 0 && numberOnController <= 5)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid input. Please enter color.");
                }

                if (numberOnController == 0)
                {
                    break;
                }

                switch (numberOnController)
                {
                    case 1:
                        currentBackgroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        currentBackgroundColor = ConsoleColor.Green;
                        break;
                    case 3:
                        currentBackgroundColor = ConsoleColor.Blue;
                        break;
                    case 4:
                        currentBackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 5:
                        currentBackgroundColor = ConsoleColor.Black;
                        break;
                }
                UseBackGroundColor();
            }
            ThemesAndDesigns();
        }

        public void UseBackGroundColor()
        {
            Console.BackgroundColor = currentBackgroundColor;
        }

        public void WaitPressEnter()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
            }
        }
    }
}


