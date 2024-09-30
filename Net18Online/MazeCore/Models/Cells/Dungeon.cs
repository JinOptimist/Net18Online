using MazeCore.Models.Cells.Character;
using MazeCore.Models.Cells.Enums;

namespace MazeCore.Models.Cells
{
    public class Dungeon : BaseCell
    {
        public Dungeon(int x, int y, Maze maze) : base(x, y, maze)
        {

        }

        public override char Symbol => 'v';

        public override void InteractWithCell(BaseCharacter character)
        {
            Maze[X, Y] = new Ground(X, Y, Maze);
            var maxValue = GetEnumMaxValue<Treasure>();
            var treasureValue = Maze.Random.Next(1, maxValue + 1);
            var treasure = GetTreasureByValue(treasureValue);

            AddEventInfo($"You've discovered {treasure} in the dungeon");

            switch (treasure)
            {
                case Treasure.GodOfBlood:
                    var monsterTreasure = GetMonsterValue(character);
                    AddEventInfo($"The God of blood changed your health level to {character.Health}, but he gave you {monsterTreasure} in return ");
                    break;
                case Treasure.HealthPoints:
                    var procent = (double)character.Health / 100 * 20;
                    character.Health += (int)procent;
                    AddEventInfo($"You found some health points, your health has increased by 20%. Now you have {character.Health}");
                    break;
                case Treasure.HandfulOfCoins:
                    character.Coins += 3;
                    AddEventInfo($"You found some coins. Now you have {character.Coins}");
                    break;
                case Treasure.LotOfCoins:
                    character.Coins += 10;
                    AddEventInfo($"You found a lot of coins. Now you have {character.Coins}");
                    break;
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            AddEventInfo("Are you sure you want to go down to the dungeon?");
            return true;
        }


        private Treasure GetTreasureByValue(int treasureValue)
        {
            if (treasureValue >= 1 && treasureValue <= 2)
            {
                return Treasure.Noting;
            }
            if (treasureValue >= 3 && treasureValue <= 6)
            {
                return Treasure.GodOfBlood;
            }
            if (treasureValue == 7)
            {
                return Treasure.HealthPoints;
            }
            if (treasureValue == 8)
            {
                return Treasure.HandfulOfCoins;
            }
            else
            {
                return Treasure.LotOfCoins;
            }
        }

        private MonsterTreasure GetMonsterValue(BaseCharacter character)
        {
            var procent = (double)character.Health / 100 * Maze.Random.Next(0, 51);
            character.Health -= (int)procent;
            var maxValue = GetEnumMaxValue<MonsterTreasure>();
            var treasureValue = Maze.Random.Next(1, maxValue + 1);

            if (treasureValue >= 1 && treasureValue <= 3)
            {
                return MonsterTreasure.Noting;
            }

            return (MonsterTreasure)treasureValue;
        }
        public static int GetEnumMaxValue<T>() where T : Enum => Enum
                .GetValues(typeof(T))
                .Cast<int>()
                .Max();
    }
}