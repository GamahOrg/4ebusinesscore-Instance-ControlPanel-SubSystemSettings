
using _4EPlatform.Domain.Models;

namespace Infrastructure.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<SubsytemSettings> SubsytemSettings { get; set; }
        public DbSet<LK_NoteTypes> LK_NoteTypes { get; set; }
        public DbSet<SubsytemSettingsMainClassification> SubsytemSettingsMainClassification { get; set; }
        public DbSet<SubsytemMainLookup> SubsytemMainLookup { get; set; }
        public DbSet<SubsystemSettingsMainClassificationLink> SubsystemSettingsMainClassificationLinks { get; set; }
        public DbSet<SSOSecuritySetting> SSOSecuritySetting { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubsytemSettings>().HasIndex(x => new { x.WorkSpaceKey }).IsUnique();
            modelBuilder.Entity<SubsytemSettings>().Property(x=>x.SubsytemMainLookupId).IsRequired(false);
            modelBuilder.Entity<SubsytemSettingsMainClassification>().Property(x=>x.SubsytemMainLookupId).IsRequired(false);
            modelBuilder.Entity<LK_NoteTypes>().HasIndex(x => new { x.NameAr, x.NameEn }).IsUnique();
            modelBuilder.Entity<SubsytemSettings>().Property(x => x.SSOSecuritySettingId).IsRequired(false);



        }
        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}