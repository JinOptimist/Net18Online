using MazeConsole.Models;
using MazeConsole.Models.Cells;
using MazeConsole.Models.Cells.Character;

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
            BuildBomb();

            BuildHero();
            BuilPit();
            return _maze;
        }

        private void BuildBomb()
        {
            var bom = new Bomb(1, 3, _maze);
            _maze[1, 3] = bom;
        }

        private void BuildHero()
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();
            var ground = GetRandom(grounds);
            var hero = new Hero(ground.X, ground.Y, _maze);
            _maze.Hero = hero;
        }

        private void BuildCoin()
        {
            var grounds = _maze.Cells
                .OfType<Ground>()
                .ToList();

            var randomGround = GetRandom(grounds);

            var coinX = randomGround.X;
            var coinY = randomGround.Y;
            var coin = new Coin(coinX, coinY, _maze);
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
                        _maze[x, y] = new Snake(x, y, _maze);
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
            _maze[x, y] = new Ghost(x, y, _maze);
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

                _maze[x, y] = new Dungeon(x, y, _maze);
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
            var wallReadyToDestroy = new List<BaseCell>();
            wallReadyToDestroy.Add(GetRandom(_maze.Cells));

            do
            {
                var miner = GetRandom(wallReadyToDestroy);
                _maze[miner.X, miner.Y] = new Ground(miner.X, miner.Y, _maze);
                wallReadyToDestroy.Remove(miner);

                var nearWalls = GetNearCells<Wall>(miner);
                wallReadyToDestroy.AddRange(nearWalls);

                wallReadyToDestroy = wallReadyToDestroy
                    .Where(wall =>
                        GetNearCells<Ground>(wall).Count == 1)
                    .ToList();

            } while (wallReadyToDestroy.Any());
        }

        private List<CellType> GetNearCells<CellType>(BaseCell miner)
            where CellType : BaseCell
        {
            return _maze
                .Cells
                .OfType<CellType>()
                .Where(cell =>
                   cell.X == miner.X && cell.Y == miner.Y + 1
                || cell.X == miner.X && cell.Y == miner.Y - 1
                || cell.X == miner.X + 1 && cell.Y == miner.Y
                || cell.X == miner.X - 1 && cell.Y == miner.Y)
                .ToList();
        }

        public void BuildWall()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    _maze[x, y] = new Wall(x, y, _maze);
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
                        _maze[x, y] = new Water(x, y, _maze);
                    }
                }
            }
        }
        private void BuilPit()
        {
            var count = 0;
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    count++;
                    if (count % 4 == 0)
                    {
                        _maze[x, y] = new Pit(x, y, _maze);
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
                            _maze[x, y] = new Window(x, y, _maze);
                            windowCount++;
                            break;
                        case 2:
                            return;
                    }
                }
            }
        }

        private T GetRandom<T>(List<T> cells)
        {
            var countOfCells = cells.Count();
            var randomIndex = _random.Next(0, countOfCells);
            var randomCell = cells[randomIndex];
            return randomCell;
        }
    }
}
