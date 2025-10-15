namespace GameModel{
public interface IPuzzleService
{
    Task<bool> CheckAllPieces(PuzzleCorrectionRequest request);
    Task<OperationResult<PuzzleUnsolved>> PuzzleGenerator(string originalImgUrl);
}
}