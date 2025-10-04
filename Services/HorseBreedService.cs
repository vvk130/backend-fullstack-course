namespace GameModel{
public class HorseBreedService : IHorseBreedService
{
    private readonly List<HorseBreed> _breedHeights = new()
    {
        new HorseBreed { Breed = Breed.Unknown, MinHeightCm = 125, MaxHeightCm = 185 },
        new HorseBreed { Breed = Breed.Arabian, MinHeightCm = 145, MaxHeightCm = 155 },
        new HorseBreed { Breed = Breed.Thoroughbred, MinHeightCm = 160, MaxHeightCm = 170 },
        new HorseBreed { Breed = Breed.QuarterHorse, MinHeightCm = 148, MaxHeightCm = 163 },
        new HorseBreed { Breed = Breed.Andalusian, MinHeightCm = 150, MaxHeightCm = 160 },
        new HorseBreed { Breed = Breed.Appaloosa, MinHeightCm = 145, MaxHeightCm = 160 },
        new HorseBreed { Breed = Breed.Mustang, MinHeightCm = 140, MaxHeightCm = 150 },
        new HorseBreed { Breed = Breed.Friesian, MinHeightCm = 155, MaxHeightCm = 165 },
        new HorseBreed { Breed = Breed.Hanoverian, MinHeightCm = 160, MaxHeightCm = 180 },
        new HorseBreed { Breed = Breed.Clydesdale, MinHeightCm = 170, MaxHeightCm = 180 },
        new HorseBreed { Breed = Breed.Shire, MinHeightCm = 170, MaxHeightCm = 185 },
        new HorseBreed { Breed = Breed.Belgian, MinHeightCm = 160, MaxHeightCm = 170 },
        new HorseBreed { Breed = Breed.Percheron, MinHeightCm = 160, MaxHeightCm = 170 },
        new HorseBreed { Breed = Breed.Paint, MinHeightCm = 145, MaxHeightCm = 160 },
        new HorseBreed { Breed = Breed.ShetlandPony, MinHeightCm = 100, MaxHeightCm = 107 },
        new HorseBreed { Breed = Breed.WelshPony, MinHeightCm = 120, MaxHeightCm = 148 },
        new HorseBreed { Breed = Breed.Icelandic, MinHeightCm = 130, MaxHeightCm = 140 },
        new HorseBreed { Breed = Breed.Fjord, MinHeightCm = 130, MaxHeightCm = 145 },
        new HorseBreed { Breed = Breed.AkhalTeke, MinHeightCm = 150, MaxHeightCm = 160 },
        new HorseBreed { Breed = Breed.Lusitano, MinHeightCm = 150, MaxHeightCm = 160 },
        new HorseBreed { Breed = Breed.Finnhorse, MinHeightCm = 145, MaxHeightCm = 160 }
    };

    private readonly Random _random = new();

    public int GetRandomHeightForBreed(Breed breed)
    {
        var breedHeight = _breedHeights.FirstOrDefault(b => b.Breed == breed) ?? new HorseBreed { Breed = Breed.Unknown, MinHeightCm = 125, MaxHeightCm = 185 };

        int randomHeight = _random.Next(breedHeight.MinHeightCm, breedHeight.MaxHeightCm + 1);
        
        return randomHeight;
    }
}
}
