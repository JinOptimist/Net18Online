using MazeConsole.Builders;
using MazeConsole.Models;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public virtual void Draw(Maze maze)
        {
            Console.Clear();

            for (int y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    var cell = maze.GetTopLevelItem(x, y);
                    Console.Write(cell.Symbol);
                }

                Console.WriteLine();
            }
        }

        public void UpdatePlayerPosition(int oldX, int oldY, int newX, int newY, Maze maze)
        {
            maze.Hero.X = newX;
            maze.Hero.Y = newY;

            Console.SetCursorPosition(oldX, oldY);
            Console.Write(maze.GetTopLevelItem(oldX, oldY).Symbol);

            Console.SetCursorPosition(newX, newY);
            Console.Write(maze.GetTopLevelItem(newX, newY).Symbol);

        }
        public void DrawEmty(Maze maze)
        {
            Console.SetCursorPosition(0, maze.Height);
            Console.Write(' ');
            Console.SetCursorPosition(0, maze.Height);
        }
    }
}



