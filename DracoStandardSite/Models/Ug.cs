using DracoStandardSite.Components;
using Newtonsoft.Json.Linq;

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

        private int currentBases { get; set; }

        public bool shattered { get; set; }
        public bool shoved { get; set; }

        public bool wounded { get; set; }
        public bool broken { get; set; }
   
        public Ug() { }

        public Ug(string Quality, string ugType, string melee, List<string> characteristics, int ranks, int columns)
        {
            bases = 0;
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
                    bases++;
                }
                Files.Add(f);
               
            }
            currentBases = bases;
        }

        // Copy constructor
        public Ug(Ug other)
        {
            Id = other.Id;
            Name = other.Name;
            Files = new List<UgFile>(other.Files.Select(f => new UgFile(f)));
            ranks = other.ranks;
            bases = other.bases;
            columns = other.columns;
            currentBases = other.currentBases;
            shattered = other.shattered;
            shoved = other.shoved;
            wounded = other.wounded;
            broken = other.broken;
        }


        public void casualty(UgFile f)
        {
            if (f.ranks > 0)
            {
                f.removeBase();
            }
            else
            {
                // Find the file with the most bases and remove a base from that
                UgFile fileWithMostBases = Files.OrderByDescending(file => file.bases.Count).FirstOrDefault();
                if (fileWithMostBases != null && fileWithMostBases.bases.Count > 0)
                {
                    fileWithMostBases.removeBase();
                }
            }
            currentBases--;
            broken = isBroken();
        }

        public bool isBroken()
        {
            int damage = (bases - currentBases) * 2;
            if (wounded) { damage++; }

            //f.dead = true;
            if (damage > (bases)) { broken = true; }

            return broken;
        }

        public int combat(Ug enemy)
        {

            int result = 0;

            return result;
        }
        public void resetFlags()
        {
            foreach (UgFile f in Files)
            {
                f.shoved = false;
                f.overlap = false;
                f.shattered = false;
            };
        }

    }
}
