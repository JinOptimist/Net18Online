using MazeCore.Models;

namespace MazeCore.Models.Cells.Character
{
    public class Hero : BaseCharacter
    {
        public Hero(int x, int y, Maze maze) : base(x, y, maze)
        {
            Health = 100;
            Coins = 10;
        }

        public string Name { get; set; }
        public bool HasLadder { get; set; } = false;
        public bool IsTrappedInPit { get; set; } = false;

        public override char Symbol => '@';

        public override void InteractWithCell(BaseCharacter character)
        {
            throw new NotImplementedException();
        }
    }
}
