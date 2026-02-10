using DracoStandardSite.Models;

public class BattleEngine
{
    private readonly Ug attacker;
    private readonly Ug defender;

    private readonly BattleLine[] lines;
    private readonly CombatResolver resolver;

    public BattleEngine(Ug attacker, Ug defender)
    {
        this.attacker = new Ug(attacker);
        this.defender = new Ug(defender);

        lines = BattleLineBuilder.AlignUnits(this.attacker, this.defender);
        resolver = new CombatResolver();
    }

    public string RunBattle()
    {
        // IMPACT STEP
        foreach (var line in lines)
        {
            resolver.ResolveImpact(line);
        }

        attacker.resetFlags();
        defender.resetFlags();

        // MELEE LOOP
        int round = 0;
        while (!attacker.broken && !defender.broken)
        {
            round++;
            if (round > 100) break;

            OverlapDetector.UpdateOverlaps(lines);

            foreach (var line in lines)
            {
                resolver.ResolveMelee(line);
            }
        }

        if (attacker.isBroken() && !defender.isBroken()) return "d";
        if (defender.isBroken() && !attacker.isBroken()) return "a";
        return "nope";
    }
}
