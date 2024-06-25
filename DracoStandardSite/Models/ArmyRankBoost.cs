#nullable disable

namespace DracoStandardSite.Models
{
    public partial class ArmyRankBoost
    {
        public int? Position { get; set; }
        public int ArmyId { get; set; }
        public string Army { get; set; }
        public double? RankScore { get; set; }
        public int? CompsPlayed { get; set; }
        public double? Boost { get; set; }
        public string Region { get; set; }
    }
}
