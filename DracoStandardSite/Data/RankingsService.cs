using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using DracoStandardSite.Models;
using System.Security.Cryptography;

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



    }

    public class RankingsService : IRankingsService
    {
        private readonly rankingsContext _context;

        public RankingsService(rankingsContext context)
        {
            _context = context;
        }


        public async Task<List<Comp>> GetRecentComps()
        {
            IQueryable<Comp> cmps = from c in _context.Comps select c;
            cmps = cmps.Where(c => c.Old == false);

            cmps = cmps.OrderByDescending(s => s.Date);
            cmps = cmps.Take(3);

            return await cmps.ToListAsync();
        }

        public async Task<List<ArmyRankBoost>> GetArmyRankBoostsByRegion(string region)
        {
            IQueryable<ArmyRankBoost> arb = from a in _context.ArmyRankBoosts select a;
            arb=arb.Where(s => s.Region == region);
            arb = arb.OrderBy(s => s.Position);

            

            return await arb.ToListAsync();
        }

        public async Task<List<ArmyRankBoost>> GetArmyRankBoosts()
        {
            IQueryable<ArmyRankBoost>arb = from a in _context.ArmyRankBoosts select a;
            arb = arb.OrderBy(s => s.Position);
            return await arb.ToListAsync();
        }

        public async Task<List<AllRankings>> getRankings(string system, bool topFive)
        {

            IQueryable<AllRankings> rankQuery = from s in _context.AllRankings
                                              select s;
            rankQuery = rankQuery.Where(s => s.gameSystem == system);
            if (topFive) { rankQuery = rankQuery.Take(5); }

            return await rankQuery.ToListAsync();
        }

        public async Task<List<Ukranking>> GetRankings()
        {
            return await _context.Ukrankings.ToListAsync();
        }

        public async Task<List<PlayerArmyStats>> GetPlayerArmyStats(Guid PID)
        {
            IQueryable<PlayerArmyStats> plArSt = from s in _context.PlayerArmyStats
                                                 select s;
            plArSt = plArSt.Where(s => s.PlayerId == PID);

            plArSt = plArSt.OrderByDescending(s => s.avgPoints);
            plArSt = plArSt.Take(3);

            return await plArSt.ToListAsync();
        }

        public async Task<List<PlayerResult>> GetPlayerResults(Guid PID)
        {

            IQueryable<PlayerResult> PResIQ = from s in _context.PlayerResults
                                              select s;
            PResIQ = PResIQ.Where(s => s.PlayerId == PID);


            return await PResIQ.ToListAsync();
        }

        public async Task<List<Comp>> GetComps(bool old)
        {
            IQueryable<Comp> cmps = from c in _context.Comps select c;
            cmps = cmps.Where(c => c.Old == old);
            return await cmps.ToListAsync();
        }

        public async Task<List<CompResult>> GetCompResults(Guid CID)
        {
            IQueryable<CompResult> CR = from r in _context.CompResults select r;
            CR = CR.Where(r => r.CompId == CID);
            CR = CR.OrderBy(r => r.Position);
            return await CR.ToListAsync();

        }
        public async Task<List<ArmyUsed>> GetArmyUseds(int AID)
        {
            IQueryable<ArmyUsed> armies = from a in _context.ArmyUseds select a;
            armies = armies.Where(a => a.ArmyId == AID);
            return await armies.ToListAsync();
        }
        public async Task<List<ArmyBoost>> GetArmyBoosts()
        {
            return await _context.ArmyBoosts.ToListAsync();
        }
    }


}

