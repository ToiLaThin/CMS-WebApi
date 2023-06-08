namespace CMS.Base
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        IUnitOfWork<TEntity> UnitOfWork { get; }
        List<TEntity> ToList(IEnumerable<TEntity> entitiesList);
        IEnumerable<TEntity> sortByCreatedDate(IEnumerable<TEntity> iEntity, bool isIncrease);
        TEntity FindById<T>(T id);
        IEnumerable<TEntity> GetAll();
        TEntity Create(TEntity iEntity);
        IEnumerable<TEntity> Create(IEnumerable<TEntity> entitiesList);
        bool Delete(TEntity iEntity);
        bool Delete(IEnumerable<TEntity> entitiesList);
        TEntity Update(TEntity iEntity);
        bool Validate(TEntity iEntity);
    }
}
