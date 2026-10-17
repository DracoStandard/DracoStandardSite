using BlazorTable;
using DracoStandardSite.Data;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o =>
{
    o.LoggingFields = HttpLoggingFields.RequestMethod
                    | HttpLoggingFields.RequestPath
                    | HttpLoggingFields.RequestQuery
                    | HttpLoggingFields.ResponseStatusCode
                    | HttpLoggingFields.Duration
                    | HttpLoggingFields.RequestHeaders;
    o.RequestHeaders.Add("User-Agent");
    o.RequestHeaders.Add("X-Forwarded-For");
    o.CombineLogs = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<rankingsContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IArmyBuilderService, ArmyBuilderService>();
builder.Services.AddTransient<IRankingsService, RankingsService>();
builder.Services.AddMemoryCache();
builder.Services.AddBlazorTable();
builder.Services.AddSingleton<PdfServices>();

var app = builder.Build();

app.UseHttpLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();