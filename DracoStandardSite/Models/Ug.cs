using DracoStandardSite.Components;

namespace DracoStandardSite.Models
{
    public class Ug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UgFile> Files { get; set; }
        public int ranks { get; set; }
        public int bases { get; set; }
        public int columns { get; set; }

        public Ug() { }

        public Ug(string Quality, string ugType, string melee, List<string> characteristics, int ranks, int columns)
        {
            Files = new List<UgFile>();
            this.ranks = ranks;
            this.columns = columns;
            for (int i = 0; i < columns; i++)
            {
                UgFile f = new UgFile();
                for (int j = 0; j < ranks; j++)
                {
                    Base b = new Base(Quality, ugType, melee, characteristics);
                    f.addBase(b);
                }
                Files.Add(f);
            }
        }

        public int combat(Ug enemy)
        {
            int result = 0;

            return result;
        }
    }
}
