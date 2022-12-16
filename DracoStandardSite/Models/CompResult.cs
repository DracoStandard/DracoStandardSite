using System;
using System.Collections.Generic;

#nullable disable

namespace DracoStandardSite.Models
{
    public partial class CompResult
    {
        public Guid CompId { get; set; }
        public string Comp { get; set; }
        public DateTime? Date { get; set; }
        public Guid PlayerId { get; set; }
        public string Name { get; set; }

        public int Position { get; set; }
        public string Army { get; set; }
        public int? ArmyId { get; set; }
    }
}
