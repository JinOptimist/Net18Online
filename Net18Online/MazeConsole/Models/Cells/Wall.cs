
using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Wall : BaseCell
    {
        public override char Symbol => '#';

        public Wall(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep(BaseCharacter character)
        {
            return false;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Boom boom");
        }
    }
}
