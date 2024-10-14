using GameOfLife.Models;
namespace GameOfLife
{
    public class FieldDrawer
    {
        public void Draw(bool[,] field)
        {
            for (int i = 0; i < field.GetLength(1); i++)
            {
                var tempString = new char[field.GetLength(1)];
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    if (field[i, j])
                    {
                        tempString[i] = '0';
                    }
                    else
                    {
                        tempString[i] = 'X';
                    }
                }
                Console.WriteLine(tempString);
            }
            Console.SetCursorPosition(0, 0);
        }
    }
}
