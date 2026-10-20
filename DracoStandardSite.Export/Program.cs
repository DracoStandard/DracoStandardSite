using System.Text.Json;
using DracoStandardSite.Export;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Exports the data the public site shows into JSON files.
//
// Connection string (first one found wins):
//   --ConnectionStrings:DefaultConnection "..."            (command line)
//   ConnectionStrings__DefaultConnection environment variable  (used by GitHub Actions)
//   user secrets shared with the original web project       (your PC)
//
// Output folder: --out <folder>, default DracoStandardSite.Static/wwwroot/data in the repo.

var config = new ConfigurationBuilder()
    .AddUserSecrets<ExportContext>(optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.Error.WriteLine("No connection string found. Set ConnectionStrings:DefaultConnection in user secrets, " +
                            "the ConnectionStrings__DefaultConnection environment variable, or pass it on the command line.");
    return 1;
}

var repoRoot = FindRepoRoot();
var outDir = config["out"] ?? Path.Combine(repoRoot, "DracoStandardSite.Static", "wwwroot", "data");
Directory.CreateDirectory(outDir);

var options = new DbContextOptionsBuilder<ExportContext>()
    .UseSqlServer(connectionString)
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    .Options;

await using var db = new ExportContext(options);

var json = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = false };

async Task Write<T>(string name, IQueryable<T> query)
{
    var rows = await query.ToListAsync();
    var path = Path.Combine(outDir, name + ".json");
    await File.WriteAllTextAsync(path, JsonSerializer.Serialize(rows, json));
    Console.WriteLine($"  {name,-16} {rows.Count,6} rows");
}

Console.WriteLine($"Exporting to {outDir}");

// Rankings are ordered by position so "top five" really is the top five.
await Write("allrankings", db.AllRankings
    .OrderBy(r => r.gameSystem).ThenBy(r => r.Position == null).ThenBy(r => r.Position));
await Write("comps", db.Comps.OrderByDescending(c => c.Date));
await Write("compresults", db.CompResults.OrderBy(r => r.CompId).ThenBy(r => r.Position));
await Write("playerresults", db.PlayerResults.OrderByDescending(r => r.Date));
await Write("playerarmystats", db.PlayerArmyStats);
await Write("armyrankboost", db.ArmyRankBoosts.OrderBy(a => a.Position));
await Write("armyused", db.ArmyUseds);
await Write("armyindex", db.ArmyIndices.OrderBy(a => a.No));

// List of downloadable PDFs (still kept in the original project's wwwroot/files folder).
var pdfDir = Path.Combine(repoRoot, "DracoStandardSite", "wwwroot", "files");
var pdfs = Directory.Exists(pdfDir)
    ? Directory.GetFiles(pdfDir, "*.pdf").Select(Path.GetFileName).Order().ToList()
    : new List<string>();
await File.WriteAllTextAsync(Path.Combine(outDir, "files.json"), JsonSerializer.Serialize(pdfs, json));
Console.WriteLine($"  {"files",-16} {pdfs.Count,6} PDFs");

Console.WriteLine("Done. Commit and push the data folder to publish the update.");
return 0;

static string FindRepoRoot()
{
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    while (dir != null && !File.Exists(Path.Combine(dir.FullName, "DracoStandardSite.sln")))
        dir = dir.Parent;
    return dir?.FullName ?? Directory.GetCurrentDirectory();
}
