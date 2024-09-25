using MazeConsole.Models.Cells;
using MazeConsole.Models;
using System.Linq;
using System.Collections.Generic;

namespace MazeConsole.Builders
{
    public class MazeBuilder
    {
        private Maze _maze;
        private Random _random = new Random();

        public Maze Build(int width, int height)
        {
            _maze = new Maze
            {
                Width = width,
                Height = height,
            };

            BuildWall();

            ///<summary>
            /// Randomly select a starting point on the first row (y == 0)
            ///</summary>
            int startX = _random.Next(1, width - 1);
            if (startX % 2 == 0) startX++; // Ensure its an odd for proper passage creation

            CreatePassages(startX, 1);

            CreateSingleEntranceOnTopRow(startX);

            PlaceCoinsAndChests();

            return _maze;
        }

        /// <summary>
        /// Initialize all cells as walls
        /// </summary>
        public void BuildWall()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    _maze[x, y] = new Wall(x, y); 
                }
            }
        }

        /// <summary>
        /// Modify top row to make there is only one entrance
        /// </summary>
        private void CreateSingleEntranceOnTopRow(int entranceX)
        {
            for (int x = 0; x < _maze.Width; x++)
            {
                if (x == entranceX)
                {
                    _maze[x, 0] = new Ground(x, 0); 
                }
                else
                {
                    _maze[x, 0] = new Wall(x, 0); 
                }
            }
        }

        /// <summary>
        /// Create passages using a simple "random walk" algorithm
        /// </summary>
        private void CreatePassages(int x, int y)
        {
            ///<summary>
            /// Set the chosen cell as a GROUND cell (cell that can be passed through)
            ///</summary>
            _maze[x, y] = new Ground(x, y); 

            // Randomizing directions order (north, south, east, west)
            var directions = new List<(int dx, int dy)> { (0, -2), (0, 2), (-2, 0), (2, 0) };
            directions = directions.OrderBy(d => _random.Next()).ToList();

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (nx > 0 && ny > 0 && nx < _maze.Width - 1 && ny < _maze.Height - 1)
                {
                    if (_maze[nx, ny] is Wall)
                    {
                        // Set a passage through the intermediate cell
                        _maze[x + dx / 2, y + dy / 2] = new Ground(x + dx / 2, y + dy / 2);
                        _maze[nx, ny] = new Ground(nx, ny);

                        CreatePassages(nx, ny);
                    }
                }
            }
        }

        private void PlaceCoinsAndChests()
        {
            var totalCells = _maze.Width * _maze.Height;

            var maxCoins = (totalCells / 10) * 5;
            var maxChests = totalCells / 30;

            var coinCount = 0;
            var chestCount = 0;

            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    if (_maze[x, y] is Wall)
                        continue;

                    if (coinCount < maxCoins && CanPlaceCoin(x, y))
                    {
                        _maze[x, y] = new Coin(x, y);
                        coinCount++;
                    }

                    else if (chestCount < maxChests && CanPlaceChest(x, y))
                    {
                        _maze[x, y] = new Chest(x, y);
                        chestCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Checking if a COIN can be placed
        /// </summary>
        private bool CanPlaceCoin(int x, int y)
        {
            var groundCount = CountAdjacentGround(x, y);
            return groundCount >= 2;
        }

        /// <summary>
        /// Checking if a CHEST can be placed
        /// </summary>
        private bool CanPlaceChest(int x, int y)
        {
            var groundCount = CountAdjacentGround(x, y);
            return groundCount >= 1;
        }

        /// <summary>
        /// Counting neighboring GROUND cells
        /// </summary>
        private int CountAdjacentGround(int x, int y)
        {
            var count = 0;
            if (x > 0 && _maze[x - 1, y] is Ground) count++;
            if (x < _maze.Width - 1 && _maze[x + 1, y] is Ground) count++;
            if (y > 0 && _maze[x, y - 1] is Ground) count++;
            if (y < _maze.Height - 1 && _maze[x, y + 1] is Ground) count++;

            return count;
        }
    }
}
