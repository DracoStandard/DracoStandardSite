using DracoStandardSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

//https://www.blogofpi.com/crud-using-blazor-and-entity-framework-core/'

namespace DracoStandardSite.Data
{

    public interface IRankingsService
    {
        Task<List<Ukranking>> GetRankings();
        Task<List<PlayerResult>> GetPlayerResults(Guid PID);
        Task<List<Comp>> GetComps(bool old);
        Task<List<CompResult>> GetCompResults(Guid CID);
        Task<List<ArmyUsed>> GetArmyUseds(int AID);
        Task<List<ArmyBoost>> GetArmyBoosts();
        Task<List<PlayerArmyStats>> GetPlayerArmyStats(Guid PID);

        Task<List<ArmyRankBoost>> GetArmyRankBoosts();
        Task<List<ArmyRankBoost>> GetArmyRankBoostsByRegion(string region);


        Task<List<AllRankings>> getRankings(string system, bool topFive);

        Task<List<Comp>> GetRecentComps();

        Task<List<string>> getUniqueRegions();

        Task<List<ArmyUsed>> GetAllArmyUseds();

        Task<List<ArmyIndex>> GetArmyIndexes();


    }

    public class RankingsService : IRankingsService
    {
        private readonly rankingsContext _context;
        private readonly IMemoryCache _cache;

        public RankingsService(rankingsContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<ArmyIndex>> GetArmyIndexes()
        {
            return await _cache.GetOrCreateAsync("armyindexes", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await _context.ArmyIndices.ToListAsync();
            });
        }

        public async Task<List<Comp>> GetRecentComps()
        {
            return await _cache.GetOrCreateAsync("recentcomps", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<Comp> cmps = from c in _context.Comps select c;
                cmps = cmps.Where(c => c.Old == false);

                cmps = cmps.OrderByDescending(s => s.Date);
                cmps = cmps.Take(3);

                return await cmps.ToListAsync();
            });
        }

        public async Task<List<ArmyRankBoost>> GetArmyRankBoostsByRegion(string region)
        {
            return await _cache.GetOrCreateAsync($"armyrankboosts:region:{region}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<ArmyRankBoost> arb = from a in _context.ArmyRankBoosts select a;
                arb = arb.Where(s => s.Region == region);
                arb = arb.OrderBy(s => s.Position);

                return await arb.ToListAsync();
            });
        }

        public async Task<List<string>> getUniqueRegions()
        {
            return await _cache.GetOrCreateAsync("uniqueregions", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await _context.ArmyIndices
                    .Select(ai => ai.Region)
                    .Distinct()
                    .ToListAsync();
            });
        }

        public async Task<List<ArmyRankBoost>> GetArmyRankBoosts()
        {
            return await _cache.GetOrCreateAsync("armyrankboosts", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<ArmyRankBoost> arb = from a in _context.ArmyRankBoosts select a;
                arb = arb.OrderBy(s => s.Position);
                return await arb.ToListAsync();
            });
        }

        public async Task<List<AllRankings>> getRankings(string system, bool topFive)
        {
            return await _cache.GetOrCreateAsync($"rankings:{system}:{topFive}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<AllRankings> rankQuery = from s in _context.AllRankings
                                                    select s;
                rankQuery = rankQuery.Where(s => s.gameSystem == system);
                if (topFive) { rankQuery = rankQuery.Take(5); }

                return await rankQuery.ToListAsync();
            });
        }

        public async Task<List<Ukranking>> GetRankings()
        {
            return await _cache.GetOrCreateAsync("ukrankings", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await _context.Ukrankings.ToListAsync();
            });
        }

        public async Task<List<PlayerArmyStats>> GetPlayerArmyStats(Guid PID)
        {
            return await _cache.GetOrCreateAsync($"playerarmystats:{PID}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<PlayerArmyStats> plArSt = from s in _context.PlayerArmyStats
                                                     select s;
                plArSt = plArSt.Where(s => s.PlayerId == PID);

                plArSt = plArSt.OrderByDescending(s => s.avgPoints);
                plArSt = plArSt.Take(3);

                return await plArSt.ToListAsync();
            });
        }

        public async Task<List<PlayerResult>> GetPlayerResults(Guid PID)
        {
            return await _cache.GetOrCreateAsync($"playerresults:{PID}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<PlayerResult> PResIQ = from s in _context.PlayerResults
                                                  select s;
                PResIQ = PResIQ.Where(s => s.PlayerId == PID);

                return await PResIQ.ToListAsync();
            });
        }

        public async Task<List<Comp>> GetComps(bool old)
        {
            return await _cache.GetOrCreateAsync($"comps:{old}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<Comp> cmps = from c in _context.Comps select c;
                cmps = cmps.Where(c => c.Old == old);
                return await cmps.ToListAsync();
            });
        }

        public async Task<List<CompResult>> GetCompResults(Guid CID)
        {
            return await _cache.GetOrCreateAsync($"compresults:{CID}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<CompResult> CR = from r in _context.CompResults select r;
                CR = CR.Where(r => r.CompId == CID);
                CR = CR.OrderBy(r => r.Position);
                return await CR.ToListAsync();
            });
        }
        public async Task<List<ArmyUsed>> GetArmyUseds(int AID)
        {
            return await _cache.GetOrCreateAsync($"armyuseds:{AID}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                IQueryable<ArmyUsed> armies = from a in _context.ArmyUseds select a;
                armies = armies.Where(a => a.ArmyId == AID);
                return await armies.ToListAsync();
            });
        }

        public async Task<List<ArmyUsed>> GetAllArmyUseds()
        {
            return await _cache.GetOrCreateAsync("allarmyuseds", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await _context.ArmyUseds.ToListAsync();
            });
        }
        public async Task<List<ArmyBoost>> GetArmyBoosts()
        {
            return await _cache.GetOrCreateAsync("armyboosts", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await _context.ArmyBoosts.ToListAsync();
            });
        }
    }


}

