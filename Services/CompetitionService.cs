using Bogus;
using GameModel;
using System.Linq;

namespace GameModel
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IGenericService<Horse> _horseService;
        private readonly IGenericService<Competition> _competitionService;
        private readonly IGenericService<Wallet> _walletService;
        private readonly IGenericService<CompResult> _compResultService;
        private readonly AppDbContext _context;
        
        public CompetitionService(IGenericService<Horse> horseService,
        IGenericService<Competition> competitionService, 
        IGenericService<Wallet> walletService, 
        IGenericService<CompResult> compResultService,
        AppDbContext context)
        {
            _walletService = walletService;
            _horseService = horseService;
            _competitionService = competitionService;
            _compResultService = compResultService;
            _context = context;
        }

        public async Task<OperationResult<CompetitionResult>> GetCompetitionResult(Guid competitionId, List<Guid> horseIds, CancellationToken ct)
        {
            var result = new OperationResult<CompetitionResult>();

            var competition = await _competitionService.GetByIdAsync(competitionId);
            if (competition == null)
            {
                result.AddError(nameof(competition.Id), "Competition not found.");
                return result;
            }
            if (DateTime.UtcNow < competition.StartTime){
                result.AddError(nameof(competition.StartTime), "Competition has not started yet.");
                return result;
            }
            if (DateTime.UtcNow > competition.EndTime){
                result.AddError(nameof(competition.EndTime), "Competition has ended.");
                return result;      
            }
          
            var horseEntities = await _horseService.FindAsync(h => horseIds.Contains(h.Id));

            var rankedHorses = horseEntities
                .OrderByDescending(h => h.Capacity)
                .Select((h, index) => new RankedHorse(
                    index + 1,
                    new HorseShortDto(h.Id, h.Name, h.ImgUrl),
                    h.OwnerId,
                    competition.Id,
                    MoneyWon(index + 1)
                ))
                .ToList();

            await CreateCompResults(rankedHorses, ct); //put in rabbitMQ?

            var winners = rankedHorses
                            .Take(3)
                            .ToList();

            await PayWinners(winners, ct); //put in rabbitMQ?

            result.Value = new CompetitionResult(rankedHorses);
            return result;
        }

        public int MoneyWon(int ranking)
        {
            if (ranking == 1)
                return 1000;
            if (ranking == 2)
                return 500;
            if (ranking == 3)
                return 200;
            return 0;
        }

        public async Task PayWinners(List<RankedHorse> winners, CancellationToken ct)
        {
            foreach (var winner in winners)
            {
                if (winner.OwnerId is not null)
                {
                    var wallet = (await _walletService.FindAsync(w => w.OwnerId == winner.OwnerId, ct)).FirstOrDefault();
                    if (wallet is not null)
                    {
                        wallet.Balance += winner.MoneyWon;
                        await _walletService.SaveChangesAsync();
                    }
                }
            }

        }

        public async Task CreateCompResults(List<RankedHorse> rankedHorses, CancellationToken ct)
        {
            var compResults = rankedHorses.Select(horse => new CompResult
            {
                HorseId = horse.Horse.Id,            
                CompId = horse.CompetitionId,        
                Ranking = horse.Ranking,             
                MoneyWon = horse.MoneyWon,         
                CompetitionTime = DateTime.UtcNow    
            }).ToList();

            await _compResultService.AddRangeAsync(compResults, ct);
        }
    }

}
