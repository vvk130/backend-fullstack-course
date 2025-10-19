
namespace GameModel
{
    public class CompStatisticsService : ICompStatisticsService
    {
        private readonly AppDbContext _context;
        
        public CompStatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompResultStatisticsDto> CreateCompResultStatistics(Guid horseId)
        {
            var statistics = await _context.CompResults
                                .Where(h => h.HorseId == horseId)
                                .GroupBy(h => h.HorseId)
                                .Select(c => new 
                                {
                                    TotalMoneyWon = c.Sum(c => c.MoneyWon),
                                    AverageRanking = c.Average(c => c.Ranking), 
                                    BestRanking = c.Min(c => c.Ranking),
                                    CompEntryCount = c.Count(),
                                })
                                .FirstOrDefaultAsync();

            if (statistics is null)
                return new CompResultStatisticsDto(0, 0, 0, 0);

            return new CompResultStatisticsDto(
                statistics.TotalMoneyWon,
                statistics.AverageRanking,
                statistics.BestRanking,
                statistics.CompEntryCount
            );
        }
    }
}