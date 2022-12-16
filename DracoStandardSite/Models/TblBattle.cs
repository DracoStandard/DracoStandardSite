using System;
using System.Collections.Generic;

#nullable disable

namespace DracoStandardSite.Models
{
    public partial class TblBattle
    {
        public long BattleId { get; set; }
        public short? Attacker { get; set; }
        public short? Defender { get; set; }
        public short? Awin { get; set; }
        public short? Dwin { get; set; }
    }
}
