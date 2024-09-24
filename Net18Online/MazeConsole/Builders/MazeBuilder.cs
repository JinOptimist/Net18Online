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
            BuildChest();

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

        private void BuildChest()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    if (x == 3 && y % 2 != 1)
                    {
                        _maze[x, y] = new Chest(x, y);
                    }
                }
            }
        }
    }
}
