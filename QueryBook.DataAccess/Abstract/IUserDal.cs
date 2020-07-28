using DevFramework.Core.DataAccess;
using QueryBook.Entites.ComplexType;
using QueryBook.Entites.Concrete.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UserRoleItem> GetUserRoles(User user);
        void Close();
    }
}
