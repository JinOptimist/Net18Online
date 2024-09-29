using MazeConsole.Models.Cells;
using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{

    public class Ghost : MonsterCell
    {
        public Ghost(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '0';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("BooOoo");
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
                    _maze[newX, newY] = new Ghost(x, y, maze);
                    _maze[X, Y] = new Ground(x, y, maze);
                    HasMoved = true;
                    break;
                }
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }   
    }
}

