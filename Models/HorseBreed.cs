namespace GameModel{

[Index(nameof(Breed), IsUnique = true)]
public class HorseBreed
{
    public required Breed Breed { get; set; }
    public required int MinHeightCm { get; set; }
    public required int MaxHeightCm { get; set; }
    public required ICollection<Color> PossibleColors { get; set; } 
}
}