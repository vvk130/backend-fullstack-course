namespace GameModel{
public interface IPuzzleService
{
    Task<bool> CheckAllPieces(PuzzleCorrectionRequest request);
    Task<OperationResult<PuzzleAnswer>> PuzzleGenerator(string originalImgUrl);
}
}