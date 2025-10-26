namespace GameModel{

public class Alpaca {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string? ImgUrl {get; set; }
    public required string Name {get; set; }
    public required double Age {get; set; } = 0.0;
    public required AlpacaColor AlpacaColor {get; set; }
    public required Gender Gender {get; set; }
    public required AlpacaBreed AlpacaBreed {get; set; }
    public required int Capacity { get; set; }
    public required int Relationship { get; set; }
    public required int Energy { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? SireId { get; set; }
    public Guid? DamId { get; set; }
    public required AlpacaQualities AlpacaQualities { get; set; }
    public required ICollection<PersonalityType> Personalities { get; set; }
}

    [ComplexType]
    public class AlpacaQualities
    {
        public int Agility { get; set; }
        public int Speed { get; set; }
        public int Intelligence { get; set; }
        public int JumpingAbility { get; set; }
        public int WoolQuality { get; set; }
    }

    public enum AlpacaColor
    {
        White,
        Beige,
        LightFawn,
        MediumFawn,
        DarkFawn,
        LightBrown,
        MediumBrown,
        DarkBrown,
        BayBlack,
        TrueBlack,
        LightSilverGrey,
        MediumSilverGrey,
        DarkSilverGrey,
        LightRoseGrey,
        MediumRoseGrey,
        DarkRoseGrey
    }

    public enum AlpacaBreed
    {
        Huacaya,
        Suri
    }
}


