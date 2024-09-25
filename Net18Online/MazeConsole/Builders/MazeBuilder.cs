using MazeConsole.Models;
using MazeConsole.Models.Cells;

namespace MazeConsole.Builders
{
    public class MazeBuilder
    {
        private Random _random = new();
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
            BuildWater();
            BuildGhost();
            BuildSnake();
            BuildDungeon();
            BuildWindow();
            BuildCoin();

            return _maze;
        }

        private void BuildCoin()
        {
            var grounds = _maze.Cells
                .OfType<Ground>()
                .ToList();

            var countOfCellOfTypeGround = grounds.Count();
            var randomIndex = _random.Next(0, countOfCellOfTypeGround);
            var randomGround = grounds[randomIndex];

            var coinX = randomGround.X;
            var coinY = randomGround.Y;
            var coin = new Coin(coinX, coinY);
            _maze[coin.X, coin.Y] = coin;
        }

        private void BuildSnake()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    if (x == y)
                    {
                        _maze[x, y] = new Snake(x, y);
                    }
                }
            }
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
                x = _random.Next(0, _maze.Width - 1);
                y = _random.Next(0, _maze.Height - 1);
            }
            while (_maze[x, y].Symbol == '.');
            _maze[x, y] = new Ghost(x, y);
        }

        /// <summary>
        /// Find a ground with a random coordinates and replace it with Dungeon
        /// </summary>
        private void BuildDungeon()
        {
            var dungeonCount = AutoDungeonCount();
            var x = 0;
            var y = 0;

            for (var i = 0; i < dungeonCount; i++)
            {
                do
                {
                    x = _random.Next(0, _maze.Width - 1);
                    y = _random.Next(0, _maze.Height - 1);
                }
                while (_maze[x, y].Symbol == '#' || _maze[x, y] is Dungeon);

                _maze[x, y] = new Dungeon(x, y);
            }

        }

        private int AutoDungeonCount()
        {
            var size = Math.Min(_maze.Width, _maze.Height);
            var dungeonCount = size / 10;

            return dungeonCount > 0
                ? dungeonCount
                : 1;

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

        /// <summary>
        /// Build cell with Water type
        /// </summary>
        public void BuildWater()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    if (x % 3 == 0 && y % 2 == 0)
                    {
                        _maze[x, y] = new Water(x, y);
                    }
                }
            }
        }

        public void BuildWindow()
        {
            int windowCount = 0;
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    switch (windowCount)
                    {
                        case < 2 when _maze[x, y] is Wall:
                            _maze[x, y] = new Window(x, y);
                            windowCount++;
                            break;
                        case 2:
                            return;
                    }
                }
            }
        }
    }
}
