using Ninject.Modules;
using QueryBook.Business.Abstract;
using QueryBook.Business.Concrete;
using QueryBook.DataAccess.Abstract;
using QueryBook.DataAccess.Concrete;
using QueryBook.DataAccess.Concrete.EntityFramework;
using QueryBook.DataAccess.Concrete.StoredProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.DependencyResolved.Ninject
{
   public class BusinessModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IInstutionService>().To<InstutionManager>().InSingletonScope();
            Bind<IInstutionDal>().To<SpInstutionDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<SpUserDal>().InSingletonScope();

            Bind<IQueryService>().To<QueryManager>().InSingletonScope();
            Bind<IQueryDal>().To<EfQueryDal>().InSingletonScope();
        }
    }
}
