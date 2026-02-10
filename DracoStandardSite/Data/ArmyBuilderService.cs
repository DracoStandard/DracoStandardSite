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
        Task<List<Unit>> getUnits();
        Task<List<string>> GetUniqueTypes();
        Task<List<string>> GetUniqueQualities();
        Task<List<string>> GetUniqueShootingSkills();
        Task<List<string>> GetUniqueNames();
        Task<List<string>> GetUniqueMeleeWeaponries();
        Task<List<string>> GetUniqueMandatoryCharacteristics();
        Task<List<string>> GetUniqueOptionalCharacteristics();
        Task<List<string>> GetUniqueUGSizes();
        Task<List<string>> GetUniqueTrainingAndFormations();
        Task<List<string>> GetUniqueProtections();
        Task<List<string>> GetUniqueWeaponries();
    }



    public class ArmyBuilderService : IArmyBuilderService
    {
        private readonly rankingsContext _context;

        public ArmyBuilderService(rankingsContext context)
        {
            _context = context;
        }

        public async Task<List<Unit>> getUnits()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<List<armyBuilderTroops>> GetUGs(int AID)
        {
            IQueryable<armyBuilderTroops> ugs = from u in _context.ArmyBuilderTroops
                                                select u;
            ugs = ugs.Where(u => u.ArmyNo == AID);

            var result = await ugs.ToListAsync();

            // Handle null values
            foreach (var ug in result)
            {
                ug.Troop_Type = ug.Troop_Type ?? string.Empty;
                ug.Description = ug.Description ?? string.Empty;
                ug.Min = ug.Min ?? string.Empty;
                ug.Max = ug.Max ?? string.Empty;
                ug.UG_size = ug.UG_size ?? string.Empty;
                ug.Type = ug.Type ?? string.Empty;
                ug.Drill = ug.Drill ?? string.Empty;
                ug.Quality = ug.Quality ?? string.Empty;
                ug.Armour = ug.Armour ?? string.Empty;
                ug.Weapon = ug.Weapon ?? string.Empty;
                ug.Shoot_Skill = ug.Shoot_Skill ?? string.Empty;
                ug.Skill = ug.Skill ?? string.Empty;
                ug.Char1 = ug.Char1 ?? string.Empty;
                ug.Char2 = ug.Char2 ?? string.Empty;
                ug.Char3 = ug.Char3 ?? string.Empty;
                ug.Opt_Char = ug.Opt_Char ?? string.Empty;
            }

            return result;
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

        public async Task<List<string>> GetUniqueTypes()
        {
            return await _context.Units.Select(u => u.Type).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueQualities()
        {
            return await _context.Units.Select(u => u.Quality).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueShootingSkills()
        {
            return await _context.Units.Select(u => u.ShootingSkill).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueNames()
        {
            return await _context.Units.Select(u => u.Name).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueMeleeWeaponries()
        {
            return await _context.Units.Select(u => u.Melee_Weaponry).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueMandatoryCharacteristics()
        {
            return await _context.Units.Select(u => u.Mandatory_Characteristics).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueOptionalCharacteristics()
        {
            return await _context.Units.Select(u => u.Optional_Characteristics).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueUGSizes()
        {
            return await _context.Units.Select(u => u.UGSize).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueTrainingAndFormations()
        {
            return await _context.Units.Select(u => u.Training_and_Formation).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueProtections()
        {
            return await _context.Units.Select(u => u.Protection).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetUniqueWeaponries()
        {
            return await _context.Units.Select(u => u.Weaponry).Distinct().ToListAsync();
        }
    }
}
