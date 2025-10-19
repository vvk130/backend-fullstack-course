namespace GameModel{
public interface ICompStatisticsService
{
    Task<OperationResult<CompResultStatisticsDto>> CreateCompResultStatistics(Guid horseId);
    Task<PaginatedResult<CompResultStatisticsDto>> GetPaginatedAsync(PaginationRequest request);
}
}