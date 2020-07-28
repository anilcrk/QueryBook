using DevFramework.Core.DataAccess;
using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Abstract
{
   public interface IInstutionDal:IEntityRepository<Institution>
    {
    }
}
