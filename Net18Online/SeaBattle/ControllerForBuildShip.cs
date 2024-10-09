using SeaBattle.Model;

namespace SeaBattle
{
    public class ControllerForBuildShip
    {
        private BuildBattleground _buildShip = new();
        private BattlegroundDrawer _draw = new();

        public void StartBuildShip(Battleground battleground)
        {
            var countOfDeck = 4;
            var countShip = 1;
            while (countShip < 5)
            {
                _draw.DrawYourGround(battleground);
                for (int i = 0; i < countShip; i++)
                {
                    Console.WriteLine($"You're drawing {countOfDeck} deck ship");
                    for (int j = 0; j < countOfDeck; j++)
                    {
                        Console.WriteLine("Input point horizont: ");
                        var x = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input point vertical: ");
                        var y = int.Parse(Console.ReadLine());

                        _buildShip.BuildShip(x, y, battleground, countOfDeck);
                        _draw.DrawYourGround(battleground);
                    }
                }
                countShip++;
                countOfDeck--;
            }
        }
    }
}
