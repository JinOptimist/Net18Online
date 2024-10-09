using GameOfLife.Models;
using GameOfLife.Models.Cells;

namespace GameOfLife.Builder
{
    public class FieldBuilder
    {
        private Field _field;
        public Field Build(int width, int height)
        {

            _field = new Field
            {
                Width = width,
                Height = height,
            };
            return _field;
            BuildBaseDeadCellField();
        }
        public void BuildBaseDeadCellField()
        {
            var cellsRearyToBeDead = _field.Cells.OfType<BaseCell>().ToList();

            var deadCellX = cellsRearyToBeDead.X;
            var deadCellY = cellsRearyToBeDead.Y;
            var deadCell = new DeadCell(deadCellX, deadCellY, _field);
            _field[deadCell.X, deadCell.Y] = deadCell;
        }
    }
}
