namespace GameModel{

[Index(nameof(LevelNumber), IsUnique = true)]
public class Level {
    public required int LevelNumber { get; set; } 
    public required int EntryPoints { get; set; } 
    public required Stable Stable { get; set; } 
}

[ComplexType]
public class Stable
{
    public int StableSlots { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
    public StableType StableType { get; set; }
    public int StableAmount { get; set; }
    public int EnvironmentScore { get; set; }
    public int Cleanleness { get; set; } 
}

public enum StableType{
    OpenStable,
    IndoorStable
}

}