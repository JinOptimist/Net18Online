using MazeCore.Helpers;
namespace MazeCore.Models.Cells.Character
{
    public abstract class BaseNpc : BaseCharacter
    {
        protected BaseNpc(int x, int y, Maze maze) : base(x, y, maze)
        {
        }
        public virtual void Move()
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
