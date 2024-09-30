using MazeCore.Models;
using MazeCore.Models.Cells;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace MazeCore.Helpers
{
    public class MazeHelper
    {
        public static List<CellType> GetNearCells<CellType>(Maze maze, BaseCell miner)
             where CellType : BaseCell
        {
            return maze
                .Cells
                .OfType<CellType>()
                .Where(cell =>
                   cell.X == miner.X && cell.Y == miner.Y + 1
                || cell.X == miner.X && cell.Y == miner.Y - 1
                || cell.X == miner.X + 1 && cell.Y == miner.Y
                || cell.X == miner.X - 1 && cell.Y == miner.Y)
                .ToList();
        }

        public static T GetRandom<T>(Maze maze, List<T> cells)
        {
            var countOfCells = cells.Count();
            var randomIndex = maze.Random.Next(0, countOfCells);
            var randomCell = cells[randomIndex];
            return randomCell;
        }
    }
}
