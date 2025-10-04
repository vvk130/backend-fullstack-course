namespace GameModel{

[Index(nameof(Level), IsUnique = true)]
public class Level {
    public Int Level { get; set; } 
    public Int EntryPoints { get; set; } 
    public Stable Stable { get; set; } 
}


[ComplexType]
public class Stable
{
    public required int StableSlots { get; set; }
    public string ImgUrl { get; set; }
}

}