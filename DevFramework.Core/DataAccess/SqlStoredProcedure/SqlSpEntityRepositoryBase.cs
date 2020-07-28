using DevFramework.Core.DataAccess.SqlStoredProcedure.SpHelpers;
using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.SqlStoredProcedure
{
   public class SqlSpEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class,IEntity, new()
    {
        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TEntity filter)
        {
            List<DbQueryParameter> parameter = new List<DbQueryParameter>()
           {
               new DbQueryParameter("InsCode","HACYAZILIM")
           };
            return SqlDbManager.Instance().ExecuteSingle<TEntity>("sp_selectQuery", parameter, SqlDbManager.SqlConnectionMode.QUERYBOOK);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetList(TEntity filter)
        {
            //return SqlDbManager.Instance().ExecuteList<TEntity>(QuerySpHelper.GetListSpName, QuerySpHelper.GetListParameters("HACYAZILIM"), SqlDbManager.SqlConnectionMode.QUERYBOOK);
            return null;
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
