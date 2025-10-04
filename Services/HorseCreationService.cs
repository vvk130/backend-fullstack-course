public class HorseService : IHorseService
{
    private readonly AppDbContext _context;

    public HorseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Horse?> GetByIdAsync(Guid id)
    {
        return await _context.Horses
    }

    public async Task<Horse> CreateAsync(Horse horse)
    {
        _context.Horses.Add(horse);
    }

    public async Task<bool> UpdateAsync(Horse horse)
    {
    }

    public async Task<bool> DeleteAsync(Guid id)
    {

    }
}
