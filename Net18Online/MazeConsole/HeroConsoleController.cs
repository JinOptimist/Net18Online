using MazeConsole.Builders;
using MazeConsole.Models.Interfaces;

namespace MazeConsole
{
    public class HeroConsoleController
    {
        public void StartGame()
        {
            var mazeBuilder = new MazeBuilder();
            var mazeDrawer = new MazeDrawer();

            ///<summary>
            ///Use 'true' as third parameter to use alternate algorithm to generate Maze
            ///</summary>
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
                        continue;
                    case ConsoleKey.Escape:
                        return;
                }

                var destinationCell = maze[destinationX, destinationY];

                #region Interfaces logic
                ///<summary>
                ///All interfaces logic realization
                ///</summary>
                if (destinationCell is IInteractable interactable)
                {
                    interactable.Interact(maze.Hero);
                }

                if (destinationCell is IAttackable attackable)
                {
                    attackable.Attack(maze.Hero);
                }
                #endregion

                if (destinationCell?.TryStep(maze.Hero) ?? false)
                {
                    maze.Hero.X = destinationX;
                    maze.Hero.Y = destinationY;


                    mazeDrawer.Draw(maze);
                }
            }
        }
    }
}
