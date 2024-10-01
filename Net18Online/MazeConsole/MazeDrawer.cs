using MazeCore.Models;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public virtual void Draw(Maze maze)
        {
            Console.Clear();
            Console.WriteLine($"Maze has {maze.Cells.Count} cells");

            for (int y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    var cell = maze.GetTopLevelItem(x, y);
                    Console.Write(cell.Symbol);
                }

                Console.WriteLine();
            }

            var copyOfHistory = maze.HistoryOfEvents.ToList();
            copyOfHistory.Reverse();
            var lastEvents = copyOfHistory.Take(5);

            foreach (var eventInfo in lastEvents)
            {
                Console.WriteLine(eventInfo);
            }
        }


        public void DrawHeroStep(Maze maze, int x, int y)
        {
            if(maze.Hero.X != x || maze.Hero.Y != y)
            {
                DrawHeroMoving(maze);
                DrawPastCell(maze, x, y);
            }
        }


        public void DrawNpcStep(Maze maze, int x, int y, int i)
        {
            if (maze.Npcs[i].X != x || maze.Npcs[i].Y != y)
            {
                DrawNpcMoving(maze, i);
                DrawNpcCell(maze, x, y);
            }
        }




        public void DrawHeroMoving(Maze maze)
        {
            Console.SetCursorPosition(maze.Hero.X+1, maze.Hero.Y + 1);
            Console.Write("\b@");
        }
        public void DrawPastCell(Maze maze, int x, int y)
        {
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("\b");
            Console.Write(maze[x, y].Symbol);
        }





        public void DrawNpcMoving(Maze maze, int i)
        {
            Console.SetCursorPosition(maze.Npcs[i].X + 1, maze.Npcs[i].Y + 1);
            Console.Write("\b");
            Console.Write(maze.Npcs[i].Symbol);
        }
        public void DrawNpcCell(Maze maze, int x, int y)
        {
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("\b");
            Console.Write(maze[x, y].Symbol);
        }
    }

}