public static class OverlapDetector
{
    public static void UpdateOverlaps(BattleLine[] lines)
    {
        int n = lines.Length;

        // Clear old overlap flags
        foreach (var line in lines)
        {
            line.Attacker.overlap = false;
            line.Defender.overlap = false;
        }

        for (int i = 0; i < n; i++)
        {
            var line = lines[i];

            if (line.Attacker.isEmpty() && !line.Defender.isEmpty())
                line.Defender.overlap = true;

            if (line.Defender.isEmpty() && !line.Attacker.isEmpty())
                line.Attacker.overlap = true;
        }

        // Build overlap lists
        for (int i = 0; i < n; i++)
        {
            lines[i].AttackerOverlaps.Clear();
            lines[i].DefenderOverlaps.Clear();

            if (i > 0)
            {
                if (lines[i - 1].Attacker.overlap)
                    lines[i].AttackerOverlaps.Add(lines[i - 1].Attacker);

                if (lines[i - 1].Defender.overlap)
                    lines[i].DefenderOverlaps.Add(lines[i - 1].Defender);
            }

            if (i < n - 1)
            {
                if (lines[i + 1].Attacker.overlap)
                    lines[i].AttackerOverlaps.Add(lines[i + 1].Attacker);

                if (lines[i + 1].Defender.overlap)
                    lines[i].DefenderOverlaps.Add(lines[i + 1].Defender);
            }
        }
    }
}
