using MazeCore.Models;

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
    }
}