﻿using MazeCore.Helpers;
using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Builders
{
    public class MazeBuilder
    {
        private Maze _maze;

        public Maze BuildEmptyMaze(int width = 10, int height = 10)
        {
            _maze = new Maze
            {
                Width = width,
                Height = height,
            };

            // Build maze
            BuildWall();
            BuildGround();

            return _maze;
        }

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
            BuildDungeon();
            BuildWindow();
            BuildCoin();
            BuildTeleport();
            BuilPit();
            BuilWolfs();
            BuildChest();
            BuildMagic();

            // Build Npc
            BuildGoblins();
            BuildBunny(2);
            BuildAlcoholic();
            BuildSnake();
            BuildCat();
            BuildWanderingMerchant();
            BuildAlcoholic();
            BuildSnake();
            BuildCat();
            BuildGhost();
            BuildKing();
            BuildSlime();
            BuildOrc();
            BuildLamer();

            // Build Hero
            BuildHero();

            return _maze;
        }

        private void BuildWanderingMerchant()
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();
            var merchantCount = Math.Max(1, _maze.Width * _maze.Height / 200); // To Ensure, that at least 1 Merchant present at Maze

            for (int i = 0; i < merchantCount; i++)
            {
                var ground = GetRandom(grounds);

                var merchant = new WanderingMerchant(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(merchant);

                grounds.Remove(ground);
            }
        }

        private void BuildAlcoholic()
        {
            var grounds = _maze
                .Cells
                .OfType<Ground>()
                .ToList();
            var randomGround = GetRandom(grounds);

            var alcoholic = new Alcoholic(randomGround.X, randomGround.Y, _maze);
            _maze.Npcs.Add(alcoholic);
            grounds.Remove(randomGround);
        }

        private void BuilWolfs()
        {
            var places = _maze.Cells.OfType<BaseCell>()
           .Where(cell => !(cell is Wall))
           .ToList();
            var place = GetRandom(places);
            var wolf = new Wolf(place.X, place.Y, _maze);
            _maze.Npcs.Add(wolf);
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
        private void BuildBunny(int bunnyCount)
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();
            for (int i = 0; i < bunnyCount; i++)
            {
                var ground = GetRandom(grounds);
                var bunny = new Bunny(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(bunny);
                grounds.Remove(ground);
            }
        }

        private void BuildCat()
        {
            var CatInCenter = new Cat((int)_maze.Height / 2, (int)_maze.Width / 2, _maze);
            _maze.Npcs.Add(CatInCenter);
        }

        private void BuildKing()
        {
            var walls = _maze.Cells.OfType<Wall>().ToList();
            var wall = GetRandom(walls);

            var king = new King(wall.X, wall.Y, _maze);
            _maze.Npcs.Add(king);
        }

        private void BuildSlime()
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();

            var ground = GetRandom(grounds);
            var Slime = new Slime(ground.X, ground.Y, _maze);
            _maze.Npcs.Add(Slime);
            grounds.Remove(ground);
        }

        private void BuildOrc(int orcCount = 2)
        {

            var grounds = _maze.Cells
            .OfType<Ground>()
            .Where(ground =>
                   _maze[ground.X - 1, ground.Y] is Treasury || _maze[ground.X + 1, ground.Y] is Treasury
                || _maze[ground.X, ground.Y - 1] is Treasury || _maze[ground.X, ground.Y + 1] is Treasury
            )
            .ToList();

            if (!grounds.Any())
            {
                return;
            }

            for (int i = 0; i < orcCount; i++)
            {
                var ground = GetRandom(grounds);
                var orc = new Orc(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(orc);
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

        private void BuildSnake(int snakeCount = 1)
        {
            var listOfLandsThatHaveTwoWalls = GetSurroundedCells<Ground, Wall>(2);
            var listOfCorners = GetCorners<Ground, Wall>(listOfLandsThatHaveTwoWalls);

            for (int i = 0; i < snakeCount; i++)
            {
                var ground = GetRandom(listOfCorners);
                var snake = new Snake(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(snake);
                listOfCorners.Remove(ground);
            }
        }
        private List<ResultingCell> GetSurroundedCells<ResultingCell, SurroundingCell>(int theNumberOfThoseWhoSurrounded)
            where ResultingCell : BaseCell
            where SurroundingCell : BaseCell
        {
            return _maze
                .Cells
                .OfType<ResultingCell>()
                .Where(ground => MazeHelper.GetNearCells<SurroundingCell>(_maze, ground).Count == theNumberOfThoseWhoSurrounded)
                .ToList();
        }

        private List<ResultingCell> GetCorners<ResultingCell, SurroundingCell>(List<ResultingCell> cells)
            where ResultingCell : BaseCell
            where SurroundingCell : BaseCell
        {
            return cells.
                Where(cell =>
                   _maze[cell.X, cell.Y + 1] is SurroundingCell && _maze[cell.X - 1, cell.Y] is SurroundingCell
                || _maze[cell.X, cell.Y + 1] is SurroundingCell && _maze[cell.X + 1, cell.Y] is SurroundingCell
                || _maze[cell.X, cell.Y - 1] is SurroundingCell && _maze[cell.X - 1, cell.Y] is SurroundingCell
                || _maze[cell.X, cell.Y - 1] is SurroundingCell && _maze[cell.X + 1, cell.Y] is SurroundingCell)
                .ToList();
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
            var ghost = new Ghost(randomGround.X, randomGround.Y, _maze);
            _maze.Npcs.Add(ghost);

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
            var wallReadyToDestroy = new List<IBaseCell>();
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

        private List<CellType> GetNearCells<CellType>(IBaseCell miner)
            where CellType : IBaseCell
        {
            return MazeHelper.GetNearCells<CellType>(_maze, miner);
        }

        private void BuildWall()
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
        private void BuildWater()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                var randomX = _maze.Random.Next(0, _maze.Width + 1);
                if (_maze[randomX, y] is Ground)
                {
                    _maze[randomX, y] = new Water(randomX, y, _maze);
                }
            }
        }
        private void BuilPit()
        {

            var groundCells = _maze.Cells.OfType<Ground>().ToList();

            foreach (var groundCell in groundCells)
            {
                var nearGrounds = GetNearCells<Ground>(groundCell);
                if (nearGrounds.Count >= 3)
                {
                    _maze[groundCell.X, groundCell.Y] = new Pit(groundCell.X, groundCell.Y, _maze);
                }
            }
        }
        private void BuildWindow()
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
            return _maze
                    .Cells
                    .OfType<Window>()
                    .Any(cell => Math.Sqrt(Math.Pow(cell.X - x, 2) + Math.Pow(cell.Y - y, 2)) < 5);
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

        private void BuildMagic()
        {
            var magics = _maze.Cells
                .OfType<Ground>()
                .ToList();

            var randomMagic = GetRandom(magics);

            var magicX = randomMagic.X;
            var magicY = randomMagic.Y;
            var magic = new Magic(magicX, magicY, _maze);
            _maze[magic.X, magic.Y] = magic;
        }
        private void BuildLamer(int lamerCount = 2)
        {
            var grounds = _maze
                .Cells
                .OfType<Ground>()
                .ToList();

            for (int i = 0; i < lamerCount; i++)
            {
                var ground = GetRandom(grounds);
                if (ground is null)
                {
                    _maze.HistoryOfEvents.Add("No Grounds to build Lamer");
                    break;
                }

                var lamer = new Lamer(ground.X, ground.Y, _maze);
                _maze.Npcs.Add(lamer);
                grounds.Remove(ground);
            }
        }

        private void BuildChest()
        {
            var groundsWithThreeWalls = _maze
                .Cells
                .OfType<Ground>()
                .Where(cell =>
                GetNearCells<Wall>(cell).Count == 3)
                .ToList();

            for (var i = 0; i < groundsWithThreeWalls.Count; i++)
            {
                var ground = groundsWithThreeWalls[i];
                _maze[ground.X, ground.Y] = new Chest(ground.X, ground.Y, _maze);
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
