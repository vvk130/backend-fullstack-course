namespace GameModel{
public interface ICompStatisticsService
{
    Task<CompResultStatisticsDto> CreateCompResultStatistics(Guid horseId);
}
}