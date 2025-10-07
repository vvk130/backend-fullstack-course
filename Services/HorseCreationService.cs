using Bogus;

namespace GameModel
{
    public class HorseService : IHorseService
    {
        private readonly AppDbContext _context;
        private readonly IHorseBreedService _horseBreedService;
        private readonly Faker _faker = new();

        public HorseService(AppDbContext context, IHorseBreedService horseBreedService)
        {
            _context = context;
            _horseBreedService = horseBreedService;
        }

    public List<Horse> GetAll() => _context.Horses.ToList();

    public Horse CreateHorse(){
            var chosenBreed = _faker.PickRandom<Breed>();

            var horse = new Horse
            {
                Name = GenerateRandomHorseName(),
                Age = 10.00,
                Breed = chosenBreed,       
                Gender = _faker.PickRandom<Gender>(),       
                Color = _horseBreedService.GetRandomColorForBreed(chosenBreed),           
                Capacity = 150, 
                Relationship = 0,
                Energy = 100,
                Height = _horseBreedService.GetRandomHeightForBreed(chosenBreed),
                Qualities = new Qualities { Strength = 10, Agility = 8, Endurance = 7, Speed = 9, Intelligence = 6, Stamina = 8, JumpingAbility = 7 },
                Fears = new List<FearType>
                {
                    new FearType { FearItem = FearItem.Puddles, Discovered = false, Severity = 5 },
                    new FearType { FearItem = FearItem.Thunder, Discovered = true, Severity = 8 },
                    new FearType { FearItem = FearItem.Crowds, Discovered = false, Severity = 3 }
                },
                Personalities = new List<PersonalityType> { 
                    new PersonalityType { PersonalityTrait = PersonalityTrait.Bold, Discovered = false, Severity = 5 },
                    new PersonalityType { PersonalityTrait = PersonalityTrait.Cuddly, Discovered = true, Severity = 8 },
                    new PersonalityType { PersonalityTrait = PersonalityTrait.Anxious, Discovered = false, Severity = 3 }
                }
            };
            _context.Horses.Add(horse);
            _context.SaveChanges();
        return horse;
    }

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
//     var context = new ValidationContext(horse);
//     var validationResults = new List<ValidationResult>();
//     Validator.TryValidateObject(horse, context, validationResults, true);

//     return (horse, validationResults);
// }
