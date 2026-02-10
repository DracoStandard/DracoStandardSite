public class DicePool
{
    private static readonly char[,] EffectMatrix =
    {
        { 'n','n','n','n','w' },
        { 'n','n','n','w','w' },
        { 'n','n','w','w','w' },
        { 'n','w','w','w','k' },
        { 'w','w','k','k','k' },
        { 's','s','s','s','s' }
    };

    private readonly Random rnd = new();

    public List<int> FromBonus(int bonus)
    {
        return DiceTables.FromBonus(bonus);
    }

    public void ApplyShoveShatter(UgFile f, List<int> dice)
    {
        if (f.shoved) dice.AddRange(FromBonus(1));
        if (f.shattered) dice.AddRange(FromBonus(2));
    }

    public void AddOverlapDice(IEnumerable<UgFile> overlaps, List<int> dice, UgFile target)
    {
        foreach (var file in overlaps)
        {
            int b = file.bonus(target, true) - 1;
            dice.AddRange(FromBonus(b));
        }
    }

    public string Roll(List<int> diceList)
    {
        var result = new StringBuilder();

        foreach (var die in diceList)
        {
            int row = rnd.Next(0, 6);
            result.Append(EffectMatrix[row, die]);
        }

        return result.ToString();
    }
}
