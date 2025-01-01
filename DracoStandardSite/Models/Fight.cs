using DracoStandardSite.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DracoStandardSite.Models
{
    public class Fight
    {

        private char[,] effectarray;
        public int awin, dwin;
        public Ug defender;
        public Ug attacker;

        public int samecount;
        public int difcount;

        public string winner;
        public Random rnd;
        
        public Fight(Ug attTug, Ug defTUG)

        {
            awin = 0;
            dwin = 0;
            attacker = new Ug(attTug); // Create a copy of the attacker
            defender = new Ug(defTUG); // Create a copy of 

            rnd = new Random();
            //set up dice array
            effectarray = new char[6, 5] { { 'n', 'n', 'n', 'n', 'w' }, { 'n', 'n', 'n', 'w', 'w' }, { 'n', 'n', 'w', 'w', 'w' }, { 'n', 'w', 'w', 'w', 'k' }, { 'w', 'w', 'k', 'k', 'k' }, { 's', 's', 's', 's', 's' } };
            char test = effectarray[1, 4];
        }
        public string battle()
        {
            //set up array or list of files from each tug
            int widest = Math.Max(attacker.columns, defender.columns);
            Ug largest;
            Ug smallest;
            UgFile[,] battlearray = new UgFile[widest, 2];
            int attArray;
            int defArray;


            if (attacker.columns > defender.columns)
            {
                largest = attacker;
                smallest = defender;
                attArray = 0;
                defArray = 1;

            }
            else
            {
                largest = defender;
                smallest = attacker;
                attArray = 1;
                defArray = 0;
            }


            int ofset = (largest.columns - smallest.columns) / 2;

            for (int b = 0; b < widest; b++)
            {
                battlearray[b, 0] = largest.Files[b];

            }
            for (int s = ofset; s < smallest.Files.Count + ofset; s++)
            {
                battlearray[s, 1] = smallest.Files[s - ofset];

            }

            // Fill remaining slots with empty UgFile instances
            for (int b = 0; b < widest; b++)
            {
                if (battlearray[b, 1] == null)
                {
                    battlearray[b, 1] = new UgFile();
                }
            }




            //impact
            for (int f = 0; f < widest; f++)
            {
                if (f < ofset) { largest.Files[f + 1].overlap = true; }
                else
                {
                    if (f >= (ofset + smallest.Files.Count)) { largest.Files[f - 1].overlap = true; }
                    else
                    {


                        if (!battlearray[f, attArray].isEmpty()  && !battlearray[f, defArray].isEmpty())
                        {
                            impact(battlearray[f, attArray], battlearray[f, defArray], attacker, defender);
                        }
                    }
                }
            }
            //fight until break

            //remove all overlap and shove flags

            attacker.resetFlags();
            defender.resetFlags();

            int round = 0;

            while (!attacker.broken && !defender.broken)
            {//add a count to break out of loop if it goes on too long
                round++;
                if (round > 100) { break; }
           

                //set overlap flags
                for (int o= 0; o < widest; o++)
                { 
                   
                        if (battlearray[o, 1].isEmpty() && !battlearray[o,0].isEmpty())
                        {
                            battlearray[o, 0].overlap = true;
                        }
                    if (battlearray[o, 0].isEmpty() && !battlearray[o,1].isEmpty())
                    {
                        battlearray[o, 1].overlap = true;
                    }

                }


                for (int f = 0; f < widest; f++)
                {
     
                List<UgFile> Aoverlaps = new List<UgFile>();
                List<UgFile> Doverlaps = new List<UgFile>();




                    if (!battlearray[f, attArray].isEmpty() && !battlearray[f, defArray].isEmpty())
                    {

           
                        //check for overalaps
                        if (f>0 && battlearray[f-1, attArray].overlap)
                        {

                            Aoverlaps.Add(battlearray[f-1,attArray]);

                        }
                        if (f > widest && battlearray[f + 1, attArray].overlap)
                        {

                            Aoverlaps.Add(battlearray[f + 1, attArray]);

                        }
                        if (f > 0 && battlearray[f - 1, defArray].overlap)
                        {

                            Doverlaps.Add(battlearray[f - 1, defArray]); 

                        }
                        if (f > widest && battlearray[f + 1, defArray].overlap)
                        {

                            Doverlaps.Add(battlearray[f + 1, defArray]);

                        }


                        int testa = battlearray[f, attArray].bases.Count;
                        int testb = battlearray[f, defArray].bases.Count;

                        
                        melee(battlearray[f, attArray], battlearray[f, defArray], attacker, defender, Aoverlaps, Doverlaps); 
                    }
                    
                }
            }
            if (attacker.isBroken() && !defender.isBroken()) { return "d"; }
            if (defender.isBroken() && !attacker.isBroken()) { return "a"; }
            else
            {
                return "nope";
            }
        }




        public void impact(UgFile attacker, UgFile defender, Ug aTUG, Ug dTUG)
        {
            int AttB = attacker.bonus(defender, false);
            int DefB = defender.bonus(attacker,false);
            // overlaps and shoves and shatters

            if (aTUG.shattered) { DefB = DefB + 2; aTUG.shattered = false; }
            if (dTUG.shattered) { AttB = AttB + 2; dTUG.shattered = false; }

            if (aTUG.shoved) { DefB = DefB + 1; aTUG.shoved = false; }
            if (dTUG.shoved) { AttB = AttB + 1; dTUG.shoved = false; }

   

            //other impact bonuses

            if (AttB > DefB)
            {
                AttB = AttB - DefB;
                DefB = -AttB;
            }
            else
            {
                DefB = DefB - AttB;
                AttB = -DefB;
            };





            //dice choice
            List<int> AttD = new List<int>();
            AttD.AddRange(dice(AttB));
            List<int> DefD = new List<int>();
            DefD.AddRange(dice(DefB));
            string AttEffect = rolldice(AttD);
            string DefEffect = rolldice(DefD);

            //apply effect
            applyeffect(AttEffect, defender, dTUG, attacker, DefEffect);
            applyeffect(DefEffect, attacker, aTUG, defender, AttEffect);


        }
        private void melee(UgFile attacker, UgFile defender, Ug aTUG, Ug dTUG, List<UgFile> AttOvers, List<UgFile> DefOvers)
        {
           
            


                int testa = attacker.bases.Count;
                int testb = defender.bases.Count;


                int AttB = attacker.bonus(defender, true);
                int DefB = defender.bonus(attacker, true);
                //quality


                //overlap bonuses
                List<int> AttOverB = new List<int>();
                foreach (UgFile f in AttOvers)
                {
                    AttOverB.Add(f.bonus(defender, true));
                }
                List<int> DefOverB = new List<int>();
                foreach (UgFile f in DefOvers)
                {
                    AttOverB.Add(f.bonus(attacker, true));
                }


                if (aTUG.shoved) { DefB = DefB + 1; aTUG.shoved = false; }
                if (dTUG.shoved) { AttB = AttB + 1; dTUG.shoved = false; }

                //normalise bonuses
                if (AttB > DefB)
                {
                    AttB = AttB - DefB;
                    DefB = -AttB;
                }
                else
                {
                    DefB = DefB - AttB;
                    AttB = -DefB;
                };


                //dice choice
                List<int> AttD = new List<int>();
                    AttD.AddRange(dice(AttB));
                List<int> DefD = new List<int>();
                    DefD.AddRange(dice(DefB));


                //overlap dice
                foreach (int a in AttOverB)
                {
                    AttD.AddRange(dice(a).Select(x => x - 1));
                }

                foreach (int d in DefOverB)
                {
                    DefD.AddRange(dice(d).Select(x => x - 1));
                }






                string AttEffect = rolldice(AttD);
                string DefEffect = rolldice(DefD);

                if (AttEffect != DefEffect)

                { difcount++; }
                else
                {
                    samecount++;
                };
                //apply effect
                applyeffect(AttEffect, defender, dTUG, attacker, DefEffect);
                applyeffect(DefEffect, attacker, aTUG, defender, AttEffect);
            
        }

        private void applyeffect(string effect, UgFile f, Ug t, UgFile causer, string causereffect)
        {
            foreach (char c in effect)
            {
                switch (c)
                {
                    case 'k':
                        t.casualty(f);
                        if (causer.shove() && !(causereffect.Contains("k") || causereffect.Contains("s")))
                        {
                            f.applyshove();
                            //t.shoved = true;
                        }
                        if (causer.shatter() && !(causereffect.Contains("k") || causereffect.Contains("s")))
                        {
                            f.applyshatter();
                            //t.shattered = true;
                        }
                        break;
                    case 'w':
                        if (t.wounded)
                        {
                            t.wounded = !t.wounded;
                            t.casualty(f);

                        }
                        else { t.wounded = !t.wounded; }
                        break;
                    case 's':
                        //make shove do something
                        if (causer.shove() && !(causereffect.Contains("k") || causereffect.Contains("s")))
                        {
                            f.applyshove();
                            //t.shoved = true;
                        }
                        if (causer.shatter() && !(causereffect.Contains("k") || causereffect.Contains("s")))
                        {
                            f.applyshatter();
                            //t.shattered = true;
                        }
                        break;
                    default:
                        break;
                }





            }
        }
        private int DiceRoll()
        {
            int result;


            result = rnd.Next(0, 6);
            return result;
        }



        private string rolldice(List<int> Dlist)
        {
            string effect = "";
            int col = 0;

            foreach (int c in Dlist)
            {
                col = c;
                int row = DiceRoll();

                effect = effect + effectarray[row, col].ToString();

            }
            return effect;
        }

        private List<int> dice(int dif)
        {
            //black 0, white 1 , green 2, yellow 3, red 4

            List<int> d = new List<int>();
            if (dif <= -3)
            {
                d.Add(0);

            }
            else
            {
                if (dif < 0) { d.Add(1); }
                else
                {
                    switch (dif)
                    {
                        case 0:
                            d.Add(2);
                            break;
                        case 1:
                            d.Add(2);
                            break;
                        case 2:
                            d.Add(3);
                            break;
                        case 3:
                            d.Add(4);
                            break;
                        case 4:
                            d.Add(4);
                            d.Add(1);
                            break;
                        case 5:
                            d.Add(4);
                            d.Add(2);
                            break;
                        case 6:
                            d.Add(4);
                            d.Add(4);
                            break;
                        default:
                            d.Add(4);
                            d.Add(4);
                            break;
                    }
                }

            }
            return d;

        }
       


    }
}



