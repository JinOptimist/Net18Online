namespace MazeCore.Models.Cells.Character
{
    public class Slime : BaseNpc
    {
        public Slime(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '$';

        public override void InteractWithCell(BaseCharacter character)
        {
            if (character.IsDied())
            {
                return;
            }
            character.Health -= 3;
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
