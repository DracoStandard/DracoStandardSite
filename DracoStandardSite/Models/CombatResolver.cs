public class CombatResolver
{
    private readonly DicePool dice = new();
    private readonly EffectResolver effects = new();

    public void ResolveImpact(BattleLine line)
    {
        if (!line.IsEngaged) return;

        int attBonus = line.Attacker.bonus(line.Defender, false);
        int defBonus = line.Defender.bonus(line.Attacker, false);

        (attBonus, defBonus) = BonusNormalizer.Normalize(attBonus, defBonus);

        var attDice = dice.FromBonus(attBonus);
        var defDice = dice.FromBonus(defBonus);

        dice.ApplyShoveShatter(line.Attacker, attDice);
        dice.ApplyShoveShatter(line.Defender, defDice);

        string attEffects = dice.Roll(attDice);
        string defEffects = dice.Roll(defDice);

        effects.Apply(attEffects, line.Defender, line.Attacker, defEffects);
        effects.Apply(defEffects, line.Attacker, line.Defender, attEffects);
    }

    public void ResolveMelee(BattleLine line)
    {
        if (!line.IsEngaged) return;

        int attBonus = line.Attacker.bonus(line.Defender, true);
        int defBonus = line.Defender.bonus(line.Attacker, true);

        var attDice = dice.FromBonus(attBonus);
        var defDice = dice.FromBonus(defBonus);

        // Overlaps → reduce die quality by 1
        dice.AddOverlapDice(line.AttackerOverlaps, attDice, line.Defender);
        dice.AddOverlapDice(line.DefenderOverlaps, defDice, line.Attacker);

        dice.ApplyShoveShatter(line.Attacker, attDice);
        dice.ApplyShoveShatter(line.Defender, defDice);

        string attEffects = dice.Roll(attDice);
        string defEffects = dice.Roll(defDice);

        effects.Apply(attEffects, line.Defender, line.Attacker, defEffects);
        effects.Apply(defEffects, line.Attacker, line.Defender, attEffects);
    }
}

internal class BonusNormalizer
{
    internal static (int attBonus, int defBonus) Normalize(int attBonus, int defBonus)
    {
        throw new NotImplementedException();
    }
}