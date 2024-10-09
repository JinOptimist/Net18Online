using SeaBattle.Model.Cell;

namespace SeaBattle.Model
{
    public class Player
    {
        public Battleground battlegroundPlayer { get; set; }
        private int _id;
        public Player(Battleground battleground, int id)
        {
            battlegroundPlayer = battleground;
            _id = id;
        }
        public bool IsShooting(int whoShooting)
        {
            return whoShooting == _id;
        }
        public int Shoot(Player opponent, int x, int y)
        {
            if (opponent.battlegroundPlayer[x, y] is Ship)
            {
                opponent.battlegroundPlayer[x, y] = new Hit(x, y, opponent.battlegroundPlayer);
                return _id;
            }

            opponent.battlegroundPlayer[x, y] = new Miss(x, y, opponent.battlegroundPlayer);
            return opponent._id;
        }
    }
}
