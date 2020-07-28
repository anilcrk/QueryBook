using QueryBook.Entites.ComplexType;
using QueryBook.Entites.Concrete.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Abstract
{
   public interface IUserService
    {
        User GetByUserNameAndOassword(User user);
        List<UserRoleItem> GetUserRoles(User user);
        void AutClear();
        User Get(User filter);
    }
}
