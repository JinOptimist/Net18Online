using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Snake : BaseCharacter
    {
        public Snake(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 's';

        public override int Health { get; set; } = 20;
        public override int Coins { get; set; } = 2;
        public override int Damage { get; set; } = 5;

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Fight");
            while (character.Health > 0 && Health > 0)
            {
                Console.WriteLine($"HP Hero: {character.Health} HP Snake: {Health}");

                Health -= character.Damage;

                if (Health > 0)
                {
                    character.Health -= Damage;
                }
            }

            if (character.Health > 0)
            {
                Console.WriteLine($"You have defeated the snake and you will receive {Coins} coins");
                Maze[X, Y] = new Ground(X, Y, Maze);
                character.Coins += Coins;
                Console.WriteLine($"You have {character.Coins} coins");
            }
            else
            {
                Console.WriteLine("You're dead");
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
