namespace GameModel{

public class SalesAd {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required AdType AdType { get; set; }
    public required ItemType ItemType { get; set; }
    public required DateTime StartTime { get; set; } = DateTime.UtcNow;
    public required DateTime EndTime { get; set; }
    public required int Price { get; set; }
    public required Guid OwnerId { get; set; }
    public required Guid HorseId { get; set; }
    public Guid? HighestBidderId { get; set; } 
}

public enum AdType
{
    PaidAd,
    NormalAd,
    Auction
}

public enum ItemType
{
    Horse,
    Alpaca
}

}