using Bogus;
namespace GameModel{

public class HorseService : IHorseService
{
//     private readonly AppDbContext _context;
    private readonly Faker _faker = new();

    public string GenerateRandomHorseName()
    {
        var horseName = _faker.PickRandom(new[]
        {
            _faker.Commerce.ProductAdjective()
        }) + " " +
        _faker.PickRandom(new[]
        {
            _faker.Name.FirstName()
        });

        var capitalizedHorseName = char.ToUpper(horseName[0]) + horseName.Substring(1);

        return capitalizedHorseName;
    }

}
}


