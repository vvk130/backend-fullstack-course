namespace GameModel{

public class SalesAd {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required AdType AdType { get; set; }
    public required DateTime StartTime { get; set; } = DateTime.UtcNow;
    public required DateTime EndTime { get; set; }
    public required int Price;
    public required Guid OwnerId { get; set; }
    public required Guid HorseId { get; set; } 
}

public enum AdType
{
    PaidAd,
    NormalAd
}

}