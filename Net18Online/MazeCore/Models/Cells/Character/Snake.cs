using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Snake : BaseNpc
    {
        public Snake(int x, int y, Maze maze) : base(x, y, maze)
        {
            Health = 5;
            Coins = 2;
        }

        public override char Symbol => 's';

        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("You crushed a snake!");
            character.Coins += Coins;
            Maze.Npcs.Remove(this);
        }

        public override void Move()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if (!nearGrounds.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);

            foreach (var npc in Maze.Npcs)
            {
                if (destinationCell.X == npc.X && destinationCell.Y == npc.Y 
                    || destinationCell.X == Maze.Hero.X && destinationCell.Y == Maze.Hero.Y)
                {
                    return;
                }
            }

            if (destinationCell.TryStep(this))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
