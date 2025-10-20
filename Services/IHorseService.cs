namespace GameModel{
public interface IHorseService
{
   string GenerateRandomHorseName(Gender gender);
   Horse CreateHorse();
   Horse CreateFoal(Horse sire, Horse dam);
   void BatchHorsesEnergyUpdate();
   void BatchHorsesAgeUpdate();
}
}