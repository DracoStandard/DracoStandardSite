
using System;
using System.Collections.Generic;
using System.Linq;

namespace DracoStandardSite.Models
{
    public class Camp
    {
        public string Quality { get; set; }
        public string Type { get; set; }

        public List<string> QualList { get; set; }
        public List<string> TypeList { get; set; }

        public int points { get; set; }


        public Camp()
        {
            Quality = "Poor";
            Type = "Unfortified";

            QualList = new List<string>();
            QualList.Add("Poor");
            QualList.Add("Average");
            QualList.Add("Superior");
            QualList.Add("Exceptional");

            TypeList = new List<string>();
            TypeList.Add("Unfortified");
            TypeList.Add("Fortified");
            TypeList.Add("Mobile");
            TypeList.Add("Flexible");
            TypeList.Add("No Camp");

            points = calcPoints();
        }

        public int calcPoints()
        {
            double pts = 60;
            //get multipliers
            double multi = 1;
            double addon = 0;

            switch (Quality)
            {
                case "Poor":
                    multi = 0.6;
                    break;
                case "Superior":
                    multi = 1.4;
                    break;
                case "Exceptional":
                    multi = 1.7;
                    break;
                default:
                    multi = 1;
                    break;
            }
            switch(Type)
            {
                case "Fortified":
                    addon = 60;
                    break;
                case "Mobile":
                    addon = 40;
                    break;
                case "Flexible":
                    addon = 10;
                    break;
                case "No Camp":
                    addon = 150;
                    break;
                default:
                    addon = 0;
                    break;


            }
            pts = (pts + addon) * multi;
            points= (int)pts*3;
            return points;

        }



    }
    
}
