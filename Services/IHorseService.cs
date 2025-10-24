namespace GameModel{
public interface IHorseService
{
   string GenerateRandomHorseName(Gender gender);
   Horse CreateHorse(Guid id, Breed? breed);
   Horse CreateFoal(Horse sire, Horse dam);
   void BatchHorsesEnergyUpdate();
   void BatchHorsesAgeUpdate();
}
}