using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMS.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal DbContext _dbContext; 
        internal DbSet<TEntity> _dbSet;
        //public _dbContext _dbContext //TODO Prop?
        //{ 
        //    get { return __dbContext; } 
        //    set { __dbContext = value; } 
        //}

        //public _dbSet<TEntity> _dbSet
        //{
        //    get { return __dbSet; }
        //    set { __dbSet = value; }
        //}

        public BaseRepository() { }

        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<TEntity>();
        }

        //atomic function
        //virtual for overriding
        public virtual TEntity Add(TEntity entity)
        {
            this._dbSet.Add(entity);
            return entity;

        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.AddRange(entities);
        }

        public virtual void ExecSql(List<SqlParameter> parameters, string sql)
        {
            this._dbSet.FromSqlRaw(sql: sql, parameters: parameters);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> lambda)
        {
            var result = this._dbSet.Where(lambda);
            return result;
        }

        public virtual IEnumerable<TEntity> Generic_ExecSql_GetMany(List<SqlParameter> parameters, string sql)
        {
            var result = this._dbSet.FromSqlRaw(sql,parameters.ToArray());
            return result.ToList();
        }

        public virtual TEntity Generic_ExecSql_GetOne(List<SqlParameter> parameters, string sql)
        {
            var result = this._dbSet.FromSqlRaw(sql, parameters.ToArray()).AsEnumerable().FirstOrDefault();
            return result;
        }

        public virtual TEntity Get<T>(T id)
        {
            TEntity entity = this._dbSet.Find(id);
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> entities = this._dbSet.ToList();
            return entities;
        }

        public virtual TEntity GetFirstEntity()
        {
            TEntity fEntity = this._dbSet.FirstOrDefault();
            return fEntity;
        }

        public virtual void Remove(TEntity entityToRemove)
        {
            if(this._dbContext.Entry(entityToRemove).State != EntityState.Detached)
            {
                this._dbSet.Attach(entityToRemove); //begin tracking
            }
            this._dbSet.Remove(entityToRemove);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entitiesToRemove)
        {
            foreach(TEntity entity in entitiesToRemove)
            {
                if (this._dbContext.Entry(entity).State != EntityState.Detached)
                {
                    this._dbSet.Attach(entity); //begin tracking
                }
            }
            this._dbSet.RemoveRange(entitiesToRemove);
        }

        public virtual void Update(TEntity uEntity)
        {
            this._dbSet.Attach(uEntity);
            this._dbContext.Entry(uEntity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.AttachRange(entities);
            this._dbContext.Entry(entities).State = EntityState.Modified;
        }
    }
}
