using DevFramework.Core.DataAccess.SqlStoredProcedure;
using QueryBook.DataAccess.Abstract;
using QueryBook.Entites.Concrete;
using QueryBook.Entites.Concrete.Querys;
using QueryBook.Entites.StoredProcedure.SpHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete
{
    public class SpQueryDal : SqlSpEntityRepositoryBase<Query>, IQueryDal
    {
    }
}
