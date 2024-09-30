﻿using MazeConsole.Builders;

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
                        Console.SetCursorPosition(0, maze.Height);
                        continue;
                    case ConsoleKey.Escape:
                        return;
                }
                
                var destinationCell = maze[destinationX, destinationY];
                if (destinationCell?.TryStep(maze.Hero) ?? false)
                {
                    mazeDrawer.UpdatePlayerPosition(maze.Hero.X, maze.Hero.Y, destinationX, destinationY, maze);
                }
                mazeDrawer.DrawEmty(maze);
            }
        }
    }
}
