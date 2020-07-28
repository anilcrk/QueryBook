using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.SqlStoredProcedure.SpHelpers
{
    public static class QuerySpHelper<TEntity>
    {   
        public static string GetListSpName = "sp_selectQuery";
        public static List<DbQueryParameter> GetListParameters(string InsCode)
        {
            return new List<DbQueryParameter>
            {
                new DbQueryParameter("InsCode",InsCode)
            };
        }
        public static string InsertSpName = "sp_insertInstution";
        public static List<DbQueryParameter> InsertSpParameters(Query)
        {
            return new List<DbQueryParameter>
            {
                new DbQueryParameter("InsCode",InsCode)
            };
        }
    }
}
