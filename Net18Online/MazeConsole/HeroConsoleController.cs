using MazeCore.Actions;
using MazeCore.Builders;
using MazeCore.Models.Cells;
using MazeCore.Models.Enum;
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
                if (destinationCell is WanderingMerchant merchant)
                {
                    var merchantActions = new WanderingMerchantActions(merchant);
                    bool interacting = true;

                    while (interacting)
                    {
                        Console.Clear();
                        Console.WriteLine("You encountered a merchant. What do you want to do?");
                        Console.WriteLine("1. Buy Healing Salve (+5 Health) - 5 Coins");
                        Console.WriteLine("2. Exit");

                        var inputKey = Console.ReadKey(true).Key;
                        WanderingMerchantOptions selectedOption;

                        switch (inputKey)
                        {
                            case ConsoleKey.D1:
                                selectedOption = WanderingMerchantOptions.BuyHealingSalve;
                                break;
                            case ConsoleKey.D2:
                            case ConsoleKey.Escape:
                                selectedOption = WanderingMerchantOptions.Exit;
                                interacting = false;
                                continue;
                            default:
                                continue; // Invalid input, repeat the loop
                        }

                        var result = merchantActions.PerformAction(maze.Hero, selectedOption);

                        /// <summary>
                        /// Handle feedback based on the result of the action
                        /// </summary>
                        switch (result)
                        {
                            case WanderingMerchantActionResult.Success:
                                Console.Clear();
                                Console.WriteLine("You bought a Healing Salve. +5 Health.");
                                break;
                            case WanderingMerchantActionResult.InsufficientFunds:
                                Console.Clear();
                                Console.WriteLine("You don't have enough coins to buy the Healing Salve.");
                                break;
                            case WanderingMerchantActionResult.Exit:
                                interacting = false;
                                continue;
                        }

                        Console.WriteLine("Press any key to return to the merchant menu...");
                        Console.ReadKey(true);
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
