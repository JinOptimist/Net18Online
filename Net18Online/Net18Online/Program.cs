using Net18Online.Net18Online.Models;

namespace Net18Online.Net18Online;
public class Program
{
    List<Gamer> gamers = new List<Gamer>();
    public static void Main( string[] args )
    {
        var gamerService = new GamerService();
        Bounds bounds = new Bounds();
        bounds.BoundsInput();
        var game1 = new GuessTheNumber(gamerService, bounds);
        game1.Start();
    }
}