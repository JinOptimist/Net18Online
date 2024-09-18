using Net18Online.Models;

var game1 = new GuessTheNumber();

game1.Start();

Console.WriteLine("Press any key to play with the bot!");
Console.ReadKey();
Console.Clear();

game1.StartWithImbaBot();