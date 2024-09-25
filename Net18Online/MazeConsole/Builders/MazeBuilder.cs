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
            BuildGhost();

            return _maze;
        }


        /// <summary>
        /// Find a Wall with random coordinates and replace it with a Ghost 
        /// </summary>
        private void BuildGhost()
        {
            var x = 0;
            var y = 0;
            do
            {
                Random random_coordinate = new Random();
                x = random_coordinate.Next(0, _maze.Width - 1);
                y = random_coordinate.Next(0, _maze.Height - 1);
            }
            while (_maze[x, y].Symbol != '#');
            _maze[x, y] = new Ghost(x, y);
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
    }
}
