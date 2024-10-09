using SeaBattle.Model;
using SeaBattle.Model.Cell;

namespace SeaBattle
{
    public class BuildBattleground
    {
        private Battleground _battelground;

        public Battleground Build()
        {
            _battelground = new Battleground();

            BuildWater();

            return _battelground;
        }

        public void BuildShip(int x, int y, Battleground battleground, int countOfDeck)
        {
            battleground[x, y] = new Ship(x, y, battleground, countOfDeck);
        }

        private void BuildWater()
        {
            for (int y = 0; y < Battleground.HEIGHT; y++)
            {
                for (int x = 0; x < Battleground.WIDTH; x++)
                {
                    _battelground[x, y] = new Water(x, y, _battelground);
                }
            }
        }
    }
}
