public class EffectResolver
{
    public void Apply(string effects, UgFile target, UgFile causer, string oppositeEffects)
    {
        foreach (char e in effects)
        {
            switch (e)
            {
                case 'k':
                    target.parent.casualty(target);

                    if (!oppositeEffects.Contains('k') && !oppositeEffects.Contains('s'))
                    {
                        if (causer.shove()) target.applyshove();
                        if (causer.shatter()) target.applyshatter();
                    }
                    break;

                case 'w':
                    if (target.parent.wounded) 
                        target.parent.casualty(target);
                    else 
                        target.parent.wounded = true;
                    break;

                case 's':
                    if (!oppositeEffects.Contains('k') && !oppositeEffects.Contains('s'))
                    {
                        if (causer.shove()) target.applyshove();
                        if (causer.shatter()) target.applyshatter();
                    }
                    break;
            }
        }
    }
}
