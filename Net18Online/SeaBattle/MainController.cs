using SeaBattle.Enum;
using SeaBattle.Model;
using SeaBattle.Model.Cell;

namespace SeaBattle
{
    public class MainController
    {
        private Player _firstPlayer;
        private Battleground _theLandOfTheFirstPlayer;
        private Player _secondPlayer;
        private Battleground _theLandOfTheSecondPlayer;
        private BuildBattleground _build = new();
        private BattlegroundDrawer _drawer = new();
        private ControllerForBuildShip _controllerForBuldShip = new();
        private Random _random = new Random();
        private int _nowShootingPlayer;
        private int _shootForX;
        private int _shootForY;
        public void StartGame()
        {
            _theLandOfTheFirstPlayer = _build.Build();
            _controllerForBuldShip.StartBuildShip(_theLandOfTheFirstPlayer);
            _firstPlayer = new Player(_theLandOfTheFirstPlayer, (int)PlayerEnum.First);

            _theLandOfTheSecondPlayer = _build.Build();
            _controllerForBuldShip.StartBuildShip(_theLandOfTheSecondPlayer);
            _secondPlayer = new Player(_theLandOfTheSecondPlayer, (int)PlayerEnum.Second);

            Console.Clear();
            _drawer.DrawBatlleground(_firstPlayer.BattlegroundPlayer, _secondPlayer.BattlegroundPlayer);
            _nowShootingPlayer = _random.Next(1, 3);

            while (_theLandOfTheFirstPlayer.Cells.OfType<Ship>().Any()
                && _theLandOfTheSecondPlayer.Cells.OfType<Ship>().Any())
            {
                if (_firstPlayer.IsShooting(_nowShootingPlayer))
                {
                    Console.WriteLine("first player step");
                    Console.WriteLine("Input point horizont: ");
                    _shootForX = int.Parse(Console.ReadLine());

                    Console.WriteLine("Input point vertical: ");
                    _shootForY = int.Parse(Console.ReadLine());

                    _nowShootingPlayer = _firstPlayer.Shoot(_secondPlayer, _shootForX, _shootForY);

                    _drawer.DrawBatlleground(_firstPlayer.BattlegroundPlayer, _secondPlayer.BattlegroundPlayer);

                    continue;
                }
                Console.WriteLine("second player step");
                Console.WriteLine("Input point horizont: ");
                _shootForX = int.Parse(Console.ReadLine());

                Console.WriteLine("Input point vertical: ");
                _shootForY = int.Parse(Console.ReadLine());

                _nowShootingPlayer = _secondPlayer.Shoot(_firstPlayer, _shootForX, _shootForY);

                _drawer.DrawBatlleground(_firstPlayer.BattlegroundPlayer, _secondPlayer.BattlegroundPlayer);
            }
            Console.WriteLine($"Win {_nowShootingPlayer} player!");
        }
    }
}
