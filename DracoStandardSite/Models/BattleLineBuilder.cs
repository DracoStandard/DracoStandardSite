public static class BattleLineBuilder
{
    public static BattleLine[] AlignUnits(Ug attacker, Ug defender)
    {
        int widest = Math.Max(attacker.columns, defender.columns);

        var lines = new BattleLine[widest];

        Ug largest = attacker.columns >= defender.columns ? attacker : defender;
        Ug smallest = largest == attacker ? defender : attacker;

        int offset = (largest.columns - smallest.columns) / 2;

        for (int i = 0; i < widest; i++)
        {
            UgFile largeFile = largest.Files[i];
            UgFile smallFile =
                (i >= offset && i - offset < smallest.Files.Count)
                    ? smallest.Files[i - offset]
                    : new UgFile();

            lines[i] = new BattleLine(
                largest == attacker ? largeFile : smallFile,
                largest == attacker ? smallFile : largeFile
            );
        }

        return lines;
    }
}
