namespace MazeConsole.Models.Cells.Character
{
    public abstract class BaseCharacter : BaseCell
    {
        protected BaseCharacter(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public abstract int Health { get; set; }

        public abstract int Coins { get; set; }

        public abstract int Damage { get; set; }


        public override bool TryStep(BaseCharacter character)
        {
            return false;
        }
    }
}
