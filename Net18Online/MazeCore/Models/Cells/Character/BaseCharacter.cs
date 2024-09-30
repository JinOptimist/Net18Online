using MazeCore.Models;

namespace MazeCore.Models.Cells.Character
{
    public abstract class BaseCharacter : BaseCell
    {
        protected BaseCharacter(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public int Health { get; set; }

        public int Coins { get; set; }


        public override bool TryStep(BaseCharacter character)
        {
            return false;
        }
    }
}
