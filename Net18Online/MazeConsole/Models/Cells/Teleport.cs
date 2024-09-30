using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Teleport : BaseCell
    {
        public override char Symbol => '^';
        private Random _random = new();

        public Teleport(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            var cellsWhichWeMove = Maze.Cells
                .OfType<Teleport>()
                .Where(cell => cell.X != character.X && cell.Y != character.Y)
                .ToList();

            var cellWhichWeMove = GetRandom(cellsWhichWeMove);

            character.X = cellWhichWeMove.X;
            character.Y = cellWhichWeMove.Y;
        }

        private T GetRandom<T>(List<T> cells)
        {
            var countOfCells = cells.Count();
            var randomIndex = _random.Next(0, countOfCells);
            var randomCell = cells[randomIndex];
            return randomCell;
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
