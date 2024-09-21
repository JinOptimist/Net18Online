using Net18Online.Models;
public class Program
{
    List<Gamer> gamers = new List<Gamer>();
    public static void Main( string[] args )
    {
        Console.WriteLine( "Hello World!" );
        var game1 = new GuessTheNumber();
        game1.Start();
    }
}