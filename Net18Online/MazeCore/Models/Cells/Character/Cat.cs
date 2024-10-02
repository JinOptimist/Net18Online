using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Cat : BaseNpc
    { 
        public Cat(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '?';
        

        public override void InteractWithCell(IBaseCharacter character)
        {
            character.Health++;
            character.Coins+=2;
            AddEventInfo($"Cat give coins to {character}");
        }

        public override void Move()
        {
            var nearCellToStepByCat = MazeHelper.GetNearCells<BaseCell>(Maze, this);
            if (!nearCellToStepByCat.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearCellToStepByCat);
            if (destinationCell.TryStep(this))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
