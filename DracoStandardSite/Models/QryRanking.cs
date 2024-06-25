#nullable disable

namespace DracoStandardSite.Models
{
    public partial class QryRanking
    {
        public int? Position { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public double? RankScore { get; set; }
        public int? CompsPlayed { get; set; }
    }
}
