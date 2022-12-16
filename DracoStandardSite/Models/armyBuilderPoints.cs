using System;
using System.Collections.Generic;

namespace DracoStandardSite.Models
{
    public partial class armyBuilderPoints
    {
        public string index { get; set; }
        public int Value { get; set; }

        public armyBuilderPoints()
        {
            index = "tug";
            Value = 0;
        }
    }

   
}

