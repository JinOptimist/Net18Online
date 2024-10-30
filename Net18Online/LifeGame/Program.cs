using LifeGame.Model;
namespace LifeGame
{
    /*Запустив этот код, вы увидите эмуляцию игры "Жизнь" в консоли, где каждая клетка обозначена символом "O" для живых и "." для мёртвых.*/

    public class Program
    {
        public static void Main()
        {
            int rows = 20;
            int cols = 40;
            Field field = new Field(rows, cols);

            field.Randomize();

            while (true)
            {
                field.Display();
                field.UpdateField();
                Thread.Sleep(500); // Задержка между поколениями
            }
        }
    }
}