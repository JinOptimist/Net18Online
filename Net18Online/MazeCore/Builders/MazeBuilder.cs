using MazeCore.Helpers;
using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Builders
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

            // Build maze
            BuildWall();
            BuildGround();
            BuildTreasury();
            BuildWater();
            BuildGhost();
            BuildSnake();
            BuildDungeon();
            BuildWindow();
            BuildCoin();
            BuildTeleport();
            BuilPit();

            // Build Npc
            BuildGoblins();


            // Build Hero
            BuildHero();
            return _maze;
        }

        private void BuildGoblins(int goblinCount = 3)
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();
            for (int i = 0; i < goblinCount; i++)
            {
                var ground = GetRandom(grounds);
                var goblin = new Goblin(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(goblin);
                grounds.Remove(ground);
            }
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
        /// Find a Ground with random coordinates and replace it with a Ghost 
        /// </summary>
        private void BuildGhost()
        {
            var grounds = _maze.Cells
                .OfType<Ground>()
                .ToList();

            var randomGround = GetRandom(grounds);

            var ghostX = randomGround.X;
            var ghostY = randomGround.Y;
            var ghost = new Ghost(ghostX, ghostY, _maze);
            _maze[ghost.X, ghost.Y] = ghost;
        }

        /// <summary>
        /// Find a ground with a random coordinates and replace it with Dungeon
        /// </summary>
        private void BuildDungeon()
        {
            var dungeonCount = AutoDungeonCount();

            for (var i = 0; i < dungeonCount; i++)
            {
                var grounds = _maze.Cells
                .OfType<Ground>()
                .ToList();

                var randomGround = GetRandom(grounds);

                var dungeX = randomGround.X;
                var dungeY = randomGround.Y;

                var dunge = new Dungeon(dungeX, dungeY, _maze);

                _maze[dunge.X, dunge.Y] = dunge;
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
            return MazeHelper.GetNearCells<CellType>(_maze, miner);
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

            var groundCells = _maze.Cells.OfType<Ground>().ToList();

            foreach (var groundCell in groundCells)
            {
                var nearGrounds = GetNearCells<Ground>(groundCell);
                if (nearGrounds.Count == 3)
                {
                    _maze[groundCell.X, groundCell.Y] = new Pit(groundCell.X, groundCell.Y, _maze);
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
            return MazeHelper.GetRandom(_maze, cells);
        }
        private void BuildTreasury()
        {
            var grounds = _maze.Cells
                .OfType<Ground>()
                .Where(ground => 
                       _maze[ground.X - 1, ground.Y] is Wall && _maze[ground.X + 1, ground.Y] is Wall
                    || _maze[ground.X, ground.Y - 1] is Wall && _maze[ground.X, ground.Y + 1] is Wall
                )
                .ToList();

            var randomGround = GetRandom(grounds);
            var treasuryX = randomGround.X;
            var treasuryY = randomGround.Y;
            var treasury = new Treasury(treasuryX, treasuryY, _maze);
        }

        private void BuildTeleport()
        {
            var numberTeleportCellsInMaze = 5;

            var groundCellsWithTwoWallNeighborhood = _maze
                .Cells
                .OfType<Ground>()
                .Where(cell => GetSimilarCellOnAxisX<Teleport>(cell).Count == 0)
                .ToList();

            var countIteration = groundCellsWithTwoWallNeighborhood.Count < numberTeleportCellsInMaze
                ? groundCellsWithTwoWallNeighborhood.Count
                : numberTeleportCellsInMaze;

            for (var i = 1; i <= countIteration; i++)
            {
                var randomCell = GetRandom(groundCellsWithTwoWallNeighborhood);

                _maze[randomCell.X, randomCell.Y] = new Teleport(randomCell.X, randomCell.Y, _maze);

                groundCellsWithTwoWallNeighborhood.Remove(randomCell);
            }
        }

        private List<T> GetSimilarCellOnAxisX<T>(BaseCell cell)
            where T : BaseCell
        {
            return _maze
                .Cells
                .OfType<T>()
                .Where(c => c.X == cell.X)
                .ToList();
        }
    }
}
