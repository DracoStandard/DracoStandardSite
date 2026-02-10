using DracoStandardSite.Models;

public class BattleLine
{
    public UgFile Attacker { get; }
    public UgFile Defender { get; }

    public List<UgFile> AttackerOverlaps { get; } = new();
    public List<UgFile> DefenderOverlaps { get; } = new();

    public bool IsEngaged => !Attacker.isEmpty() && !Defender.isEmpty();

    public BattleLine(UgFile att, UgFile def)
    {
        Attacker = att;
        Defender = def;
    }
}
