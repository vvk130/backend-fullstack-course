using Bogus;
namespace GameModel{

public class HorseService : IHorseService
{
    private readonly AppDbContext _context;

    private readonly Faker _faker = new();

    public HorseService(AppDbContext context)
    {
        _context = context;
    }

    public List<Horse> GetAll() => _context.Horses.ToList();

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


// public (Horse horse, IList<ValidationResult> validationResults) GenerateHorseByLevel(Int Level)
// {
//     var assignedBreed = GetRandomEnumValue<Breed>();

//     var horse = new Horse
//     {
//         Name = GenerateRandomHorseName(),
//         Breed = assignedBreed,       
//         Gender = GetRandomEnumValue<Gender>(),       
//         Color = GetColorByBreed(assignedBreed),  //TODO         
//         Capacity = GetCapacityByLevel(Level), //TODO
//         Relationship = 0,
//         Energy = 100,
//         Height = GetRandomHeightForBreed(assignedBreed),
//         Qualities = new Qualities { /* fill as needed */ },
//         Fears = new List<FearType> { FearType.Heights },
//         Personalities = new List<PersonalityType> { PersonalityType.Friendly }
//     };

//     var context = new ValidationContext(horse);
//     var validationResults = new List<ValidationResult>();
//     Validator.TryValidateObject(horse, context, validationResults, true);

//     return (horse, validationResults);
// }

//create qualities
//create color
//create fear function
//create personality function
//create capacity (depends on the level!!)

// public static T GetRandomEnumValue<T>()
// {
//     var values = Enum.GetValues(typeof(T));
//     var random = new Random();
//     return (T)values.GetValue(random.Next(values.Length))!;
// }

// Color randomColor = GetRandomEnumValue<Color>();
// Gender randomGender = GetRandomEnumValue<Gender>();


