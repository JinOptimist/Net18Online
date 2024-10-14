using TagGame.Classes.Base;

namespace TagGame.Classes.ConsoleDrawer
{
    public class Drawer
    {
        public void FirstPrint(Field field)
        {
            Console.Clear();
            for (var masX = 0; masX < field.GetTags().GetLength(0); masX++)
            {
                for (var masY = 0; masY < field.GetTags().GetLength(1); masY++)
                {
                    if (field.GetTags()[masX, masY] != 0)
                    {
                        Console.Write(field.GetTags()[masX, masY] + " ", 2);
                        continue;
                    }
                    Console.Write("  ", 2);
                }
                Console.WriteLine();
            }
        }
    }
}
