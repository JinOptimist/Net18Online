using MazeConsole.Builders;
using System.Security.Cryptography.X509Certificates;

namespace MazeConsole
{
    public class HeroConsoleController
    {
        public void StartGame()
        {
            var mazeBuilder = new MazeBuilder();
            var mazeDrawer = new MazeDrawer();

            var maze = mazeBuilder.Build(15, 12);
            mazeDrawer.Draw(maze);


            while (true)
            {
                var destinationX = maze.Hero.X;
                var destinationY = maze.Hero.Y;

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        destinationX++;
                        MovingPlayer();
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        destinationX--;
                        MovingPlayer();
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        destinationY--;
                        MovingPlayer();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        destinationY++;
                        MovingPlayer();
                        break;
                    case ConsoleKey.Spacebar:
                        Console.SetCursorPosition(0, 13);
                        maze[destinationX, destinationY].InteractWithCell(maze.Hero);
                        Console.SetCursorPosition(maze.Hero.X+1, maze.Hero.Y + 1);
                        Console.Write(maze[maze.Hero.X+1, maze.Hero.Y].Symbol);
                        continue;
                    case ConsoleKey.Escape:
                        return;
                }

                void MovingPlayer()
                {
                    var destinationCell = maze[destinationX, destinationY];
                    if (destinationCell?.TryStep(maze.Hero) ?? false)
                    {
                        Console.SetCursorPosition(maze.Hero.X + 1, maze.Hero.Y + 1);
                        Console.Write("\b");
                        Console.Write(maze[maze.Hero.X, maze.Hero.Y].Symbol);
                        maze.Hero.X = destinationX;
                        maze.Hero.Y = destinationY;
                        Console.SetCursorPosition(destinationX + 1, destinationY + 1);
                        Console.Write("\b@");
                    }
                }


                //mazeDrawer.Draw(maze);
                //Console.WriteLine(maze.Hero.Health);
                //Console.WriteLine(maze[destinationX, destinationY].Symbol);
            }
        }
    }
}
