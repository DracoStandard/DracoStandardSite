using System;
using System.Collections.Generic;

namespace DracoStandardSite.Models
{
    public class Force
    {
        public string forceName { get; set; }
        public int armyNumber { get; set; }
        public Guid forceId { get; set; }
        public string forceDate { get; set; }
        public int pbs { get; set; }
        public int scouting { get; set; }
        public string terrain { get; set; }
        public List<General> generals { get; set; }
        public Camp camp { get; set; }
        public List<UGdetails> tugs { get; set; }
        public List<UGdetails> sugs { get; set; }
        public int totalPoints { get; set; }


        public Force()
        {
            tugs = new List<UGdetails>();
            sugs = new List<UGdetails>();
            generals = new List<General>();
            forceId = new Guid();

            forceName = "none";
            armyNumber = 1101;
            pbs = 0;
            scouting = 0;
            terrain = "none";
            camp=new Camp();

            forceDate = "2500BCE";

              

        }

        public Force(string jsonForce)
        {

        }
        public int calcPoints()
        {
            int pts =0;
            foreach(UGdetails u in tugs)
            {
                pts +=(int) u.TotalPts;

            }
            foreach(UGdetails u in sugs){
                pts += (int)u.TotalPts;

            }
            foreach (General g in generals)
            {
                pts += g.points;
            }

            pts += camp.points;

            totalPoints = pts; 
            return pts;
        }

    }
    
 
}
