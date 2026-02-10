using DracoStandardSite.Models;
using System.Text;

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
        return bonus switch
        {
            -3 => new() { 0 },
            -2 => new() { 1 },
            -1 => new() { 2 },
            0 => new() { 3 },
            1 => new() { 4 },
            2 => new() { 4, 4 },
            _ => new() { 4 }
        };
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
            int b = file.bonus(target, true);

            // If target is NOT exempt, downgrade by 1
            if (!Rules.IsOverlapDowngradeExempt(target))
                b -= 1;

            // Safety clamp: do not allow impossible die columns
            if (b < -3) b = -3;

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

public static class Rules
{
    public static bool IsOverlapDowngradeExempt(UgFile target)
    {
        // Use the front/base of the file for melee and characteristics.
        // Guard for empty files.
        Base? frontBase = target.isEmpty() ? null : target.front();

        string melee = (frontBase?.Melee ?? "").Trim().ToLowerInvariant();

        // Long Spear or Pike?
        bool isLspOrPk =
            melee == "fls" || melee == "lsp" ||
            melee == "pk"  || melee == "pike";

        // Has Keil?
        bool hasKeil = frontBase?.Characteristics?.Contains("keil",
                StringComparer.OrdinalIgnoreCase) ?? false;

        // Exempt from overlap downgrade if LSP/Pk AND NOT Keil
        return isLspOrPk && !hasKeil;
    }
}
