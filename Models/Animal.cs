namespace GameModel{

public class Animal {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string? ImgUrl {get; set; }
    public required string Name {get; set; }
    public required double Age {get; set; } = 0.0;
    public required Gender Gender {get; set; }
    public required int Capacity { get; set; }
    public required int Relationship { get; set; }
    public required int Energy { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? SireId { get; set; }
    public Guid? DamId { get; set; }
    public required ICollection<PersonalityType> Personalities { get; set; }
}
[Owned]
public class PersonalityType
{
    public required PersonalityTrait PersonalityTrait { get; set; }
    public required int Severity { get; set; } 
    public required bool Discovered { get; set; }
}

public enum PersonalityTrait
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
    Anxious,
    Careful
}

}