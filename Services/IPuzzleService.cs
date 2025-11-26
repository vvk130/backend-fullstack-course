namespace GameModel{
public interface IPuzzleService
{
    Task<OperationResult<PuzzleAnswer>> PuzzleGenerator(string originalImgUrl);
}
}