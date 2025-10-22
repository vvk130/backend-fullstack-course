using Bogus;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

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

        // public async Task<PaginatedResult<HorseShortDto>> FindAsync(
        // Expression<Func<Horse, bool>> predicate,
        // int pageNumber = 1,
        // int pageSize = 10)
        // {
        // if (pageNumber < 1) pageNumber = 1;
        // if (pageSize < 1) pageSize = 10;

        // // Get the total count of matching records
        // var totalCount = await _repository.CountAsync(predicate);

        // // Get the paginated data and project it to HorseShortDto directly in the query
        // var horseShortDtos = await _repository
        //     .Where(predicate) // Apply the predicate filter
        //     .Skip((pageNumber - 1) * pageSize) // Skip previous pages
        //     .Take(pageSize) // Take the page size
        //     .ProjectTo<HorseShortDto>(_mapper.ConfigurationProvider) // Project to DTO
        //     .ToListAsync(); // Execute the query and fetch results

        // // Return the paginated result with the mapped DTOs
        // return new PaginatedResult<HorseShortDto>(horseShortDtos, totalCount, pageNumber, pageSize);
        // }

    public double RandomHorseAge() => Math.Round(_random.NextDouble() * (21.0 - 3.0) + 3.0, 1);

    public Horse CreateHorse(Guid id){
            var chosenBreed = _faker.PickRandom<Breed>();
            var chosenGender =_faker.PickRandom<Gender>();
            var horse = new Horse
            {
                Name = GenerateRandomHorseName(chosenGender),
                Age = RandomHorseAge(),
                Breed = chosenBreed,  
                Gender = chosenGender,       
                Color = _horseBreedService.GetRandomColorForBreed(chosenBreed),           
                Capacity = _random.Next(130,151), 
                Relationship = 0,
                OwnerId = id,
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

            var chosenGender = PickRandomEnumValue(new[] { Gender.Stallion, Gender.Mare });

            var chosenColor = chosenBreed != Breed.Unknown
                ? PickRandomEnumValue(new[] { sire.Color, dam.Color })
                : _horseBreedService.GetRandomColorForBreed(chosenBreed);

            var horse = new Horse
            {
                Name = GenerateRandomHorseName(chosenGender),
                ImgUrl = "https://res.cloudinary.com/dn4bwpln0/image/upload/v1760099887/foal_a8kxhz.jpg",
                Age = 0.0,
                Breed = chosenBreed,  
                Gender = chosenGender,       
                Color = chosenColor,           
                Capacity = SafeRandomNext(sire.Capacity, dam.Capacity), 
                Relationship = 0,
                Energy = 100,
                Height = SafeRandomNext(sire.Height, dam.Height),
                SireId = sire.Id,
                DamId = dam.Id,
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

    public string GenerateRandomHorseName(Gender gender)
    {
        var fakerGender = MapToFakerGender(gender);

        var horseName = _faker.Commerce.ProductAdjective() + " " + _faker.Name.FirstName(fakerGender);

        var capitalizedHorseName = char.ToUpper(horseName[0]) + horseName.Substring(1);

        return capitalizedHorseName;
    }

    private Bogus.DataSets.Name.Gender MapToFakerGender(Gender gender)
    {
        return gender switch
        {
            Gender.Mare => Bogus.DataSets.Name.Gender.Female,
            Gender.Stallion => Bogus.DataSets.Name.Gender.Male,
            Gender.Gelding => Bogus.DataSets.Name.Gender.Male
        };
    }

    
    public void BatchHorsesEnergyUpdate()
    {
        _context.Horses.ExecuteUpdateAsync(h =>
            h.SetProperty(h => h.Energy, h => 100));
    }

    public void BatchHorsesAgeUpdate()
    {
        _context.Horses.ExecuteUpdateAsync(h =>
            h.SetProperty(h => h.Age, h => h.Age + 0.1));
    }

    }
}
