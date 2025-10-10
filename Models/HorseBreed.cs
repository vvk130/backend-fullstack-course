namespace GameModel{

public class HorseBreed
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public required Breed Breed { get; set; }
    public required int MinHeightCm { get; set; }
    public required int MaxHeightCm { get; set; }
    public required ICollection<Color> PossibleColors { get; set; } 
}
}