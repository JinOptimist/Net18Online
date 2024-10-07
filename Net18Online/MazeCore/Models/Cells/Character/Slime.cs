using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Slime : BaseNpc
    {
        public Slime(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '&';

        public override void InteractWithCell(IBaseCharacter character)
        {
            character.Health --;
            AddEventInfo($"The Slime burned you. You'r health is {character.Health}");
        }

        public override void Move()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if (!nearGrounds.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);
            if (destinationCell.GetType() != typeof(Wall))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
