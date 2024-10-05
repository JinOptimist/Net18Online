using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y, IMaze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'c';

        public override void InteractWithCell(IBaseCharacter character)
        {
            AddEventInfo("Wow it's a coin");
        }

        public override bool TryStep(IBaseCharacter character)
        {
            character.Coins++;
            Maze.ReplaceCellToGround(this);
            return true;
        }
    }
}
