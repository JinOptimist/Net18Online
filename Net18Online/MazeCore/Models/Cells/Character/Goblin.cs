using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Goblin : BaseNpc
    {
        public Goblin(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'g';

        public override void InteractWithCell(BaseCharacter character)
        {
            character.Health--;
            AddEventInfo($"Goblin fight back to {character}");
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
