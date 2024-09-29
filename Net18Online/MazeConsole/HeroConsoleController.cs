using MazeConsole.Builders;
using MazeConsole.Models;
using MazeConsole.Models.Cells;

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
                        continue;
                    case ConsoleKey.Escape:
                        return;
                }

                for (int y = 0; y < maze.Height; y++)
                {
                    for (var x = 0; x < maze.Width; x++)
                    {
                        if(maze[x,y] is MonsterCell monsterCell)
                        {
                            monsterCell.Move(x,y, maze);   
                           try 
                            {
                                monsterCell.Move(x,y,maze);
                            }
                            catch (NullReferenceException ex)
                            {Console.WriteLine(ex.Message);}
                            catch (Exception ex)
                            {Console.WriteLine(ex.Message);}
                        }
                    }
                }
                

                var destinationCell = maze[destinationX, destinationY];
                if (destinationCell?.TryStep(maze.Hero) ?? false)
                {
                    maze.Hero.X = destinationX;
                    maze.Hero.Y = destinationY;
                }

                mazeDrawer.Draw(maze);
            }
        }
    }
}
