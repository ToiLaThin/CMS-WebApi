namespace CMS.Base
{
    //internal -> public 
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork<TEntity> _unitOfWork;

        public BaseService(IUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TEntity Create(TEntity iEntity)
        {
            //TODO validate, check before adding, might use helper
            var result = this._unitOfWork.EntityRepository.Add(iEntity);
            return result;
        }

        public IEnumerable<TEntity> Create(IEnumerable<TEntity> entitiesList)
        {
            //TODO validate, check before adding, might use helper
            var result = entitiesList.ToList();
            this._unitOfWork.EntityRepository.AddRange(entitiesList);
            return result;

        }

        public bool Delete(TEntity iEntity)
        {
            //TODO only if TEntity to be found
            this._unitOfWork.EntityRepository.Remove(iEntity);
            return true;
        }

        public bool Delete(IEnumerable<TEntity> entitiesList)
        {
            //TODO only if TEntity to be found
            this._unitOfWork.EntityRepository.RemoveRange(entitiesList);
            return true;
        }

        public TEntity FindById<T>(T id)
        {
            var result = this._unitOfWork.EntityRepository.Get(id);
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = this._unitOfWork.EntityRepository.GetAll();
            return this.ToList(result);
        }

        public IEnumerable<TEntity> sortByCreatedDate(IEnumerable<TEntity> iEntity, bool isIncrease)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> ToList(IEnumerable<TEntity> entitiesList)
        {
            return entitiesList.ToList();
        }

        public TEntity Update(TEntity iEntity)
        {
            var result = iEntity;
            this._unitOfWork.EntityRepository.Update(iEntity); 
            return result;
        }

        public bool Validate(TEntity iEntity)
        {
            throw new NotImplementedException();
        }
    }
}
