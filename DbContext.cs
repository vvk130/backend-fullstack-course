using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // public DbSet<MyEntity> MyEntities { get; set; }
}

// public class MyEntity
// {
//     public int Id { get; set; }
//     public required string Name { get; set; }
// }
