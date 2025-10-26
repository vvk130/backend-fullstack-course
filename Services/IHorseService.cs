namespace GameModel{
public interface IHorseService
{
   string GenerateRandomHorseName(Gender gender);
   Horse CreateHorse(Guid id, Breed? breed);
   Alpaca CreateAlpaca(Guid id, AlpacaBreed? breed);
   Horse CreateFoal(Horse sire, Horse dam);
   void BatchHorsesEnergyUpdate();
   void BatchHorsesAgeUpdate();
   public Alpaca CreateAlpacaFoal(Alpaca sire, Alpaca dam);
}
}