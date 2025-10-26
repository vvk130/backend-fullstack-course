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

        public async Task<OperationResult<Horse>> FoalTaskHandler(Guid SireId, Guid DamId, ItemType type){
            var result = new OperationResult<Horse>();
            
            // TODO Check previous foal - is it too soon
            // TODO Create FoalingEvent db 

            if (!Enum.IsDefined(typeof(ItemType), type))
            {
                result.AddError(nameof(type), "Not valid type");
                return result;
            }

            var sire;
            var dam;

            switch (type)
            {
                case ItemType.Horse:
                    sire = await _context.Horses.FindAsync(SireId);
                    dam  = await _context.Horses.FindAsync(DamId);
                    break;

                case ItemType.Alpaca:
                    sire = await _context.Alpacas.FindAsync(SireId);
                    dam  = await _context.Alpacas.FindAsync(DamId);
                    break;
            }

            if (sire == null)
                result.AddError(nameof(sire.Id), "Sire not found.");

            if (dam == null)
                result.AddError(nameof(dam.Id), "Dam not found.");

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

            var foal = _horseService.CreateFoal(sire,dam);
           switch (type)
            {
                case ItemType.Horse:
                    sire = await _context.Horses.FindAsync(SireId);
                    dam  = await _context.Horses.FindAsync(DamId);
                    break;

                case ItemType.Alpaca:
                    sire = await _context.Alpacas.FindAsync(SireId);
                    dam  = await _context.Alpacas.FindAsync(DamId);
                    break;
            }

            result.Value = foal;
            return result;
        }
    }
}