namespace DracoStandardSite.Models
{
    public class UgFile
    {
        public int ranks { get; set; }
        public Stack<Base> bases { get; set; }

        public UgFile()
        {
            ranks = 0;
            bases = new Stack<Base>();
        }
        public void addBase(Base b)
        {
            bases.Push(b);
            ranks += 1;
        }
        public void removeBase(Base b)
        {
            bases.Pop();
            ranks -= 1;
        }
        public bool isEmpty()
        {
            return ranks == 0;
        }
        public Base front()
        {
            return bases.Peek();
        }
        private int GetBonus(UgFile op, bool melee)
        {
            int bonus = 0;
            if (!melee)
            {
                switch (this.bases.Peek().Melee)
                {
                    case "Impact_Weapon":
                        bonus = 2;
                        break;
                    case "Long_Spear":
                        if (this.ranks > 1)
                        {


                            if (this.bases.Peek().Type == "CAVALRY")
                            {
                                bonus = 1;
                            }
                            else
                            {

                                switch (op.front().Type)
                                {
                                    case "INFANTRY":
                                        bonus = 1;
                                        break;
                                    case "CAVALRY":
                                        bonus = 2;
                                        break;
                                    default:
                                        bonus = 0;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            bonus = 0;
                        }
                        break;
                    case "Polearm":
                        bonus = 1;
                        break;
                    case "2-H_Cut-Crush":
                        switch (op.front().Type)
                        {
                            case "INFANTRY":
                                bonus = 1;
                                break;
                            case "CAVALRY":
                                bonus = 0;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                        break;
                    case "Pike":
                        switch (op.front().Type)
                        {
                            case "INFANTRY":
                                bonus = 1;
                                break;
                            case "CAVALRY":
                                bonus = 2;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                        if (this.ranks > 3)
                        {
                            bonus += 1;
                        }
                        break;
                    case "Charging_Lancer":
                        if (op.front().Type == "INFANTRY")
                        {
                            if (op.front().Type == "INFANTRY" && (((op.front().Melee == "Pike" || op.front().Melee == "Long_Spear") && op.ranks > 1) || op.front().Melee == "Polearm"))
                            {
                                bonus = 0;
                            }

                        }
                        else
                        {

                            bonus = 2;
                        }
                        break;
                    default:
                        bonus = 0;
                        break;
                }
                if (this.front().Characteristics.Contains("Devastating_Chargers"))
                {
                    if (this.front().Type == "CAVALRY")
                    {
                        if (op.front().Type == "INFANTRY")
                        {
                            if (op.front().Melee != "Pike" || op.front().Melee != "Long_Spear")
                            {
                                bonus += 1;
                            }

                        }
                        else
                        {

                            bonus += 1;
                        }
                    }
                    else if (this.front().Type == "INFANTRY")
                    {
                        switch (op.front().Type)
                        {
                            case "INFANTRY":
                                bonus += 2;
                                break;
                            case "CAVALRY":
                                bonus += 1;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                    }
                }
            }

            else
            {
                switch (this.bases.Peek().Melee)
                {

                    case "Long_Spear":
                        if (this.ranks > 1)
                        {


                            if (this.bases.Peek().Type == "CAVALRY")
                            {
                                bonus = 1;
                            }
                            else
                            {

                                switch (op.front().Type)
                                {
                                    case "INFANTRY":
                                        bonus = 1;
                                        break;
                                    case "CAVALRY":
                                        bonus = 2;
                                        break;
                                    default:
                                        bonus = 0;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            bonus = 0;
                        }
                        break;
                    case "Polearm":
                        bonus = 1;
                        break;
                    case "2-H_Cut-Crush":
                        switch (op.front().Type)
                        {
                            case "INFANTRY":
                                bonus = 1;
                                break;
                            case "CAVALRY":
                                bonus = 2;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                        break;
                    case "Pike":
                        switch (op.front().Type)
                        {
                            case "INFANTRY":
                                bonus = 1;
                                break;
                            case "CAVALRY":
                                bonus = 2;
                                break;
                            default:
                                bonus = 0;
                                break;
                        }
                        if (this.ranks > 2)
                        {
                            bonus += 1;
                        }
                        break;

                }

                if (this.front().Characteristics.Contains("Melee_Expert"))
                {

                    if (this.front().Type == "CAVALRY")
                    {
                        if ((op.front().Type == "INFANTRY" && (((op.front().Melee == "Pike" || op.front().Melee == "Long_Spear") && op.ranks > 1) || (op.front().Characteristics.Contains("ShieldWall") || (op.front().Melee == "Long_Spear" && op.ranks > 1)))))
                        {
                            bonus = 0;
                        }

                    }
                    else
                    {

                        bonus += 1;
                    }

                }
                switch (this.front().Protection)
                {
                    case "Fully Armoured":
                        bonus += 1;
                        break;
                    case "aArmHrs/F_Armoured":
                        bonus += 1;
                        break;
                    case "Unprotected":
                        bonus -= 1;
                        break;

                    default:
                        bonus = 0;
                        break;
                }
            }


            if (this.front().Characteristics.Contains("Combat_Shy"))
            {
                bonus -= 1;
            }

            switch (this.front().Quality)
            {
                case "Exceptional":
                    bonus += 2;
                    break;
                case "Superior":
                    bonus += 1;
                    break;
                case "Poor":
                    bonus -= 1;
                    break;

                default:
                    bonus = 0;
                    break;
            }



            return bonus;

        }

    } 
}
