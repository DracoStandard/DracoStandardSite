using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;

namespace DracoStandardSite.Models

{

    public class Base
    {
        public string Characteristic1 { get; set; }
        public string Characteristic2 { get; set; }
        public string Characteristic3 { get; set; }
        public List<string> Characteristics { get; set; }
        public string Type { get; set; }
        public string Melee { get; set; }
        public string Quality { get; set; }
        public string Protection { get; set; }

        public Base(UGdetails ug)
        {
            Characteristics = ug.Characteristics?.Select(c => c.charName).ToList() ?? new List<string>();
            Type = ug.Type;
            Melee = ug.Melee;
            Protection = ug.Protection;
            Quality = ug.Quality?.grade ?? string.Empty; // Fix for CS0029 and CS8601
        }
        public Base(string Qual, string ugType, string mel, List<string> characteristics)
        {
            Characteristics = characteristics;
            Type = ugType;
            Melee = mel;
            Quality = Qual;
        }


    }
}
