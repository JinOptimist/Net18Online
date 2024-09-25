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
            BuildTeleport();

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

        private void BuildTeleport()
        {
            var portalInputCells = _maze.Cells.First(c => c.Symbol == '.');
            var portalOutputCells = _maze.Cells.Last(c => c.Symbol == '.');

            _maze[portalInputCells.X, portalInputCells.Y] = new Teleport(portalInputCells.X, portalInputCells.Y);
            _maze[portalOutputCells.X, portalOutputCells.Y] = new Teleport(portalOutputCells.X, portalOutputCells.Y);
        }
    }
}
