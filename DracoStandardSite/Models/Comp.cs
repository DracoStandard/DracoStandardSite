#nullable disable

namespace DracoStandardSite.Models
{
    public partial class Comp
    {
        public string Comp1 { get; set; }
        public Guid CompId { get; set; }
        public DateTime? Date { get; set; }
        public int? Players { get; set; }
        public int? MaxPoints { get; set; }
        public bool? Old { get; set; }
        public string Region { get; set; }
        public string System { get; set; }
    }
}
