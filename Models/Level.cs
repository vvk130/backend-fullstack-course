namespace GameModel{

public class Level {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required int LevelNumber { get; set; } 
    public required int EntryPoints { get; set; } 
    public required Stable Stable { get; set; } 
}

[ComplexType]
public class Stable
{
    public required string ImgUrl { get; set; }
    public required string Description { get; set; }
    public required StableType StableType { get; set; }
    public required int StableAmount { get; set; }
    public required int EnvironmentScore { get; set; }
    public required int Cleanleness { get; set; } 
}

public enum StableType{
    OpenStable,
    IndoorStable
}

}