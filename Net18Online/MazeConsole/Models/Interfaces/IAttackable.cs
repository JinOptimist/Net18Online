using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Interfaces
{
    public interface IAttackable
    {
        int Damage { get; }
        void Attack(BaseCharacter character);
    }
}
