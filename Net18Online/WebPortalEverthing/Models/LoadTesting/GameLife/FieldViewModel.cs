using LifeGame.Model;

namespace WebPortalEverthing.Models.LoadTesting.GameLife
{
    /* представляет клетку и её состояние. бизнес модель с данными находится в C:\Users\svetlana.terekhova\source\repos\Net18Online\Net18Online\LifeGame\Model\Cell.cs 
 это модель представления без методов, одни поля и пустой конструктор. При необходимости, перекладывать из View модели в дата модель (бизнес) */

    public class FieldViewModel
    {
        private int _rows;
        private int _cols;
        private CellViewModel[,] _cells; // 2D-массив клеток

        public int Rows { get => _rows; set => _rows = value; }
        public int Cols { get => _cols; set => _cols = value; }
        public CellViewModel[,] Cells { get => _cells; set => _cells = value; }
    }
}