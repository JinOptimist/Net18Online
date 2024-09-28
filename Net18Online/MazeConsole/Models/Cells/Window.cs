using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Window : BaseCell
    {
        public Window(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '◎';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("clap clap");
        }

        public override bool TryStep(BaseCharacter character)
        {

            character.Health--;
            character.Coins = 0;
            return true; 
        }
    }
}