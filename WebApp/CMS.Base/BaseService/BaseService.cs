using AutoMapper;

namespace CMS.Base
{
    //internal -> public 
    public class BaseService<TEntity, TApiModel> : IBaseService<TEntity, TApiModel> where TEntity : class, new() where TApiModel: class
    {
        protected readonly IUnitOfWork<TEntity> _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork<TEntity> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork<TEntity> UnitOfWork
        {
            get => _unitOfWork;
        }

        public virtual TApiModel Create(TApiModel iApiModel)//i for injected
        {
            //TODO validate, check before adding, might use helper
            var entity = this._mapper.Map<TEntity>(iApiModel);
            var resultEntity = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Add(entity);
            var returnApiModel = this._mapper.Map<TApiModel>(resultEntity);
            return returnApiModel;
        }

        public virtual IEnumerable<TApiModel> Create(IEnumerable<TApiModel> apiModelList)
        {
            //TODO validate, check before adding, might use helper
            var entityList = this._mapper.Map<IEnumerable<TEntity>>(apiModelList);
            var result = apiModelList.ToList();
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().AddRange(entityList);
            return result;

        }

        public virtual bool Delete(TApiModel iApiModel)
        {
            //TODO only if TEntity to be found
            var entity = this._mapper.Map<TEntity>(iApiModel);
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Remove(entity);
            return true;
        }

        public virtual bool Delete(IEnumerable<TApiModel> apiModelList)
        {
            //TODO only if TEntity to be found
            var entityList = this._mapper.Map<IEnumerable<TEntity>>(apiModelList);
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().RemoveRange(entityList);
            return true;
        }

        public virtual TApiModel FindById<T>(T id)
        {
            var resultEntity = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Get(id);
            var returnApiModel = this._mapper.Map<TApiModel>(resultEntity);
            return returnApiModel;
        }

        public virtual IEnumerable<TApiModel> GetAll()
        {
            var resultEntityList = this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().GetAll();
            var resultApiList = this._mapper.Map<IEnumerable<TApiModel>>(resultEntityList);

            return this.ToList(resultApiList);
        }

        public virtual IEnumerable<TApiModel> sortByCreatedDate(IEnumerable<TApiModel> iApiModel, bool isIncrease)
        {
            throw new NotImplementedException();
        }

        public virtual List<TApiModel> ToList(IEnumerable<TApiModel> apiModelList)
        {
            return apiModelList.ToList();
        }

        public virtual TApiModel Update(TApiModel iApiModel)
        {
            var entity = this._mapper.Map<TEntity>(iApiModel);
            this._unitOfWork.EntityRepository<BaseRepository<TEntity>>().Update(entity); 
            return iApiModel;
        }

        public virtual bool Validate(TApiModel iApiModel)
        {
            throw new NotImplementedException();
        }
    }
}
