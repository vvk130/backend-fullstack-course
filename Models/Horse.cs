namespace GameModel{

public class Horse {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();  
    public required string Name {get; set; }
    public required Color Color {get; set; }
    public required Gender Gender {get; set; }
    public required Breed Breed {get; set; }
    public required int Capacity { get; set; }
    public required int Height { get; set; }
    public required List<Fear> Fears { get; set; } = new();
    public required List<PersonalityTrait> Personality { get; set; } = new();
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
    Appaloosa,
    Other
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
    Lusitano
}

public class Fear
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();  
    public required bool Discovered { get; set; }
    public required FearType FearType { get; set; }
    public required int Severity { get; set; }
}

public enum FearType
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
    PlastigBag,
    Fillers,
    Balloons,
    Shadows,
    IndoorArenas
}

public class PersonalityTrait
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();  
    public required PersonalityType Type { get; set; }
    public required int Severity { get; set; } 
    public required bool Discovered { get; set; }
}

public enum PersonalityType
{
    Bold,
    Shy,
    Curious,
    Cuddly,
    Calm,
    Energetic,
    Lazy,
    Friendly,
    Reserved,
    Spooky,
    Anxious
}

}