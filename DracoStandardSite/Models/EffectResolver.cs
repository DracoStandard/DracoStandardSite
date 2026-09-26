using DracoStandardSite.Models;

public class EffectResolver
{
    public void Apply(string effects, UgFile f,Ug t ,UgFile causer, string causereffect)
    {
        foreach (char e in effects)
        {
            switch (e)
            {
                case 'k':
                    t.casualty(f);

                    if (!causer.shove())
                    {
                        if (causer.shoved) f.applyshove();
                        if (causer.shattered) f.applyshatter();
                    }
                    break;

                case 'w':
                    if (t.wounded)
                    {
                        t.casualty(f);
                        t.wounded = false;
                    }
                    else
                        t.wounded = true;
                    break;

                case 's':
                    if (causer.shatter())
                    {
                        
                        if (causer.shattered) f.applyshatter();
                    }
                    if (causer.shove())
                        {
                        if (causer.shoved) f.applyshove();
                    }
                    break;
            }
        }
    }

    internal void Apply(string defEffects, UgFile attacker, UgFile defender, string attEffects)
    {
        throw new NotImplementedException();
    }
}
