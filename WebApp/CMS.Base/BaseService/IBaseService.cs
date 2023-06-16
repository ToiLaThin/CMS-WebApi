using AutoMapper;

namespace CMS.Base
{
    public interface IBaseService<TEntity, TApiModel> where TEntity : class, new() where TApiModel : class
    {
        IUnitOfWork<TEntity> UnitOfWork { get; }
        List<TApiModel> ToList(IEnumerable<TApiModel> entitiesList);
        IEnumerable<TApiModel> sortByCreatedDate(IEnumerable<TApiModel> iApiModel, bool isIncrease);
        TApiModel FindById<T>(T id);
        IEnumerable<TApiModel> GetAll();
        TApiModel Create(TApiModel iApiModel);
        IEnumerable<TApiModel> Create(IEnumerable<TApiModel> entitiesList);
        bool Delete(TApiModel iApiModel);
        bool Delete(IEnumerable<TApiModel> entitiesList);
        TApiModel Update(TApiModel iApiModel);
        bool Validate(TApiModel iApiModel);
    }
}
