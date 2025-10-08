namespace GameModel{
public interface IFoalCreationService
{
//    Horse FoalGenerator();
   Task<OperationResult<Horse>> FoalTaskHandler(Guid SireId, Guid DamId);
}
}