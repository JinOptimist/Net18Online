using MazeConsole.Models;

namespace MazeConsole
{
    public class MazeDrawer
    {
        private char[,] _previousState;

        public MazeDrawer()
        {
            _previousState = null;
        }

        public virtual void Draw(Maze maze)
        {
            if (_previousState == null)
            {
                _previousState = new char[maze.Width, maze.Height];
                InitializeAndDrawFullMaze(maze);
            }
            else
            {
                UpdateMaze(maze);
            }
        }

        private void InitializeAndDrawFullMaze(Maze maze)
        {
            Console.Clear();
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var cell = maze.GetTopLevelItem(x, y);
                    _previousState[x, y] = cell.Symbol;
                    Console.Write(cell.Symbol);
                }
                Console.WriteLine();
            }
        }

        private void UpdateMaze(Maze maze)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var cell = maze.GetTopLevelItem(x, y);
                    if (_previousState[x, y] != cell.Symbol)
                    {
                        ///<summary>
                        ///If a cell has changed, we update its symbol on the screen
                        ///</summary>
                        Console.SetCursorPosition(x, y);
                        Console.Write(cell.Symbol);

                        ///<summary>
                        ///Update the state of the previous
                        ///</summary>
                        _previousState[x, y] = cell.Symbol;
                    }
                }
            }

            ///<summary>
            ///Return the cursor to the end of the screen
            ///</summary>
            Console.SetCursorPosition(0, maze.Height);
        }
    }
}
