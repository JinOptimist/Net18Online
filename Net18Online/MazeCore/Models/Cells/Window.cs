using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Window : BaseCell
    {
        public Window(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '◎';

        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("clap clap");
        }

        public override bool TryStep(BaseCharacter character)
        {

            character.Health--;
            character.Coins = 0;
            return true;
        }
    }
}