using Microsoft.EntityFrameworkCore;

namespace CMS.Base
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : class
    {
        IBaseRepository<TEntity> EntityRepository { get; }
        void SaveChanges();
        void RollBack();


    }
}
