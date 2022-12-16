using System;

namespace DracoStandardSite.Models
{
   
    public class General
    {
        public List<string> typeList { get; set; } 

        public string type { get; set; }

        public bool isAlly { get; set; }
      
        public int points { get; set; }
        public bool isCinC { get; set; }

        public string position { get; set; }


        public General(bool cinc, bool isAchilles = false)
        {

            List<string> generalTypes = new List<string>();
            isCinC = cinc;

            if (isCinC)
            { 
                position = "CinC"; 
            }
            else
            {
                if (isAlly) 
                { 
                    position = "Ally"; 
                }
                else { position = "Sub"; } }



            
            points = 0;
            typeList = new List<string>();
            //type = "Competent Professional";

            generalTypes.Add("NA");
            if (isCinC || isAchilles) { generalTypes.Add("Legendary Professional"); }
            generalTypes.Add("Talented Professional");
            generalTypes.Add("Competent Professional");
            generalTypes.Add("Mediocre Professional");
            if (isCinC || isAchilles) generalTypes.Add("Legendary Instinctive");
            generalTypes.Add("Talented Instinctive");
            generalTypes.Add("Competent Instinctive");
            generalTypes.Add("Mediocre Instinctive");
            generalTypes.Add("NA");


            foreach (string s in generalTypes)
            {
                typeList.Add(s);
            }

         
        }

    }
}
