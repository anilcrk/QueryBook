using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using QueryBook.Business.Abstract;
using QueryBook.DataAccess.Abstract;
using QueryBook.DataAccess.Concrete;
using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger))]
    public class QueryManager : IQueryService
    {
        private IQueryDal _IQueryDal;
            public QueryManager(IQueryDal queryDal)
        {
            _IQueryDal = queryDal;
        }

        public Query AddUp(Query query)
        {
            try
            {
                Query result;
                if(query.Id>0)
                {
                     result = _IQueryDal.Update(query);
                }
                else
                {
                    result = _IQueryDal.Add(query);
                }
                
                if(result.Id>0)
                {
                    result.sError = "İşlem Başarılı. ";
                }

                return result;
                
            }
            catch
            {
                throw new Exception();
            }
        }

        public Query Get(Expression<Func<Query, bool>> filter)
        {
            try
            {
                return _IQueryDal.Get(filter);
            }
            catch
            {
                throw new Exception();
            }
        }

        public Query Get(Query query)
        {
            return _IQueryDal.Get(query);
        }



        [SecuredOperation(Roles = "Editör,Admin")]
        
        public List<Query> GetList(Query filter)
        {
            try
            {
                return _IQueryDal.GetList(filter);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
