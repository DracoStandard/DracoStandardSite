using DracoStandardSite.Models;

using Microsoft.EntityFrameworkCore;



//https://www.blogofpi.com/crud-using-blazor-and-entity-framework-core/'

namespace DracoStandardSite.Data
{



    public interface IArmyBuilderService
    {
        Task<List<armyBuilderTroops>> GetUGs(int AID);
        Task<IEnumerable<ArmyIndex>> GetArmies();
        Task<Dictionary<string, int>> GetArmyBuilderPoints();
        Task<Dictionary<string, double>> GetArmyBuilderMultipliers();
        Task<IEnumerable<Generals>> getGeneralPoints();
    }



    public class ArmyBuilderService : IArmyBuilderService
    {
        private readonly rankingsContext _context;

        public ArmyBuilderService(rankingsContext context)
        {
            _context = context;
        }
        public async Task<List<armyBuilderTroops>> GetUGs(int AID)
        {
            //return await _context.AbtroopDatabases.ToListAsync();

            IQueryable<armyBuilderTroops> ugs = from u in _context.ArmyBuilderTroops
                                                select u;
            ugs = ugs.Where(u => u.ArmyNo == AID);


            return await ugs.ToListAsync();
        }

        public async Task<IEnumerable<ArmyIndex>> GetArmies()
        {
            return await _context.ArmyIndices.ToListAsync();
        }
        public async Task<Dictionary<string, int>> GetArmyBuilderPoints()
        {
            return await _context.ArmyBuilderPoints.ToDictionaryAsync(x => x.index, x => x.Value);

        }
        public async Task<Dictionary<string, double>> GetArmyBuilderMultipliers()
        {
            return await _context.ArmyBuilderMultipliers.ToDictionaryAsync(x => x.index, x => x.multiplier);
        }

        public async Task<IEnumerable<Generals>> getGeneralPoints()
        {
            return await _context.Generals.ToListAsync();
        }

    }
}
