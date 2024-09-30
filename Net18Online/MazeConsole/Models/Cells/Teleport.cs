using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Teleport : BaseCell
    {
        public override char Symbol => '^';

        public Teleport(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Moving...");
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
