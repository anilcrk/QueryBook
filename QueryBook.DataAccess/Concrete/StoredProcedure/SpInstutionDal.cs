using DevFramework.Core.DataAccess;
using DevFramework.Core.DataAccess.SqlStoredProcedure;
using QueryBook.DataAccess.Abstract;
using QueryBook.Entites.Concrete.Institutions;
using QueryBook.Entites.StoredProcedure.SpHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete.StoredProcedure
{
    public class SpInstutionDal : IInstutionDal
    {
        public Institution Add(Institution entity)
        {
            List<DbQueryParameter> parameter = new List<DbQueryParameter>()
           {
               new DbQueryParameter("Code",string.IsNullOrEmpty(entity.Code)?"":entity.Code ),
               new DbQueryParameter("Name",string.IsNullOrEmpty(entity.Name)?"":entity.Name),
               new DbQueryParameter("Email",string.IsNullOrEmpty(entity.Email)?"":entity.Email),
               new DbQueryParameter("Tel01",string.IsNullOrEmpty(entity.Tel01)?"":entity.Tel01),
               new DbQueryParameter("Tel02",string.IsNullOrEmpty(entity.Tel02)?"":entity.Tel02),
               new DbQueryParameter("Tel03",string.IsNullOrEmpty(entity.Tel03)?"":entity.Tel03),
               new DbQueryParameter("Tel04",string.IsNullOrEmpty(entity.Tel04)?"":entity.Tel04),
               new DbQueryParameter("Fax01",string.IsNullOrEmpty(entity.Fax01)?"":entity.Fax02),
               new DbQueryParameter("Fax02",string.IsNullOrEmpty(entity.Fax02)?"":entity.Fax02),
               new DbQueryParameter("Fax03",string.IsNullOrEmpty(entity.Fax03)?"":entity.Fax03),
               new DbQueryParameter("Fax04",string.IsNullOrEmpty(entity.Fax04)?"":entity.Fax04),
               new DbQueryParameter("Address",string.IsNullOrEmpty(entity.Address)?"":entity.Address),
               new DbQueryParameter("Explanation",string.IsNullOrEmpty(entity.Explanation)?"":entity.Explanation),
               new DbQueryParameter("WebSite01",string.IsNullOrEmpty(entity.WebSite01)?"":entity.WebSite01),
               new DbQueryParameter("WebSite02",string.IsNullOrEmpty(entity.WebSite02)?"":entity.WebSite02),
               new DbQueryParameter("WebSite03",string.IsNullOrEmpty(entity.WebSite03)?"":entity.WebSite03),
               new DbQueryParameter("WebSite04",string.IsNullOrEmpty(entity.WebSite04)?"":entity.WebSite04),
               new DbQueryParameter("LogoPath",string.IsNullOrEmpty(entity.LogoPath)?"":entity.LogoPath),
               new DbQueryParameter("BannerPath",string.IsNullOrEmpty(entity.BannerPath)?"":entity.BannerPath)
           };
            return SqlDbManager.Instance().ExecuteSingle<Institution>(InstutionSpHelper.AddUpName(),parameter, SqlDbManager.SqlConnectionMode.QUERYBOOK);
        }



        public void Delete(Institution entity)
        {
            throw new NotImplementedException();
        }

        public Institution Get(Institution filter)
        {
            throw new NotImplementedException();
        }

        public Institution Get(Expression<Func<Institution, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Institution> GetList(Institution filter)
        {
            throw new NotImplementedException();
        }

        public Institution Update(Institution entity)
        {
            throw new NotImplementedException();
        }
    }
}
