using System;
using System.Collections.Generic;

#nullable disable

namespace DracoStandardSite.Models
{
    public partial class Ukranking
    {
        public int? Position { get; set; }
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public double? RankScore { get; set; }
        public int? CompsPlayed { get; set; }
    }
}
