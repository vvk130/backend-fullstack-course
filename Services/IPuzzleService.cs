namespace GameModel{
public interface IPuzzleService
{
    bool CheckAllPieces(PuzzleCorrectionRequest request);
    Task<OperationResult<PuzzleUnsolved>> PuzzleGenerator(string originalImgUrl);
}
}