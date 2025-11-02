using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Horse> Horses { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<HorseBreed> HorseBreeds { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<PuzzleAnswer> PuzzleAnswers { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<SalesAd> SalesAds { get; set; }
    public DbSet<CompResult> CompResults { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<StockImg> StockImgs { get; set; }
    public DbSet<Alpaca> Alpacas { get; set; }
    public DbSet<Animal> Animals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Animal>()
                    .OwnsMany(h => h.Personalities);

        modelBuilder.Entity<Horse>()
                    .HasIndex(h => h.SireId);
        modelBuilder.Entity<Horse>()
                    .HasIndex(h => h.DamId);
        modelBuilder.Entity<Horse>()
                    .HasIndex(h => new { h.OwnerId, h.Gender, h.Age });

        modelBuilder.Entity<Horse>()
                    .OwnsMany(h => h.Fears);

        modelBuilder.Entity<PuzzleAnswer>()
                    .OwnsMany(p => p.PuzzlePieces);

        modelBuilder.Entity<Question>()
                    .OwnsMany(p => p.Options);
}

}
