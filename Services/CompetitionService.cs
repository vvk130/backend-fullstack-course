using Bogus;
using GameModel;

namespace GameModel
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IGenericService<Horse> _horseService;
        private readonly IGenericService<Competition> _competitionService;
        private readonly IGenericService<Wallet> _walletService;
        
        public CompetitionService(IGenericService<Horse> horseService,
        IGenericService<Competition> competitionService, IGenericService<Wallet> walletService)
        {
            _walletService = walletService;
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
                    new HorseShortDto(h.Id, h.Name, h.ImgUrl),
                    h.OwnerId
                ))
                .ToList();

            var winners = rankedHorses
                            .Take(3)
                            .ToList();

            await PayWinners(winners);

            result.Value = new CompetitionResult(rankedHorses);
            return result;
        }

        public async Task PayWinners(List<RankedHorse> winners)
        {
            foreach (var winner in winners)
            {
                if (winner.OwnerId is not null)
                {
                    var wallet = (await _walletService.FindAsync(w => w.OwnerId == winner.OwnerId)).FirstOrDefault();
                    if (wallet is not null)
                    {
                        wallet.Balance += 100;
                        await _walletService.SaveChangesAsync();
                    }
                }
            }

        }
    }

}
