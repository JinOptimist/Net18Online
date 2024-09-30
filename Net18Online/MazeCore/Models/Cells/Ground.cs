using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '.';

        public override void InteractWithCell(BaseCharacter character)
        {
        }

        public override bool TryStep(BaseCharacter character)
        {
            AddEventInfo("step step");
            return true;
        }
    }
}
