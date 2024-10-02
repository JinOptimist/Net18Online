using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class King : BaseNpc
    {
        public King(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '%';

        public override void InteractWithCell(IBaseCharacter character)
        {

        }

        public override void Move()
        {
            var walls = Maze.Cells.OfType<Wall>().ToList();
            var destinationCell = MazeHelper.GetRandom(Maze, walls);

            X = destinationCell.X;
            Y = destinationCell.Y;
        }

        public override bool TryStep(IBaseCharacter character)
        {
            var addHealth = 1000;
            character.Health += addHealth;
            AddEventInfo($"The king gave {addHealth} health");

            Maze.Npcs.Remove(this);

            return false;
        }
    }
}
