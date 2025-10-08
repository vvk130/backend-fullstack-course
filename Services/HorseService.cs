using Bogus;

namespace GameModel
{
    public class HorseService : IHorseService
    {
        private readonly AppDbContext _context;
        private readonly IHorseBreedService _horseBreedService;
        private readonly Faker _faker = new();
        private readonly Random _random = new();

        public HorseService(AppDbContext context, IHorseBreedService horseBreedService)
        {
            _context = context;
            _horseBreedService = horseBreedService;
        }

    public List<Horse> GetAll() => _context.Horses.ToList();

    public double RandomHorseAge() => Math.Round(_random.NextDouble() * (21.0 - 3.0) + 3.0, 1);

    public Horse CreateHorse(){
            var chosenBreed = _faker.PickRandom<Breed>();
            var horse = new Horse
            {
                Name = GenerateRandomHorseName(),
                Age = RandomHorseAge(),
                Breed = chosenBreed,  
                Gender = _faker.PickRandom<Gender>(),       
                Color = _horseBreedService.GetRandomColorForBreed(chosenBreed),           
                Capacity = _random.Next(130,151), 
                Relationship = 0,
                Energy = 100,
                Height = _horseBreedService.GetRandomHeightForBreed(chosenBreed),
                Qualities = new Qualities { Strength = _random.Next(1,11), Agility = _random.Next(1,11), Endurance = _random.Next(1,11), Speed = _random.Next(1,11), Intelligence = _random.Next(1,11), Stamina = _random.Next(1,11), JumpingAbility = _random.Next(1,11) },
                Fears = new List<FearType>
                {
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = false, Severity = _random.Next(1,11) },
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = true, Severity = _random.Next(1,11) },
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = false, Severity = _random.Next(1,11) }
                },
                Personalities = new List<PersonalityType> { 
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = false, Severity = _random.Next(1,11) },
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = true, Severity = _random.Next(1,11) },
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = false, Severity = _random.Next(1,11) }
                }
            };
            _context.Horses.Add(horse);
            _context.SaveChanges();
        return horse;
    }

    public T PickRandomEnumValue<T>(IList<T> allowedValues) where T : Enum
    {
        if (allowedValues == null || allowedValues.Count == 0)
            throw new ArgumentException("Allowed values list cannot be null or empty.");

        return allowedValues[_random.Next(allowedValues.Count)];
    }

    public int SafeRandomNext(int value1, int value2)
    {
        int min = Math.Min(value1, value2);
        int max = Math.Max(value1, value2);

        if (min == max)
            return min;

        return _random.Next(min, max + 1);  
    }

    public Horse CreateFoal(Horse sire, Horse dam){
            var chosenBreed = sire.Breed == dam.Breed 
                            ? sire.Breed 
                            : Breed.Unknown;

            var chosenColor = chosenBreed != Breed.Unknown
                ? PickRandomEnumValue(new[] { sire.Color, dam.Color })
                : _horseBreedService.GetRandomColorForBreed(chosenBreed);

            var horse = new Horse
            {
                Name = GenerateRandomHorseName(),
                Age = 0.0,
                Breed = chosenBreed,  
                Gender = PickRandomEnumValue(new[] { Gender.Stallion, Gender.Mare }),       
                Color = chosenColor,           
                Capacity = SafeRandomNext(sire.Capacity, dam.Capacity), 
                Relationship = 0,
                Energy = 100,
                Height = SafeRandomNext(sire.Height, dam.Height),
                Qualities = new Qualities { Strength = _random.Next(1,11), Agility = _random.Next(1,11), Endurance = _random.Next(1,11), Speed = _random.Next(1,11), Intelligence = _random.Next(1,11), Stamina = _random.Next(1,11), JumpingAbility = _random.Next(1,11) },
                Fears = new List<FearType>
                {
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = false, Severity = _random.Next(1,11) },
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = true, Severity = _random.Next(1,11) },
                    new FearType { FearItem = _faker.PickRandom<FearItem>(), Discovered = false, Severity = _random.Next(1,11) }
                },
                Personalities = new List<PersonalityType> { 
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = false, Severity = _random.Next(1,11) },
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = true, Severity = _random.Next(1,11) },
                    new PersonalityType { PersonalityTrait = _faker.PickRandom<PersonalityTrait>(), Discovered = false, Severity = _random.Next(1,11) }
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
    
    public async Task<bool> BatchHorsesEnergyUpdate()
    {
        try
        {
            await _context.Horses.ExecuteUpdateAsync(h =>
                h.SetProperty(h => h.Energy, h => 100));

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating horse energy: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> BatchHorsesAgeUpdate()
    {
        try
        {
            await _context.Horses.ExecuteUpdateAsync(h =>
                h.SetProperty(h => h.Age, h => h.Age + 0.1));

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating horse age: {ex.Message}");
            return false;
        }
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
