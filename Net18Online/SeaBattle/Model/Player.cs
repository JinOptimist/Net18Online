using SeaBattle.Model.Cell;

namespace SeaBattle.Model
{
    public class Player
    {
        public Battleground BattlegroundPlayer { get; set; }
        private int _id;
        public Player(Battleground battleground, int id)
        {
            BattlegroundPlayer = battleground;
            _id = id;
        }
        public bool IsShooting(int whoShooting)
        {
            return whoShooting == _id;
        }
        public int Shoot(Player opponent, int x, int y)
        {
            if (opponent.BattlegroundPlayer[x, y] is Ship)
            {
                opponent.BattlegroundPlayer[x, y] = new Hit(x, y, opponent.BattlegroundPlayer);
                return _id;
            }

            opponent.BattlegroundPlayer[x, y] = new Miss(x, y, opponent.BattlegroundPlayer);
            return opponent._id;
        }
    }
}
