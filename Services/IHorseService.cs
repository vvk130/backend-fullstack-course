namespace GameModel{
public interface IHorseService
{
   string GenerateRandomHorseName();
   List<Horse> GetAll();
   Horse CreateHorse();
   Horse CreateFoal(Horse sire, Horse dam);
   Task<bool> BatchHorsesEnergyUpdate();
}
}