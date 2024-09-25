using MazeConsole.Models;
using MazeConsole.Models.Cells;

namespace MazeConsole.Builders
{
    public class MazeBuilder
    {
        private Maze _maze;

        public Maze Build(int width, int height)
        {
            _maze = new Maze
            {
                Width = width,
                Height = height,
            };

            BuildWall();
            BuildGround();
            BuildPit();

            return _maze;
        }

        private void BuildGround()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    if (x % 2 != y % 2)
                    {
                        _maze[x, y] = new Ground(x, y);
                    }
                }
            }
        }

        public void BuildWall()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    _maze[x, y] = new Wall(x, y);
                }
            }
        }
        public void BuildPit()
        {
            var count = 0;
            for (var y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    count++;
                    if (count % 4 == 0)
                    {
                        _maze[x, y] = new Pit(x, y);
                    }
                }
            }
        }
    
    }
}
