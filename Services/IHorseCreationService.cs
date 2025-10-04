// Services/IHorseService.cs
public interface IHorseService
{
    Task<Horse?> GetByIdAsync(Guid id);
    Task<Horse> CreateAsync(Horse horse);
    Task<bool> UpdateAsync(Horse horse);
    Task<bool> DeleteAsync(Guid id);
}
