using GameOfLife.Models;
namespace GameOfLife
{
    public class FieldDrawer
    {
        public void Draw(Field field)
        {
            Console.Clear();
            Console.WriteLine($"Field has {field.Cells.Count} cells");
        }
    }
}
