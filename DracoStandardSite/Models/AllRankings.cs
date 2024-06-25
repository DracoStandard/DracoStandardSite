#nullable disable

namespace DracoStandardSite.Models
{
    public partial class AllRankings
    {
        public string gameSystem { get; set; }
        public int? Position { get; set; }
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public double? RankScore { get; set; }
        public int? CompsPlayed { get; set; }
    }
}
