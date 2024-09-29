using MazeConsole.Models.Cells;
using MazeConsole.Models;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

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

            int startX = _random.Next(1, width - 1);
            if (startX % 2 == 0)
            {
                startX++; // Ensure the start is an odd for proper working of CreatePassages()
            }

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
        /// Create passages using a "random walk" algorithm
        /// </summary>
        private void CreatePassages(int x, int y)
        {
            _maze[x, y] = new Ground(x, y); // Set the chosen cell as a passage cell (ground is passagable now)

            // Randomize directions: north, south, east, west
            var directions = new List<(int changeX, int changeY)> { (0, -2), (0, 2), (-2, 0), (2, 0) };
            directions = directions.OrderBy(d => _random.Next()).ToList();

            foreach (var (changeX, changeY) in directions)
            {
                var newX_Position = x + changeX;
                var newY_Position = y + changeY;

                if (newX_Position > 0 && newY_Position > 0 && newX_Position < _maze.Width - 1 && newY_Position < _maze.Height - 1)
                {
                    if (_maze[newX_Position, newY_Position] is Wall)
                    {
                        _maze[newX_Position, newY_Position] = new Ground(newX_Position, newY_Position);

                        // Create a passage through the intermediate cell
                        var intermediateCell_X = x + changeX / 2;
                        var intermediateCell_Y = y + changeY / 2;
                        _maze[intermediateCell_X, intermediateCell_Y] = new Ground(intermediateCell_X, intermediateCell_Y);

                        CreatePassages(newX_Position, newY_Position);
                    }
                }
            }
        }

        /// <summary>
        /// Place coins and chests in the maze
        /// </summary>
        private void PlaceCoinsAndChests()
        {
            var totalCells = _maze.Width * _maze.Height;

            var maxCoins = totalCells / 15;
            var maxChests = totalCells / 90;

            var coinCount = 0;
            var chestCount = 0;

            var availableCells = new List<(int x, int y)>();

            /// <summary>
            /// Shuffle all available passagable cells to make it look naturally
            /// </summary>
            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    if (_maze[x, y] is Ground)
                    {
                        availableCells.Add((x, y));
                    }
                }
            }
            availableCells = availableCells.OrderBy(c => _random.Next()).ToList();

            foreach (var (x, y) in availableCells)
            {
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

                if (coinCount >= maxCoins && chestCount >= maxChests)
                {
                    break;
                }
            }
        }


        /// <summary>
        /// Checking if a COIN can be placed
        /// </summary>
        private bool CanPlaceCoin(int x, int y)
        {
            var validAdjacentCount = CountAdjacentValidCells(x, y);
            return validAdjacentCount >= 2; 
        }

        /// <summary>
        /// Counts adjacent valid cells
        /// </summary>
        private int CountAdjacentValidCells(int x, int y)
        {
            var count = 0;

            if (x > 0 && IsValidAdjacentCell(_maze[x - 1, y]))
            {
                count++;
            }
            if (x < _maze.Width - 1 && IsValidAdjacentCell(_maze[x + 1, y]))
            {
                count++;
            }
            if (y > 0 && IsValidAdjacentCell(_maze[x, y - 1]))
            {
                count++;
            }
            if (y < _maze.Height - 1 && IsValidAdjacentCell(_maze[x, y + 1]))
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Determines if a cell is valid for adjacency (Ground, Coin, or Chest)
        /// </summary>
        private bool IsValidAdjacentCell(BaseCell cell)
        {
            return cell is Ground || cell is Coin || cell is Chest;
        }

        /// <summary>
        /// Checking if a CHEST can be placed
        /// </summary>
        private bool CanPlaceChest(int x, int y)
        {
            var validAdjacentCount = CountAdjacentValidCells(x, y);
            return validAdjacentCount >= 1; 
        }
    }
}
