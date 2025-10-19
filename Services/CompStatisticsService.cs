
namespace GameModel
{
    public class CompStatisticsService : ICompStatisticsService
    {
        private readonly AppDbContext _context;
        
        public CompStatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<CompResultStatisticsDto>> CreateCompResultStatistics(Guid horseId)
        {
            var result = new OperationResult<CompResultStatisticsDto>();

            var statistics = await _context.CompResults
                                .Where(h => h.HorseId == horseId)
                                .GroupBy(h => h.HorseId)
                                .Select(c => new { 
                                    Id = c.Key,
                                    TotalMoneyWon = c.Sum(c => c.MoneyWon),
                                    AverageRanking = c.Average(c => c.Ranking), 
                                    BestRanking = c.Min(c => c.Ranking),
                                    CompEntryCount = c.Count(),
                                })
                                .FirstOrDefaultAsync();

            if (statistics is null)
            {
                result.AddError("Horse", "Horse has no competition results");
                return result;
            }

            result.Value = new CompResultStatisticsDto(
                    statistics.Id,
                    statistics.TotalMoneyWon,
                    statistics.AverageRanking,
                    statistics.BestRanking,
                    statistics.CompEntryCount);

            return result;
        }

        public async Task<PaginatedResult<CompResultStatisticsDto>> GetPaginatedAsync(PaginationRequest request)
        {

            var query = _context.CompResults
                .GroupBy(h => h.HorseId)
                .Select(c => new 
                {
                    HorseId = c.Key,
                    TotalMoneyWon = c.Sum(c => c.MoneyWon),
                    AverageRanking = c.Average(c => c.Ranking),
                    BestRanking = c.Min(c => c.Ranking),
                    CompEntryCount = c.Count(),
                });

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.TotalMoneyWon)  
                .ThenByDescending(c => c.HorseId)        
                .Skip((request.PageNumber - 1) * request.PageSize)  
                .Take(request.PageSize)
                .ToListAsync();

            var result = items.Select(item => new CompResultStatisticsDto(
                item.HorseId,
                item.TotalMoneyWon,
                item.AverageRanking,
                item.BestRanking,
                item.CompEntryCount
            )).ToList();

            return new PaginatedResult<CompResultStatisticsDto>(result, totalCount, request.PageNumber, request.PageSize);
        }

    }

}
