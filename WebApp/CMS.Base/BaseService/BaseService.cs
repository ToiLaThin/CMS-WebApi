namespace CMS.Base
{
    //internal -> public 
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected readonly IUnitOfWork<TEntity> _unitOfWork;

        public BaseService(IUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork<TEntity> UnitOfWork
        {
            get => _unitOfWork;
        }

        public virtual TEntity Create(TEntity iEntity)
        {
            //TODO validate, check before adding, might use helper
            var result = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Add(iEntity);
            return result;
        }

        public virtual IEnumerable<TEntity> Create(IEnumerable<TEntity> entitiesList)
        {
            //TODO validate, check before adding, might use helper
            var result = entitiesList.ToList();
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().AddRange(entitiesList);
            return result;

        }

        public virtual bool Delete(TEntity iEntity)
        {
            //TODO only if TEntity to be found
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Remove(iEntity);
            return true;
        }

        public virtual bool Delete(IEnumerable<TEntity> entitiesList)
        {
            //TODO only if TEntity to be found
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().RemoveRange(entitiesList);
            return true;
        }

        public virtual TEntity FindById<T>(T id)
        {
            var result = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Get(id);
            return result;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var result = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().GetAll();
            return this.ToList(result);
        }

        public virtual IEnumerable<TEntity> sortByCreatedDate(IEnumerable<TEntity> iEntity, bool isIncrease)
        {
            throw new NotImplementedException();
        }

        public virtual List<TEntity> ToList(IEnumerable<TEntity> entitiesList)
        {
            return entitiesList.ToList();
        }

        public virtual TEntity Update(TEntity iEntity)
        {
            var result = iEntity;
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Update(iEntity); 
            return result;
        }

        public virtual bool Validate(TEntity iEntity)
        {
            throw new NotImplementedException();
        }
    }
}
