namespace GameModel{
public class PuzzleAnswer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required ICollection<PuzzlePiece> PuzzlePieces { get; set; }

    [Owned]
    public class PuzzlePiece
    {
    public required int XCoordinate { get; set; } 
    public required int YCoordinate { get; set; } 
    public required string ImgUrl { get; set; }
    }
}
}