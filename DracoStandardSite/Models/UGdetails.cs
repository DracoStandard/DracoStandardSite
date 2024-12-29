using System.ComponentModel;

namespace DracoStandardSite.Models
{
    public partial class UGdetails
    {

        public int? Ugno { get; set; }
        public string? Description { get; set; }
        public string? Formation { get; set; }
        public RegradeableChar? Quality { get; set; }
        public RegradeableChar? Shooting { get; set; }
        public string? Melee { get; set; }
        public string? ShootingWeap { get; set; }
        public string? Protection { get; set; }
        public List<ugCharacteristic>? Characteristics { get; set; }
        public int? Bases { get; set; }
        public string? Type { get; set; }
        public string? Class { get; set; }
        public double? PtsPerBase { get; set; }
        public double? TotalPts { get; set; }

        public bool delFlag { get; set; }

        public UGdetails()
        {
            Characteristics = new List<ugCharacteristic>();

        }

        public UGdetails(armyBuilderTroops troop)
        {
            try
            {
                if (troop == null) { throw new ArgumentNullException(nameof(troop)); }
            }
            catch (Exception ex) { return; }

            delFlag = false;

            Characteristics = new List<ugCharacteristic>();

            //convert troopdatabase to ug
            Description = troop.Description;
            Type = troop.Type;
            Quality = new RegradeableChar(troop.Quality);
            Formation = troop.Drill;
            Protection = troop.Armour;
            Shooting = new RegradeableChar(troop.Shoot_Skill);
            ShootingWeap = troop.Skill;
            Melee = troop.Weapon;

            //default no of bases for TUG
            Bases = 4;

            if (troop.Drill.Contains("Skirm"))
            { Class = "SUG"; }
            else { Class = "TUG"; }

            List<string> manList = new List<string>();
            manList.Add(troop.Char1);
            manList.Add(troop.Char2);
            manList.Add(troop.Char3);

            foreach (string m in manList)
            {
                if (m.Length >= 2)
                {
                    ugCharacteristic c = new ugCharacteristic(m, "mandatory");
                    Characteristics.Add(c);
                }
            }
            //***REMOVED***ise optional
            string opt = troop.Opt_Char;
            if (opt.Length >= 2)
            {
                List<string> optList = new List<string>();
                optList = opt.Split(',').ToList();
                foreach (string o in optList)
                {

                    string oT = o.Trim();

                    ugCharacteristic c = new ugCharacteristic(oT, "optional");
                    Characteristics.Add(c);
                }
            }




        }




    }
    

    [TypeConverter(typeof(RegradeableCharConverter))]
 


    public class RegradeableChar
    {
        public string grade { get; set; }
        public bool downgraded { get; set; }
        private string originalGrade { get; set; }

        public RegradeableChar()
        {
            originalGrade = "Average";
            downgraded = false;
            grade = originalGrade;
        }
        public RegradeableChar(string val)
        {
            originalGrade = val;
            downgraded = false;
            grade = originalGrade;
        }
        public override string ToString()
        {
            return grade;
        }

        public void regrade()
        {
            downgraded = !downgraded;
            grade = originalGrade;
            if (downgraded)
            {
                grade = getDownGrade(originalGrade);
            }
            else
            {
                grade = originalGrade;
            }
        }



        private string getDownGrade(string grade)
        {
            string newGrade;
            switch (grade)

            {
                case "Exceptional":
                    newGrade = "Superior";
                    break;
                case "Superior":
                    newGrade = "Average";
                    break;
                case "Average":
                    newGrade = "Poor";
                    break;
                case "Skilled":
                    newGrade = "Experienced";
                    break;
                case "Experienced":
                    newGrade = "Unskilled";
                    break;
                default:
                    newGrade = grade;
                    break;

            }

            return newGrade;
        }



    }
}



