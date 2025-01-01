using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace DracoStandardSite.Models
{
    public class UgFile
    {
        public int ranks { get; set; }
        public Queue<Base> bases { get; set; }

        public bool shattered { get; set; }
        public bool overlap { get; set; }
        public bool shoved { get; set; }


   

        public UgFile()
        {
            ranks = 0;
            bases = new Queue<Base>();
        }


        // Copy constructor
        public UgFile(UgFile other)
        {
            ranks = other.ranks;
            bases = new Queue<Base>(other.bases.Select(b => new Base(b)));
            shattered = other.shattered;
            overlap = other.overlap;
            shoved = other.shoved;
        }

        public void addBase(Base b)
        {
            bases.Enqueue(b);
            ranks += 1;
        }
        public void removeBase()
        {
            bases.Dequeue();
            ranks -= 1;
        }
        public bool isEmpty()
        {
            return this.bases.Count == 0; ;
        }
        public Base front()
        {

            return bases.Peek();
        }
        public void applyshove()
        {
            if (shieldWall()) { shoved = false; }
            else { shoved =  true; }
        }
        public void applyshatter()
        {
            if (shieldWall()) { shattered = false; }
            else { shattered = true; }
        }
        public bool shieldWall()
        {
            if (this.ranks == 0) {  return false; }
            else if (this.front().Characteristics.Contains("sw") && this.rankBonus("c", "sw") > 1) { return true; }
            
            else { return false; } 
        }

        public bool shove()
        { if (isEmpty()) { return false; }
            else
            {


                if (this.front().Characteristics.Contains("shv")) { return true; }
                else { return false; }
            }
            
        }
        public bool shatter()
        {
            if (isEmpty()) { return false; }
            else if (this.front().Characteristics.Contains("fdc") || this.front().Characteristics.Contains("mdc") || this.front().Melee == "cl" || this.front().Melee == "mpa") 
                { return true; }
            else { return false; }
        }
        public int bonus(UgFile op, bool melee)
        {
            int b = 0;
            if (melee)
            {
                if (ranks == 0) {
                    string itsallwrong = "yep"; }
                b= GetMeleeBonus(op);
            }
            else
            {
                b= GetImpactBonus(op);
            }
            if (this.front().Characteristics.Contains("cs"))
            {
                b -= 1;
            }

            switch (this.front().Quality)
            {
                case "x":
                    b += 2;
                    break;
                case "s":
                    b += 1;
                    break;
                case "p":
                    b -= 1;
                    break;

                default:
                    b += 0;
                    break;
            }
            return b;
        }
        public int rankBonus(string group, string value)
        {
            int r = 1;
            
            {
                for (int i = 1; i < this.ranks; i++)
                {
                    if (group == "w")
                    { 
                        if (this.bases.ElementAt(i).Melee == value)
                        {  r++; }
                    else if (this.bases.ElementAt(i).Characteristics.Contains(value))
                        { r++; }
                    }

                }

            }

            return r;
        }

        private int GetImpactBonus(UgFile op)
        {
            int bonus = 0;
           
            
                switch (this.bases.Peek().Melee)
                {
                    case "iw":
                        bonus = 2;
                        break;
                    case "fls":
                        if (this.rankBonus("w", "fls")>1) 
                        {                           
                               switch (op.front().Type)
                                {
                                    case "inf":
                                        bonus = 1;
                                        break;
                                    case "cav":
                                        bonus = 2;
                                        break;
                                    default:
                                        bonus = 0;
                                        break;
                                }
                            
                        }
                        else
                        {
                            bonus = 0;
                        }
                        break;
                    case "fpa":
                        bonus = 1;
                        break;
                    case "2hc":
                        switch (op.front().Type)
                        {
                            case "inf":
                                bonus = 1;
                                break;
                            case "cav":
                                bonus = 0;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                        break;
                    case "pk":
                        if (this.rankBonus("w", "pk") > 1)
                        {
                            switch (op.front().Type)
                            {
                                case "inf":
                                    bonus = 1;
                                    break;
                                case "cav":
                                    bonus = 2;
                                    break;
                                default:
                                    bonus = 0;
                                    break;
                            }
                        }
                        else
                        {
                            bonus = 0;
                        }
                        if (this.rankBonus("w", "pk") > 3)
                        {                            
                            bonus += 1;
                        }
                        break;

                    case "cl":
                
                            if ((op.front().Type == "inf" && (op.front().Melee == "pk" || op.front().Melee == "fls"|| op.front().Melee == "fpa"))|| op.front().Type=="el")
                            {
                                bonus = 0;
                            }
                        
                            else
                            {
                                bonus = 2;
                            }
                
          
                            break;
                    case "mss":

                        if (op.front().Type == "inf" && (op.front().Melee == "pk" || op.front().Melee == "fls" || op.front().Melee == "pa"))
                        {
                            bonus = 0;
                        }

                        else
                        {
                            bonus = 1;
                        }


                        break;

                    case "mls":
                        bonus = 1;
                        break;

                    case "fss":
                        bonus = 1;
                        break;


                    case "mpa":
                        if (op.front().Type == "cav" && (op.front().Melee == "cl" || op.front().Melee == "fls" ))
                        {
                            bonus = 0;
                        }
                        else
                        {
                            bonus = 1;
                        }
                        break;

              
                    default:
                        bonus = 0;
                        break;
                }
                if (this.front().Characteristics.Contains("mdc"))
                {
                    if (this.rankBonus("char", "mdc") > 1)
                    {
                        if (!(op.front().Type == "inf" && (op.front().Melee == "pk" || op.front().Melee == "fls" || op.front().Melee == "pa")) || op.front().Type == "el")
                        {
                            bonus += 1;
                        }
                    }
                }
                if (this.front().Characteristics.Contains("fdc"))
                {
                    if (this.rankBonus("char", "fdc") > 1)
                    {
                        if (op.front().Type == "inf")
                        {
                            bonus += 2;
                        }
                        else if ((op.front().Type == "cav" && (op.front().Melee=="cl")) ||op.front().Type == "el")
                        {
                            bonus += 0;
                        }
                        else
                        {
                            bonus += 1;
                        }
                    }
                }

                


            



            return bonus;

        }
        private int GetMeleeBonus(UgFile op)
        {
            int bonus = 0;
            switch (this.bases.Peek().Melee)
            {

                case "fls":
                    if (this.rankBonus("w", "fls") > 1)
                    {
                        switch (op.front().Type)
                        {
                            case "inf":
                                bonus = 1;
                                break;
                            case "cav":
                                bonus = 2;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                    }
                    else
                    {
                        bonus = 0;
                    }
                    break;

                case "fpa":
                    switch (op.front().Type)
                    {
                        case "inf":
                            if (this.rankBonus("w", "fpa") > 1)
                            { bonus = 1; }
                            else { bonus = 0; }

                            break;
                        case "cav":
                            bonus = 1;
                            break;
                        default:
                            bonus = 0;
                            break;
                    }
                    break;
                case "2hc":
                    switch (op.front().Type)
                    {
                        case "inf":
                            bonus = 1;
                            break;
                        case "cav":
                            bonus = 2;
                            break;
                        default:
                            bonus = 0;
                            break;
                    }
                    break;
                case "pk":
                    if (this.rankBonus("w", "pk") > 1)
                    {
                        switch (op.front().Type)
                        {
                            case "inf":
                                bonus = 1;
                                break;
                            case "cav":
                                bonus = 2;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                    }
                    else
                    {
                        bonus = 0;
                    }
                    if (this.rankBonus("w", "pk") > 2)
                    {
                        bonus += 1;
                    }
                    break;
                case "mls":
                    bonus = 1;
                    break;
                case "mpa":
                    bonus = 1;
                    break;


                default:
                    bonus = 0;
                    break;
            }

            if (this.front().Characteristics.Contains("mme"))
            {


                if (!(op.front().Type == "inf" && ((op.front().Melee == "pk" && op.rankBonus("w","pk")>1)|| (op.front().Characteristics.Contains("sw") && op.rankBonus("c", "sw") > 1)) || op.front().Type == "el" || (op.front().Melee == "fls" && op.rankBonus("w", "fls") > 1) || (op.front().Melee == "mls" && op.rankBonus("w", "mls") > 1)))
                {
                    bonus += 1;
                }
            }
            if(this.front().Characteristics.Contains("fme") && op.front().Type!="el")
            {               
                    bonus += 1;
               

            }
            switch (this.front().Protection)
            {
                case "fa":
                    if (op.front().Melee != "2hc")
                    {
                        bonus += 1;
                    }
                    break;
     
                case "u":
                    bonus -= 1;
                    break;

                default:
                    bonus  +=0;
                    break;
            }
            return bonus;
        }
        
        } 
}
