using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMS.Base
{
    //internal -> public since other service might use this 
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get<T>(T id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> lambda);
        TEntity GetFirstEntity();
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity uEntity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void ExecSql(List<SqlParameter> parameters, string sql);
        TEntity Generic_ExecSql_GetOne(List<SqlParameter> parameters, string sql);
        IEnumerable<TEntity> Generic_ExecSql_GetMany(List<SqlParameter> parameters, string sql);




    }
}
