namespace GameModel{
public interface IHorseBreedService
{
   int GetRandomHeightForBreed(Breed breed);
   Color GetRandomColorForBreed(Breed breed);
}
}