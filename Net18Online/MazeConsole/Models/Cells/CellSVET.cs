using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class CellSVET: BaseCell
    {
        public CellSVET( int x, int y, Maze maze ) : base(x, y, maze)
        {
        }

        public override char Symbol => 'S';

        public override void InteractWithCell( BaseCharacter character )
        {
            Console.WriteLine("=== just a moment ===:)");
        }

        public override bool TryStep( BaseCharacter character )
        {
            character.Magic++;
            Maze[X, Y] = new Ground(X, Y, Maze);
            return true;
        }
    }
}
