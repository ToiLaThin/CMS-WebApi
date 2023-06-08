using Microsoft.EntityFrameworkCore;

namespace CMS.Base
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class, new()
    {
        private DbContext _context;
        private bool _disposed = false;
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        //Prop -> Method
        //public IBaseRepository<TEntity> EntityRepository
        //{
        //    get
        //    {
        //        if (this._entityRepository == null)
        //        {
        //            this._entityRepository = new BaseRepository<TEntity>(_context);
        //        }
        //        return this._entityRepository;
        //    }
        //}
        public IBaseRepository<TEntity> EntityRepository<RepoType>() where RepoType : IBaseRepository<TEntity>, new()
        {
            IBaseRepository<TEntity> repo = new RepoType();
            BaseRepository<TEntity> temp = (BaseRepository<TEntity>)repo;
            temp.AddDbContextAndDbSet(this._context);
            return temp;
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
