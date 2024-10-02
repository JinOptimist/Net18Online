using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Wall : BaseCell
    {
        public override char Symbol => '#';

        public Wall(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep(IBaseCharacter character)
        {
            AddEventInfo("Boom boom");
            return false;
        }

        public override void InteractWithCell(IBaseCharacter character)
        {
        }
    }
}
