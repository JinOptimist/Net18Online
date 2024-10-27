using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Model
{
    public class Field
    {
        private int _rows;
        private int _cols;
        private Cell[,] _cells;

        public Field(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _cells = new Cell[rows, cols];

            // Инициализация клеток
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _cells[i, j] = new Cell();
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
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    _cells[i, j].SetAlive(random.Next(2) == 0);
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

                    if (neighborRow >= 0 && neighborRow < _rows &&
                        neighborCol >= 0 && neighborCol < _cols &&
                        _cells[neighborRow, neighborCol].IsAlive)
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
            Cell[,] newField = new Cell[_rows, _cols];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    int aliveNeighbors = GetAliveNeighbors(i, j);
                    bool isAlive = _cells[i, j].IsAlive;

                    // Применяем правила
                    if (isAlive && (aliveNeighbors < 2 || aliveNeighbors > 3))
                    {
                        isAlive = false;
                    }
                    else if (!isAlive && aliveNeighbors == 3)
                    {
                        isAlive = true;
                    }

                    newField[i, j] = new Cell(isAlive);
                }
            }

            _cells = newField;
        }

        // Вывод поля на экран
        public void Display()
        {
            Console.Clear();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Console.Write(_cells[i, j].IsAlive ? "O" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}
