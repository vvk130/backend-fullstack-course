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

        public async Task<OperationResult<Horse>> FoalTaskHandler(Horse foalSire, Horse foalDam){
            var result = new OperationResult<Horse>();
            
            // Breed determined in the FoalGenerator, can be crossbred
            // TODO Check previous foal - is it too soon
            // TODO Create FoalingEvent db 

            var sire = await _context.Horses.FindAsync(foalSire.Id);
            var dam = await _context.Horses.FindAsync(foalDam.Id);

            if (sire == null)
                result.AddError(nameof(sire.Id), "Sire not found.");

            if (dam == null)
                result.AddError(nameof(dam.Id), "Dam not found.");

            if (sire?.Gender != Gender.Stallion)
                result.AddError(nameof(sire.Id), "Sire must be a stallion.");

            if (dam?.Gender != Gender.Mare)
                result.AddError(nameof(dam.Id), "Dam must be a mare.");

            if (sire?.Age < 3.0)
                result.AddError(nameof(sire.Id), "Sire must be at least 3 years old.");

            if (dam?.Age < 3.0)
                result.AddError(nameof(dam.Id), "Dam must be at least 3 years old.");

            if (_random.Next(0,101) > 90)
                result.AddError("Foaling", "Foaling was unsuccessful due to natural/random failure.");
                
            if (!result.Success)
                return result;

            var foal = _horseService.CreateHorse();

            result.Value = foal;
            return result;
        }

        // public Horse FoalGenerator(){
        //     return _horseService.CreateHorse();
        // }
    }
}