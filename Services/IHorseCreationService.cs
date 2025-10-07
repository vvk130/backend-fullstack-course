namespace GameModel{
public interface IHorseService
{
   string GenerateRandomHorseName();
   List<Horse> GetAll();
   Horse CreateHorse();
   Task<bool> BatchHorsesEnergyUpdate();
}
}