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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Horse>()
                    .OwnsMany(h => h.Personalities);

        modelBuilder.Entity<Horse>()
                    .OwnsMany(h => h.Fears);
    }

}
