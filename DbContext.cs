using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Horse> Horses { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<HorseBreed> HorseBreeds { get; set; }
    public DbSet<Level> Levels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasDefaultSchema("public"); 
        // base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Horse>()
                    .OwnsMany(h => h.Personalities);

        modelBuilder.Entity<Horse>()
                    .OwnsMany(h => h.Fears);
    }

}
