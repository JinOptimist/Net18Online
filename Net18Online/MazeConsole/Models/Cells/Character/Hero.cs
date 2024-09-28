namespace MazeConsole.Models.Cells.Character
{
    public class Hero : BaseCharacter
    {
        public Hero(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public string Name { get; set; }
        public override int Health { get; set; } = 20;
        public override int Coins { get; set; } = 5;
        public override int Damage { get; set; } = 10;


        public override char Symbol => '@';

        public override void InteractWithCell(BaseCharacter character)
        {
            throw new NotImplementedException();
        }
    }
}
