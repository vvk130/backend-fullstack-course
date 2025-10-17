namespace GameModel{

public class CompResult {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required Guid HorseId { get; set; }
    public required Guid CompId { get; set; }
    public required int Ranking { get; set; }
    public required double MoneyWon { get; set; }
    public required DateTime CompetitionTime { get; set; } = DateTime.UtcNow;
}
}