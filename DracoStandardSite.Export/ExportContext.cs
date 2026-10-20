using DracoStandardSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DracoStandardSite.Export;

/// <summary>
/// Read-only context covering just the views the public site shows.
/// Mappings are copied from rankingsContext in the original web project.
/// </summary>
public class ExportContext : DbContext
{
    public ExportContext(DbContextOptions<ExportContext> options) : base(options) { }

    public DbSet<AllRankings> AllRankings => Set<AllRankings>();
    public DbSet<Comp> Comps => Set<Comp>();
    public DbSet<CompResult> CompResults => Set<CompResult>();
    public DbSet<PlayerResult> PlayerResults => Set<PlayerResult>();
    public DbSet<PlayerArmyStats> PlayerArmyStats => Set<PlayerArmyStats>();
    public DbSet<ArmyRankBoost> ArmyRankBoosts => Set<ArmyRankBoost>();
    public DbSet<ArmyUsed> ArmyUseds => Set<ArmyUsed>();
    public DbSet<ArmyIndex> ArmyIndices => Set<ArmyIndex>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dracostandard");

        modelBuilder.Entity<AllRankings>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("AllRankings");
        });

        modelBuilder.Entity<Comp>(entity =>
        {
            entity.HasKey(e => e.CompId);
            entity.ToTable("comps", "dbo");
            entity.Property(e => e.CompId).HasColumnName("CompID");
            entity.Property(e => e.Comp1).HasColumnName("Comp");
            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<CompResult>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("CompResults");
            entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            entity.Property(e => e.CompId).HasColumnName("CompID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Position).HasColumnName("position");
        });

        modelBuilder.Entity<PlayerResult>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("PlayerResults");
            entity.Property(e => e.Army).HasColumnName("army");
            entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            entity.Property(e => e.CompId).HasColumnName("CompID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Position).HasColumnName("position");
        });

        modelBuilder.Entity<PlayerArmyStats>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("playerArmyStats");
        });

        modelBuilder.Entity<ArmyRankBoost>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("ArmyRankBoost");
            entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
        });

        modelBuilder.Entity<ArmyUsed>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("ArmyUsed");
            entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            entity.Property(e => e.Comp).HasColumnName("comp");
            entity.Property(e => e.CompId).HasColumnName("CompID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Position).HasColumnName("position");
        });

        modelBuilder.Entity<ArmyIndex>(entity =>
        {
            entity.HasKey(e => e.No);
            entity.ToTable("ArmyIndex");
        });
    }
}
