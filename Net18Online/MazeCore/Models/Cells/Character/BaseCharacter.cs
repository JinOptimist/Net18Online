namespace MazeCore.Models.Cells.Character
{
    public abstract class BaseCharacter : BaseCell, IBaseCharacter
    {
        protected BaseCharacter(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public int Health { get; set; }

        public int Coins { get; set; }


        public override bool TryStep(IBaseCharacter character)
        {
            return false;
        }
    }
}