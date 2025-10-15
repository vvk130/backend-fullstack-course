using Bogus;
using GameModel;

namespace GameModel
{
    public class CompetitionService : ICompetitionService
    {
        private readonly AppDbContext _context;
        private readonly IHorseBreedService _horseBreedService; 
        private readonly Faker _faker = new();
        private readonly Random _random = new();
        private readonly IGenericService<Horse> _horseService;
        private readonly IGenericService<Competition> _competitionService;
        
        public CompetitionService(AppDbContext context, IHorseBreedService horseBreedService, IGenericService<Horse> horseService,
        IGenericService<Competition> competitionService)
        {
            _context = context;
            _horseBreedService = horseBreedService;
            _horseService = horseService;
            _competitionService = competitionService;
        }

        public async Task<OperationResult<CompetitionResult>> GetCompetitionResult(Guid competitionId, List<Guid> horseIds)
        {
            var result = new OperationResult<CompetitionResult>();

            var competition = await _competitionService.GetByIdAsync(competitionId);
            if (competition == null)
                result.AddError(nameof(competition.Id), "Competition not found.");

            if (DateTime.UtcNow < competition.StartTime)
                result.AddError(nameof(competition.StartTime), "Competition hasn't started yet.");

            if (DateTime.UtcNow > competition.EndTime)
                result.AddError(nameof(competition.EndTime), "Competition has ended.");

            var horseEntities = await _horseService.FindAsync(h => horseIds.Contains(h.Id));

            var rankedHorses = horseEntities
                .OrderByDescending(h => h.Capacity)
                .Select((h, index) => new RankedHorse(
                    index + 1,
                    new HorseShortDto(h.Id, h.Name, h.ImgUrl)
                ))
                .ToList();

            result.Value = new CompetitionResult(rankedHorses);
            return result;
        }
    }

}
