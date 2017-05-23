using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStorageMode.Models;
using System.Linq.Expressions;

namespace MVCStorageMode.Repositories
{
    interface IGenericRepository<TEntity>
       
    {
         IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
            );
        TEntity GetByID(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        
    }
}
