using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Lamer: BaseNpc
    {
        public Lamer( int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'L';

        public override void InteractWithCell(IBaseCharacter character)
        {
            character.Health++;
            AddEventInfo($"healthhUp {character}");
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
                Maze.ReplaceCellToGround(this);
            }
        }

        public void ToGround()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if( !nearGrounds.Any() )
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);
            if( destinationCell.TryStep(this) )
            {
                Maze.ReplaceCellToGround(this);
            }
        }
    }
}
