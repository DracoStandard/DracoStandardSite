using System.Net.Http.Json;
using System.Text.Json;
using DracoStandardSite.Models;

namespace DracoStandardSite.Data
{
    // Same method names as the original server-side service, so the pages and
    // components work unchanged. The data now comes from JSON files in
    // wwwroot/data, written by the DracoStandardSite.Export tool.
    public interface IRankingsService
    {
        Task<List<AllRankings>> getRankings(string system, bool topFive);
        Task<List<Comp>> GetComps(bool old);
        Task<List<Comp>> GetRecentComps();
        Task<List<CompResult>> GetCompResults(Guid CID);
        Task<List<PlayerResult>> GetPlayerResults(Guid PID);
        Task<List<PlayerArmyStats>> GetPlayerArmyStats(Guid PID);
        Task<List<ArmyRankBoost>> GetArmyRankBoosts();
        Task<List<ArmyRankBoost>> GetArmyRankBoostsByRegion(string region);
        Task<List<string>> getUniqueRegions();
        Task<List<ArmyUsed>> GetAllArmyUseds();
        Task<List<ArmyUsed>> GetArmyUseds(int AID);
        Task<List<ArmyIndex>> GetArmyIndexes();
        Task<List<string>> GetPdfFiles();
    }

    public class RankingsService : IRankingsService
    {
        private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

        private readonly HttpClient _http;

        // Each file is downloaded once per visit and then shared by every component.
        private readonly Dictionary<string, Task> _files = new();

        public RankingsService(HttpClient http) => _http = http;

        private async Task<List<T>> Load<T>(string name)
        {
            if (!_files.TryGetValue(name, out var task))
            {
                task = _http.GetFromJsonAsync<List<T>>($"data/{name}.json", Json);
                _files[name] = task;
            }

            try
            {
                return await (Task<List<T>>)task ?? new List<T>();
            }
            catch
            {
                _files.Remove(name);   // allow a retry on the next call
                throw;
            }
        }

        public async Task<List<AllRankings>> getRankings(string system, bool topFive)
        {
            var rows = (await Load<AllRankings>("allrankings")).Where(r => r.gameSystem == system);
            return (topFive ? rows.Take(5) : rows).ToList();
        }

        public async Task<List<Comp>> GetComps(bool old) =>
            (await Load<Comp>("comps")).Where(c => c.Old == old).ToList();

        public async Task<List<Comp>> GetRecentComps() =>
            (await Load<Comp>("comps"))
                .Where(c => c.Old == false)
                .OrderByDescending(c => c.Date)
                .Take(3)
                .ToList();

        public async Task<List<CompResult>> GetCompResults(Guid CID) =>
            (await Load<CompResult>("compresults"))
                .Where(r => r.CompId == CID)
                .OrderBy(r => r.Position)
                .ToList();

        public async Task<List<PlayerResult>> GetPlayerResults(Guid PID) =>
            (await Load<PlayerResult>("playerresults")).Where(r => r.PlayerId == PID).ToList();

        public async Task<List<PlayerArmyStats>> GetPlayerArmyStats(Guid PID) =>
            (await Load<PlayerArmyStats>("playerarmystats"))
                .Where(s => s.PlayerId == PID)
                .OrderByDescending(s => s.avgPoints)
                .Take(3)
                .ToList();

        public async Task<List<ArmyRankBoost>> GetArmyRankBoosts() =>
            (await Load<ArmyRankBoost>("armyrankboost")).OrderBy(a => a.Position).ToList();

        public async Task<List<ArmyRankBoost>> GetArmyRankBoostsByRegion(string region) =>
            (await Load<ArmyRankBoost>("armyrankboost"))
                .Where(a => a.Region == region)
                .OrderBy(a => a.Position)
                .ToList();

        public async Task<List<string>> getUniqueRegions() =>
            (await Load<ArmyIndex>("armyindex")).Select(a => a.Region).Distinct().ToList();

        public async Task<List<ArmyUsed>> GetAllArmyUseds() =>
            (await Load<ArmyUsed>("armyused")).ToList();

        public async Task<List<ArmyUsed>> GetArmyUseds(int AID) =>
            (await Load<ArmyUsed>("armyused")).Where(a => a.ArmyId == AID).ToList();

        public async Task<List<ArmyIndex>> GetArmyIndexes() =>
            (await Load<ArmyIndex>("armyindex")).ToList();

        public async Task<List<string>> GetPdfFiles() =>
            (await Load<string>("files")).ToList();
    }
}
