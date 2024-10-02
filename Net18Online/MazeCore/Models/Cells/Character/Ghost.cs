using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{ 
    public class Ghost : BaseNpc
    {
        public Ghost(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '0';

        /// <summary>
        /// If we interact witn Ghost, we replaced it to new cell Coin
        /// </summary>
        public override void InteractWithCell(BaseCharacter character)
        {
            character.Health--;
            AddEventInfo("BooOoo");
            
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

