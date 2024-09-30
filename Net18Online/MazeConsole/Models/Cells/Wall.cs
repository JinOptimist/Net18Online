using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Wall : BaseCell
    {
        public Wall(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '#';


        public override bool TryStep(BaseCharacter baseCharacter)
        {
            return false;
        }

        public override void InteractWithCell(BaseCharacter baseCharacter)
        {
            Console.WriteLine("Boom boom");
        }
    }
}
