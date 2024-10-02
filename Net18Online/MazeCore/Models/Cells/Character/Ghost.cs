namespace MazeCore.Models.Cells.Character
{

    public class Ghost : BaseCharacter
    {
        public Ghost(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '0';

        /// <summary>
        /// If we interact witn Ghost, we replaced it to new cell Coin
        /// </summary>
        public override void InteractWithCell(IBaseCharacter character)
        {
            AddEventInfo("BooOoo");
            Maze[X, Y] = new Coin(X, Y, Maze);
        }

        public override bool TryStep(IBaseCharacter character)
        {
            return true;
        }
    }
}

