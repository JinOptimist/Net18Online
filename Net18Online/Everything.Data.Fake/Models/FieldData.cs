using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class FieldData : BaseModel, IFieldData
    {

        private int _rows;
        private int _cols;
        private CellData[,] _cells; // 2D-массив клеток

        public int Rows { get => _rows; set => _rows = value; }
        public int Cols { get => _cols; set => _cols = value; }
        public CellData[,] Cells { get => _cells; set => _cells = value; }
        ICellData[,] IFieldData.Cells { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FieldData(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            Cells = new CellData[rows, cols];

            // Инициализация клеток
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cells[i, j] = new CellData();
                }
            }
        }

        /* Заполняем поле случайными клетками
        random.Next(2) – этот метод возвращает случайное целое число от 0 до 1 (всего два возможных значения: 0 или 1).
        random.Next(2) == 0 – здесь происходит проверка: если random.Next(2) вернул 0, то условие будет истинным (true), а если 1 – ложным (false).
        SetAlive(...) – метод SetAlive у клетки (например, Cell) устанавливает её состояние. Если передать в него true, клетка станет "живой", если false – "мертвой". */

        public void Randomize()
        {
            Random random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Cells[i, j].SetAlive(random.Next(2) == 0);
                }
            }
        }

        // Метод для получения числа живых соседей
        private int GetAliveNeighbors(int row, int col)
        {
            int aliveNeighbors = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    if (neighborRow >= 0 && neighborRow < Rows &&
                        neighborCol >= 0 && neighborCol < Cols &&
                        Cells[neighborRow, neighborCol].IsAlive)
                    {
                        aliveNeighbors++;
                    }
                }
            }

            return aliveNeighbors;
        }

        // Обновление поля по правилам игры "Жизнь"
        public void UpdateField()
        {
            CellData[,] newField = new CellData[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int aliveNeighbors = GetAliveNeighbors(i, j);
                    bool isAlive = Cells[i, j].IsAlive;

                    // Применяем правила
                    if (isAlive && (aliveNeighbors < 2 || aliveNeighbors > 3))
                    {
                        isAlive = false;
                    }
                    else if (!isAlive && aliveNeighbors == 3)
                    {
                        isAlive = true;
                    }

                    newField[i, j] = new CellData(isAlive);
                }
            }

            Cells = newField;
        }

        // Вывод поля на экран
        public void Display()
        {
            Console.Clear();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write(Cells[i, j].IsAlive ? "O" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}