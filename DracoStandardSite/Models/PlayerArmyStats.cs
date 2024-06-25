
namespace DracoStandardSite.Models
{
    public class PlayerArmyStats
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public String Army { get; set; }
        public double avgPoints { get; set; }
        public int timesUsed { get; set; }


    }
}
