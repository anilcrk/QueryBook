using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Abstract
{
   public interface IInstutionService
    {
        Institution Add(Institution entity);
    }
}
