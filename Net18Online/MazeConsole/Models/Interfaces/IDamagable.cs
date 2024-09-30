using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Interfaces
{
    public interface IDamageable
    {
        int Health { get; set; }
        void TakeDamage(int damage);
    }
}
