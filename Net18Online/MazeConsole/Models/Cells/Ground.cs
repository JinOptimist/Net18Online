using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '.';

        public override void InteractWithCell(BaseCharacter baseCharacter)
        {
            Console.WriteLine("step step");
        }

        public override bool TryStep(BaseCharacter baseCharacter)
        {
            return true;
        }
    }
}
