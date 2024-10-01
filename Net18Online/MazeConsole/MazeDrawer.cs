using MazeConsole.DrawingAssistant;
using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeConsole
{
    public class MazeDrawer
    {
        private DrawingPoints _startingPointsForDrawingAMaze = new();
        public virtual void Draw(Maze maze)
        {
            Console.Clear();
            Console.WriteLine($"Maze has {maze.Cells.Count} cells");

            _startingPointsForDrawingAMaze.ConsoleCursorDrawerLeft = Console.CursorLeft;
            _startingPointsForDrawingAMaze.ConsoleCursorDrawerTop = Console.CursorTop;

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

            foreach(var cell in maze.Cells)
            {
                Console.SetCursorPosition(_startingPointsForDrawingAMaze.ConsoleCursorDrawerLeft + cell.X, _startingPointsForDrawingAMaze.ConsoleCursorDrawerTop + cell.Y);
                Console.Write(maze.GetTopLevelItem(cell.X, cell.Y).Symbol);
            }

            Console.SetCursorPosition(consoleCursorIsNow.Left, consoleCursorIsNow.Top);
        }
    }
}