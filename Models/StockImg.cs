namespace GameModel{

public class StockImg {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required string ImgUrl { get; set; } 
}
}