namespace DracoStandardSite.Models
{

    public class ugCharacteristic
    {
        public string charName { get; set; }
        public string charType { get; set; }
        public bool selected { get; set; }
        public ugCharacteristic(string cName, string cType)
        {
            charName = cName;
            charType = cType;
            //to deal with optional chars

            if (cType == "mandatory") { selected = true; }
            else { selected = false; }

        }
    }
}
