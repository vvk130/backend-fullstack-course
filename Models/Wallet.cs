namespace GameModel{

public class Wallet {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required Guid OwnerId { get; set; } 
    public int Balance { get; set; } = 0;
}
}