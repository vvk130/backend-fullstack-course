namespace GameModel{

public class Competition {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required ICollection<FearItem> ScaryObject { get; set; }
    public required CompetitionType CompetitionType { get; set; }
    public required DateTime StartTime { get; set; } = DateTime.UtcNow;
    public required DateTime EndTime { get; set; }
}

public enum CompetitionType
{
    ShowJumping,
    FlatRacing
}

}