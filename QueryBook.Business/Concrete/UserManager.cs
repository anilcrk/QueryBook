using QueryBook.Business.Abstract;
using QueryBook.DataAccess.Abstract;
using QueryBook.Entites.ComplexType;
using QueryBook.Entites.Concrete.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void AutClear()
        {
            _userDal.Close();
        }

        public User Get(User filter)
        {
            try
            {
                return _userDal.Get(filter);
            }
            catch
            {
                throw new Exception();
            }
        }

        public User GetByUserNameAndOassword(User user)
        {
            return _userDal.Get(user);
        }

        public List<UserRoleItem> GetUserRoles(User user)
        {
            return _userDal.GetUserRoles(user);
        }
    }
}
