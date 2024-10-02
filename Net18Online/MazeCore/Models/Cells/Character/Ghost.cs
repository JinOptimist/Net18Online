using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Ghost : BaseNpc
    {
        public Ghost(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '0';

        public int hitRate => Maze.Random.Next(0, 2);

        /// <summary>
        /// If we interact witn Ghost, we replaced it to new cell Coin
        /// </summary>
        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("BooOoo");
            if (hitRate == 0)
            {
                character.Health--;
            }
            Maze[X, Y] = new Coin(X, Y, Maze);
        }

        public override void Move()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if (!nearGrounds.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);
            if (destinationCell is not Dungeon)
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}

