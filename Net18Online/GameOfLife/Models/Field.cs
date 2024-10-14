using System;

namespace GameOfLife.Models
{
    public class Field
    {

        public int CurentGeneration {  get; set; }

        private int width;

        private int height;

        private bool[,]? field;
        public void StartGame(int height, int width)
        {
            SetDefaultField(height, width);
            var fieldDrawer = new FieldDrawer();
            do
            {
                Console.Title = CurentGeneration.ToString();
                var field = GetCurentGeneration();
                fieldDrawer.Draw(field);
                GetFutureGeneration();
            }
            while (true);
        }
        public void SetDefaultField(int width, int height)
        {
            this.width = width;
            this.height = height;
            field = new bool[height, width];
            var random = new Random();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    field[i, j] = random.Next(2) == 0;
                }
            }
        }

        public int CountNeibor(int x, int y)
        {
            var numberOfCells = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + height) % height;
                    var row = (x + j + width) % width;
                    var check = false;
                    if (col == x && row == y)
                    {
                        check = true;
                    }
                    var life = field[col, row];
                    if (life && !check)
                    {
                        numberOfCells++;
                    }
                }
            }
            return numberOfCells;
        }

        public void GetFutureGeneration()
        {
            var newField = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var numberOfNeigbors = CountNeibor(i, j);
                    var life = field[i, j];
                    if (!life && numberOfNeigbors == 3)
                    {
                        newField[i, j] = true;
                    }
                    else if (life && (numberOfNeigbors < 2 || numberOfNeigbors > 3))
                    {
                        newField[i, j] = false;
                    }
                    else
                    {
                        newField[i, j] = field[i, j];
                    }
                }
            }
            field = newField;
            CurentGeneration++;
        }

        public bool[,] GetCurentGeneration()
        {
            var result = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = field[i, j];
                }
            }
            return result;
        }
    }
}