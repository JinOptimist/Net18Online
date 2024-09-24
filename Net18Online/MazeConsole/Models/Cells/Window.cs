namespace MazeConsole.Models.Cells
{
    public class Window : BaseCell
    {
        public Window(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => '◎';

        public override void InteractWithCell()
        {
            Console.WriteLine("clap clap");
        }

        public override bool TryStep()
        {
            return false; 
        }
    }
}