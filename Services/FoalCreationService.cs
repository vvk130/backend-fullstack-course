using Bogus;
using GameModel;

namespace GameModel
{
    public class FoalCreationService : IFoalCreationService
    {
        private readonly AppDbContext _context;
        private readonly IHorseBreedService _horseBreedService;
        private readonly IHorseService _horseService;
        private readonly Faker _faker = new();
        private readonly Random _random = new();

        public FoalCreationService(AppDbContext context, IHorseBreedService horseBreedService, IHorseService horseService)
        {
            _context = context;
            _horseBreedService = horseBreedService;
            _horseService = horseService;
        }

        public async Task<OperationResult<Animal>> FoalTaskHandler(Guid SireId, Guid DamId, ItemType type){
            var result = new OperationResult<Animal>();

            if (!Enum.IsDefined(typeof(ItemType), type))
            {
                result.AddError(nameof(type), "Not valid type");
                return result;
            }

            var sire = await GetAnimalByTypeAsync(SireId, type);
            var dam  = await GetAnimalByTypeAsync(DamId, type);

            if (sire == null)
                result.AddError(nameof(sire.Id), "Sire not found.");

            if (dam == null)
                result.AddError(nameof(dam.Id), "Dam not found.");

            if (!result.Success)
                return result;

            if (sire.GetType() != dam.GetType())
                result.AddError(nameof(sire.Id), "Sire and Dam must be of the same animal type");

            if (sire?.Gender != Gender.Stallion)
                result.AddError(nameof(sire.Gender), "Sire must be a stallion.");

            if (dam?.Gender != Gender.Mare)
                result.AddError(nameof(dam.Gender), "Dam must be a mare.");

            if (sire?.Age < 3.0)
                result.AddError(nameof(sire.Age), "Sire must be at least 3 years old.");

            if (dam?.Age < 3.0)
                result.AddError(nameof(dam.Age), "Dam must be at least 3 years old.");

            if (_random.Next(0,101) > 90)
                result.AddError("Foaling", "Foaling was unsuccessful due to natural/random failure.");
                
            if (!result.Success)
                return result;
            
            if (type == ItemType.Horse){
                var foal = _horseService.CreateFoal((Horse)sire,(Horse)dam);
                result.Value = foal;
            } else {
                var alpacaFoal = _horseService.CreateAlpacaFoal((Alpaca)sire,(Alpaca)dam);
                result.Value = alpacaFoal;
            }         
            return result;
        }

        private async Task<Animal?> GetAnimalByTypeAsync(Guid id, ItemType type)
        {
            return type switch
            {
                ItemType.Horse  => await _context.Horses.FindAsync(id),
                ItemType.Alpaca => await _context.Alpacas.FindAsync(id),
                _ => null
            };
        }

    }
}