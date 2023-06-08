using Microsoft.EntityFrameworkCore;

namespace CMS.Base
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private DbContext _context;
        private bool _disposed = false;
        private IBaseRepository<TEntity> _entityRepository;
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IBaseRepository<TEntity> EntityRepository
        {
            get
            {
                if (this._entityRepository == null)
                {
                    this._entityRepository = new BaseRepository<TEntity>(_context);
                }
                return this._entityRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) { _context.Dispose(); }
                _disposed = true;
            }
        }

        public void Dispose() //?
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollBack()
        {
            this._context.Database.RollbackTransaction();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
    }
}
