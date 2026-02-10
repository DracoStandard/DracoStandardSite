using DracoStandardSite.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DracoStandardSite.Data
{
    public partial class rankingsContext : DbContext
    {
        public rankingsContext()
        {
        }

        public rankingsContext(DbContextOptions<rankingsContext> options)
            : base(options)
        {
        }




        public virtual DbSet<AbtroopDatabase> AbtroopDatabases { get; set; }

        public virtual DbSet<AllTimeRank> AllTimeRanks { get; set; }

        public virtual DbSet<ArmyBoost> ArmyBoosts { get; set; }
        public virtual DbSet<ArmyIndex> ArmyIndices { get; set; }

        public virtual DbSet<ArmyRank> ArmyRanks { get; set; }
        public virtual DbSet<ArmyRankBoost> ArmyRankBoosts { get; set; }
        public virtual DbSet<ArmyUsed> ArmyUseds { get; set; }
        public virtual DbSet<AvgPlayer> AvgPlayers { get; set; }
        public virtual DbSet<AvgPlayerPoint> AvgPlayerPoints { get; set; }
        public virtual DbSet<Booklist> Booklists { get; set; }
        public virtual DbSet<Characteristic> Characteristics { get; set; }
        public virtual DbSet<Comp> Comps { get; set; }
        public virtual DbSet<CompResult> CompResults { get; set; }


        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerResult> PlayerResults { get; set; }
        public virtual DbSet<QryRanking> QryRankings { get; set; }

        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ShootingSkill> ShootingSkills { get; set; }
        public virtual DbSet<TblBattle> TblBattles { get; set; }

        public virtual DbSet<Ukranking> Ukrankings { get; set; }
        public virtual DbSet<training> training { get; set; }

        public virtual DbSet<armyBuilderMultiplier> ArmyBuilderMultipliers { get; set; }
        public virtual DbSet<armyBuilderPoints> ArmyBuilderPoints { get; set; }

        public virtual DbSet<armyBuilderTroops> ArmyBuilderTroops { get; set; }

        public virtual DbSet<PlayerArmyStats> PlayerArmyStats { get; set; }

        public virtual DbSet<AllRankings> AllRankings { get; set; }

        public virtual DbSet<Generals> Generals { get; set; }

        public virtual DbSet<Unit> Units { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=sql10.hostinguk.net;Initial Catalog=rankings;Persist Security Info=True;User ID=dracostandard;Password=MxfiH5CykDI0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dracostandard")
                .HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");



            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("unit_info", "dracostandard");
            });

            modelBuilder.Entity<Generals>(entity =>
            {
                entity.HasKey(e => new { e.Grade, e.position });
                entity.ToTable("Generals", "dbo");
            });



            modelBuilder.Entity<PlayerArmyStats>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("playerArmyStats");
            });




            modelBuilder.Entity<AllRankings>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AllRankings");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            });

            modelBuilder.Entity<armyBuilderPoints>(entity =>
            {
                entity.HasKey(e => e.index);
                entity.ToTable("armyBuilderPoints");
            }
            );

            modelBuilder.Entity<armyBuilderMultiplier>(entity =>
            {
                entity.HasKey(e => e.index);
                entity.ToTable("armyBuilderMultiplier");
            });
            modelBuilder.Entity<armyBuilderTroops>(entity =>
            {
                entity.HasKey(e => new { e.ArmyNo, e.Num });

                entity.ToTable("armyBuilderTroops");

                entity.Property(e => e.Troop_Type).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Min).HasMaxLength(255);
                entity.Property(e => e.Max).HasMaxLength(255);
                entity.Property(e => e.UG_size).HasMaxLength(255);
                entity.Property(e => e.Type).HasMaxLength(255);
                entity.Property(e => e.Drill).HasMaxLength(255);
                entity.Property(e => e.Quality).HasMaxLength(255);
                entity.Property(e => e.Armour).HasMaxLength(255);
                entity.Property(e => e.Weapon).HasMaxLength(255);
                entity.Property(e => e.Shoot_Skill).HasMaxLength(255);
                entity.Property(e => e.Skill).HasMaxLength(255);
                entity.Property(e => e.Char1).HasMaxLength(255);
                entity.Property(e => e.Char2).HasMaxLength(255);
                entity.Property(e => e.Char3).HasMaxLength(255);
                entity.Property(e => e.Opt_Char).HasMaxLength(255);
            }
            );







            modelBuilder.Entity<AbtroopDatabase>(entity =>
            {
                entity.HasKey(e => new { e.ArmyNo, e.LineNumber });

                entity.ToTable("ABTroopDatabase");

                entity.Property(e => e.Description).HasMaxLength(255);



                entity.Property(e => e.Formation).HasMaxLength(255);

                entity.Property(e => e.Mandatory1).HasMaxLength(255);

                entity.Property(e => e.Mandatory2).HasMaxLength(255);

                entity.Property(e => e.Mandatory3).HasMaxLength(255);

                entity.Property(e => e.Max).HasMaxLength(255);

                entity.Property(e => e.Melee).HasMaxLength(255);

                entity.Property(e => e.Min).HasMaxLength(255);

                entity.Property(e => e.Optional).HasMaxLength(255);

                entity.Property(e => e.Protection).HasMaxLength(255);

                entity.Property(e => e.Quality).HasMaxLength(255);

                entity.Property(e => e.ShootingSkill).HasMaxLength(255);

                entity.Property(e => e.ShootingWeapon).HasMaxLength(255);

                entity.Property(e => e.TroopType).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Ugsize)
                    .HasMaxLength(255)
                    .HasColumnName("UGsize");
            });



            modelBuilder.Entity<AllTimeRank>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AllTimeRank");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.TotalPoints).HasColumnName("totalPoints");
            });



            modelBuilder.Entity<ArmyBoost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ArmyBoost");

                entity.Property(e => e.Army)
                    .HasMaxLength(255)
                    .HasColumnName("army");

                entity.Property(e => e.ArmyBoost1).HasColumnName("ArmyBoost");
            });


            modelBuilder.Entity<ArmyIndex>(entity =>
            {
                entity.HasKey(e => e.No);
                entity.ToTable("ArmyIndex");
            });



            modelBuilder.Entity<ArmyRank>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ArmyRank");

                entity.Property(e => e.Army).HasMaxLength(255);

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            });

            modelBuilder.Entity<ArmyRankBoost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ArmyRankBoost");

                entity.Property(e => e.Army).HasMaxLength(255);

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            });

            modelBuilder.Entity<ArmyUsed>(entity =>
            {
                entity.HasKey(e => new { e.ArmyId, e.CompId, e.PlayerId });

                entity.ToView("ArmyUsed");

                entity.Property(e => e.Army).HasMaxLength(255);

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");

                entity.Property(e => e.Comp)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("comp");

                entity.Property(e => e.CompId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("CompID");

                entity.Property(e => e.Competition).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Position).HasColumnName("position");
            });

            modelBuilder.Entity<AvgPlayer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AvgPlayers");
            });

            modelBuilder.Entity<AvgPlayerPoint>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AvgPlayerPoints");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            });

            modelBuilder.Entity<Booklist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("booklist");

                entity.Property(e => e.Region).HasMaxLength(255);
            });

            modelBuilder.Entity<Characteristic>(entity =>
            {
                entity.HasKey(e => new { e.Characteristic1, e.Type, e.Class });

                entity.ToTable("Characteristics", "dbo");

                entity.Property(e => e.Characteristic1)
                    .HasMaxLength(50)
                    .HasColumnName("Characteristic");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Class).HasMaxLength(50);
            });

            modelBuilder.Entity<Comp>(entity =>
            {
                entity.ToTable("comps", "dbo");

                entity.Property(e => e.CompId)
                    .HasMaxLength(255)
                    .HasColumnName("CompID");

                entity.Property(e => e.Comp1)
                    .HasMaxLength(255)
                    .HasColumnName("Comp");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Region).HasMaxLength(50);
            });

            modelBuilder.Entity<CompResult>(entity =>
            {
                entity.HasKey(e => new { e.CompId, e.PlayerId });

                entity.ToView("CompResults");

                entity.Property(e => e.Army).HasMaxLength(255);

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");

                entity.Property(e => e.Comp).HasMaxLength(255);

                entity.Property(e => e.CompId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("CompID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Position).HasColumnName("position");

            });





            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players", "dbo");

                entity.Property(e => e.PlayerId)
                    .ValueGeneratedNever()
                    .HasColumnName("PlayerID");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<PlayerResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PlayerResults");

                entity.Property(e => e.Army)
                    .HasMaxLength(255)
                    .HasColumnName("army");

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");

                entity.Property(e => e.Comp).HasMaxLength(255);

                entity.Property(e => e.CompId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("CompID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.Position).HasColumnName("position");

            });


            modelBuilder.Entity<QryRanking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryRankings");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            });



            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.Comp, e.PlayerId });

                entity.ToTable("results", "dbo");

                entity.Property(e => e.Comp)
                    .HasMaxLength(255)
                    .HasColumnName("comp");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Army)
                    .HasMaxLength(255)
                    .HasColumnName("army");

                entity.Property(e => e.ArmyId).HasColumnName("ArmyID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.Score).HasColumnName("score");
            });

            modelBuilder.Entity<ShootingSkill>(entity =>
            {
                entity.HasKey(e => new { e.Shooting, e.Class });

                entity.ToTable("ShootingSkill", "dbo");

                entity.Property(e => e.Shooting).HasMaxLength(50);

                entity.Property(e => e.Class).HasMaxLength(50);
            });

            modelBuilder.Entity<TblBattle>(entity =>
            {
                entity.HasKey(e => e.BattleId);

                entity.ToTable("tblBattle");

                entity.Property(e => e.BattleId)
                    .ValueGeneratedNever()
                    .HasColumnName("battleID");

                entity.Property(e => e.Attacker).HasColumnName("attacker");

                entity.Property(e => e.Awin).HasColumnName("awin");

                entity.Property(e => e.Defender).HasColumnName("defender");

                entity.Property(e => e.Dwin).HasColumnName("dwin");
            });






            modelBuilder.Entity<Ukranking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UKRankings");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            });

            modelBuilder.Entity<training>(entity =>
            {
                entity.HasKey(e => new { e.Training1, e.Type });

                entity.ToTable("Training", "dbo");

                entity.Property(e => e.Training1)
                    .HasMaxLength(50)
                    .HasColumnName("Training");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
