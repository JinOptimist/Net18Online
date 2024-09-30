using MazeConsole.Models.Cells.Character;
using MazeConsole.Models.Interfaces;

namespace MazeConsole.Models.Cells
{
    public class Snake : BaseCell, IInteractable, IAttackable, IDamageable
    {
        private const int INITIAL_HEALTH = 20;

        public int Health { get; set; } = INITIAL_HEALTH;

        public int Damage => 1;

        public Snake(int x, int y, Maze maze) : base(x, y, maze) { }

        public override char Symbol => 's';

        public override void InteractWithCell(BaseCharacter character)
        {
            Interact(character);
        }

        public void Interact(BaseCharacter character)
        {
            Console.WriteLine("The snake hisses and prepares to attack!");
            Attack(character); 
        }

        public void Attack(BaseCharacter character)
        {
            Console.WriteLine("The snake bites the hero! -1 Health");
            character.Health -= Damage;  
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"The snake takes {damage} damage! Remaining health: {Health}");

            if (Health <= 0)
            {
                Console.WriteLine("The snake is defeated!");
                Maze[base.X, base.Y] = new Ground(base.X, base.Y, Maze);
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
