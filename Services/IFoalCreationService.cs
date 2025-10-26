namespace GameModel{
public interface IFoalCreationService
{
   Task<OperationResult<Animal>> FoalTaskHandler(Guid SireId, Guid DamId, ItemType type);
}
}