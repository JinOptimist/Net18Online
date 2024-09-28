using MazeConsole.Models;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public virtual void Draw(Maze maze)
        {
            Console.Clear();
            Console.WriteLine($"Maze has {maze.Cells.Count} cells");

            maze.ConsoleCursorDrawerTop = Console.CursorTop;
            maze.ConsoleCursorDrawerLeft = Console.CursorLeft;

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

        public void Move(Maze maze, int x, int y)
        {
            var consoleCursorIsNow = Console.GetCursorPosition();

            Console.SetCursorPosition(maze.ConsoleCursorDrawerLeft + maze.Hero.X, maze.ConsoleCursorDrawerTop + maze.Hero.Y);
            Console.Write(maze[maze.Hero.X, maze.Hero.Y].Symbol);
            Console.SetCursorPosition(maze.ConsoleCursorDrawerLeft + x, maze.ConsoleCursorDrawerTop + y);
            Console.Write(maze.Hero.Symbol);

            Console.SetCursorPosition(consoleCursorIsNow.Left, consoleCursorIsNow.Top);
        }
    }
}