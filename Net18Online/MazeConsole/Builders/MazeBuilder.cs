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
            BuildTreasury();

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
        public void BuildTreasury()
        {
            var numberOfTreasuries = 0;
            if (_maze.Width < _maze.Height)
            {
                numberOfTreasuries = _maze.Width / 5;
            }
            else
            {
                numberOfTreasuries = _maze.Height / 5;
            }
            for (int y = 0; y < numberOfTreasuries; y++)
            {
                Random rnd = new Random();
                int valueWidth = rnd.Next(0, _maze.Width - 1);
                int valueHeight = rnd.Next(0, _maze.Height - 1);
                _maze[valueWidth, valueHeight] = new Treasury(valueWidth, valueHeight);

            }
        }
    }
}
