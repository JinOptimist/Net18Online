using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public class Dungeon : BaseCell
    {
        private Random _random = new Random();
        public Dungeon(int x, int y, Maze maze) : base(x, y, maze)
        {

        }

        public override char Symbol => 'v';

        public override void InteractWithCell(BaseCharacter character)
        {


            Console.WriteLine("Are you sure you want to go down to the dungeon?");
            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.Spacebar:
                    Console.WriteLine("Step Step Step....");
                    Maze[X, Y] = new Ground(X, Y, Maze);

                    var maxValue = GetEnumMaxValue<Treasure>();
                    var treasureValue = _random.Next(1, maxValue + 1);
                    var treasure = GetTreasureByValue(treasureValue, character);

                    Console.WriteLine($"You've discovered {treasure} in the dungeon");
                    if ((int)treasure == 3)
                    {
                        var monsterTreasure = GetMonsterValue(character);
                        Console.WriteLine($"The God of blood changed your health level to {character.Health}, but he gave you {monsterTreasure} in return ");
                    }

                    Console.ReadKey();
                    break;
                default:
                    break;

            }
        }

        private object GetTreasureByValue(int treasureValue, BaseCharacter character)
        {
            if (treasureValue >= 1 && treasureValue <= 2)
            {
                return (Treasure)1;
            }
            if (treasureValue >= 3 && treasureValue <= 6)
            {
                return (Treasure)3;
            }
            if (treasureValue == 7)
            {
                var procent = (double)character.Health / 100 * 20;
                character.Health += (int)procent;
                return (Treasure)treasureValue;
            }
            if (treasureValue == 8)
            {
                character.Coins += 3;
                return (Treasure)treasureValue;
            }
            else
            {
                character.Coins += 10;
                return (Treasure)treasureValue;
            }
        }

        private object GetMonsterValue(BaseCharacter character)
        {
            var procent = (double)character.Health / 100 * _random.Next(0, 51);
            character.Health -= (int)procent;
            var maxValue = GetEnumMaxValue<MonsterTreasure>();
            var treasureValue = _random.Next(1, maxValue + 1);

            if (treasureValue >= 1 && treasureValue <= 3)
            {
                return (MonsterTreasure)1;
            }

            return (MonsterTreasure)treasureValue;
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        public static int GetEnumMaxValue<T>() where T : Enum => Enum
                .GetValues(typeof(T))
                .Cast<int>()
                .Max();
    }
}