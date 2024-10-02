using MazeCore.Builders;
using MazeCore.Models.Cells;
using MazeCore.Models.Interfaces;

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

                if (maze.Hero.IsTrappedInPit)
                {
                    var pit = maze[maze.Hero.X, maze.Hero.Y] as Pit; 
                    pit?.InteractWithCell(maze.Hero);
                    continue;
                }

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
                
                var destinationCell = maze.GetTopLevelItem(destinationX, destinationY);
                if (destinationCell?.TryStep(maze.Hero) ?? false)
                {
                    maze.Hero.X = destinationX;
                    maze.Hero.Y = destinationY;
                }

                #region WanderingMerchantInteractions
                ///<summary>
                ///Check if the hero stepped on an NPC that can be interacted with
                ///</summary>
                if (destinationCell is IInteractable interactable && destinationCell is WanderingMerchant)
                {
                    Console.WriteLine("You encountered a merchant. Do you want to interact?");
                    Console.WriteLine("Press Spacebar to interact or Escape to continue.");

                    var interactionKey = Console.ReadKey(true).Key;
                    if (interactionKey == ConsoleKey.Spacebar)
                    {
                        interactable.Interact(maze.Hero);
                    }
                    else if (interactionKey == ConsoleKey.Escape)
                    {
                        // Player chose not to interact, hero stays in place
                        continue;
                    }
                }
                #endregion

                foreach (var npc in maze.Npcs)
                {
                    npc.Move();
                }

                mazeDrawer.Draw(maze);
            }
        }
    }
}
