using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Abstract
{
    public interface IQueryService
    {
        List<Query> GetList(Query filter);
        Query Get(Expression<Func<Query, bool>> filter);
        Query Get(Query query);
        Query AddUp(Query query);
    }
}
