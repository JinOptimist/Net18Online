using MazeConsole.Models.Cells;
using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{

    public class Ghost : BaseCell
    {
        public Ghost(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '0';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("BooOoo");
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}

