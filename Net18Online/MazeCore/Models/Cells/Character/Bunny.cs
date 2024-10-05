using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Bunny : BaseNpc
    {
        public Bunny(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '&';

        public override void InteractWithCell(IBaseCharacter character)
        {
            if (character.Health <= 0)
            {
                return;
            }
            character.Health -= 3;
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
