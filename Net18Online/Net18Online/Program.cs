using Net18Online.Models;

Console.WriteLine("Hello, let's play? " +
    "\nThere are two games to choose from: " +
    "\nGame 1 for two players (the first guesses, the second guesses), " +
    "\nGame 2 for single people :( (You guess the number, and the computer guesses it)");
while (true)
{
    Console.WriteLine("\nEnter \"G1\" to play the Game1 or \"G2\" to play the Game2");
    var choose_game = Console.ReadLine();
    if (choose_game == "G1")
    {
        var game1 = new GuessTheNumber();
        game1.Start();
        break;
    }
    else if (choose_game == "G2")
    {
        var game2 = new GuessTheNumberWithComputer();
        game2.Start();
        break;
    }
    else
    {
        Console.WriteLine("\nCould not recognize the answer, try again.");
    }
}