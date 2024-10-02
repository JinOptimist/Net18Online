using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, IMaze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '.';

        public override void InteractWithCell(IBaseCharacter character)
        {
        }

        public override bool TryStep(IBaseCharacter character)
        {
            AddEventInfo("step step");
            return true;
        }
    }
}
