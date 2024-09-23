using System;
namespace Net18Online.Net18Online
{
    public class Bounds
    {
        public int min { get; set; }
        public int max { get; set; }

        public bool BoundsInput()
        {
            try
            {
                Console.WriteLine("Введите нижнюю границу диапазона: ");
                min = int.Parse(Console.ReadLine());  // Преобразование строки в int

                Console.WriteLine("Введите верхнюю границу диапазона: ");
                max = int.Parse(Console.ReadLine());  // Преобразование строки в int

                return true;  // Возвращаем true, если ввод прошёл успешно
            } catch( FormatException )
            {
                Console.WriteLine("Ошибка: Введите корректное число.");
                return false;  // Возвращаем false при ошибке формата
            }
        }

        public bool InRange( int value )
        {
            return value >= min && value <= max;
        }
    }
}