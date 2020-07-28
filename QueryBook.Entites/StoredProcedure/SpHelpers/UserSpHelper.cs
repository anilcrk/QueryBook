using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.StoredProcedure.SpHelpers
{
    public static class UserSpHelper
    {
        public static string GetSpName()
        {
            return "sp_GetUser";
        }
        public static string GetUserRolesSpName()
        {
            return "sp_GetUserRoles";
        }
    }
}
