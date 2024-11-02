namespace Everything.Data.Interface.Models
{
    public interface IFieldData : IBaseModel
    {

        public int Rows { get; set; }
        public int Cols { get; set; }
        public ICellData[,] Cells
        {
            get
            {
                // Return the cells of the field
                return Cells;
            }
            set
            {
                // Set the cells of the field
                Cells = value;
            }
        }

        /* Заполняем поле случайными клетками
        random.Next(2) – этот метод возвращает случайное целое число от 0 до 1 (всего два возможных значения: 0 или 1).
        random.Next(2) == 0 – здесь происходит проверка: если random.Next(2) вернул 0, то условие будет истинным (true), а если 1 – ложным (false).
        SetAlive(...) – метод SetAlive у клетки (например, Cell) устанавливает её состояние. Если передать в него true, клетка станет "живой", если false – "мертвой". */

        public void Randomize();

        // Метод для получения числа живых соседей
        public int GetAliveNeighbors(int row, int col);

        // Обновление поля по правилам игры "Жизнь"
        public void UpdateField();


        // Вывод поля на экран
        public void Display();
    }
}
