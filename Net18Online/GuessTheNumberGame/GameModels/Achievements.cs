
namespace GuessTheNumberGame.GameModels
{
    public class Achievement
    {
        /// <summary>
        /// Name of achievement
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status achievement
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status achievement after game
        /// </summary>
        public bool IsUnlocked { get; set; }

        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
            IsUnlocked = false;
        }

        public void Unlock()
        {
            IsUnlocked = true;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Achievement unlocked: {Name} - {Description}");
            Console.ResetColor();
        }
    }
}
