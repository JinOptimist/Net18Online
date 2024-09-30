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

            BuildHero();
            BuilPit();
            return _maze;
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

        public void BuildGround()
        {
            var sets = new Dictionary<int, List<BaseCell>>();
            var nextSetId = 1;

            for (int y = 0; y < _maze.Height; y += 2)
            {
                for (int x = 0; x < _maze.Width; x += 2)
                {
                    _maze[x, y] = new Ground(x, y, _maze);
                    if (!sets.ContainsKey(nextSetId))
                    {
                        sets[nextSetId] = new List<BaseCell>();
                    }
                    sets[nextSetId].Add(_maze[x, y]);
                    nextSetId++;
                }

                if (y < _maze.Height - 1)
                {
                    MergeSets(sets, y);
                    CreateVerticalConnections(sets, y);
                }
            }
        }

        private void MergeSets(Dictionary<int, List<BaseCell>> sets, int y)
        {
            foreach (var set in sets.Values)
            { 
                var mergeCandidates = set.Where(cell => cell.Y == y).ToList();
                foreach (var cell in mergeCandidates)
                {
                    if (_random.Next(2) == 0)
                    {
                        var rightCell = _maze[cell.X + 2, cell.Y];
                        if (rightCell is Wall && GetNearCells<Ground>(rightCell).Count == 1)
                        {
                            _maze[cell.X + 1, cell.Y] = new Ground(cell.X + 1, cell.Y, _maze);
                            _maze[cell.X + 2, cell.Y] = new Ground(cell.X + 2, cell.Y, _maze);
                            set.Add(_maze[cell.X + 2, cell.Y]);
                        }
                    }
                }
            }
        }

        private void CreateVerticalConnections(Dictionary<int, List<BaseCell>> sets, int y)
        {
            foreach (var set in sets.Values)
            {
                var verticalConnections = set.Where(cell => cell.Y == y).ToList();
                foreach (var cell in verticalConnections)
                {
                    if (_random.Next(2) == 0)
                    {
                        var belowCell = _maze[cell.X, cell.Y + 2];
                        if (belowCell is Wall && GetNearCells<Ground>(belowCell).Count == 1)
                        {
                            _maze[cell.X, cell.Y + 1] = new Ground(cell.X, cell.Y + 1, _maze);
                            _maze[cell.X, cell.Y + 2] = new Ground(cell.X, cell.Y + 2, _maze);
                            set.Add(_maze[cell.X, cell.Y + 2]);
                        }
                    }
                }
            }
        }

        private List<CellType> GetNearCells<CellType>(BaseCell cell)
        where CellType : BaseCell
        {
            return _maze.Cells
            .OfType<CellType>()
            .Where(c =>
                (c.X == cell.X && (c.Y == cell.Y + 1 || c.Y == cell.Y - 1)) ||
                (c.Y == cell.Y && (c.X == cell.X + 1 || c.X == cell.X - 1)))
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
            var windowCount = 0;
            for (int y = 0; y < _maze.Height; y++)
            {
                for (var x = 0; x < _maze.Width; x++)
                {
                    if (windowCount <= 2 && _maze[x, y] is Wall && !IsWindowNearby(x, y))
                    {
                        _maze[x, y] = new Window(x, y, _maze);
                        windowCount++;
                    }
                     if (windowCount > 2)
                    {
                        return;
                     }
                }
            }
        }
        private bool IsWindowNearby(int x, int y)
        {
            var radius = 5;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    var newX = x + i;
                    var newY = y + j;
                    if (newX >= 0 && newX < _maze.Width && newY >= 0 && newY < _maze.Height)
                    {
                        if (_maze[newX, newY] is Window)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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
