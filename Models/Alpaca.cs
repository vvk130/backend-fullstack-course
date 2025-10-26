namespace GameModel{

public class Alpaca : Animal {
    public required AlpacaColor AlpacaColor {get; set; }
    public required AlpacaBreed AlpacaBreed {get; set; }
    public required AlpacaQualities AlpacaQualities { get; set; }
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
        Unknown,
        Huacaya,
        Suri
    }
}


