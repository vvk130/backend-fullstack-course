namespace GameModel{
public interface ICompetitionService
{
    Task<OperationResult<CompetitionResult>> GetCompetitionResult(Guid competitionId, List<Guid> horseIds);
}
}