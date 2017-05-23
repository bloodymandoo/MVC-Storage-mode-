using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCStorageMode.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MVCStorageMode.Repositories
{
    public class GenericRepository<TEntity>
        :IGenericRepository<TEntity> where TEntity : class
    {
        internal XEngineContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(XEngineContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        //查询所有
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity,bool>> filter=null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
            string includeProperties=""
            )
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
            
        }
        //按id查询
        public TEntity GetByID(int id)
        {
            return dbSet.Find(id);
        }
        //插入新数据
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        //按id删除数据
        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
       
    }
}