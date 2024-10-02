using MazeCore.Builders;
using MazeCore.Models.Cells;

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
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        destinationX--;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        destinationY--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        destinationY++;
                        break;
                    case ConsoleKey.Spacebar:
                        maze[destinationX, destinationY].InteractWithCell(maze.Hero);
                        destinationX = maze.Hero.X;
                        destinationY = maze.Hero.Y;
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
                if (maze.Hero.IsTrappedInPit)
                {
                    
                    continue;
                }
                var destinationCell = maze.GetTopLevelItem(destinationX, destinationY);
                if (destinationCell?.TryStep(maze.Hero) ?? false)
                {
                    maze.Hero.X = destinationX;
                    maze.Hero.Y = destinationY;
                }

                foreach (var npc in maze.Npcs)
                {
                    npc.Move();
                }

                mazeDrawer.Draw(maze);
            }
        }
    }
}
