#nullable disable

namespace DracoStandardSite.Models
{
    public partial class ArmyRank
    {
        public int? Position { get; set; }
        public int ArmyId { get; set; }
        public string Army { get; set; }
        public double? RankScore { get; set; }
        public int? CompsPlayed { get; set; }
    }
}
