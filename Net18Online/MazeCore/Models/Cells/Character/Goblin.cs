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
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if (!nearGrounds.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);
            if (destinationCell.TryStep(this))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
