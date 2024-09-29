using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Snake : MonsterCell
    {
        private const int HEALTH = 20;

        public int Damage => 1;

        public Snake(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        private Maze _maze;
        public override void Move(int x, int y, Maze maze)
        {

            if(this.HasMoved)
            {
                this.HasMoved = false;
                return;
            }
            int[,] directions = new int[,] { {1, 0}, {-1, 0}, {0, 1}, {0, -1} };

            for (int i = 0; i < directions.GetLength(0); i++)
            { 
                int newX = X + directions[i, 0];
                int newY = Y + directions[i, 1];

                if (_maze[newX, newY] is Ground)
                {
                    _maze[newX, newY] = new Snake(x, y, maze);
                    _maze[X, Y] = new Ground(x, y, maze);
                    HasMoved = true;
                    break;
                }
            }
        }
        public override char Symbol => 's';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Fight");
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
