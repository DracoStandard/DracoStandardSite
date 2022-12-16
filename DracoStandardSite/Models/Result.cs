using System;
using System.Collections.Generic;

#nullable disable

namespace DracoStandardSite.Models
{
    public partial class Result
    {
        public double? Position { get; set; }
        public string Name { get; set; }
        public string Army { get; set; }
        public double? Score { get; set; }
        public double? Points { get; set; }
        public string Comp { get; set; }
        public int PlayerId { get; set; }
        public int? ArmyId { get; set; }
    }
}
