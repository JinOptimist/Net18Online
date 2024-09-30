using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Snake : BaseCell
    {
        private const int HEALTH = 20;

        public int Damage => 1;

        public Snake(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 's';

        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("Fight");
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
