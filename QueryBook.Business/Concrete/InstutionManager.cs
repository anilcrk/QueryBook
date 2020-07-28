using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.ValidationAspect;
using QueryBook.Business.Abstract;
using QueryBook.Business.ValidationTool.FluentValidation;
using QueryBook.DataAccess.Abstract;
using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Concrete
{
   //[LogAspect(typeof(DatabaseLogger))]
    public class InstutionManager : IInstutionService
    {
        private IInstutionDal _instutionDal;
        public InstutionManager(IInstutionDal instutionDal)
        {
            _instutionDal = instutionDal;
        }
        [SecuredOperation(Roles = "Editör,Admin")]
        [FluenValidationAspect(typeof(InstituionValidator))]
        public Institution Add(Institution entity)
        {
            try
            {
                return _instutionDal.Add(entity);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
