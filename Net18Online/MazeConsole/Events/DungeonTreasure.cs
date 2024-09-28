public class EnumRangeAttribute : Attribute
{
    private int v1;
    private int v2;

    public EnumRangeAttribute(int v1, int v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

}

enum Treasure
{
    [EnumRange(1, 2)]
    Noting = 1,
    [EnumRange(3, 6)]
    GodOfBlood = 3,
    HealthPoints = 7,
    HandfulOfCoins = 8,
    LotOfCoins = 9,
}
enum MonsterTreasure
{
    [EnumRange(1, 3)]
    Noting = 1,
    Sword = 4,
    ChainArmour = 5,
    Boots = 6,
}
