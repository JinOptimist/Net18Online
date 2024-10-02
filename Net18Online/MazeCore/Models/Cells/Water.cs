using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Water : BaseCell
    {
        public override char Symbol => '~';

        public Water(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("Glug glug");
        }
    }
}
