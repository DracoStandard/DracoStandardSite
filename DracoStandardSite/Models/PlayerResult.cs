#nullable disable

namespace DracoStandardSite.Models
{
    public partial class PlayerResult
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public Guid CompId { get; set; }
        public string Army { get; set; }
        public int? Position { get; set; }

        public double? Points { get; set; }
        public string Comp { get; set; }
        public DateTime? Date { get; set; }
        public int? Players { get; set; }
        public int? ArmyId { get; set; }
        public bool? Old { get; set; }
    }
}
