namespace GameModel{
public class HorseBreedService : IHorseBreedService
{
private readonly List<HorseBreed> _breedHeights = new()
{
    new HorseBreed { 
        Breed = Breed.Unknown, 
        MinHeightCm = 125, 
        MaxHeightCm = 185, 
        PossibleColors = new List<Color> 
        { 
            Color.Chestnut, Color.Bay, Color.Black, Color.Gray 
        }
    },
    new HorseBreed {
        Breed = Breed.Arabian,
        MinHeightCm = 145,
        MaxHeightCm = 155,
        PossibleColors = new List<Color>
        {
            Color.Bay, Color.Gray, Color.Black, Color.Chestnut
        }
    },
    new HorseBreed {
        Breed = Breed.Thoroughbred,
        MinHeightCm = 160,
        MaxHeightCm = 170,
        PossibleColors = new List<Color>
        {
            Color.Bay, Color.Chestnut, Color.Black, Color.Gray, Color.Roan
        }
    },
    new HorseBreed {
        Breed = Breed.QuarterHorse,
        MinHeightCm = 148,
        MaxHeightCm = 163,
        PossibleColors = new List<Color>
        {
            Color.Buckskin, Color.Palomino, Color.Dun, Color.Bay, Color.Chestnut, Color.Black
        }
    },
    new HorseBreed {
        Breed = Breed.Andalusian,
        MinHeightCm = 150,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Gray, Color.Bay, Color.Black
        }
    },
    new HorseBreed {
        Breed = Breed.Appaloosa,
        MinHeightCm = 145,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Appaloosa
        }
    },
    new HorseBreed {
        Breed = Breed.Mustang,
        MinHeightCm = 140,
        MaxHeightCm = 150,
        PossibleColors = new List<Color>
        {
            Color.Bay, Color.Chestnut, Color.Black, Color.Grullo, Color.Roan
        }
    },
    new HorseBreed {
        Breed = Breed.Friesian,
        MinHeightCm = 155,
        MaxHeightCm = 165,
        PossibleColors = new List<Color>
        {
            Color.Black
        }
    },
    new HorseBreed {
        Breed = Breed.Hanoverian,
        MinHeightCm = 160,
        MaxHeightCm = 180,
        PossibleColors = new List<Color>
        {
            Color.Bay, Color.Black, Color.Chestnut, Color.Gray
        }
    },
    new HorseBreed {
        Breed = Breed.Clydesdale,
        MinHeightCm = 170,
        MaxHeightCm = 180,
        PossibleColors = new List<Color>
        {
            Color.Bay, Color.Black, Color.Roan
        }
    },
    new HorseBreed {
        Breed = Breed.Shire,
        MinHeightCm = 170,
        MaxHeightCm = 185,
        PossibleColors = new List<Color>
        {
            Color.Black, Color.Bay, Color.Gray
        }
    },
    new HorseBreed {
        Breed = Breed.Belgian,
        MinHeightCm = 160,
        MaxHeightCm = 170,
        PossibleColors = new List<Color>
        {
            Color.Chestnut, Color.Roan
        }
    },
    new HorseBreed {
        Breed = Breed.Percheron,
        MinHeightCm = 160,
        MaxHeightCm = 170,
        PossibleColors = new List<Color>
        {
            Color.Black, Color.Gray
        }
    },
    new HorseBreed {
        Breed = Breed.Paint,
        MinHeightCm = 145,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Pinto
        }
    },
    new HorseBreed {
        Breed = Breed.ShetlandPony,
        MinHeightCm = 100,
        MaxHeightCm = 107,
        PossibleColors = new List<Color>
        {
            Color.Black, Color.Bay, Color.Chestnut, Color.Gray, Color.Roan, Color.Pinto
        }
    },
    new HorseBreed {
        Breed = Breed.WelshPony,
        MinHeightCm = 120,
        MaxHeightCm = 148,
        PossibleColors = new List<Color>
        {
            Color.Gray, Color.Bay, Color.Chestnut, Color.Black
        }
    },
    new HorseBreed {
        Breed = Breed.Icelandic,
        MinHeightCm = 130,
        MaxHeightCm = 140,
        PossibleColors = new List<Color>
        {
            Color.Black, Color.Bay, Color.Chestnut, Color.Gray, Color.Palomino, Color.Buckskin
        }
    },
    new HorseBreed {
        Breed = Breed.Fjord,
        MinHeightCm = 130,
        MaxHeightCm = 145,
        PossibleColors = new List<Color>
        {
            Color.Dun
        }
    },
    new HorseBreed {
        Breed = Breed.AkhalTeke,
        MinHeightCm = 150,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Buckskin, Color.Palomino, Color.Bay, Color.Chestnut, Color.Cremello, Color.Perlino
        }
    },
    new HorseBreed {
        Breed = Breed.Lusitano,
        MinHeightCm = 150,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Gray, Color.Bay, Color.Chestnut, Color.Black
        }
    },
    new HorseBreed {
        Breed = Breed.Finnhorse,
        MinHeightCm = 145,
        MaxHeightCm = 160,
        PossibleColors = new List<Color>
        {
            Color.Chestnut, Color.Black, Color.Bay
        }
    }
};

    private readonly Random _random = new();

    public int GetRandomHeightForBreed(Breed breed)
    {
        var breedHeight = _breedHeights.Where(b => b.Breed == breed).Select(b => new { b.MinHeightCm, b.MaxHeightCm }).FirstOrDefault();

        int randomHeight = _random.Next(breedHeight.MinHeightCm, breedHeight.MaxHeightCm + 1);
        
        return randomHeight;
    }

    public Color GetRandomColorForBreed(Breed breed)
    {
        var breedColors = _breedHeights
            .Where(b => b.Breed == breed)
            .Select(b => b.PossibleColors)
            .FirstOrDefault();

        if (breedColors == null || breedColors.Count == 0)
        {
            return Color.Black; 
        }

        var colorList = breedColors.ToList();

        var randomColor = colorList[_random.Next(colorList.Count)];

        return randomColor;
    }

}
}
