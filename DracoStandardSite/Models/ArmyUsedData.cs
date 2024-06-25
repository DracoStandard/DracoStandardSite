#nullable disable

namespace DracoStandardSite.Models
{
    public partial class ArmyUsedData
    {
        public int ArmyId { get; set; }
        public string Army { get; set; }
        public string Comp { get; set; }
        public Guid PlayerId { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Competition { get; set; }
        public Guid CompId { get; set; }
        public string Region { get; set; }
    }
}
