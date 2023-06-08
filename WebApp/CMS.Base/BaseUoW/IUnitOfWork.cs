namespace CMS.Base
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : class, new()
    {
        IBaseRepository<TEntity> EntityRepository<RepoType>() where RepoType : IBaseRepository<TEntity>, new();
        void SaveChanges();
        void RollBack();


    }
}
