namespace GameModel{
public interface IFoalCreationService
{
//    Horse FoalGenerator();
   Task<OperationResult<Horse>> FoalTaskHandler(Horse sire, Horse dam);
}
}