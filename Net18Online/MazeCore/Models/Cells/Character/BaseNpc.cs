namespace MazeCore.Models.Cells.Character
{
    public abstract class BaseNpc : BaseCharacter
    {
        protected BaseNpc(int x, int y, Maze maze) : base(x, y, maze)
        {
        }
        public abstract void Move();
    }
}
