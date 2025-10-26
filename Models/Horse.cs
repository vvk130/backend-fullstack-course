namespace GameModel{

public class Horse : Animal {
    public required Color Color {get; set; }
    public required Breed Breed {get; set; }
    public required int Height { get; set; }
    public required Qualities Qualities { get; set; }
    public required ICollection<FearType> Fears { get; set; } 
}

[ComplexType]
public class Qualities
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Endurance { get; set; }
    public int Speed { get; set; }
    public int Intelligence { get; set; }
    public int Stamina { get; set; }
    public int JumpingAbility { get; set; }
}

public enum Color
{
    Chestnut,
    Bay,
    Black,
    Gray,
    Palomino,
    Buckskin,
    Roan,
    Dun,
    Cremello,
    Perlino,
    Grullo,
    Pinto,
    Appaloosa
}

public enum Gender
{
    Mare,       
    Stallion,   
    Gelding
}

public enum Breed
{
    Unknown = 0,
    Arabian,
    Thoroughbred,
    QuarterHorse,
    Andalusian,
    Appaloosa,
    Mustang,
    Friesian,
    Hanoverian,
    Clydesdale,
    Shire,
    Belgian,
    Percheron,
    Paint,
    ShetlandPony,
    WelshPony,
    Icelandic,
    Fjord,
    AkhalTeke,
    Lusitano,
    Finnhorse
}

[Owned]
public class FearType
{
    public required FearItem FearItem { get; set; }
    public required bool Discovered { get; set; }
    public required int Severity { get; set; }
}

public enum FearItem
{
    Puddles,
    Thunder,
    Crowds,
    Predators,
    LoudNoises,
    Trucks,
    MailBoxes,
    ColorBlue,
    ColorRed,
    ColorYellow,
    Trailer,
    ShetlandPony,
    Separation,
    PlasticBag,
    Fillers,
    Balloons,
    Shadows,
    IndoorArenas
}

}