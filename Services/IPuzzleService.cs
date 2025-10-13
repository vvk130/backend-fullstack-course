namespace GameModel{
public interface IPuzzleService
{
    bool CheckAllPieces(PuzzleCorrectionRequest request);
    PuzzleUnsolved PuzzleGenerator();
}
}