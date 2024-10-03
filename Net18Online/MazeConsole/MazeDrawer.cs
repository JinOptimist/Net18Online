using MazeCore.Models;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public int ConsoleCursorDrawerTop { get; set; }
        public int ConsoleCursorDrawerLeft { get; set; }
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

        public void DrawMove(Maze maze)
        {
            var consoleCursorIsNow = Console.GetCursorPosition();

            if (ConsoleCursorDrawerTop == consoleCursorIsNow.Top && ConsoleCursorDrawerLeft == consoleCursorIsNow.Left)
            {
                ConsoleCursorDrawerTop = Console.CursorTop;
                ConsoleCursorDrawerLeft = Console.CursorLeft;
            }

            foreach (var cell in maze.Cells)
            {
                Console.SetCursorPosition(ConsoleCursorDrawerLeft + cell.X, ConsoleCursorDrawerTop + cell.Y);
                Console.Write(maze.GetTopLevelItem(cell.X, cell.Y).Symbol);
            }

            if(ConsoleCursorDrawerTop == consoleCursorIsNow.Top && ConsoleCursorDrawerLeft == consoleCursorIsNow.Left)
            {
                Console.SetCursorPosition(0, maze.Height);
                return;
            }

            Console.SetCursorPosition(0, ConsoleCursorDrawerTop + maze.Height);

            var copyOfHistory = maze.HistoryOfEvents.ToList();
            copyOfHistory.Reverse();
            var lastEvents = copyOfHistory.Take(5);

            foreach (var eventInfo in lastEvents)
            {
                Console.WriteLine($"{eventInfo}{new String(' ', Console.BufferWidth - eventInfo.Length)}");
            }
        }
    }
}