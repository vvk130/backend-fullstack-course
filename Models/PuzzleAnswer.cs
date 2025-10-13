namespace GameModel{
public class PuzzleAnswer
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required ICollection<PuzzlePiece> PuzzlePieces { get; set; }

    [Owned]
    public class PuzzlePiece
    {
    public int XCoordinate { get; set; } 
    public int YCoordinate { get; set; } 
    public string ImgUrl { get; set; }
    }
}
}