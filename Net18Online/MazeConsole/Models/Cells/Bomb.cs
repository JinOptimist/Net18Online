using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Bomb : BaseCell
    {
        public int Damage => 1;

        public override char Symbol => 'B';

        public Bomb(int x, int y, Maze maze) : base(x, y, maze)
        {

        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            character.Health-= Damage;
            Console.WriteLine("BOOOOM");
        }
    }
}
