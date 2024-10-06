using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Magic : BaseCell
    {
        public Magic( int x, int y, IMaze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '.';

        public override void InteractWithCell(IBaseCharacter character)
        {
            AddEventInfo("It`s magic!");
        }

        public override bool TryStep(IBaseCharacter character)
        {
            character.Magic++;
            Maze.ReplaceCellToGround(this);
            return true;
        }
    }
}
