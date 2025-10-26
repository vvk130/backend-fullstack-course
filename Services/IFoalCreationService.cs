namespace GameModel{
public interface IFoalCreationService
{
   Task<OperationResult<Horse>> FoalTaskHandler(Guid SireId, Guid DamId, ItemType type);
}
}