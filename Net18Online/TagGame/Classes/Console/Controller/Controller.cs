using TagGame.Classes.Base;
using TagGame.Classes.Builders;

namespace TagGame.Classes.ConsoleDrawer
{
    public class Controller
    {
        private Field _field;

        public void Start()
        {
            var builder = new Builder();
            var drawer = new Drawer();

            Console.WriteLine("Game '15'");
            Console.WriteLine("Choose:\n (1) Start \n (2) Exit");

            while (true)
            {
                var key = Console.ReadLine();
                if (key == "1")
                {
                    break;
                }
                if (key == "2")
                {
                    return;
                }
            }

            builder.Build();
            _field = builder.GetField();
            drawer.FirstPrint(_field);

            var playerPositionX = 0;
            var playerPositionY = 0;

            for (var masX = 0; masX < _field.GetTags().GetLength(0); masX++)
            {
                for (var masY = 0; masY < _field.GetTags().GetLength(1); masY++)
                {
                    if (_field.GetTags()[masX, masY] == 0)
                    {
                        playerPositionX = masX;
                        playerPositionY = masY;
                    }
                }
            }

            while (true)
            {
                var readKey = Console.ReadKey();
                switch (readKey.Key)
                {
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        {
                            if (!CanGo(playerPositionX - 1, playerPositionY))
                            {
                                continue;
                            }
                            playerPositionX--;
                            break;
                        }
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        {
                            if (!CanGo(playerPositionX + 1, playerPositionY))
                            {
                                continue;
                            }
                            playerPositionX++;
                            break;
                        }
                    case ConsoleKey.A or ConsoleKey.LeftArrow:
                        {
                            if (!CanGo(playerPositionX, playerPositionY - 1))
                            {
                                continue;
                            }
                            playerPositionY--;
                            break;
                        }
                    case ConsoleKey.D or ConsoleKey.RightArrow:
                        {
                            if (!CanGo(playerPositionX, playerPositionY + 1))
                            {
                                continue;
                            }
                            playerPositionY++;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                }

                _field.ChangePositions(playerPositionX, playerPositionY);
                drawer.FirstPrint(_field);

                if (_field.IsWin())
                {
                    Console.WriteLine("Congrats");
                    return;
                }
            }
        }

        public bool CanGo(int positionX, int positionY)
        {
            if (positionX < 0 || positionY < 0 || positionX >= _field.GetTags().GetLength(1) || positionY >= _field.GetTags().GetLength(1))
            {
                return false;
            }
            return true;
        }
    }
}
