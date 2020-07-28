using DevFramework.Core.DataAccess.SqlStoredProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Entities
{
   public class SpInfo
    {
        public string SpName { get; set; }
        List<DbQueryParameter> Parameters { get; set; }
    }
}
