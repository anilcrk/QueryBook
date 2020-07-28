using DevFramework.Core.DataAccess.EntityFramework;
using QueryBook.DataAccess.Abstract;
using QueryBook.DataAccess.Concrete.EntityFramework;
using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete.EntityFramework
{
  public  class EfQueryDal:EfEntityRepositoryBase<Query,QueryBookContext>,IQueryDal
    {
    }
}
